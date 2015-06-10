using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace ResourceMerge.Core
{
    internal static class Helper
    {
        internal enum CompressionType
        {
            GZip,
            Delfate
        }

        internal static byte[] UnCompress(byte[] bytes, CompressionType type)
        {
            if (bytes == null || bytes.Length == 0)
                return new byte[0];
            using (var stream = type == CompressionType.GZip
                ? (Stream)new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress)
                : new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress))
            {
                var buffer = new byte[4096];
                using (var memory = new MemoryStream())
                {
                    int count;
                    do
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    bytes = memory.ToArray();
                }
            }
            return bytes;
        }

        internal static void Compress(string content, Stream s, CompressionType type)
        {
            using (Stream stream = type == CompressionType.Delfate ?
                (Stream)new DeflateStream(s, CompressionMode.Compress) :
                new GZipStream(s, CompressionMode.Compress))
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
