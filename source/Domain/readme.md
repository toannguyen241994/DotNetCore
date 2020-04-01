# DotNetCore.Domain

The package provides generic classes for **domain**.

## Entity

```cs
public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    protected Entity() { }

    protected Entity(TId id) { }

    public TId Id { get; }

    public static bool operator !=(Entity<TId> a, Entity<TId> b) { }

    public static bool operator ==(Entity<TId> a, Entity<TId> b) { }

    public override bool Equals(object obj) { }

    public bool Equals(Entity<TId> other) { }

    public override int GetHashCode() { }
}
```

## Event

```cs
public abstract class Event
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime DateTime { get; } = DateTime.UtcNow;
}
```

## ValueObject

```cs
public abstract class ValueObject
{
    public static bool operator !=(ValueObject a, ValueObject b) { }

    public static bool operator ==(ValueObject a, ValueObject b) { }

    public override bool Equals(object obj) { }

    public override int GetHashCode() { }

    protected abstract IEnumerable<object> GetEquals();
}
```
