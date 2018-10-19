using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestAsync
{
    public class MainPage:ContentPage
    {
        Button button;
        Button buttonPost;
        Label text;
        string description;
      
        StackLayout stackLayout;
        public MainPage()
        {
            button = new Button
            {
                Text = "GET"
            };

            buttonPost = new Button
            {
                Text = "POST"
            };

            text = new Label {};


            button.Clicked += async (sender, e) => {
                var i = await CallGet();
            };

            buttonPost.Clicked += async (sender, e) => {
                var i = await CallPost();
            };


            this.Padding = new Thickness(20);
            stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Children = {
                    buttonPost,
                    button,
                    text
                }
            };
        this.Content = stackLayout;
    
        }

   

        public async Task<int> CallGet()
        {
           
            var apiResponse = RestService.For<IBookApi>("http://192.168.1.70:3002");
            var data = await apiResponse.GetItems();
            int j = 0;
            string str = "";
            foreach (Book i in data)
            {
                str += i.Title + "\n";
            }

            text.Text = str;
            return j;
        }


        public async Task<int> CallPost()
        {
        
            var apiResponse = RestService.For<IBookApi>("http://192.168.1.70:3002");
            int i = 0;
           

            var data = new Dictionary<string, object> {
                {"Title", "Zemsta"},
                {"Year", 1990},
                {"Price", 2},
                {"Genre", "Comedy of manners"},
            };

            await apiResponse.PostItem(data);
            return i;

         }

        
    }
}
