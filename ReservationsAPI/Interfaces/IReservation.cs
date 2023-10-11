using ReservationsAPI.Models;

namespace ReservationsAPI.Interfaces
{
    public interface IReservation
    {
        Task<List<Reservation>> GetReservations();
        Task UpdateMailStatus(int id);
        Task InsertReservation(Reservation reservation);    
    }
}
