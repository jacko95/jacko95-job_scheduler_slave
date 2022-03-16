using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Models
{
    public partial class Jobs
    {
        public Jobs()
        {
            Logs = new HashSet<Logs>();
        }

        public long Id { get; set; }
        public string Command { get; set; }
        public string Arguments { get; set; }
        public string CronString { get; set; }
        public long? GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual ICollection<Logs> Logs { get; set; }
    }
}
