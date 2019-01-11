using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace XFMasterDetailPageNavigation.Models
{
    [Serializable]
    public class FullUser
    {
        public User user { get; set; }
        public Role role { get; set; }

        public FullUser(User user, Role role)
        {
            this.role = role;
            this.user = user;
        }
    }
}
