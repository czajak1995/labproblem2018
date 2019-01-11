using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMasterDetailPageNavigation.Models;

namespace XFMasterDetailPageNavigation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserPage : ContentPage
	{
        string http = UrlSettings.httpUrl;

        public UserPage ()
		{
			InitializeComponent ();
            Task.Run(async () => { await LoadUsersList(); }).Wait();

		}

        private async Task LoadUsersList()
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            FullUser[] users = await apiResponse.GetAllUsers(sessionId);
            UsersView.ItemsSource = users;

        }

        public class UserViewCell : ViewCell
        {
            public UserViewCell()
            {

            }
        }

    

        void Edit(object sender, ItemTappedEventArgs e)
        {
            Button button = (Button) sender;
            StackLayout listViewItem = (StackLayout)button.Parent;         
            Label idLabel= (Label)listViewItem.Children[0];

            int id = int.Parse(idLabel.Text);        
            this.Navigation.PushAsync(new UserEditPage(id));
            
        }

        private void AddUser(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new UserEditPage(10000));
        }
    }
}

//void OnEditTap(object sender, EventArgs e)
//{

//    //this.Navigation.PushAsync(new SettingPage());
//}

//void OnDeleteTap(object sender, ItemTappedEventArgs e)
//{
//    //this.Navigation.PushAsync(new SettingPage());
//    ToolbarItem button = (ToolbarItem) sender;      
//    ContentPage listViewItem = (ContentPage)button.Parent;

//}



//do usuniecie:
//Label name = (Label)listViewItem.Children[1];
//Label forename = (Label)listViewItem.Children[2];
//Label surname = (Label)listViewItem.Children[3];