using System.IO;

namespace OOP_Application
{
    public interface IArchiverPlugin
    {
        string Extension { get; }
        void Compress(FileStream fileStream);
        void Decompress(MemoryStream fileStream);
    }
}
