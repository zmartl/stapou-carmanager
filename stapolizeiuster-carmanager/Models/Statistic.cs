using System;
using System.ComponentModel.DataAnnotations;

namespace stapolizeiuster_carmanager.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartDate { get;set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
    }
}