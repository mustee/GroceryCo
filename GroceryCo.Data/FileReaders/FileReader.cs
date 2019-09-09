using System;
using System.IO;

namespace GroceryCo.Data.FileReaders
{
    public class FileReader : IFileReader
    {
        protected readonly string filePath;

        public FileReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            if (!File.Exists(filePath)) throw new FileNotFoundException(nameof(filePath));

            this.filePath = filePath;
            
        }

        public string Read()
        {
            return File.ReadAllText(filePath);
        }

    }
}
