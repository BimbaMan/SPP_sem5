namespace DirectoryScanner.Core
{
    public sealed class FileNode : Node
    {
        private readonly long _size;
        public FileNode(string path, long size, DirectoryNode parent) : base(path, parent)
        {
            _size = size;
        }

        public override long? Size => _size;
    }
}
