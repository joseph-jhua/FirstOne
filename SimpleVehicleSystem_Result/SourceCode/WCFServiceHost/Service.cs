using System.ServiceModel;
using System.ServiceProcess;

namespace SimpleVehicleSystem.WCFServiceHost
{
    public class WCFServiceHost : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public WCFServiceHost()
        {
            ServiceName = "VehicleWCFService";
        }

        static void Main(string[] args)
        {
            ServiceBase.Run(new WCFServiceHost());
        }

        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(SimpleVehicleSystem.VehicleWCFService.VehicleWCFService));

            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = null;
        }
    }
}
