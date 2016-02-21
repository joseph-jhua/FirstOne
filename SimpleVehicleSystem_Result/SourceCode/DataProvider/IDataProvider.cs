using SimpleVehicleSystem.VehicleDataContract;
using System.Collections.Generic;

namespace SimpleVehicleSystem.DataProvider
{
    public interface IDataProvider
    {
        List<Vehicle> GetVehicle();
        
        bool DeleteVehicle(long vid);
        
        bool AddVehicle(Vehicle newVehicle);
        
        bool UpdateVehicle(Vehicle updatingVehicle);
    }
}
