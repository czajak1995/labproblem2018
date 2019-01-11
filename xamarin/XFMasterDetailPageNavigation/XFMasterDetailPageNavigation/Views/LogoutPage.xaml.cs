using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XFMasterDetailPageNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogoutPage : ContentPage
	{
		public LogoutPage ()
		{
			InitializeComponent ();
            App.Current.Properties["sessionId"] = "";
            App.Current.MainPage = new Login();
            //Navigation.RemovePage(this);
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }
	}
}