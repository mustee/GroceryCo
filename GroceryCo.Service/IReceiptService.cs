using GroceryCo.Service.Models;

namespace GroceryCo.Service
{
    public interface IReceiptService
    {
        void Print(Sale sale);
    }
}
