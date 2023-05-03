using System.IO;
using System.IO.Compression;
using System.IO.Pipes;

namespace ZipArchiverPlugin
{
    public class ZipArchiver : OOP_Application.IArchiverPlugin
    {
        public string Extension => "*.zip";

        public void Compress(FileStream fileStream)
        {
            string originalFile = Path.GetFileNameWithoutExtension(fileStream.Name);
            fileStream.Seek(0, SeekOrigin.Begin);
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(originalFile);
                using (var entryStream = entry.Open())
                {
                    fileStream.CopyTo(entryStream);
                }
            }
        }

        public void Decompress(MemoryStream archiveStream)
        {
            using (ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Read, true))
            {
                ZipArchiveEntry entry = archive.Entries[0];
                using (Stream stream = entry.Open())
                {
                    archiveStream.Seek(0, SeekOrigin.Begin);
                    archiveStream.SetLength(entry.Length);
                    stream.CopyTo(archiveStream);
                }
                archiveStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
