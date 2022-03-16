using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Models
{
    public partial class Groups
    {
        public Groups()
        {
            GroupsNodes = new HashSet<GroupsNodes>();
            Jobs = new HashSet<Jobs>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GroupsNodes> GroupsNodes { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
