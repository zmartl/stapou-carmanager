using System;

namespace stapolizeiuster_carmanager.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual Car Car { get; set; }
        public virtual State State { get; set; }
    }
}