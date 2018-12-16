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
        [Get("/api/device/all?sessionId=session")]
        Task<List<Device>> GetDeviceList();

        [Get("/api/temperature/all?deviceId={id}&sessionId=session")]
        Task<List<int>> GetAllTemperaturesPerDevice(int id);

        [Get("/api/temperature/info?deviceId={id}&sessionId=session")]
        Task <TemperatureInfo> GetTempInfoPerDevice(int id);

        //TODO
        [Get("/api/temperature/export?sessionId=session")]
        Task<Stream> GetExcel();

        [Post("/api/users/login?login={login}&password={password}")]
        Task<SessionRole> Login(string login, string password);

    }

}