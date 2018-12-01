using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.Linq;
using System.IO;

namespace XFMasterDetailPageNavigation.Droid
{
    [Activity(Label = "XFMasterDetailPageNavigation", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Downloaded();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public void Downloaded()
        {
            CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>
               (file =>
               {
                   string filename = Android.Net.Uri.Parse(file.Url).Path.Split('/').Last();
                   return Path.Combine(ApplicationContext.GetExternalFilesDir
                       (Android.OS.Environment.DirectoryDownloads).AbsolutePath, filename);

               });
        }
    }
}

