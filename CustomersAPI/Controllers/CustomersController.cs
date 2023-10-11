using AutoMapper;
using CustomersAPI.DTOs.Customer;
using CustomersAPI.Interfaces;
using CustomersAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomer _customerService;
        private IMapper _mapper;

        public CustomersController(ICustomer customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }


        // POST api/<CustomersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCustomerDTO customerDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(customerDto);
                await _customerService.AddCustomer(customer);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

          
        }

    }
}
