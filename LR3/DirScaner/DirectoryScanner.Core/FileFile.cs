namespace DirectoryScanner.Core
{
    public sealed class FileFile : File
    {
        private readonly long _size;
        public FileFile(string path, long size, DirectoryFile parent) : base(path, parent)
        {
            _size = size;
        }

        public override long? Size => _size;
    }
}
