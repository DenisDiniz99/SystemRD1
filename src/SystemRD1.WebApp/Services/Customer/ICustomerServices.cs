using System.Collections;
using System.Threading.Tasks;

namespace SystemRD1.WebApp.Services.Customer
{
    public interface ICustomerServices
    {
        Task<IEnumerable> GetCustomers();
        Task GetCustomerById();
        Task AddCustomer();
        Task UpdateCustomer();
        Task DeleteCustomer();
    }
}
