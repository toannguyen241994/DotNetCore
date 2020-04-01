# DotNetCore.IoC

The package provides **static classes** with **extensions methods** for **inversion of control**.

## ServiceCollectionExtensions

```cs
public static class Extensions
{
    public static void AddClassesInterfaces(this IServiceCollection services, params Assembly[] assemblies) { }

    public static string GetConnectionString(this IServiceCollection services, string name) { }
}
```
