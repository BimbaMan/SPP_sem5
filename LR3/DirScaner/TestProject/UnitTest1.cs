using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryScanner.Core;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ChildrenTest()
        {
            var _token = new CancellationTokenSource();

            Scanner scanner = new(4, @"D:\music", _token.Token);
            scanner.StartProcess();
            Assert.IsTrue(scanner.Root.Childs.Count() == 14);
        }

        [TestMethod]
        public void FolderTest()
        {
            var _token = new CancellationTokenSource();

            Scanner scanner = new(10, @"D:\music", _token.Token);
            scanner.StartProcess();

            Assert.IsTrue(scanner.Root.Childs.All(x => x is DirectoryFile));
        }

        [TestMethod]
        public void PercentTest()
        {
            var _token = new CancellationTokenSource();

            Scanner scanner = new(10, @"D:\music\placebo", _token.Token);
            scanner.StartProcess();

            Assert.IsTrue(scanner.Root.Childs.Count() == 1 && ((List<File>)scanner.Root.Childs)[0].Percent == 100);
        }
    }
}