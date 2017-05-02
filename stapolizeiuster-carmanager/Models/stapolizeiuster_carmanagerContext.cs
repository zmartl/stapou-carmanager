using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace stapolizeiuster_carmanager.Models
{
    public class stapolizeiuster_carmanagerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public stapolizeiuster_carmanagerContext() : base("name=stapolizeiuster_carmanagerContext")
        {
        }

        public System.Data.Entity.DbSet<stapolizeiuster_carmanager.Models.Car> Cars { get; set; }

        public System.Data.Entity.DbSet<stapolizeiuster_carmanager.Models.State> States { get; set; }

        public System.Data.Entity.DbSet<stapolizeiuster_carmanager.Models.Planning> Plannings { get; set; }
        
        public System.Data.Entity.DbSet<stapolizeiuster_carmanager.Models.Statistic> Statistics { get; set; }
    }
}
