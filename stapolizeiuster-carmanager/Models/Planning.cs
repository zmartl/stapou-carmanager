using System;

namespace stapolizeiuster_carmanager.Models
{
    public class Planning
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Car Car { get; set; }
        public State State { get; set; }
    }
}