using System;
using System.Net.Sockets;
using Machine.Fakes;
using Machine.Specifications;

namespace MSpecDemo._1._Basics
{
    [Subject("Testing for Exceptions")]
    public class When_calling_a_method_that_throws_an_exception : WithSubject<DiMessageSender>
    {
        static Exception caughtException;

        // set up my IMessageTransmitter to throw an exception
        Establish context = () => 
            The<IMessageTransmitter>()
                .WhenToldTo(t => t.Transmit(Param.IsAny<string>()))
                .Throw(new SocketException());

        // Call the method you expect to throw inside Catch.Exception
        Because of = () => 
            caughtException = Catch.Exception(() => Subject.SendMessage("Test Message"));

        It should_throw_an_exception = () => caughtException.ShouldNotBeNull();

        // Test the correct exception was thrown
        It should_be_expected_type = () => caughtException.ShouldBeOfExactType<SocketException>();
    }
}