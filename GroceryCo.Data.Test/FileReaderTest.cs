using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using System;
using System.IO;
using Xunit;

namespace GroceryCo.Data.Test
{
    public class FileReaderTest
    {

        [Fact]
        public void CreateReaderException()
        {
            Assert.Throws<ArgumentNullException>(() => new FileReader(null));
            Assert.Throws<ArgumentNullException>(() => new FileReader(""));

            Assert.Throws<FileNotFoundException>(() => new FileReader("a.txt"));
        }
    }
}
