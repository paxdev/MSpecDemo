using System.IO.Pipes;
using System.Threading.Tasks;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Model;

namespace MSpecDemo._2._Async
{
    /*
     * See https://blog.paulaxon.co.uk/mspec/async-mspec-fakes/
     *
     * To mock an async method, you need to make sure that you return a task
     * This is easiest done using the extension methods as described in ReturnAsyncExtensions
     */

    [Subject("Mocking Async Methods")]
    public class When_it_is_specified_explicitly : WithFakes
    {
        static IAsync subject;
        static Task<string> result;

        Establish context = () =>
        {
            subject = An<IAsync>();
            subject
                .WhenToldTo(a => a.DoSomething())
                .Return(Task.FromResult("Done"));
        };

        Because of = () => result = subject.DoSomething();

        It should_return_a_task = () => result.ShouldBeAssignableTo<Task>();

        It should_return_the_result = () => result.Result.ShouldEqual("Done");
    }

    [Subject("Mocking Async Methods")]
    public class When_it_uses_extension_methods : WithFakes
    {
        static IAsync subject;
        static Task<string> result;

        Establish context = () =>
        {
            subject = An<IAsync>();
            subject
                .WhenToldTo(a => a.DoSomething())
                .ReturnAsync("Done");
        };

        Because of = () => result = subject.DoSomething();

        It should_return_a_task = () => result.ShouldBeAssignableTo<Task>();

        It should_return_the_result = () => result.Result.ShouldEqual("Done");
    }

    public interface IAsync
    {
        Task<string> DoSomething();
    }
}