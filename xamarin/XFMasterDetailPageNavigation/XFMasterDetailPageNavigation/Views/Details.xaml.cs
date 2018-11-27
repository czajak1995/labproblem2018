using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.Entry;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using Refit;

namespace XFMasterDetailPageNavigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {

        List<Device> devices = new List<Device>();
        string http = "http://192.168.1.83:3002";



        public Details()
        {
            InitializeComponent();
            Task.Run(async () => { await initializePickerDevices(); }).Wait();

            try
            {
                var chart = new LineChart() { Entries = initializeDevicesEntries(getFirstDeviceId()).Result };
                chart.MinValue = 60;
                this.chartView.Chart = chart;
            } catch(IndexOutOfRangeException ex)
            {
                DisplayAlert("Server error", ex.Message, "OK");
            }
                   
        }

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = MainPicker.Items[MainPicker.SelectedIndex];

            //DisplayAlert(name, "Selected value", "OK");
            var chart = new LineChart() { Entries = initializeDevicesEntries(getDeviceId(name)).Result };
            chart.MinValue = 60;
            this.chartView.Chart = chart;
        }


        Entry[] initializeMockEntries(int i)
        {
            var entries = new[]
                {
                new Entry(i*10)
                {
                    Label = "",
                    ValueLabel =  (i*10) + "",
                    Color = getPointColor()
                },
                new Entry(i*20)
                {
                    Label = "",
                    ValueLabel =  (i*20) + "",
                    Color = getPointColor()

                },
                new Entry(i*30)
                {
                    Label = "",
                    ValueLabel = (i*30) + "",
                    Color = getPointColor()
                }
            };
            return entries;
        }


        public async Task<List<Entry>> initializeDevicesEntries(int i)
        {
            List<Entry> entries = new List<Entry>();
            var allTemp = new List<int>();
            var apiResponse = RestService.For<TestApi>(http);
             Task.Run(async () => { allTemp = await  apiResponse.GetAllTemperaturesPerDevice(i); }).Wait();
            //var allTemp = await apiResponse.GetAllTemperaturesPerDevice(1);
            for (int j = 0; j < allTemp.Count; j++)
            {
                if (j % 10 == 0)
                    entries.Add(new Entry(allTemp[j])
                    {
                        ValueLabel = allTemp[j].ToString(),
                        Color = getPointColor(),
                        Label = "'",
                    });
                else
                    entries.Add(new Entry(allTemp[j]) { Color = getPointColor() });
            }       
            return entries;
        }

        SKColor getPointColor()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            string[] colors = { "#266489", "#68B9C0", "#90D585", "#0066FF", "#3498db", "#b455b6", "#a100ff", "#c300ff" };
            return SKColor.Parse(colors[rnd.Next(0, colors.Length)]);
        }

        List<Device> getMockDevices()
        {
            List<Device> devices = new List<Device>();
            devices.Add(new Device() { Name = "Pralka", Id = 2 });
            devices.Add(new Device() { Name = "Prostownica", Id = 5 });

            return devices;
        }

        void initializePickerItems()
        {
            devices = getMockDevices();          
            foreach (var dev in devices)
            {
                MainPicker.Items.Add(dev.Name);  
            }        
        }

        int getDeviceId(string name)
        {
            foreach (var dev in devices)
            {
                if(dev.Name == name)
                {
                    return dev.Id;
                }
                
            }
            return 0;
        }

        int getFirstDeviceId()
        {
            Device device = devices.First();
            if (device.Equals(null)) throw new IndexOutOfRangeException("Message");
            return devices.First().Id;  
        }

        public async Task initializePickerDevices()
        {
            var apiResponse = RestService.For<TestApi>(http);
            devices = await apiResponse.GetDeviceList();
            //devices = getMockDevices();
            foreach (var dev in devices)
            {
                MainPicker.Items.Add(dev.Name);
            }
            int j;
           
        }

    }

}