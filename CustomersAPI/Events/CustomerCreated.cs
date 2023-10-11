using VehicleTestDrive.Events.Models;

namespace VehicleTestDrive.Events.Customer
{
    public class CustomerCreated
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int VehicleId { get; set; }
        public CustomerVehicle Vehicle { get; set; }
    }
}
