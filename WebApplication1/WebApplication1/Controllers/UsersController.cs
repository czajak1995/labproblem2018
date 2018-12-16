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
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private WebApplication1Context db = new WebApplication1Context();
        private UserRepository userRepository = new UserRepository();

        [Route("allUsers")]
        [HttpGet]
        public List<FullUser> GetUsers(string sessionId)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.GetFullUsers();
            else return null;
        }

        [Route("user")]
        [HttpGet]
        public FullUser GetUser(string sessionId, int userId)
        {
            if (userRepository.CanManageUsers(sessionId))
                return userRepository.GetFullUser(userId);
            else return null;
        }

        [Route("role")]
        [HttpGet]
        public Role GetRole(string sessionId, int roleId)
        {
            if (userRepository.CanManageUsers(sessionId))
                return userRepository.GetRole(roleId);
            else return null;
        }

        [Route("allUserRoles")]
        [HttpGet]
        public List<UserRole> GetUserRoles(string sessionId)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.GetUserRoles();
            else return null;
        }

        [Route("allRoles")]
        [HttpGet]
        public List<Role> GetRoles(string sessionId)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.GetRoles();
            else return null;
        }

        [HttpPost]
        [Route("login")]
        public SessionRole LogUser(string login, string password)
        {
            return userRepository.LoginUser(login, password, Request.Headers.UserAgent.ToString());
        }

        [HttpPost]
        [Route("logout")]
        public bool UnlogUser(string sessionId)
        {
            return userRepository.UnlogUser(sessionId);
        }

        [HttpPost]
        [Route("addUser")]
        public int? AddUser(string sessionId, FullUser user)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.AddUser(user);
            else return null;
        }

        [HttpDelete]
        [Route("removeUser")]
        public int? RemoveUser(string sessionId, int userId)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.RemoveUser(userId);
            else return null;
        }

        [HttpPost]
        [Route("addRole")]
        public int? AddRole(string sessionId, Role role)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.AddRole(role);
            else return null;
        }

        [HttpDelete]
        [Route("removeRole")]
        public bool RemoveRole(string sessionId, int roleId)
        {
            if (userRepository.CanManageUsers (sessionId))
                return userRepository.RemoveRole(roleId);
            else return false;
        }
    }
}