using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Models
{
    public partial class Nodes
    {
        public Nodes()
        {
            GroupsNodes = new HashSet<GroupsNodes>();
        }

        public long Id { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Master { get; set; }

        public virtual ICollection<GroupsNodes> GroupsNodes { get; set; }
    }
}
