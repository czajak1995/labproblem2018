using System;
using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks;
using XFMasterDetailPageNavigation.Models;
using Plugin.DownloadManager.Abstractions;
using System.Net.Http;
using System.IO;

namespace XFMasterDetailPageNavigation
{
    [Headers("Content-Type: application/json")]
    public interface TestApi
    {
        [Get("/api/device/all")]
        Task<List<Device>> GetDeviceList();

        [Get("/api/temperature/all?deviceId={id}")]
        Task<List<int>> GetAllTemperaturesPerDevice(int id);

        [Get("/api/temperature/info?deviceId={id}")]
        Task <TemperatureInfo> GetTempInfoPerDevice(int id);

        [Get("/api//temperature/export")]
        Task<Stream> GetExcel();



    }

}