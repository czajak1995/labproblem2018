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
    [RoutePrefix("api/temperature")]
    public class TemperaturesController : ApiController
    {
        private TemperatureRepository temperatureRepository = new TemperatureRepository();

        [Route("generate")]
        [HttpGet]
        public async Task<int> GenerateData(int deviceId)
        {
            await temperatureRepository.GenerateData(deviceId);
            return 1;
        }

        [Route("deviceInfos")]
        public List<DeviceInfo> GetDeviceInfo()
        {
            return temperatureRepository.GetDeviceInfos();
        }

        [Route("info")]
        public TemperatureInfo GetTemperatureInfo(int deviceId)
        {
            TemperatureInfo info = new TemperatureInfo();
            info.Max = temperatureRepository.GetMaxTemperature(deviceId);
            info.Min = temperatureRepository.GetMinTemperature(deviceId);
            info.Average = temperatureRepository.GetAverageTemperature(deviceId);
            return info;
        }

        [Route("avgs")]
        public List<int> GetAverageTemperatures()
        {
            return temperatureRepository.GetAverageTemperatures();
        }

        [Route("all")]
        public List<int> GetTemperatures(int deviceId)
        {
            return temperatureRepository.GetTemperatures(deviceId);
        }

        [ResponseType(typeof(Temperature))]
        public async Task<IHttpActionResult> PostTemperature(Temperature temperature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await temperatureRepository.Add(temperature);
            return CreatedAtRoute("DefaultApi", new { id = temperature.Id }, temperature);
        }
    }
}