using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        private DeviceRepository deviceRepository = new DeviceRepository();
        private UserRepository userRepository = new UserRepository();

        [Route("export")]
        [HttpGet]
        public HttpResponseMessage Export(string sessionId, string filename)
        {
            if (!userRepository.CanManageExport(sessionId))
                return null; 

            XSSFWorkbook wb = new XSSFWorkbook();
            List<Device> devices = deviceRepository.GetDevices();
            ISheet sheet;

            sheet = wb.CreateSheet("Average temp of devices");

            var deviceName = sheet.CreateRow(0);
            var avgTemperatures = sheet.CreateRow(1);

            for (int j = 0; j < devices.Count; j++)
            {
                deviceName.CreateCell(j).SetCellValue(devices[j].Name);
                avgTemperatures.CreateCell(j).SetCellValue(temperatureRepository.GetAverageTemperature(devices[j].Id));
            }


            for (int i = 0; i < devices.Count; i++)
            {
                sheet = wb.CreateSheet(devices[i].Name);
                List<int> temperatures = temperatureRepository.GetTemperatures(devices[i].Id);

                var weekNumber = sheet.CreateRow(0);
                var weekTemperature = sheet.CreateRow(1);

                for(int j = 0; j < temperatures.Count; j++)
                {
                    weekNumber.CreateCell(j).SetCellValue(j + 1);
                    weekTemperature.CreateCell(j).SetCellValue(temperatures[j]);
                }
            }

            using (var memoryStream = new MemoryStream()) 
            {
                wb.Write(memoryStream);
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue
                       ("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition =
                       new ContentDispositionHeaderValue("attachment")
                       {
                           FileName = filename
                       };

                return response;
            }
        }

        [Route("generate")]
        [HttpGet]
        public async Task<int> GenerateData(int deviceId)
        {
            await temperatureRepository.GenerateData(deviceId);
            return 1;
        }

        [Route("deviceInfos")]
        public List<DeviceInfo> GetDeviceInfo(string sessionId)
        {
            if (userRepository.CanManageAverageTemperatures(sessionId))
                return temperatureRepository.GetDeviceInfos();
            else return null;
        }

        [Route("info")]
        public TemperatureInfo GetTemperatureInfo(string sessionId, int deviceId)
        {
            if (!userRepository.CanManageAverageTemperatures(sessionId))
                return null;

            TemperatureInfo info = new TemperatureInfo();
            info.Max = temperatureRepository.GetMaxTemperature(deviceId);
            info.Min = temperatureRepository.GetMinTemperature(deviceId);
            info.Average = temperatureRepository.GetAverageTemperature(deviceId);
            return info;
        }

        [Route("avgs")]
        public List<int> GetAverageTemperatures(string sessionId)
        {
            if (userRepository.CanManageAverageTemperatures(sessionId))
                return temperatureRepository.GetAverageTemperatures();
            else return null;
        }

        [Route("all")]
        public List<int> GetTemperatures(string sessionId, int deviceId)
        {
            if (userRepository.CanManageYearTemperatures(sessionId))
                return temperatureRepository.GetTemperatures(deviceId);
            else return null;
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