using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class DeviceRepository
    {
        private WebApplication1Context db = new WebApplication1Context();
        public List<Device> GetDevices()
        {
            return db.Devices.ToList();
        }

        public Task Add(Device device)
        {
            db.Devices.Add(device);
            return db.SaveChangesAsync();
        }
    }
}