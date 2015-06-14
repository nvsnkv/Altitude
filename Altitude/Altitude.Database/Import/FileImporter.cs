using System;
using System.Collections.Generic;
using System.IO;
using Altitude.Database.Annotations;

namespace Altitude.Database.Import
{
    public class FileImporter
    {
        private class ReadWrapper:IDisposable   
        {
            private readonly StreamReader _reader;

            public ReadWrapper(string fileName)
            {
                _reader = new StreamReader(File.OpenRead(fileName));
            }

            public IEnumerable<string> GetLines()
            {
                while (!_reader.EndOfStream)
                    yield return _reader.ReadLine();
            }

            public void Dispose()
            {
                _reader.Dispose();
            }
        }

        private readonly StringImporter _importer;

        public FileImporter() : this(new StringImporter())
        {
        }

        public FileImporter([NotNull] StringImporter importer)
        {
            if (importer == null) throw new ArgumentNullException(nameof(importer));
            _importer = importer;
        }

        public void Import(string fileName)
        {
            using (var file = new ReadWrapper(fileName))
            {
                _importer.Import(file.GetLines());
            }
        }
    }
}