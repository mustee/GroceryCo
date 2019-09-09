using GroceryCo.Data.Models;
using System.Collections.Generic;

namespace GroceryCo.Data.Repositories
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
        IEnumerable<Promotion> FindByProduct(string name);
    }
}
