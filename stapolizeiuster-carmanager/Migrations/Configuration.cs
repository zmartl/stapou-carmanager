namespace stapolizeiuster_carmanager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using stapolizeiuster_carmanager.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<stapolizeiuster_carmanager.Models.stapolizeiuster_carmanagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(stapolizeiuster_carmanager.Models.stapolizeiuster_carmanagerContext context)
        {
            context.Cars.AddOrUpdate(p => p.Description,
                new Car
                {
                    Description = "BMW",
                    Radio = "9801"
                });

            context.States.AddOrUpdate(p => p.Name,
                new State
                {
                    Name = "Regio"
                });
        }
    }
}
