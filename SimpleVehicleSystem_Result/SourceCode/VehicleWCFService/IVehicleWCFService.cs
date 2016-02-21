using SimpleVehicleSystem.VehicleDataContract;
using System.Collections.Generic;
using System.ServiceModel;

namespace SimpleVehicleSystem.VehicleWCFService
{
    [ServiceContract]
    public interface IVehicleWCFService
    {
        [OperationContract]
        List<Vehicle> GetVehicle();

        [OperationContract]
        bool DeleteVehicle(long vid);

        [OperationContract]
        bool AddVehicle(Vehicle newVehicle);

        [OperationContract]
        bool UpdateVehicle(Vehicle updatingVehicle);

    }
}
