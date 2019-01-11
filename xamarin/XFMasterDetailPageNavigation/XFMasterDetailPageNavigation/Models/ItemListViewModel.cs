using System.Collections.ObjectModel;
using XFMasterDetailPageNavigation.Models;

namespace XFMasterDetailPageNavigation.Models
{
    public class ItemListViewModel : BaseViewModel
    {
        public ObservableCollection<Post> Posts { get; set; }

        public ItemListViewModel()
        {
            this.Posts = new ObservableCollection<Post>
            {
                //Just for tesing
                new Post
                {
                    Nick = "dorota",
                    Message = "Hello Konrad"
                },
                new Post
                {
                    Nick = "konrad",
                    Message = "Hello Dorota"
                }
            };
        }

        public void PushMessage(string message)
        {
            this.Posts.Add(
                new Post
                {
                    Nick = "dorota",
                    Message = message
                }
            );
        }
    }
}