# DotNetCore.Objects

The package provides generic classes for **objects**.

## BinaryFile

```cs
public class BinaryFile
{
    public BinaryFile(Guid id, string name, byte[] bytes, long length, string contentType) { }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public byte[] Bytes { get; private set; }

    public long Length { get; private set; }

    public string ContentType { get; private set; }

    public static async Task<BinaryFile> ReadAsync(string directory, Guid id) { }

    public async Task SaveAsync(string directory) { }
}
```

## BinaryFileExtensions

```cs
public static class BinaryFileExtensions
{
    public static async Task<IEnumerable<BinaryFile>> SaveAsync(this IEnumerable<BinaryFile> files, string directory) { }
}
```

## Order

```cs
public class Order
{
    public Order() { }

    public bool Ascending { get; set; }

    public string Property { get; set; }
}
```

## Page

```cs
public class Page
{
    public Page() { }

    public int Index { get; set; }

    public int Size { get; set; }
}
```

## PagedListParameters

```cs
public class PagedListParameters
{
    public Order Order { get; set; }

    public Page Page { get; set; }
}
```

## PagedList

```cs
public class PagedList<T>
{
    public PagedList(IQueryable<T> queryable, PagedListParameters parameters) { }

    public long Count { get; }

    public IEnumerable<T> List { get; }
}
```

## PagedListExtensions

```cs
public static class PagedListExtensions
{
    public static PagedList<T> List<T>(this IQueryable<T> queryable, PagedListParameters parameters) { }

    public static Task<PagedList<T>> ListAsync<T>(this IQueryable<T> queryable, PagedListParameters parameters) { }
}
```
