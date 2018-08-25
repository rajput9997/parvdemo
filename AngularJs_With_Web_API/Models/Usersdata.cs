using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJs_With_Web_API.Models
{
    public class Usersdata
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public Nullable<System.Guid> Salt { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public bool? CanEdit { get; set; }
        public bool? CanFilter { get; set; }

        public bool? CanNavigate { get; set; }
    }
}