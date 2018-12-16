using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class MessageRepository
    {
        private WebApplication1Context db = new WebApplication1Context();
        private UserRepository userRepository = new UserRepository();
        public List<UserMessage> GetMessages(string source, string target)
        {
            return db.Messages.Where(m => m.SrcUsername == source && m.TgtUsername == target).ToList().Select(m =>
            {
                UserMessage userMessage = new UserMessage();
                userMessage.SrcUsername = m.SrcUsername;
                userMessage.TgtUsername = m.TgtUsername;
                userMessage.Content = m.Content;
                return userMessage;
            }).ToList();
        }


        public bool Add(UserMessage userMessage)
        {
            Message message = new Message();
            message.Date = DateTime.Now.ToLongTimeString();
            message.Content = userMessage.Content;
            message.SrcUsername = userMessage.SrcUsername;
            message.TgtUsername = userMessage.TgtUsername;
            message.SrcId = db.Users.Where(u => u.Username == userMessage.SrcUsername).First().Id;
            message.TgtId = db.Users.Where(u => u.Username == userMessage.TgtUsername).First().Id;
            db.Messages.Add(message);
            db.SaveChanges();
            return true;
        }
    }
}