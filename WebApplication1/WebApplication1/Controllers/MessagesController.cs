using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Hubs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/message")]
    public class MessagesController : ApiController
    {
        private MessageRepository messageRepository = new MessageRepository();
        private UserRepository userRepository = new UserRepository();

        [Route("all")]
        [HttpGet]
        public List<UserMessage> GetMessages(string sessionId, string src, string tgt)
        {
            if (userRepository.CanUseMessanger(sessionId))
                return messageRepository.GetMessages(src, tgt);
            else return null;
        }

        [HttpPost]
        [Route("add")]
        public bool AddMessage(string sessionId, UserMessage userMessage)
        {
            if (userRepository.CanUseMessanger(sessionId))
            {
                var result = messageRepository.Add(userMessage);
                NotificationHub.SayHello();
                return result;
            }
                
            else return false;
        }

    }
}