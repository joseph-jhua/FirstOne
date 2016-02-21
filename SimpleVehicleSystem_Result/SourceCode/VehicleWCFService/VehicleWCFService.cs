using System;
using SimpleVehicleSystem.VehicleDataContract;
using SimpleVehicleSystem.DataProvider;
using System.Configuration;
using System.ServiceModel;
using System.Collections.Generic;

namespace SimpleVehicleSystem.VehicleWCFService
{
    public class VehicleWCFService : IVehicleWCFService
    {
        IDataProvider dataProvider = null;

        public VehicleWCFService()
        {
            dataProvider = DataProviderFactory.CreateDataProvider(ConfigurationManager.AppSettings["DataProviderType"]);
        }

        public bool AddVehicle(Vehicle newVehicle)
        {
            if (dataProvider == null)
            {
                // add exception treatment.
                //throw new FaultException("DataProvider is null");
                return false;
            }
            return dataProvider.AddVehicle(newVehicle);
        }

        public bool DeleteVehicle(long vid)
        {
            if (dataProvider == null)
            {
                // add exception treatment.
                //throw new FaultException("DataProvider is null");
                return false;
            }
            return dataProvider.DeleteVehicle(vid);
        }

        public List<Vehicle> GetVehicle()
        {
            if (dataProvider == null)
            {
                // add exception treatment.
                //throw new FaultException("DataProvider is null");
                return null;
            }
            return dataProvider.GetVehicle();
        }

        public bool UpdateVehicle(Vehicle updatingVehicle)
        {
            if (dataProvider == null)
            {
                // add exception treatment.
                //throw new FaultException("DataProvider is null");
                return false;
            }
            return dataProvider.UpdateVehicle(updatingVehicle);
        }
    }
}
