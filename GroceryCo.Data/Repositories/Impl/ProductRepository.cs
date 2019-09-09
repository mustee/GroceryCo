using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using GroceryCo.Data.Serialization;

namespace GroceryCo.Data.Repositories.Impl
{
    public class ProductRepository : FileRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IFileReader reader, ITextSerializer serializer) 
            : base(reader, serializer)
        {
        }
    }
}
