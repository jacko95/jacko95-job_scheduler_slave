using System;
using System.Collections.Generic;

#nullable disable

namespace Master.Models
{
    public partial class Logs
    {
        public long Id { get; set; }
        public long? JobId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Output { get; set; }
        public long? ExitCode { get; set; }
        public long? Pid { get; set; }

        public virtual Jobs Job { get; set; }
    }
}
