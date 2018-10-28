using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/device")]
    public class DevicesController : ApiController
    {
        private DeviceRepository deviceRepository = new DeviceRepository();

        [Route("all")]
        [HttpGet]
        public List<Device> GetDevices()
        {
            return deviceRepository.GetDevices();
        }


        [HttpPost]
        [ResponseType(typeof(Device))]
        [Route("add")]
        public async Task<IHttpActionResult> PostDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            await deviceRepository.Add(device);

            return CreatedAtRoute("DefaultApi", new { id = device.Id }, device);
        }

        
    }
}