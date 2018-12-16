using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMasterDetailPageNavigation.Models;

namespace XFMasterDetailPageNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{

        string http = "http://192.168.1.83:3002";
        private SessionRole session;
        public Login ()
		{
			InitializeComponent ();
		}

        async public void OnLoginButtonClicked()
        {
            var apiResponse = Refit.RestService.For<TestApi>(http);
            String Username = usernameEntry.Text;
            String Password = passwordEntry.Text;
            session = await apiResponse.Login(Username, Password);
            if (session == null || session.Equals("")) return;
            App.Current.MainPage = new MainPage(session);
        }
    }
}