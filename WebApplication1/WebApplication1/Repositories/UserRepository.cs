﻿using Microsoft.Ajax.Utilities;
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

        public bool CanUseMessanger(string sessionId)
        {
            if (users.ContainsKey(sessionId))
            {
                int userId = users[sessionId];
                return GetRole(userId).UseMessanger;
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

        public List<FullUser> GetFullUsers()
        {
            List<User> dbUsers = GetUsers();
            List<Role> roles = GetRoles();
            List<UserRole> userRoles = GetUserRoles();
            List<FullUser> fullUsers = dbUsers.Select(user =>
            {
                FullUser fullUser = new FullUser();
                fullUser.user = user;
                int roleId = userRoles.Where(userRole => userRole.UserId == user.Id).First().RoleId;
                fullUser.role = roles.Where(role => role.Id == roleId).First();
                return fullUser;
            }).ToList();
            return fullUsers;
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

        public Role GetRole(int roleId)
        {
            return db.Roles.Where(t => t.Id == roleId).FirstOrDefault();
        }

        public FullUser GetFullUser(int userId)
        {
            List<User> dbUsers = GetUsers();
            List<Role> roles = GetRoles();
            List<UserRole> userRoles = GetUserRoles();
            FullUser fullUser = dbUsers.Where(t => t.Id == userId).ToList().Select(user =>
            {
                FullUser full = new FullUser();
                full.user = user;
                int roleId = userRoles.Where(userRole => userRole.UserId == user.Id).First().RoleId;
                full.role = roles.Where(role => role.Id == roleId).First();
                return full;
            }).First();
            return fullUser;
        }

        public int AddUser(FullUser fullUser)
        {
            User user = new User();
            user.Id = fullUser.user.Id;
            user.Forename = fullUser.user.Forename;
            user.Email = fullUser.user.Email;
            user.Password = fullUser.user.Password;
            user.Surname = fullUser.user.Surname;
            user.Username = fullUser.user.Username;
            db.Users.AddOrUpdate(user);
            db.SaveChanges();

            User userInDb = db.Users.Where(x => x.Username == fullUser.user.Username).First();
            UserRole userRole = new UserRole();
            userRole.Id = userInDb.Id;
            userRole.UserId = userInDb.Id;
            userRole.RoleId = fullUser.role.Id;

            db.UserRoles.AddOrUpdate(userRole);
            db.SaveChanges();

            return userInDb.Id;
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

        public bool RemoveRole(int roleId)
        {
            Role roleToRemove = db.Roles.Where(t => t.Id == roleId).FirstOrDefault();
            UserRole userRoleToRemove = db.UserRoles.Where(t => t.RoleId == roleId).FirstOrDefault();

            if (roleToRemove != null && db.UserRoles.Where(t => t.RoleId == roleId).Count() == 0)
            {
                db.Roles.Remove(roleToRemove);
                db.SaveChanges();
                return true;
            }
            else return false;
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