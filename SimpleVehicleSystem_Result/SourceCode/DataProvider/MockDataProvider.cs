using SimpleVehicleSystem.VehicleDataContract;
using System;
using System.Collections.Generic;

namespace SimpleVehicleSystem.DataProvider
{
    public class MockDataProvider : IDataProvider
    {
        Dictionary<long, Vehicle> vehicleDict = null;

        public MockDataProvider()
        {
            // perpare date for Mock/test
            vehicleDict = new Dictionary<long, Vehicle>();
        }

        public bool AddVehicle(Vehicle newVehicle)
        {
            if (vehicleDict.ContainsKey(newVehicle.VId))
            {
                return false;
            }
            vehicleDict.Add(newVehicle.VId, newVehicle);
            return true;
        }

        public bool DeleteVehicle(long vid)
        {
            if (!vehicleDict.ContainsKey(vid))
            {
                return false;
            }
            vehicleDict.Remove(vid);
            return true;
        }

        public List<Vehicle> GetVehicle()
        {
            return new List<Vehicle>(vehicleDict.Values);
        }

        public bool UpdateVehicle(Vehicle updatingVehicle)
        {
            if (!vehicleDict.ContainsKey(updatingVehicle.VId))
            {
                return false;
            }
            vehicleDict[updatingVehicle.VId] = updatingVehicle;
            return true;
        }
    }
}
