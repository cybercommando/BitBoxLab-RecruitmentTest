using RoutingSession.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingSession.Repository
{
    public interface IDeviceRepository
    {
        IEnumerable<DeviceData> GetAllDevice();
        bool SyncDeviceData(IEnumerable<DeviceData> deviceData);
    }
}
