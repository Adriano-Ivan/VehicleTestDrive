using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Data;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Services
{
    public class VehicleService : IVehicle
    {
        private ApiDbContext dbContext;

        public VehicleService(ApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    
        public async Task AddVehicle(Vehicle vehicle)
        {
            await dbContext.Vehicles.AddAsync(vehicle);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteVehicle(int id)
        {
            Vehicle vehicle = await dbContext.Vehicles.FindAsync(id);
            dbContext.Vehicles.Remove(vehicle); 
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var vehicles = await dbContext.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            var vehicle = await dbContext.Vehicles.FindAsync(id);
            return vehicle;
        }

        public async Task UpdateVehicle(int id, Vehicle vehicle)
        {
            var vehicleObj = await dbContext.Vehicles.FindAsync(id);

            if(vehicleObj == null)
            {
                throw new Exception("Item does not exist");
            }

            vehicleObj.DefineProps(vehicle);

            await dbContext.SaveChangesAsync();
        }
    }
}
