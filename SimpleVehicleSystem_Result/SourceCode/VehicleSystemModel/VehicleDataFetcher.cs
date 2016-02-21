using SimpleVehicleSystem.VehicleDataContract;
using System.Collections.Generic;

namespace SimpleVehicleSystem.VehicleSystemModel
{
    public class VehicleDataFetcher
    {
        public Vehicle[] GetAllVehicles()
        {
            Vehicle[] result = null;
            using (var client = new VehicleWCFServiceClient())
            {
                result = client.GetVehicle();
            }
            return result;
        }

        public bool AddNewVehicle(Vehicle newVehicle)
        {
            using (var client = new VehicleWCFServiceClient())
            {
                return client.AddVehicle(newVehicle);
            }
        }

        public bool DeleteVehicle(long vid)
        {
            using (var client = new VehicleWCFServiceClient())
            {
                return client.DeleteVehicle(vid);
            }
        }
        
        public bool UpdateVehicle(Vehicle vehicle)
        {
            using (var client = new VehicleWCFServiceClient())
            {
                return client.UpdateVehicle(vehicle);
            }
        }
    }
}
