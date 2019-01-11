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
	public partial class RoleEditPage : ContentPage
    {
        string http = UrlSettings.httpUrl;
        int idRole;
        Role currentRole;
        Role[] roles;


        public RoleEditPage(int id)
        {
            InitializeComponent();
            idRole = id;
            if (id == 10000)
            {
                Task.Run(async () => { await LoadNewRole(); }).Wait();
            }
            else
            {
                Task.Run(async () => { await LoadRolesList(id); }).Wait();
            }
 
        }

        private async Task LoadRolesList(int id)
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            Role[] roles = await apiResponse.GetAllRoles(sessionId);
            int i = 0;
            while (id != roles[i].Id)
            {
                i++;
            }         
            currentRole = roles[i];
            roleName.Text = currentRole.Name;
            manageUsers.IsToggled = currentRole.ManageUsers;
            yearTemp.IsToggled = currentRole.YearTemperatures;
            avgTemp.IsToggled = currentRole.AverageTemperatures;
            export.IsToggled = currentRole.CanExport;
        }

        private async Task LoadNewRole()
        {

        }

 

        void onSave(object sender, ItemTappedEventArgs e)
        {
            Button button = (Button) sender;
            Role role = new Role();
            role.Id = idRole;
            role.Name = roleName.Text;
            role.ManageUsers = manageUsers.IsToggled;
            role.YearTemperatures = yearTemp.IsToggled;
            role.AverageTemperatures = avgTemp.IsToggled;
            role.CanExport = export.IsToggled;

            Task.Run(async () => { await updateRole(role); }).Wait();
            //this.Navigation.RemovePage(this);
            this.Navigation.PushAsync(new RolePage());
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }


        private async Task updateRole(Role role)
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            int success = await apiResponse.AddRole(role, sessionId);

        }

        private async Task removeRole()
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            Boolean success = await apiResponse.RemoveRole(currentRole.Id, sessionId);
        }

        void onBack(object sender, ItemTappedEventArgs e)
        {
            //this.Navigation.RemovePage(this);
            this.Navigation.PushAsync(new RolePage());
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }

        void onDelete(object sender, ItemTappedEventArgs e)
        {          
            Task.Run(async () => { await removeRole(); }).Wait();
            //this.Navigation.RemovePage(this);
            this.Navigation.PushAsync(new RolePage());
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }

    }
}