﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> GetById(Guid id)
        {
            if (id == null) return CustomResponse(id);

            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> Add(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            bool document = await _customerRepository.GetDocumentExists(customerViewModel.Document);

            if (document)
            {
                NotifyError("Já existe um cliente cadastrado com este documento");
                return CustomResponse(customerViewModel);
            }
            await _customerService.AddAsync(_mapper.Map<Customer>(customerViewModel));

            return CustomResponse(customerViewModel);
        }

        [HttpPut]
        public async Task<ActionResult<CustomerViewModel>> Update(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _customerService.UpdateAsync(_mapper.Map<Customer>(customerViewModel));

            return CustomResponse(customerViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Delete(Guid id)
        {
            if (id == null) return CustomResponse(ModelState);

            await _customerService.DeleteAsync(id);

            return CustomResponse();
        }
    }
}