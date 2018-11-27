# sample-to-do

a reference application for developing custom features with cloudscribe Core

## Login Credentials

admin@admin.com and password is admin

## Rationale

People often have questions about how to build custom features that integrate with cloudscribe Core. There really isn't a lot of prescription for how you build things with cloudscribe Core as it is very open ended, so experienced developers can implement things just as they would normally do in ASP.NET Core. But for those who are relatively new to ASP.NET and Entity Framework Core there are often questions about how to setup your own DbContext, how to structure your solution, and other general patterns.

cloudscribe doesn't really impose a prescription about how to do things, and there is not "one correct way" of doing software development, but still it can be beneficial to see how others organize their code and what patterns they use.

This sample is intended to show a reasonable approach to building a typical CRUD (Create, Retrieve, Update, Delete) type of application. It is a very basic "To Do" feature, but it illustrates a few important concepts. For the purposes of this sample you should think of the ToDo feature as your own custom feature, and you could use the patterns shown here in implementing other custom features.


## Points of Interest Illustrated in this solution

* Organizing feature code into class libraries and using the main web application primarily as a composition root where everything gets wired up.
* How to keep Entity Framework data access code and migrations in a sepoarate class library
* How to [generate migrations](/src/acme.ToDo.Data/README.md) when the EF code is in a separate class library
* How to automatically apply EFCore migrations during application startup, see Program.cs in the web app
* How to keep views in a separate class library so you can easily change to different views in the future (ie using bootstrap 4 now but what if something else comes along in the future)
* How to use dependency injection and organize dependency registration into tidy extension methods to keep the startup code uncluttered.
* The ToDo feature uses a named authorization policy "ToDoPolicy" which requires an authenticated user.
* The menu item for the ToDo feature is in the navigation.xml file and uses the named authorization policy to filter it from the menu if not logged in.
* The ToDoService depends on the cloudscribe IUserContextResolver to get the current user in order to tag the ToDo items with the userid and the siteid, and in order to retrieve them using userid and siteid
* The example includes implementations of cloudscribe IHandleSitePreDelete and IHandleUserPreDelete in order to delete To Do items if a user or site is deleted.
