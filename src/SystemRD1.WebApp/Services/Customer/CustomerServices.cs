using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SystemRD1.WebApp.Models.Customer;

namespace SystemRD1.WebApp.Services.Customer
{
    public class CustomerServices : Services, ICustomerServices
    {
        private readonly HttpClient _httpClient;
        

        public CustomerServices(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(BASE_URL);

            _httpClient = httpClient;
        }


        public async Task<IEnumerable> GetCustomers()
        {
            var response = await  _httpClient.GetAsync("/api/v1/customers");

            HandleErrors(response);
            
            return await DeserializeObjectResponse<IEnumerable<CustomerViewModel>>(response);
        }

        public async Task GetCustomerById()
        {
            throw new NotImplementedException();
        }

        public async Task AddCustomer()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCustomer()
        {
            throw new NotImplementedException();
        }      

        public async Task UpdateCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
