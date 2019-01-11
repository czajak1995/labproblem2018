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
        [Get("/api/device/all?sessionId={sessionId}")]
        Task<List<Device>> GetDeviceList(string sessionId);

        [Get("/api/temperature/all?deviceId={id}&sessionId={sessionId}")]
        Task<List<int>> GetAllTemperaturesPerDevice(int id, string sessionId);

        [Get("/api/temperature/info?deviceId={id}&sessionId={sessionId}")]
        Task <TemperatureInfo> GetTempInfoPerDevice(int id, string sessionId);

        //TODO
        [Get("/api/temperature/export?sessionId={sessionId}&filename=SampleExcel.xlsx")]
        Task<Stream> GetExcel(string sessionId);

        [Post("/api/users/login?login={login}&password={password}")]
        Task<SessionRole> Login(string login, string password);

        [Post("/api/users/logout?sessionId=session")]
        Task<bool> Logout();

        [Get("/api/users/allUsers?sessionId={sessionId}")]
        Task<FullUser[]> GetAllUsers(string sessionId);

        [Post("/api/users/addUser?sessionId={sessionId}")]
        Task<int> AddUser([Body] FullUser user, string sessionId);

        [Delete("/api/users/removeUser?sessionId={sessionId}&userId={userId}")]
        Task<int> RemoveUser(int userId, string sessionId);

        [Get("/api/users/allRoles?sessionId={sessionId}")]
        Task<Role[]> GetAllRoles(string sessionId); 

        [Post("/api/users/addRole?sessionId={sessionId}")]
        Task<int> AddRole([Body] Role role, string sessionId);

        [Delete("/api/users/removeRole?sessionId={sessionId}&roleId={roleId}")]
        Task<Boolean> RemoveRole(int roleId, string sessionId);



    }

}