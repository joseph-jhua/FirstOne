using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleVehicleSystem.VehicleDataContract;
using System.Configuration;
using System.Data.SqlClient;

namespace SimpleVehicleSystem.DataProvider
{
    public class SqlServerDataProvider : IDataProvider
    {
        private string connectionString = string.Empty;

        public SqlServerDataProvider()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SqlDataProviderConnectionString"].ConnectionString;
        }

        public bool AddVehicle(Vehicle newVehicle)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("AddVehicle", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@Brand", newVehicle.Brand));
                    sqlCommand.Parameters.Add(new SqlParameter("@Model", newVehicle.Model));
                    sqlCommand.Parameters.Add(new SqlParameter("@Colour", newVehicle.Colour));
                    sqlCommand.Parameters.Add(new SqlParameter("@ProduceYear", newVehicle.ProduceYear));

                    sqlCommand.ExecuteNonQuery();
                }
            }
            
            return true;
        }

        public bool DeleteVehicle(long vid)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("DeleteVehicleByVId", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@VId", vid));

                    sqlCommand.ExecuteNonQuery();
                }
            }
            
            return true;
        }

        public List<Vehicle> GetVehicle()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("GetAllVehicle", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var sqlReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            Vehicle oneVehicle = new Vehicle();
                            oneVehicle.Brand = (string)sqlReader["Brand"];
                            oneVehicle.Model = (string)sqlReader["Model"];
                            oneVehicle.Colour = (string)sqlReader["Colour"];
                            oneVehicle.VId = sqlReader.GetInt64(0);
                            oneVehicle.ProduceYear = sqlReader.GetInt32(4);

                            vehicleList.Add(oneVehicle);
                        }
                    }
                }
            }

            return vehicleList;
        }

        public bool UpdateVehicle(Vehicle updatingVehicle)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand("UpdateVehicleById", sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@VId", updatingVehicle.VId));
                    sqlCommand.Parameters.Add(new SqlParameter("@Brand", updatingVehicle.Brand));
                    sqlCommand.Parameters.Add(new SqlParameter("@Model", updatingVehicle.Model));
                    sqlCommand.Parameters.Add(new SqlParameter("@Colour", updatingVehicle.Colour));
                    sqlCommand.Parameters.Add(new SqlParameter("@ProduceYear", updatingVehicle.ProduceYear));

                    sqlCommand.ExecuteNonQuery();
                }
            }
            
            return true;
        }
    }
}
