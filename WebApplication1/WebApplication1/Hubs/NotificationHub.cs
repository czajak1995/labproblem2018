using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebApplication1.Hubs
{
    public class NotificationHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();

        public void Hello()
        {
            Clients.All.hello();
        }

        public static void SayHello()
        {
            hubContext.Clients.All.hello();
        }
    }
}