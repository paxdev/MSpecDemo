using Machine.Specifications;

namespace MSpecDemo._1._Basics
{
    // A common pattern is to extract common test helper functions into base classes
    // MSpec will run all the Establishes in order up the inheritance chain
    // You only need the Subject attribute on the highest ancestor
	
    [Subject(typeof(MessageSender))]
    public abstract class MessageSenderContext
    {
        protected static MessageSender MessageSender;
        protected static bool MessageWasSent;
        protected static string Message;

        Establish context = () => MessageSender = new MessageSender();

        protected static void SetMessageToSend(string message)
        {
            Message = message;
        }

        protected static bool SendMessage()
        {
          return MessageSender.Send(Message);
        }
    }

    public class When_trying_to_send_an_empty_message_2 : MessageSenderContext
    {
        Establish context = () => SetMessageToSend(null);

        Because of = () => MessageWasSent = SendMessage();

        It should_not_send_the_message = () => MessageWasSent.ShouldBeFalse();
    }

    [Subject(typeof(MessageSender))]
    public class When_trying_to_send_a_message_with_some_text_2 : MessageSenderContext
    {
        Establish context = () => SetMessageToSend("My Message");

        Because of = () => MessageWasSent = SendMessage();

        It should_send_the_message = () => MessageWasSent.ShouldBeTrue();
    }
}