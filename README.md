## EntityFramework Reverse POCO Code First Generator

This generator creates code like an expert reverse-engineered your database and created the code for you. Perfectly.

Reverse engineers an existing database and generates Entity Framework Code First Poco classes, Configuration mappings, Enumerations, DbContext, FakeDbContext (for easy unit testing) and calling Stored procedures and table-valued functions.

**Beautifully generated code first code that is fully customisable**

* Downloadable VSIX installer from the [visual studio gallery](https://marketplace.visualstudio.com/items?itemName=SimonHughes.EntityFrameworkReversePOCOGenerator).
* Watch the v2 in-depth course at [pluralsight](https://app.pluralsight.com/library/courses/code-first-entity-framework-legacy-databases/table-of-contents) ![logo](http://www.simonhughes.co.uk/pluralsight-logo-tiny.png) I cover everything the v2 generator can do, and show you step-by-step how to reverse engineer your database properly.

Please note that this is not the Microsoft reverse generator. This generator creates code as if you reverse-engineered a database and lovingly created the code by hand. It also allows you to customise the generated code to your liking.

### Give a Star! :star:
If you like or are using this project, please give it a star. Thanks!

### Watch a short video clip (no audio)
[![Watch the video](https://reversepocostorage.blob.core.windows.net/public-file-share/efcore-first-run.jpg)](https://reversepocostorage.blob.core.windows.net/public-file-share/efcore-first-run.mp4)

## To remove trial limitations, you will require a License key.
Free to academics (you need a .edu, .ac or .sch email address), not free for commercial use.

Go to the [ReversePOCO](https://www.reversepoco.co.uk) website for your License key.

### Upgrading v2 to v3
Please read the [Upgrading documentation](https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/wiki/Upgrading-from-v2-to-v3)

### What's new

[Click here](https://github.com/sjh37/EntityFramework-Reverse-POCO-Code-First-Generator/releases) to see what's new in this release.

### Supported databases

* SQL Server
* SQL Server Compact 3.5 and 4.0
* PostgreSQL
* Coming soon: Oracle, MySQL

### Highly customisable output

This generator is designed to be customisable from the very beginning and not fixed and rigid like other generators.
Play with the settings in the `<database>.tt` file. That's what it's there for.

If your database changes, re-save the `<database>.tt` file. That's it.

[Click here](https://github.com/sjh37/efreversepoco/wiki/Full-control-over-the-generated-code) to see a full list of features.

### To install and use this project:
* Use Nuget and install the relevant NuGet package for your database.
  - .Net Core: `install-package Microsoft.EntityFrameworkCore.SqlServer`
  - EF 6: `install-package EntityFramework`
* `Settings.ConnectionString` is mandatory in v3, so you need to provide the connection string from your app.config/web.config/appsettings.json file. The generator uses this connection string to reverse-engineer your database. It no longer reads your connection strings from *.config files.

   For example:

   Settings.ConnectionString = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=True;Encrypt=false;TrustServerCertificate=true";
* The `Settings.ConnectionString` string you use must have at least these privileges: `ddladmin`, `datareader` and `datawriter`. `ddladmin` is required for reading the default constraints.
* In Visual Studio, right click project and select "add - new item".
* Select Online, and search for **reverse poco**.
* Select **EntityFramework Reverse POCO Generator**.
* Give the file a name, such as `Database.tt` and click Add.
* Edit the `Database.tt` file and specify the full connection string in `Settings.ConnectionString`. The generater uses this to read your database schema and reverse engineer it.
* Edit the `Database.tt` file and specify the connection string in `Settings.ConnectionStringName` which matches the ConnectionString key as specified in your `appsettings.json`, `app.config` or `web.config`.
* Save the `Database.tt` file, which will now generate the `Database.cs` file. Every time you save your `Database.tt` file, the generator will reverse engineer your database.
* There are many options you can use to customise the generated code. All of these settings are in the `Database.tt` files.

### Connection strings
`Settings.ConnectionString` is mandatory in version 3. The generator uses it to read your database schema. The connection string is optionally placed into the OnConfiguring function:

```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){{#newline}}
{{{#newline}}
    if (!optionsBuilder.IsConfigured){{#newline}}
    {{{#newline}}
        optionsBuilder.UseSqlServer(@""{{ConnectionString}}"");{{#newline}}
    }{{#newline}}
}{{#newline}}{{#newline}}
```

`Settings.ConnectionStringName` This is not used by the generator but is placed into the generated DbContext constructor via a call to `Settings.DefaultConstructorArgument`.

```c#
public {{DbContextName}}(){{#newline}}
{{#if HasDefaultConstructorArgument}}
    : base({{DefaultConstructorArgument}}){{#newline}}
{{/if}}
```

### UI

A simple UI for the generator is available at
[GitHub](https://github.com/sjh37/EntityFramework-Reverse-POCO-Generator-UI), which helps you to create a regex to filter your tables.

### Editing TT (T4) Files
To have full syntax highlighting and intellisense when editing TT files, I use the Resharper plugin ForTea. I can't imagine editing TT files without it.

No need to edit the `EF.Reverse.POCO.v3.ttinclude` file directly as this file is generated from a C# project. This repository includes the BuildTT file that creates the `EF.Reverse.POCO.v3.ttinclude` from the `Generator` C# project.

### Getting a pull request accepted
Have a read of [https://github.com/blog/1943-how-to-write-the-perfect-pull-request](How to write the perfect pull request)

My requirements are simple:

1. Always keep the changes to a **minimum**, so I can see **exactly** what's changed regarding the pull request. I.e. No whitespace tidy-up, Etc.
2. No tabs, only spaces (4).
3. Edit the Generator C# project. Running this project will create the `EF.Reverse.POCO.v3.ttinclude` file. This repository includes the `BuildTT` project that creates the `EF.Reverse.POCO.v3.ttinclude` from the `Generator` C# project.
4. Don't be tempted to do a few different enhancements in one pull request. Have **one** pull request for **one** bug fix/enhancement.

Regards,
Simon Hughes

* E: [simon@reversepoco.co.uk](mailto:simon@reversepoco.co.uk)
* W: [about.me/simon.hughes](http://about.me/simon.hughes)
* B: [simon-hughes.blogspot.co.uk](http://simon-hughes.blogspot.co.uk)
