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
	public partial class RolePage : ContentPage
	{
        string http = UrlSettings.httpUrl;

        public RolePage()
        {
            InitializeComponent();
            Task.Run(async () => { await LoadRolesList(); }).Wait();

        }

        private async Task LoadRolesList()
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            Role[] roles = await apiResponse.GetAllRoles(sessionId);
            RolesView.ItemsSource = roles;

        }

        public class RolesViewCell : ViewCell
        {
            public RolesViewCell()
            {

            }
        }



        void EditRole(object sender, ItemTappedEventArgs e)
        {
            Button button = (Button) sender;
            Grid listViewItem = (Grid)button.Parent;
            Label idLabel = (Label)listViewItem.Children[0];

            int id = int.Parse(idLabel.Text);
            this.Navigation.PushAsync(new RoleEditPage(id));
            //while (Navigation.NavigationStack.Count > 1) Navigation.RemovePage(Navigation.NavigationStack.ElementAt(0));

        }

        private void AddRole(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new RoleEditPage(10000));
        }
    }
}
