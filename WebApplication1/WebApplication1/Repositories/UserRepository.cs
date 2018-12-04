using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using WebApplication1.Models;
using System.Data.Entity.Migrations;

namespace WebApplication1.Repositories
{
    public class UserRepository
    {
        private WebApplication1Context db = new WebApplication1Context();
        private static Dictionary<string, int> users = new Dictionary<string, int>();  //put sessionId and userId

        public bool CanManageUsers(string sessionId)
        {
            if (users.ContainsKey(sessionId))
            {
                int userId = users[sessionId];
                return GetRole(userId).ManageUsers;
            }
            else return false;
        }

        public bool CanManageYearTemperatures(string sessionId)
        {
            if (users.ContainsKey(sessionId))
            {
                int userId = users[sessionId];
                return GetRole(userId).YearTemperatures;
            }
            else return false;
        }

        public bool CanManageAverageTemperatures(string sessionId)
        {
            if (users.ContainsKey(sessionId))
            {
                int userId = users[sessionId];
                return GetRole(userId).AverageTemperatures;
            }
            else return false;
        }
        public bool CanManageExport(string sessionId)
        {
            if (users.ContainsKey(sessionId))
            {
                int userId = users[sessionId];
                return GetRole(userId).CanExport;
            }
            else return false;
        }

        public List<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public List<Role> GetRoles()
        {
            return db.Roles.ToList();
        }

        public List<UserRole> GetUserRoles()
        {
            return db.UserRoles.ToList();
        }

        public Role GetRole(int userId)
        {
            int? roleId = db.UserRoles.Where(t => t.UserId == userId).FirstOrDefault().RoleId;

            if(roleId != null)
            {
                return db.Roles.Where(t => t.Id == roleId).FirstOrDefault();
            }
            return null;
        }

        public int AddUser(FullUser fullUser)
        {
            UserRole userRole = new UserRole();
            userRole.UserId = fullUser.user.Id;
            userRole.RoleId = fullUser.role.Id;

            db.Users.AddOrUpdate(fullUser.user);
            db.UserRoles.AddOrUpdate(userRole);
            db.SaveChanges();

            return fullUser.user.Id;
        }

        public int AddRole(Role role)
        {
            db.Roles.AddOrUpdate(role);
            db.SaveChanges();

            return role.Id;
        }

        public int RemoveUser(int userId)
        {
            User userToRemove = db.Users.Where(t => t.Id == userId).FirstOrDefault();
            UserRole userRoleToRemove = db.UserRoles.Where(t => t.UserId == userId).FirstOrDefault();

            if (userToRemove != null)
            {
                db.Users.Remove(userToRemove);
            }
            if (userRoleToRemove != null)
            {
                db.UserRoles.Remove(userRoleToRemove);
            }
            db.SaveChanges();
            return userId;
        }

        public int RemoveRole(int roleId)
        {
            Role roleToRemove = db.Roles.Where(t => t.Id == roleId).FirstOrDefault();
            UserRole userRoleToRemove = db.UserRoles.Where(t => t.RoleId == roleId).FirstOrDefault();

            if (roleToRemove != null)
            {
                db.Roles.Remove(roleToRemove);
            }
            if (userRoleToRemove != null)
            {
                db.UserRoles.Remove(userRoleToRemove);
            }
            db.SaveChanges();
            return roleId;
        }

        public SessionRole LoginUser(string login, string password, string userAgent)
        {
            var loggingUser = db.Users.Where(user => user.Username == login && user.Password == password).FirstOrDefault();

            if (loggingUser != null)
            {
                SessionRole sessionRole = new SessionRole();
                sessionRole.sessionId = addUserLogin(loggingUser.Id, userAgent);
                sessionRole.role = GetRole(loggingUser.Id);
                return sessionRole;
            }
            else
            {
                return null;
            }

        }

        public bool UnlogUser(string sessionId)
        {
            return users.Remove(sessionId);
        }

        private string addUserLogin(int userId, string userAgent)
        {
            string sessionId = Guid.NewGuid().ToString().Replace("-", string.Empty).Replace("+", string.Empty);
            users.Add(sessionId, userId);
            UserLogin userLogin = new UserLogin();
            userLogin.UserAgent = userAgent;
            userLogin.UserId = userId;
            userLogin.Date = DateTime.Now.ToLongTimeString();
            db.UserLogins.Add(userLogin);
            db.SaveChanges();
            return sessionId;
        }
    }
}