using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DirectoryScanner.Core
{
    public abstract class File : INotifyPropertyChanged
    {
        public abstract long? Size { get; }

        public string FormatedSize => Size.HasValue ? $"{Size.Value} bytes" : "-";

        public string Name => Path.GetFileName(Fullpath);

        public string Fullpath;

        public DirectoryFile? Parent;

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public float? Percent => Parent != null && Parent.Size.HasValue ? (Size / ((float)Parent.Size))*100 : null;

        public string FormatedPercent => Percent.HasValue ? $"{Percent.Value:0}%" : "";

        public File(string path, DirectoryFile? directoryNode)
        {
            Fullpath = path;
            Parent = directoryNode;
            if(Parent != null)
            {
                PropertyChanged += (x,y) => Parent.OnPropertyChanged(y.PropertyName);
            }
           
        }
    }
}
