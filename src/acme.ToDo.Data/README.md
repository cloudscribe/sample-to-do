### How to generate migrations

After adding or updating models or making changes to the dbcontext you need to generate a migration from the command line.

open a command/powershell window in this project folder

Since this project is a netstandard20 class library it is not executable, therefore you have to pass in the --startup-project that is executable

dotnet ef --startup-project ../acme.WebApp migrations add  --context acme.ToDo.Data.ToDoDbContext acme-todo-yyyymmdd

The migrations are applied automatically from code in Program.cs in the acme.WebApp

