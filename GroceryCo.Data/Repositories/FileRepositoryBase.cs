using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using GroceryCo.Data.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Data.Repositories
{
    public abstract class FileRepositoryBase<T> : IRepository<T>
        where T : IHasName
    {
        protected IList<T> items;

        protected FileRepositoryBase(IFileReader reader, ITextSerializer serializer)
        {
            var text = reader.Read();
            items = serializer.Deserialize<IList<T>>(text);
        }

        public T Find(string name)
        {
            return items.FirstOrDefault(i => i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
