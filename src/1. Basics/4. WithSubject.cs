using System;
using Machine.Fakes;
using Machine.Specifications;

namespace MSpecDemo._1._Basics
{
    // Sadly you still need the Subject Attribute even though you're using WithSubject :(
    [Subject(typeof(DiMessageSender))]
    public class When_sending_a_message_using_with_subject : WithSubject<DiMessageSender>
    {
        // WithSubject is the same as WithFakes in that it sets up the Fake Engine,
        // It also creates an object of the type specified called Subject
        // It is lazy instantiated on first call. Normally you don't need to instantiate it
        // if you do want to set it up a specific way set it up in the subject
        // It will do dependency resolution for you

        static string sentMessage;

        Establish context = () =>
                The<IMessageTransmitter>()
                    .WhenToldTo(t => t.Transmit(Param.IsAny<string>()))
                    .Callback((string message) => SendMessageCallback(message));

        // When I first try to access Subject (which is of type DiMessageSender)
        // it will instantiate/resolve a DiMessageSender
        // Because I set expectations on a singleton of IMessageTransmitter in the
        // Establish, My DiMessageSender is injected with that IMessageTransmitter
        // and I need do no additional wiring
        Because of = () => Subject.SendMessage("Test Message");

        It should_send_the_message = () => sentMessage.ShouldEqual("Test Message");

        static void SendMessageCallback(string message)
        {
            sentMessage = message;
        }    
    }
}