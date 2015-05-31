using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altitude.Domain.Storage
{
    public class StorageManager
    {
        private readonly Accuracy _accuracy;
        private readonly ICollection<Position> _storage;

        public StorageManager(ICollection<Position> storage, Accuracy desiredAccuracy)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage), "Unable to manage a null storage");

            if (desiredAccuracy.Horizontal <= 0 || desiredAccuracy.Vertical <= 0)
                throw new ArgumentOutOfRangeException(nameof(desiredAccuracy), "Both accuracy fields should be positive");

            _storage = storage;
            _accuracy = desiredAccuracy;
        }

        public bool Add(Position item)
        {
            if (item.Accuracy.CompareTo(_accuracy) < 1)
            {
                _storage.Add(item);
                return true;
            }

            return false;
        }

        public int StoredCount
        {
            get { return _storage.Count; }
        }

        public void Clear()
        {
            _storage.Clear();
        }

        public void Export(IExporter exporter)
        {
            exporter.Prepare();
            foreach(var item in _storage)
            {
                exporter.Export(item);
            }
            exporter.Finish();
        }
    }
}
