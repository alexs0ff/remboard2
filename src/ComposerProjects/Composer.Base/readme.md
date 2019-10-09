How to create migration:

```console
dotnet ef migrations add <MigrationName> -c RemboardContext -p ComposerProjects/Composer.Base -s ComposerProjects/Composer.Base 
```

How to update database:

```console
dotnet ef database update -c RemboardContext -p ComposerProjects/Composer.Base -s ComposerProjects/Composer.Base 
```