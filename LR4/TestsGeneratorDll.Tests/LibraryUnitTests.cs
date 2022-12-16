using TestsGeneratorDll;

namespace TestsGeneratorDll.Tests
{
    public class LibraryUnitTests
    {
        public LibraryUnitTests()
        {
            List<string> files = new List<string>()
            {
                @"C:\Users\Иван\Desktop\Уник\СПП\LR4\TestsGeneratorConsole\bin\Debug\TestsGeneratorConsole.exe",
                @"C:\Users\Иван\Desktop\Уник\СПП\LR4\TestsGeneratorDll\bin\Debug\TestsGeneratorDll.dll"
            };
            TestsGenerator.GenerateXUnitTests(files, @"C:\Users\Иван\Desktop\Уник\СПП\LR4\Result", 10);
        }

        [Fact]
        public void GenerateTests_WithSpecificFiles_ReturnRightNumberOfTestClassesGenerated()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\Иван\Desktop\Уник\СПП\LR4\Result");

            int filesCount = directoryInfo.GetFiles().Length;

            Assert.Equal(6, filesCount);
        }


        [Fact]
        public void GenerateTests_WithSpecificFiles_ReturnNotEmptyGeneratedFiles()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"C:\Users\Иван\Desktop\Уник\СПП\LR4\Result");

            var files = directoryInfo.GetFiles();

            bool isAnyFileEmpty = false;

            foreach (var file in files)
            {
                if (file.Length == 0)
                {
                    isAnyFileEmpty = true;
                    break;
                }
            }
            Assert.False(isAnyFileEmpty);
        }
    }
}