using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFMasterDetailPageNavigation.MenuItems;
using XFMasterDetailPageNavigation.Models;
using XFMasterDetailPageNavigation.Views;

namespace XFMasterDetailPageNavigation
{
	public partial class MainPage : MasterDetailPage
	{
        public List<MasterPageItem> menuList { get; set; }

        private SessionRole session;
        public MainPage(SessionRole session)
		{
			InitializeComponent();

            this.session = session;

            menuList = new List<MasterPageItem>();

            // Adding menu items to menuList and you can define title ,page and icon
            //menuList.Add(new MasterPageItem() { Title = "Welcome", Icon = "home.png", TargetType = typeof(Welcome) });
            if(session.role.AverageTemperatures)
            {
                menuList.Add(new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(HomePage) });
            }
            if (session.role.YearTemperatures)
            {
                menuList.Add(new MasterPageItem() { Title = "Details", Icon = "setting.png", TargetType = typeof(Details) });
            }
            if(session.role.CanExport)
            {
                menuList.Add(new MasterPageItem() { Title = "Export", Icon = "setting.png", TargetType = typeof(ExportPage) });
            }
            if(session.role.ManageUsers)
            {
                menuList.Add(new MasterPageItem() { Title = "Users", Icon = "setting.png", TargetType = typeof(UserPage) });
                menuList.Add(new MasterPageItem() { Title = "Roles", Icon = "setting.png", TargetType = typeof(RolePage) });
            }
            
            //menuList.Add(new MasterPageItem() { Title = "Help", Icon = "help.png", TargetType = typeof(HelpPage) });
            //menuList.Add(new MasterPageItem() { Title = "Help", Icon = "help.png", TargetType = typeof(HelpPage) });
            menuList.Add(new MasterPageItem() { Title = "LogOut", Icon = "logout.png", TargetType = typeof(LogoutPage) });

            // Setting our list to be ItemSource for ListView in MainPage.xaml
            navigationDrawerList.ItemsSource = menuList;

            // Initial navigation, this can be used for our home page
            try
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Welcome)));
            }
            catch (TargetInvocationException ex)
            {
            }
        }

        // Event for Menu Item selection, here we are going to handle navigation based
        // on user selection in menu ListView
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            try
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            }
            catch (TargetInvocationException ex)
            {
            }
    IsPresented = false;
        }
    }
}
