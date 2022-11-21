using System.Collections.Concurrent;

namespace DirectoryScanner.Core
{
    public class Scanner
    {
        private uint _threadCount;
        private Semaphore _semaphore;
        private ConcurrentDictionary<Thread, int> _threads = new();

        public readonly DirectoryFile Root;

        private ConcurrentQueue<DirectoryFile> _queqe = new();
        private CancellationToken _token;

        public Scanner(uint threadCount, string path, CancellationToken token)
        {
            _threadCount = threadCount;
            _semaphore = new Semaphore((int)threadCount, (int)threadCount);
            Root = new DirectoryFile(path, null);
            _queqe.Enqueue(Root);
            _token = token;
        }

        public void StartProcess()
        {
            while (_queqe.Any() && !_token.IsCancellationRequested)
            {
                _semaphore.WaitOne();
                if (_queqe.TryDequeue(out DirectoryFile directory) && !_token.IsCancellationRequested)
                {
                    Thread thread = new(obj => ScanNode((DirectoryFile)obj));
                    _threads[thread] = thread.ManagedThreadId;
                    thread.Start(directory);
                }
                _semaphore.Release();
            }
        }

        private void ScanNode(DirectoryFile node)
        {
            //_semaphore.WaitOne();
            var dir = new DirectoryInfo(node.Fullpath);

            var newNodes = new List<File>();

            foreach (var subDir in dir.EnumerateDirectories())
            {
                if (_token.IsCancellationRequested) break;
                var subNode = new DirectoryFile(subDir.FullName, node);
                _queqe.Enqueue(subNode);

                newNodes.Add(subNode);
            }

            foreach (var file in dir.EnumerateFiles())
            {
                if (_token.IsCancellationRequested) break;
                newNodes.Add(new FileFile(file.FullName, file.Length, node));
            }

            node.Childs = newNodes;
            node.IsComplited = !_token.IsCancellationRequested;
            _threads.TryRemove(new (Thread.CurrentThread, Environment.CurrentManagedThreadId));
            //_semaphore.Release();
        } 
    }
}