using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemRD1.Api.ViewModels;
using SystemRD1.Business.Contracts.Notifiers;
using SystemRD1.Business.Contracts.Services;
using SystemRD1.Domain.Contracts;
using SystemRD1.Domain.Entities;

namespace SystemRD1.Api.Controllers.V1
{
    [Route("api/{controller}")]
    public class CustomersController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, ICustomerService customerService, 
                                    IMapper mapper, INotifier notifier) : base(notifier)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> Get()
        {
            return ResponseGet(_mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAllAsync()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Get(Guid id)
        {
            return ResponseGet(_mapper.Map<CustomerViewModel>(await _customerRepository.GetByIdAsync(id)));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetByName(string name)
        {
            return ResponseGet(_mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetByName(name)));
        }

        [HttpGet("{document}")]
        public async Task<ActionResult<CustomerViewModel>> GetByDocument(string document)
        {
            return ResponseGet(_mapper.Map<CustomerViewModel>(await _customerRepository.GetByDocument(document)));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> Post(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            bool document = await _customerRepository.DocumentExists(customerViewModel.Document);

            if (document)
            {
                NotifyError("Já existe um cliente cadastrado com este documento");
                return ResponsePost(customerViewModel);
            }

            await _customerService.AddAsync(_mapper.Map<Customer>(customerViewModel));

            return ResponsePost(customerViewModel);
        }

        [HttpPut]
        public async Task<ActionResult<CustomerViewModel>> Put(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyInvalidModelError(ModelState);
                return ModelStateErrorResponseError();
            }

            if (!await CustomerExists(customerViewModel.Id))
            {
                NotifyError("Cliente não encontrado");
                return ResponsePut();
            }

            await _customerService.UpdateAsync(_mapper.Map<Customer>(customerViewModel));

            return ResponsePut();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Delete(Guid id)
        {
            if (!await CustomerExists(id))
            {
                NotifyError("Cliente não encontrado");
                return ResponseDelete(id);
            }

            await _customerService.DeleteAsync(id);

            return ResponseDelete(id);
        }



        private async Task<bool> CustomerExists(Guid id)
        {
            return await _customerRepository.CustomerExists(id);
        }
    }
}
