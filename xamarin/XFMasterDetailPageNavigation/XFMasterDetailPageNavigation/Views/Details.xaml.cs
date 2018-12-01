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
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.IO;
using PCLStorage;

namespace XFMasterDetailPageNavigation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Details : ContentPage
    {

        List<Device> devices = new List<Device>();
        Stream file;
        string http = "http://192.168.1.83:3002";
        //public IDownloadFile File;
        bool isDownloading = true;



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


            CrossDownloadManager.Current.CollectionChanged += (sender, e) =>
                System.Diagnostics.Debug.WriteLine(
                    "[DownloadManager] " + e.Action +
                    " -> New items: " + (e.NewItems?.Count ?? 0) +
                    " at " + e.NewStartingIndex +
                    " || Old items: " + (e.OldItems?.Count ?? 0) +
                    " at " + e.OldStartingIndex
                );

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

        public async Task getFile()
        {
            var apiResponse = RestService.For<TestApi>(http);
            Stream instream = await apiResponse.GetExcel();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "myfile.xlsx");
            byte[] bstream;



            //IFolder folder = FileSystem.Current.LocalStorage;

            //// create a file, overwriting any existing file  
            //IFile file = await folder.CreateFileAsync("SampleSheet.xlsx", CreationCollisionOption.ReplaceExisting);

            //// populate the file with image data  
            using (MemoryStream ms = new MemoryStream())
            {
                instream.CopyTo(ms);
                bstream = ms.ToArray();
            }

            File.WriteAllBytes(filename, bstream);
            int i = 0;

            //using (System.IO.Stream stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            //{
            //    stream.Write(bstream, 0, bstream.Length);
            //}



            /*using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.Write(bstream);
            }*/
            //int i = 0;

            // string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            // var filePath = Path.Combine(documentsPath, "abc.txt");

            //File.WriteAllText(filePath, "aaa");


            //DownloadFile("aaa");

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {


        // var Url = "http://www.orimi.com/pdf-test.pdf";
        //var Url = "http://www.pdf995.com/samples/pdf.pdf";
        var Url = "http://www.pdf995.com/samples/pdf.pdf";
       // http://localhost:51905/api/temperature/export?filename=export.xlsx
           // var Url = "http://192.168.1.83:3002/api/temperature/export?filename=export.xlsx";
            //await getFile();
            DownloadFile(Url);



        }

        public async void DownloadFile(String filename)
        {
            await Task.Yield();
            //await Navigation.PushPopupAsync
            await Task.Run(() =>
            {
                var downloadManager = CrossDownloadManager.Current;
                var file = downloadManager.CreateDownloadFile(filename);
                downloadManager.Start(file, true);

                while (isDownloading)
                {
                    isDownloading = isDownloaded(file);
                }

            });
            if (!isDownloading)
            {
                await DisplayAlert("File status", "File downloaded", "OK");
            }
        }

        public bool isDownloaded(IDownloadFile File)
        {
            if (File == null) return false;

            switch (File.Status)
            {
                case DownloadFileStatus.INITIALIZED:
                case DownloadFileStatus.PAUSED:
                case DownloadFileStatus.PENDING:
                case DownloadFileStatus.RUNNING:
                    return true;

                case DownloadFileStatus.COMPLETED:
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }


}