using Microcharts;
using Refit;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMasterDetailPageNavigation.Models;
using Entry = Microcharts.Entry;

namespace XFMasterDetailPageNavigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {

        string http = "http://192.168.1.83:3002";
        List<Device> devices = new List<Device>();

        public HelpPage()
        {
            InitializeComponent();
            Task.Run(async () => { await initDeviceList(); }).Wait();
            var chart = new BarChart() { Entries = initAvgTempAllEntries().Result };
            chart.MinValue = 70;
            this.chartView.Chart = chart;
        }


        public async Task<List<Entry>> initAvgTempAllEntries()
        {
            List<Entry> entries = new List<Entry>();
            TemperatureInfo avgTemp = new TemperatureInfo();
            var apiResponse = RestService.For<TestApi>(http);
           
            for (int j = 0; j < devices.Count; j++)
            {
                Task.Run(async () => { avgTemp = await apiResponse.GetTempInfoPerDevice(devices[j].Id); }).Wait();
                entries.Add(new Entry(avgTemp.Average) { Color = getPointColor(), Label = devices[j].Name, ValueLabel = avgTemp.Average.ToString() });
            }
            return entries;
        }

        public async Task initDeviceList()
        {
            var apiResponse = RestService.For<TestApi>(http);
            devices = await apiResponse.GetDeviceList();
        }


        public Entry[] initializeMockEntries()
        {    
            var entries = new[]
                {
                new Entry(200)
                {
                    Label = "Device1",
                    ValueLabel = "100",
                    Color =  getPointColor()
                },
                new Entry(400)
                {
                    Label = "Device2",
                    ValueLabel = "140",
                    Color =  getPointColor()

                },
                new Entry(300)
                {
                    Label = "Device3",
                    ValueLabel = "160",
                    Color =  getPointColor()
                    
                }
            };
            return entries;
        }

        SKColor getPointColor()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            string[] colors = { "#266489", "#68B9C0", "#90D585", "#0066FF", "#3498db", "#b455b6", "#a100ff", "#c300ff" };
            SKColor color = SKColor.Parse(colors[rnd.Next(0, colors.Length)]);
            return color;
        }
    }

   

}