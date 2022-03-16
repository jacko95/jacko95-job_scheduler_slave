using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Models
{
    public partial class AspNetRoleClaims
    {
        public long Id { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetRoles Roles { get; set; }
    }
}
