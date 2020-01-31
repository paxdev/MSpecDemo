using Machine.Specifications;

namespace MSpecDemo._1._Basics
{
    /* 
        Setting a subject at the top of the class makes the name of the test subject 
        show up in the test runner 
       
        You should try to name the class with a human readable sentence that describes 
        the behaviour you're testing.
        This should read as a specification of the class     
    */
    [Subject(typeof(MessageSender))]
    public class When_trying_to_send_an_empty_message
    {
        // MSpec uses delegate functions to run the tests
        // The test runner instantiates the classes and looks for
        // Establish, which is the equivalent of arrange (or given)
        // Because, which is the equivalent of act (or when)
        // It, which is the equivalent of assert (or then)

        // Static variables are used to communicate between
        // the delegate functions. 
        // You should ensure that any initialisation is done
        // in the Establish method       
             
        static MessageSender messageSender;
        static bool messageWasSent;
        
        // Setting up the test
        Establish context = () => messageSender = new MessageSender();

        // What we're testing
        Because of = () => messageWasSent = messageSender.Send(null);

        // Test that it worked
        // There are lots of really helpful .Should extensions 
        // such as .ShouldBeTrue(), ShouldBeNull(), ShouldEqual(), ShouldBeOfExactType<type>()
        It should_not_send_the_message = () => messageWasSent.ShouldBeFalse();
    }

    [Subject(typeof(MessageSender))]
    public class When_trying_to_send_a_message_with_some_text
    {
        static MessageSender messageSender;
        static bool messageWasSent;

        Establish context = () => messageSender = new MessageSender();

        Because of = () => messageWasSent = messageSender.Send("test message");

        It should_send_the_message = () => messageWasSent.ShouldBeTrue();
    }
}