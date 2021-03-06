using DotNetCore.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DotNetCore.Objects
{
    public class BinaryFile
    {
        public BinaryFile
        (
            Guid id,
            string name,
            byte[] bytes,
            long length,
            string contentType
        )
        {
            Id = id;
            Name = name;
            Bytes = bytes;
            Length = length;
            ContentType = contentType;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public byte[] Bytes { get; private set; }

        public long Length { get; private set; }

        public string ContentType { get; private set; }

        public static async Task<BinaryFile> ReadAsync(string directory, Guid id)
        {
            if (string.IsNullOrWhiteSpace(directory) || id == Guid.Empty) { return null; }

            var file = new DirectoryInfo(directory).GetFile(id.ToString());

            if (file == null) { return null; }

            var bytes = await File.ReadAllBytesAsync(file.FullName);

            if (bytes == null || bytes.LongLength == 0) { return null; }

            return new BinaryFile(id, file.Name, bytes, file.Length, file.Extension);
        }

        public async Task SaveAsync(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory) || string.IsNullOrWhiteSpace(Name) || Bytes == null || Bytes.LongLength == 0) { return; }

            Directory.CreateDirectory(directory);

            var name = string.Concat(Id, Path.GetExtension(Name));

            var path = Path.Combine(directory, name);

            await File.WriteAllBytesAsync(path, Bytes);

            Bytes = null;
        }
    }
}
