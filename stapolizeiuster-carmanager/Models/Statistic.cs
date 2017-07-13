using System;
using System.ComponentModel.DataAnnotations;

namespace stapolizeiuster_carmanager.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime StartDate { get;set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
    }
}