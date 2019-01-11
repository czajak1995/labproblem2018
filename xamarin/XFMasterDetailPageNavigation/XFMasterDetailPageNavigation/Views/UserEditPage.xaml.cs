using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFMasterDetailPageNavigation.Models;
using XFMasterDetailPageNavigation.Views;

namespace XFMasterDetailPageNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserEditPage : ContentPage
	{
        string http = UrlSettings.httpUrl;
        int idUser;
        Role currentRole;
        Role[] roles;
        User user;


        public UserEditPage (int id)
		{
			InitializeComponent();
            idUser = id;
            Task.Run(async () => { await initializePickerRoles(); }).Wait();
            if (id == 10000)
            {
                Task.Run(async () => { await LoadNewUser(); }).Wait();
            }
            else
            {
                Task.Run(async () => { await LoadUsersList(id); }).Wait();
            }
                
           
            //idLabel.Text = id.ToString();    
        }

        

        private void MainPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;
            var name = MainPicker.Items[MainPicker.SelectedIndex];
            while (!name.Equals(roles[i].Name))
            {
                i++;
            }
            currentRole = roles[i];

        }


        private async Task LoadUsersList(int id)
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            FullUser[] users = await apiResponse.GetAllUsers(sessionId);
            int i = 0;
            while (id != users[i].user.Id){
                i++;
            }
            user = users[i].user;
            username.Text = user.Username;
            surname.Text = user.Surname;
            forename.Text = user.Forename;
            password.Text = user.Password;
            email.Text = user.Email;
            currentRole = users[i].role;
            MainPicker.Title = currentRole.Name;

           
        }

        private async Task LoadNewUser()
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            FullUser[] users = await apiResponse.GetAllUsers(sessionId);
            
            
            //username.Text = "Username";
            //surname.Text = "Surname";
            //forename.Text = "Forename";
            //password.Text = "Password";
            //email.Text = "Email";
            currentRole = roles[0];
            MainPicker.Title = currentRole.Name;


        }

        public async Task initializePickerRoles()
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            roles = await apiResponse.GetAllRoles(sessionId);
            foreach (var role in roles)
            {
                MainPicker.Items.Add(role.Name);
            }
            int j;

        }

        void onSave(object sender, ItemTappedEventArgs e)
        {
            Button button = (Button) sender;

            User user = new User();
            user.Id = idUser;
            user.Forename = forename.Text;
            user.Surname = surname.Text;
            user.Username = username.Text;
            user.Password = password.Text;
            user.Email = email.Text;

            Role role = new Role();
            role = currentRole;
     
            FullUser fullUser = new FullUser(user,role);


            Task.Run(async () => { await updateUser(fullUser); }).Wait();
            this.Navigation.PushAsync(new UserPage());
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }


        private async Task updateUser(FullUser fullUser)
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
            int j = await apiResponse.AddUser(fullUser, sessionId);
            
        }

        void onBack(object sender, ItemTappedEventArgs e)
        {
            //this.Navigation.RemovePage(this);
            this.Navigation.PushAsync(new UserPage());
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }

        void onDelete(object sender, ItemTappedEventArgs e)
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];;
            Task.Run(async () => { await removeUser(); }).Wait();
            this.Navigation.PushAsync(new UserPage());
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
        }

        private async Task removeUser()
        {
            var apiResponse = RestService.For<TestApi>(http);
            string sessionId = (string)App.Current.Properties["sessionId"];
           int status = await apiResponse.RemoveUser(user.Id, sessionId);

        }

    }
}