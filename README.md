DbUnderscorer is a set of very simple extension methods for .NET Core Entity Framework's DbContext class which allows you to, via a single method call, lowercase-underscore (or snake case, if you prefer that term!) all table names, column names, primary and foreign keys, and indices in your backing data store.

This is especially useful if you're using PostgreSQL and find yourself needing to interact with your data via SQL.  Pesky capital letters don't make that fun!

### Prerequisites
See src/DbUnderscorer.csproj for the minimal dependencies.  This project is built on top of .NET Core 2.1-related packages for Entity Framework, and uses the latest 2.x releases of Humanizer.

### Usage
In your DbContext's OnModelCreating override, simply issue a call to `this.UnderscoreDatabase(modelBuilder)`.  This should be done after the call to `base.OnModelCreating(modelBuilder)` in order for any inherited behavior to have had a chance to run, such as the additional tables added by the `IdentityDbContext` class found in Microsoft.AspNetCore.Identity.EntityFrameworkCore.

Example:
```
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.UnderscoreDatabase(modelBuilder);
        }
```

### What's next?
A NuGet package is on deck next, for simple integrations.