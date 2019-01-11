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
    public partial class PostPage : ContentPage
    {
        ItemListViewModel itemListViewModel;
        public PostPage()
        {
            InitializeComponent();
            itemListViewModel = new ItemListViewModel();

            BindingContext = itemListViewModel;
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            itemListViewModel.PushMessage(message.Text);
        }
    }
}