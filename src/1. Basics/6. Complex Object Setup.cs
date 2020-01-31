using Machine.Fakes;
using Machine.Specifications;

namespace MSpecDemo._1._Basics
{
    public class ComplexObjectSetup : WithFakes
    {
        // To inject complex behaviours into your tests
        // You can inherit from WithFakes and use the OnEstablish event
        // to configure the FakeEngine to return pre-prepared objects when
        // your code asks for certain types
        // This is particularly useful if you have something heavyweight you want
        // to abstract when you're testing something unrelated, but your class 
        // has an unfortunate dependency on this complex thing
        // e.g. we might used this to mock out the behaviour of something like QuoteService
        // so we can depend on this in our tests before having to breakout the dependencies on QuoteService            
        OnEstablish context = accessor =>
        {
            var transmitter = An<IMessageTransmitter>();
            transmitter.WhenToldTo(t => t.Status).Return("It's complicated");
            transmitter.WhenToldTo(t => t.Transmit(Param.IsAny<string>())).Callback((string message) => LastMessageSent = message);

            // tell the fake engine to always use this fake instance when an IMessageTransmitter is requested 
            accessor.Configure(transmitter);
        };
        public static string LastMessageSent { get; set; }
    }

    public class When_using_something_complicated : WithSubject<DiMessageSender>
    {
        // Using the With<> syntax I can use this particular setup of this object
        // across tests/contexts
        Establish context = () => With<ComplexObjectSetup>();

        // The specific behaviour of the IMessageTransmitter has been injected in
        Because of = () => Subject.SendMessage("New test message");

        // And I can use public static methods on the Complex object to test its final state
        It should_send_the_message = () => ComplexObjectSetup.LastMessageSent = "New test message";
    }
}