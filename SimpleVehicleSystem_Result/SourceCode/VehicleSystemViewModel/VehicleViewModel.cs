using SimpleVehicleSystem.VehicleDataContract;
using SimpleVehicleSystem.VehicleSystemModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleVehicleSystem.VehicleSystemViewModel
{
    public class VehicleViewModel : INotifyPropertyChanged
    {
        private Dictionary<long, Vehicle> vehicles = null;
        private Vehicle selectedVehicle = null;

        private VehicleDataFetcher dataFetcher = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand deleteBtnClick = null;
        public ICommand updateBtnClick = null;
        public ICommand addBtnClick = null;
        
        public VehicleViewModel()
        {
            dataFetcher = new VehicleDataFetcher();

            selectedVehicle = new Vehicle
            {
                VId = 0,
                Brand = string.Empty,
                Model = string.Empty,
                Colour = string.Empty,
                ProduceYear = 0
            };
            deleteBtnClick = new ButtonCommand(deleteVehicle, canDelete);
            updateBtnClick = new ButtonCommand(updateVehicle, canUpdate);
            addBtnClick = new ButtonCommand(addVehicle, canAdd);

            refreshVehicleRecords();
        }

        private void refreshVehicleRecords()
        {
            var newVehicleList = dataFetcher.GetAllVehicles();
            vehicles = new Dictionary<long, Vehicle>();
            if (newVehicleList != null && newVehicleList.Length > 0)
            {
                foreach (var item in newVehicleList)
                {
                    vehicles.Add(item.VId, item);
                }
            }
        }

        public void deleteVehicle(object vehicle)
        {
            Vehicle vehicleToDelete = vehicle as Vehicle;
            if (vehicleToDelete == null || !vehicles.ContainsKey(vehicleToDelete.VId))
            {
                // do error handling. like add a error message string and bool to binding to a status bar to main frame to show error message.
                return;
            }

            if (!dataFetcher.DeleteVehicle(vehicleToDelete.VId))
            {
                // do error handling. like add a error message string and bool to binding to a status bar to main frame to show error message.
                return;
            }

            // to keep delete and refresh as two operation so the error cases can be handled seperatly.
            refreshVehicleRecords();

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Vehicles"));
            }
        }

        public void updateVehicle(object vehicle)
        {
            Vehicle vehicleToUpdate = vehicle as Vehicle;
            if (vehicleToUpdate == null || !vehicles.ContainsKey(vehicleToUpdate.VId))
            {
                // do error handling. like add a error message string and bool to binding to a status bar to main frame to show error message.
                return;
            }

            vehicleToUpdate.Brand = selectedVehicle.Brand;
            vehicleToUpdate.Colour = selectedVehicle.Colour;
            vehicleToUpdate.Model = selectedVehicle.Model;
            vehicleToUpdate.ProduceYear = selectedVehicle.ProduceYear;

            if (!dataFetcher.UpdateVehicle(vehicleToUpdate))
            {
                // do error handling. like add a error message string and bool to binding to a status bar to main frame to show error message.
                return;
            }

            // to keep delete and refresh as two operation so the error cases can be handled seperatly.
            refreshVehicleRecords();

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Vehicles"));
            }
        }

        public void addVehicle(object vehicle)
        {
            Vehicle newVehicle = new Vehicle
            {
                Brand = selectedVehicle.Brand,
                Model = selectedVehicle.Model,
                Colour = selectedVehicle.Colour,
                ProduceYear = selectedVehicle.ProduceYear
            };

            if (!dataFetcher.AddNewVehicle(newVehicle))
            {
                // do error handling. like add a error message string and bool to binding to a status bar to main frame to show error message.
                return;
            }

            // to keep delete and refresh as two operation so the error cases can be handled seperatly.
            refreshVehicleRecords();

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Vehicles"));
            }
        }

        public bool canDelete(object vehicle)
        {
            return true;
        }
        public bool canUpdate(object vehicle)
        {
            return true;
        }
        public bool canAdd(object vehicle)
        {
            return true;
        }

        #region Properties for binding
        public Vehicle[] Vehicles
        {
            get
            {
                return vehicles.Values.ToArray();
            }
        }

        public Vehicle SelectedVehicle
        {
            set
            {
                selectedVehicle.VId = (value == null ? 0 : value.VId);
                selectedVehicle.Brand = (value == null ? string.Empty : value.Brand);
                selectedVehicle.Model = (value == null ? string.Empty : value.Model);
                selectedVehicle.Colour = (value == null ? string.Empty : value.Colour);
                selectedVehicle.ProduceYear = (value == null ? 0 : value.ProduceYear);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedVehicle"));
                }
            }
            get
            {
                return selectedVehicle;
            }
        }

        public ICommand DeleteBtnClick
        {
            get
            {
                return this.deleteBtnClick;
            }
        }
        public ICommand UpdateBtnClick
        {
            get
            {
                return this.updateBtnClick;
            }
        }
        public ICommand AddBtnClick
        {
            get
            {
                return this.addBtnClick;
            }
        }
        #endregion Properties for binding
    }
}
