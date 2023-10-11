using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomersAPI.Models
{
    public class Vehicle
    {
        public Vehicle()
        {

        }

        public Vehicle(Vehicle vehicle)
        {
            this.Id = vehicle.Id;
            this.Name = vehicle.Name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
