namespace DirectoryScanner.Core
{
    public sealed class DirectoryFile : File
    {
        public bool IsComplited
        {
            get => _isComplited;
            set
            {
                _isComplited = value;
                OnPropertyChanged(nameof(FormatedSize));
                OnPropertyChanged(nameof(Childs));
                OnPropertyChanged(nameof(FormatedPercent));
            }
        }

        public IEnumerable<File> Childs
        {
            get => _childs;
            set {
                _childs = value;
                OnPropertyChanged(nameof(Childs));
            }
        }

        private IEnumerable<File> _childs;
        private bool _isComplited = false;

        public DirectoryFile(string path, DirectoryFile? directoryNode) : base(path, directoryNode)
        {
        }

        public override long? Size => IsComplited && Childs.All(x => x.Size.HasValue) ? Childs.Sum(x => x.Size) : null;
    }
}
