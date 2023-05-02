using System.IO;
using System.IO.Compression;

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

        public FileStream Decompress(FileStream archiveStream)
        {
            using (ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Read, true))
            {
                ZipArchiveEntry entry = archive.Entries[0];
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Stream stream = entry.Open())
                    {
                        stream.CopyTo(memoryStream);
                    }
                    string fileName = entry.Name;
                    var output = new FileStream(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    output.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                    output.Seek(0, SeekOrigin.Begin);
                    archiveStream.Close();
                    return output;
                }
            }
        }

    }
}
