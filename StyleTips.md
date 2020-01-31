* When written well, MSpec tests should read like English. Ideally they should read quite similar to "acceptance criteria" for
your classes. When someone comes to look at your code in several years time, they should be able to read your MSpec tests
and very quickly understand what your class does.

* The idea is to test behaviour not implementation, so instead of something like:

	```csharp
	When_calling_send_message_with_null
		It should_return_false_from_the_method
	```

	You might write something like: 

	```csharp
	When_there_is_no_message
		It should_not_send_a_message
	```

	That way if you change the implementation of your class, a lot of the tests will stay the same. The names of the test will
	only change if you change the behaviour of your class, not the implementation.

* Only instantiate static variables in the Establish delegate. The classes are all instantiated several times, so if you have expensive
set up code it should run only once when the Establish delegate is called.

* I generally try to structure it so that there is a folder for a certain class I'm testing, within that folder there will be 
subfolders of separate behaviours, then each class is in a separate file under those folders, e.g.

	```
	EmailingSpecifications
	EmailingSpecifications\SendingAnEmail
	EmailingSpecifications\SendingAnEmail\When_there_is_no_message.cs
	EmailingSpecifications\SendingAnEmail\When_the_email_address_is_invalid.cs
	EmailingSpecifications\ReceievingAnEmail\When_the_sender_is_blocked.cs
	... etc.
	```

* Use lots of abstract base classes to separate out common functionality to make your tests super-readable

* Each class represents a context, i.e. an initial set of conditions. You then have lots of specifications, 
so it might be something like

	```csharp
	When_the_sender_is_blocked
	{
		It should_delete_the_email
		It should_notify_the_antivirus
		It should_create_a_log_record
		... etc. 
	}
	```