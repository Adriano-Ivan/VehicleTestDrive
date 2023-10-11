using System.Security.Principal;

namespace VehiclesAPI.Models
{
    public class Vehicle
    {
        public Vehicle()
        {

        }

        public Vehicle(Vehicle vehicle)
        {
            this.DefineProps(vehicle);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Displacement { get; set; }
        public string MaxSpeed { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public void DefineProps(Vehicle vehicle)
        {
            this.Name = vehicle.Name;
            this.Price = vehicle.Price;
            this.ImageUrl = vehicle.ImageUrl;
            this.Displacement = vehicle.Displacement;
            this.MaxSpeed = vehicle.MaxSpeed;
            this.Length = vehicle.Length;
            this.Width = vehicle.Width;
            this.Height = vehicle.Height;
        }
    }
}
