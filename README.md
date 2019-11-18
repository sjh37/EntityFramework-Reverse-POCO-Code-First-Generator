## EntityFramework Reverse POCO Code First Generator

**Beautifully generated code first code that is fully customisable**
* Downloadable VSIX installer from the [visual studio gallery](https://visualstudiogallery.msdn.microsoft.com/ee4fcff9-0c4c-4179-afd9-7a2fb90f5838).
* Watch the v2 in-depth course at [pluralsight](https://app.pluralsight.com/library/courses/code-first-entity-framework-legacy-databases/table-of-contents) ![logo](http://www.simonhughes.co.uk/pluralsight-logo-tiny.png) I cover everything the v2 generator can do, and show you step-by-step how to reverse engineer your database properly.
* A v3 course will be coming in 2020.

### Project Description

Reverse engineers an existing database and generates Entity Framework Code
First Poco classes, Configuration mappings and DbContext.

Please note, this is not the Microsoft reverse generator.
This is one I created to generate beautiful code-first code, as if I
had hand-crafted the code-first code myself. It also allows me to customise
the generated code to my liking.

### What's new

[Click here](https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/releases) to see what's new in this release.

### Supported databases

* SQL Server
* SQL Server Compact 3.5 and 4.0
* Comming soon: Oracle, PostgreSQL, MySQL

### Highly customisable output

This generator was designed to be customisable from the very beginning,
and not fixed and rigid like other generators.
Go and play with the settings in the `<database>.tt` file, that's what it's there for.

If your database changes, simply re-save the `<database>.tt` file. That's it.

[Click here](https://github.com/sjh37/efreversepoco/wiki/Full-control-over-the-generated-code) to see a full list of features.

### To install and use this project:

- EF 6
  * For Visual Studio 2012 & 2013, install Entity Framework 6 Tools
  [available here](http://www.microsoft.com/en-us/download/details.aspx?id=40762)
  This installs the required EF6.Utility.CS.ttinclude which is used for pluralisation
  You only need to do this once.
  * Use Nuget and install EntityFramework.
  * Add a connect string to your app.config. Something like:
```xml
<connectionStrings>
  <add name="MyDbContext"
       providerName="System.Data.SqlClient"
       connectionString="Data Source=(local);Initial Catalog=MyDatabase;Integrated Security=True;" />
</connectionStrings>
```
- .NET Core
  * Use Nuget and install the relevant nuget package for your datbase, such as `Microsoft.EntityFrameworkCore.SqlServer`

* The connection string you use must have at least these privileges: `ddladmin`, `datareader` and `datawriter`.
  `ddladmin` is required for reading the default constraints.
* In Visual Studio, right click project and select "add - new item".
* Select Online, and search for **reverse poco**.
* Select **EntityFramework Reverse POCO Generator**.
* Give the file a name, such as `Database.tt` and click Add.

- .NET Core
  * Edit the `Database.tt` file and specify the full connection string in `Settings.ConnectionString`. This is used by the generater to read your database schema and reverse engineer it.
  * Edit the `Database.tt` file and specify the connection string in `Settings.ConnectionStringName` which matches the ConnectionString key as specified in your `appsettings.json`, `app.config` or `web.config`.
- EF 6
  * Edit the `Database.tt` file and specify the connection string as "**MyDbContext**" which matches your name in `app.config`.

* Save the `Database.tt` file, which will now generate the `Database.cs` file. Every time you save your `Database.tt` file, the generator will run and reverse engineer your database.
* There are many options you can use to customise the generated code. All of these settings are in the `Database.tt` files.

### UI

A simple UI for the generator is available at
[GitHub](https://github.com/sjh37/EntityFramework-Reverse-POCO-Generator-UI) which helps you to create a regex to filter your tables.

### Editing TT (T4) Files
To have full syntax highlighting and intellisense when editing TT files, I use the Resharper plugin ForTea. I can't imagine editing TT files without it.

With the new v3, you no longer have to edit the `EF.Reverse.POCO.v3.ttinclude` file as this file is now generated from a C# project. This repository includes the BuildTT file which creates the `EF.Reverse.POCO.v3.ttinclude` from the `Generator` C# project.

### Getting a pull request accepted
Have a read of [https://github.com/blog/1943-how-to-write-the-perfect-pull-request](How to write the perfect pull request)

My requirements are simple:

1. Always keep the changes to a **minimum**, so I can see **exactly** what's changed in regard to the pull request. I.e. No whitespace tidy up, etc.
2. No tabs, only spaces (4).
3. Edit the Generator C# project, as this is what is used to create the `EF.Reverse.POCO.v3.ttinclude` file. This repository includes the `BuildTT` project which creates the `EF.Reverse.POCO.v3.ttinclude` from the `Generator` C# project.
4. Don't be tempted to do a few different enhancements in one pull request. Have **one** pull request for **one** bug fix / enhancement.

Regards,
Simon Hughes

* E: [simon@reversepoco.co.uk](mailto:simon@reversepoco.co.uk)
* W: [about.me/simon.hughes](http://about.me/simon.hughes)
* B: [simon-hughes.blogspot.co.uk](http://simon-hughes.blogspot.co.uk)
