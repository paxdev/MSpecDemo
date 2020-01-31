using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;

namespace MSpecDemo._2._Async
{
    /*
     * Out of the box there is limited support for testing async methods
     * You need to make sure that you return a result and not a Task
     * This can be done by getting the .Result of the returned Task
     */

    [Subject("Testing Async Methods")]
    public class When_testing_async_methods
    {
        static Async subject;
        static string result;

        Establish context = () => subject = new Async();

        Because of = () => 
            result = subject
                .DoSomething()
                .Result; // <= note forcing the Task to complete and return Result

        It should_not_return_a_task = () => result.ShouldNotBeAssignableTo<Task>();

        It should_return_the_result = () => result.ShouldEqual("Done");
    }

    public class Async
    {
        public async Task<string> DoSomething()
        {
            var t = new Task(() => Thread.Sleep(1));
            t.Start();
            await t;
            return "Done";
        }
    }
}