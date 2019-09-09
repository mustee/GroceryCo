using GroceryCo.Data.Models;

namespace GroceryCo.Data.Repositories
{
    public interface IRepository<T> where T: IHasName
    {
        T Find(string name);
    }
}
