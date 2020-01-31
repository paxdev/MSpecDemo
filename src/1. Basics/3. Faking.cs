using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;

namespace MSpecDemo._1._Basics
{
	[Subject(typeof(DiMessageSender))]
    public class When_sending_a_message : WithFakes
	{
		// Inherit from WithFakes to automatically set up the Fake Engine

	    static DiMessageSender messageSender;
	    static IMessageTransmitter messageTransmitter;

	    static string sentMessage;

	    Establish context = () =>
	    {
			// If you want to tell the fake engine to return a specific instance of 
			// something you can tell it what to return using Configure.
	        Configure<IFoo>(new Foo());

	        // Get a fake instance. It's of type IMessageTransmitter with some helpful extra methods
	        messageTransmitter = An<IMessageTransmitter>();

			// An<Type> returns an instance
			// The<Type> behaves like a singleton instance
			// Some<Type> returns an IList of 3 fakes of your Type.
			
	        // Override the behaviour of a get only property
	        messageTransmitter.WhenToldTo(t => t.Status).Return("Some Status Message");

	        // get/set properties behave like properties - even though this is an interface
	        messageTransmitter.LastMessageSent = "My sent message";

			// Set expectations (i.e. return values) on methods based on value
			messageTransmitter.WhenToldTo(t => t.IsPortActive(123)).Return(true);
			
			// You can tell it to throw exceptions
			messageTransmitter.WhenToldTo(t => t.IsPortActive(456)).Throw(new ArgumentOutOfRangeException());
			
			// Or set a callback. Note the use of Param.IsAny<> to say that this happens for all input params
	        messageTransmitter.WhenToldTo(t => t.Transmit(Param.IsAny<string>()))
												.Callback((string message) => MyFakeCallback(message));

			messageSender = new DiMessageSender(messageTransmitter);
        };

	    static void MyFakeCallback(string message)
	    {
	        sentMessage = message;
	    }

	    Because of = () => messageSender.SendMessage("Test Message");

	    It should_have_sent_the_message = () => sentMessage.ShouldEqual("Test Message");

		// Check expectation. Note we can check for calls with specific parameters or use IsAny
	    It should_have_used_the_message_transmitter = 
			() => messageTransmitter.WasToldTo(t => t.Transmit(Param.IsAny<string>()));

		//Check number of times
		It should_only_use_message_transmitter_once =
            () => messageTransmitter.WasToldTo(t => t.Transmit(Param.IsAny<string>())).OnlyOnce();

	    It should_do_something_x_times =
	        () => messageTransmitter.WasToldTo(t => t.Transmit(Param.IsAny<string>())).Times(1);
	}

	public interface IFoo
    {
    
    }

    public class Foo : IFoo
    {
        
    }
}
