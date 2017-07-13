using System;
using System.ComponentModel.DataAnnotations;

namespace stapolizeiuster_carmanager.Models
{
    public class Planning
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndTime { get; set; }
        public virtual Car Car { get; set; }
        public virtual State State { get; set; }
    }
}