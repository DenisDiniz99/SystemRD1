using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SystemRD1.WebApp.Services.Customer;

namespace SystemRD1.WebApp.Controllers
{
    public class CustomersController : WebController
    {
        private readonly ICustomerServices _customerServices;

        public CustomersController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

       
        [Route("detalhes-clientes")]
        public async Task<IActionResult> Details()
        {
            var result = await _customerServices.GetCustomers();
            if(result == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}
