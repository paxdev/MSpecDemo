# Add MSpec to a project that does not already reference it

```shell
dotnet add package Machine.Specifications
dotnet add package Machine.Specifications.Should 
dotnet add package Machine.Specifications.Runner.VisualStudio
dotnet add package Machine.Fakes
dotnet add package Machine.Fakes.Moq
```

# Install the R# testing plugin
Go to ReSharper/Extension Manager/Options (or Extensions/Resharper ... in VS 2019)
Search for Machine.Specifications and you should see the runner.

When you have the R# Runner installed you can use the R# menu to find and run tests.
You will get an icon next to any class with tests in which you can right-click on to run/debug all tests in that class
You can run/debug individual tests by right-clicking the icon next to each individual It should_
I personally find it works well to right click on a folder, project or solution and Run All Tests from there. 
As you add new tests the R# runner will discover them and add them to your playlist