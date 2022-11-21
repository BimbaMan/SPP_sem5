using DirectoryScanner.Core;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace DirectoryScaner.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<DirectoryFile> testNodes = new();
        private CancellationTokenSource _token;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputFile_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog dlg = new VistaFolderBrowserDialog();

            if (dlg.ShowDialog() == true)
            {
                ((Button)sender).Tag = dlg.SelectedPath;

                treeView1.ItemsSource = testNodes;

                _token = new CancellationTokenSource();

                testNodes.Clear();
                Scanner scanner = new Scanner(4, dlg.SelectedPath, _token.Token);
                testNodes.Add(scanner.Root);
                Thread thread = new(scanner.StartProcess);
                thread.Start();
            }
        }

        private void CancelBTN_Click(object sender, RoutedEventArgs e)
        {
            if(_token != null)
                _token.Cancel();
        }

        private void OutputFile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
