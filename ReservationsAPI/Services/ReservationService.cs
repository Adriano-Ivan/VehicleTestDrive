using Microsoft.EntityFrameworkCore;
using ReservationsAPI.Data;
using ReservationsAPI.Interfaces;
using ReservationsAPI.Models;
using System.Net;
using System.Net.Mail;

namespace ReservationsAPI.Services
{
    public class ReservationService : IReservation
    {
        private ApiDbContext _dbContext;
        private IConfiguration _configuration;
        private readonly string _emailHost;
        private readonly string _emailServiceUsername;
        private readonly string _emailServicePassword;

        public ReservationService(ApiDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;

            _emailHost = configuration.GetValue<string>("emailHost");
            _emailServiceUsername = configuration.GetValue<string>("usernameEmailService");
            _emailServicePassword = configuration.GetValue<string>("passwordEmailService");
        }

        public async Task<List<Reservation>> GetReservations()
        {
            
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
            var reservationResult = await _dbContext.Reservations.FindAsync(id);
            if(reservationResult != null && reservationResult.IsMailSent == false)
            {
                var stmpClient = new SmtpClient(_emailHost)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(
                        _emailServiceUsername, _emailServicePassword,
                        _emailHost
                    ),
                    EnableSsl = false
                };

                stmpClient.Send("adrianoemail@email.com",reservationResult.Email,"Vehicle Test Drive", 
                    "Your test drive is reserved");
                reservationResult.IsMailSent = true;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
