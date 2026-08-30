# Settings Reference

Every setting in `Database.tt`, A to Z, with a one-line summary and a link to its page.

**Each page shows the generated code before and after**, produced by running the generator rather than typed
from memory, and re-checked by a test so it cannot quietly stop being true.

Settings that are always set together, or that only make sense read together, share a page - the two
collection settings, the five class modifier settings, the three connection string settings. The link takes
you to the right place either way.

## Where to start

New to the generator, or working out why something is not happening:

* [Settings.DatabaseType, TemplateType and GeneratorType](Settings.DatabaseType) - the three you must get right first
* [Settings.ConnectionString and friends](Settings.ConnectionStringName) - and why there are three of them
* [Settings.ElementsToGenerate](Settings.ElementsToGenerate) - which classes get generated at all
* [Settings.GenerateSeparateFiles](Settings.GenerateSeparateFiles) - one file or many
* [Settings.UpdateColumn](Settings.UpdateColumn) - the main hook for reshaping the model
* [Filtering](Filtering) - which tables and columns are read in the first place

Related pages that cover a whole topic rather than one setting:
[Settings Callbacks](Settings-Callbacks) |
[Common Settings Types Explained](Common-Settings-Types-Explained) |
[Full Control Over the Generated Code](Full-Control-Over-the-Generated-Code) |
[Settings runtime values and helpers](Settings.Runtime-Values)

## A to Z

| Setting | What it does |
|---------|--------------|
