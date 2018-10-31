using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class TemperatureRepository
    {
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());
        private WebApplication1Context db = new WebApplication1Context();
        public Task Add(Temperature temperature)
        {
            db.Temperatures.Add(temperature);
            return db.SaveChangesAsync();
        }

        public List<int> GetTemperatures(int deviceId)
        {
            List<Temperature> temperatures = db.Temperatures.Where(temp => temp.DeviceId == deviceId).ToList();
            return temperatures.GroupBy(t => GetIso8601WeekOfYear(t.Date)).Select(t => t.Sum(q => q.Temp)/t.Count()).ToList();
        }

        public async Task<Temperature> GetTemperature(int id)
        {
            return await db.Temperatures.FindAsync(id);
        }

        public Temperature GetMinTemperature(int deviceId)
        {
            int min = db.Temperatures.Where(temp => temp.DeviceId == deviceId).Min(t => t.Temp);
            return db.Temperatures.Where(temp => temp.DeviceId == deviceId && temp.Temp == min).First();
        }

        public int GetAverageTemperature(int deviceId)
        {
            List<int> temperatures = GetTemperatures(deviceId);
            return temperatures.Sum() / temperatures.Count();
        }

        public Temperature GetMaxTemperature(int deviceId)
        {
            int max = db.Temperatures.Where(temp => temp.DeviceId == deviceId).Max(t => t.Temp);
            return db.Temperatures.Where(temp => temp.DeviceId == deviceId && temp.Temp == max).First();
        }

        public Task GenerateData(int deviceId)
        {
            List<Temperature> temperatures = new List<Temperature>();
            for (int i = 1; i < 100000; i++)
            {
                temperatures.Add(GenerateRandomTemperature(i, deviceId));
            }
            db.Temperatures.AddRange(temperatures);
            return db.SaveChangesAsync();
        }

        public List<int> GetAverageTemperatures()
        {
            return db.Devices.ToList().Select(x =>
            {
                return GetAverageTemperature(x.Id);

            }).ToList();
        }

        public List<DeviceInfo> GetDeviceInfos()
        {
            return db.Devices.ToList().Select(x =>
            {
                DeviceInfo info = new DeviceInfo();
                info.Max = GetMaxTemperature(x.Id);
                info.Min = GetMinTemperature(x.Id);
                info.Average = GetAverageTemperature(x.Id);
                info.Device = x;
                return info;

            }).ToList();
        }

        private Temperature GenerateRandomTemperature(int id, int deviceId)
        {
            Temperature temperature = new Temperature();
            temperature.Id = id;
            temperature.Date = GenerateDateTime();
            temperature.Temp = random.Value.Next(50+deviceId, 92) + GetIso8601WeekOfYear(temperature.Date)%11;
            temperature.DeviceId = deviceId;

            return temperature;
        }

        private DateTime GenerateDateTime()
        {
            int year = 2018;
            int month = random.Value.Next(1, 13);
            int day = random.Value.Next(1, 28);
            int hour = random.Value.Next(0, 24);
            int minute = random.Value.Next(0, 60);
            int second = random.Value.Next(0, 60);
            return new DateTime(year, month, day, hour, minute, second);
        }
        public int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}