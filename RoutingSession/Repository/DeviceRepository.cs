using RoutingSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingSession.Repository
{
    class DeviceRepository : IDeviceRepository
    {
        private List<DeviceData> deviceDataStorage;

        public DeviceRepository()
        {
            this.deviceDataStorage = new List<DeviceData>();
        }

        public IEnumerable<DeviceData> GetAllDevice()
        {
            return deviceDataStorage;
        }

        public bool SyncDeviceData(IEnumerable<DeviceData> deviceData)
        {
            List<DeviceData> tempDataStorage = new List<DeviceData>();

            try
            {
                foreach (var item in deviceData)
                {
                    if (deviceDataStorage.FirstOrDefault(d => d.MacAddress == item.MacAddress) == null)
                    {
                        item.status = "ONLINE";
                        deviceDataStorage.Add(item);
                    }
                    else
                    {
                        var tempItem = deviceDataStorage.FirstOrDefault(d => d.MacAddress == item.MacAddress);
                        deviceDataStorage.Remove(tempItem);

                        if (tempItem.status == "ONLINE")
                        {
                            //Check time if less than previous than Add else replance

                            tempItem.Time = item.Time;
                            deviceDataStorage.Add(tempItem);
                        }
                        else
                        {
                            tempItem.status = "ONLINE";
                            tempItem.Time = (Convert.ToInt32(tempItem.Time) + Convert.ToInt32(item.Time)).ToString();
                            deviceDataStorage.Add(tempItem);
                        }
                    }
                }

                deviceDataStorage.ForEach(d => d.status = "OFFLINE");
                foreach (var item in deviceDataStorage)
                {
                    foreach (var d in deviceData)
                    {
                        if (item.MacAddress == d.MacAddress)
                        {
                            item.status = "ONLINE";
                        }
                    }
                    tempDataStorage.Add(item);
                }

                deviceDataStorage = tempDataStorage;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
