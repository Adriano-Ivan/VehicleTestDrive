using Microsoft.AspNetCore.Mvc;
using ReservationsAPI.Interfaces;
using ReservationsAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReservationsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservation _reservationService;

        public ReservationsController(IReservation reservationService)
        {
            _reservationService = reservationService;
        }


        // GET: api/<ReservationsController>
        [HttpGet]
        public async  Task<IEnumerable<Reservation>> Get()
        {
           var reservations = await _reservationService.GetReservations();
           return reservations;
        }

        // PUT api/<ReservationsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id)
        {
            await _reservationService.UpdateMailStatus(id);
        }
    }
}
