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

        public FileStream Decompress(FileStream compressedStream)
        {
            string tempFilePath = Path.GetTempFileName();
            FileStream tempFileStream = new FileStream(tempFilePath, FileMode.Create);
            using (GZipStream gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            {
                gzipStream.CopyTo(tempFileStream);
            }
            tempFileStream.Seek(0, SeekOrigin.Begin);
            return tempFileStream;
        }
    }
    
}
