using System;
using System.IO.Compression;

namespace InternalSystems.Service
{
    public class ZipArchiveService
    {
        public byte[] ConvertFilesToBytesForZip(string filePath)
        {
            using (var ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    string[] files = Directory.Exists(filePath) ? Directory.GetFiles(filePath) : new string[0];

                    foreach (var path in files)
                    {
                        string fileName = Path.GetFileName(path);
                        using var fs = System.IO.File.OpenRead(path);
                        byte[] bytes = new byte[fs.Length];
                        ZipArchiveEntry addEntry = archive.CreateEntry(fileName, CompressionLevel.Fastest);

                        using (var entryStream = addEntry.Open())
                        using (var zipStream = new MemoryStream(bytes))
                        {
                            zipStream.CopyTo(entryStream);
                        }
                    }
                }

                return ms.ToArray();
            }
        }

    }
}

