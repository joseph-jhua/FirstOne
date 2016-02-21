using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVehicleSystem.DataProvider
{
    public static class DataProviderFactory
    {
        public static IDataProvider CreateDataProvider(DataProviderType type)
        {
            switch (type)
            {
                case DataProviderType.Mock:
                    return new MockDataProvider();
                case DataProviderType.SqlServer:
                    return new SqlServerDataProvider();
                default:
                    return new MockDataProvider();
            }
        }

        public static IDataProvider CreateDataProvider(string type)
        {
            DataProviderType dpType;
            if (!Enum.TryParse<DataProviderType>(type, out dpType))
            {
                dpType = DataProviderType.Mock;
            }
            return CreateDataProvider(dpType);
        }
    }

    public enum DataProviderType
    {
        Mock,
        SqlServer
    }
}
