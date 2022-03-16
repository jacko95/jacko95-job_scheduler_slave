using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Models
{
    public partial class GroupsNodes
    {
        public long Id { get; set; }
        public long? NodeId { get; set; }
        public long? GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Nodes Node { get; set; }
    }
}
