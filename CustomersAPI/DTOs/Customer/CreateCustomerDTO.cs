using CustomersAPI.Models;

namespace CustomersAPI.DTOs.Customer
{
    public class CreateCustomerDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
