using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehiclesAPI.DTOs.Vehicle;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehiclesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private IVehicle _vehicleService;
        private IMapper _mapper;

        public VehiclesController(IVehicle vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        // GET: api/<VehiclesController>
        [HttpGet]
        public async Task<IEnumerable<Vehicle>> Get()
        {
            var vehicles = await _vehicleService.GetAllVehicles();
            return vehicles;
        }

        // GET api/<VehiclesController>/5
        [HttpGet("{id}")]
        public async Task<Vehicle> Get(int id)
        {
            var vehicle = await _vehicleService.GetVehicleById(id);
            return vehicle;
        }

        // POST api/<VehiclesController>
        [HttpPost]
        public async Task Post([FromBody] CreateVehicleDto vehicleDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(vehicleDto);
            await _vehicleService.AddVehicle(vehicle);
        }

        // PUT api/<VehiclesController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UpdateVehicleDto vehicleDto)
        {
            Vehicle vehicle = _mapper.Map<Vehicle>(vehicleDto);
            await _vehicleService.UpdateVehicle(id, vehicle);
        }

        // DELETE api/<VehiclesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _vehicleService.DeleteVehicle(id);
        }
    }
}
