using GroceryCo.Data.FileReaders;
using GroceryCo.Data.Models;
using GroceryCo.Data.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryCo.Data.Repositories.Impl
{
    public class PromotionRepository : FileRepositoryBase<Promotion>, IPromotionRepository
    {
        public PromotionRepository(IFileReader reader, ITextSerializer serializer) 
            : base(reader, serializer)
        {
        }

        public IEnumerable<Promotion> FindByProduct(string name)
        {
            return items.Where(i => i.ProductName.Equals(name, StringComparison.OrdinalIgnoreCase) && i.StartDate <= DateTime.UtcNow && i.EndDate >= DateTime.UtcNow);
        }
    }
}
