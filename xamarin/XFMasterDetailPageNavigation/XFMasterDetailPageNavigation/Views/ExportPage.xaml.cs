using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFMasterDetailPageNavigation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExportPage : ContentPage
	{

        public IDownloadFile File;
        bool isDownloading = true;
        public ExportPage ()
		{
            InitializeComponent();
            CrossDownloadManager.Current.CollectionChanged += (sender, e) =>
                System.Diagnostics.Debug.WriteLine(
                    "[DownloadManager]" + e.Action +
                    " -> New Items: " + (e.NewItems?.Count ?? 0) +
                    "at " + e.NewStartingIndex +
                    " || Old items: " + (e.OldItems?.Count ?? 0) +
                    "at " + e.OldStartingIndex);
        }


        public async void DownloadFile(String FileName)
        {
            await Task.Yield();
            //await Navigation.PushPopupAsync(new DownLoadingPage());
            await Task.Run(() =>
            {
                var downloadManager = CrossDownloadManager.Current;
                var file = downloadManager.CreateDownloadFile(FileName);
                downloadManager.Start(file, true);

                while (isDownloading)
                {
                    isDownloading = IsDownloading(file);
                }
            });

            //await Navigation.PopAllPopupAsync();
            if (!isDownloading)
            {
                await DisplayAlert("File Status", "File downloaded", "OK");
                //DependencyService.Get<IToast>().ShowToast("Your file has been downloaded");
            }
        }

        public bool IsDownloading(IDownloadFile File)
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

        public void AbortDownloading()
        {
            CrossDownloadManager.Current.Abort(File);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var Url = "http://www.pdf995.com/samples/pdf.pdf";

            DownloadFile(Url);
        }
    }
}