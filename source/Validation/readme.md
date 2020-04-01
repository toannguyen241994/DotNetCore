# DotNetCore.Validation

The package provides classes for **validation**.

## Regexes

```cs
public static class Regexes
{
    public const string Date;

    public const string Decimal;

    public const string Email;

    public const string Hex;

    public const string Integer;

    public const string Login;

    public const string Password;

    public const string Tag;

    public const string Time;

    public const string Url;
}
```

## Validator

```cs
public abstract class Validator<T> : AbstractValidator<T>
{
    public new async Task<IResult> ValidateAsync(T instance, CancellationToken cancellation = default) { }
}
```
