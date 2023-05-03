using System.IO.Compression;
using System.IO;

namespace GZipArchiverPlugin
{
    public class GZipArchiver : OOP_Application.IArchiverPlugin
    {

        public string Extension => "*.gz";

        public void Compress(FileStream fileStream)
        {                
            fileStream.Seek(0, SeekOrigin.Begin);
            using (MemoryStream compressedStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
                {
                    fileStream.CopyTo(gzipStream);
                }
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.SetLength(compressedStream.Length);
                compressedStream.Seek(0, SeekOrigin.Begin);
                compressedStream.CopyTo(fileStream);
            }
        }

        public void Decompress(MemoryStream archiveStream)
        {
            archiveStream.Seek(0, SeekOrigin.Begin);
            using (var gzip = new GZipStream(archiveStream, CompressionMode.Decompress, leaveOpen: true))
            {
                var unpackedStream = new MemoryStream();
                gzip.CopyTo(unpackedStream);
                unpackedStream.Seek(0, SeekOrigin.Begin);
                archiveStream.SetLength(unpackedStream.Length);
                archiveStream.Seek(0, SeekOrigin.Begin);
                unpackedStream.CopyTo(archiveStream);
            }
            archiveStream.Seek(0, SeekOrigin.Begin);
        }
    }
    
}
