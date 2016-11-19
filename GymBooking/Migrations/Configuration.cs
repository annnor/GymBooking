namespace GymBooking.Migrations {
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GymBooking.Models.ApplicationDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }
        private void reportIfError(IdentityResult result) {
            if (!result.Succeeded) {
                throw new Exception(string.Join("\n", result.Errors));
            }
        }
        protected override void Seed(GymBooking.Models.ApplicationDbContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Add role 'Admin'
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var adminRole = "Admin";
            if (!context.Roles.Any(r => r.Name == adminRole)) {
                var role = new IdentityRole { Name = adminRole };
                reportIfError(roleManager.Create(role));
            }

            // Add users
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var adminEmail = "admin@gym.se";
            var userArray = new List<ApplicationUser> {
                new ApplicationUser { Email = "anna@ab.se", UserName = "anna@ab.se", FirstName = "Anna", LastName = "Albinsson"},
                new ApplicationUser { Email = "bo@ab.se", UserName = "bo@ab.se", FirstName = "Bo", LastName = "Berg"},
                new ApplicationUser { Email = "calle@ab.se", UserName = "calle@ab.se", FirstName = "Carl", LastName = "Carlsson"},
                new ApplicationUser { Email = "david@ab.se", UserName = "david@ab.se", FirstName = "David", LastName = "Danielsson"},
                new ApplicationUser { Email = "erik@ab.se", UserName = "erik@ab.se", FirstName = "Erik", LastName = "Edvardsson"},
                new ApplicationUser { Email = "fredrik@ab.se", UserName = "fredrik@ab.se", FirstName = "Fredrik", LastName = "Fransson"},
                new ApplicationUser { Email = adminEmail, UserName = adminEmail, FirstName = "Admin", LastName = "Admin"}
            };
             foreach (var seed in userArray) {
                if (!context.Users.Any(u => u.Email.Equals(seed.Email))) {
                    reportIfError(userManager.Create(seed, "Abc123!"));
                }
            }

            // Set Admin user
            var adminUser = userManager.FindByName(adminEmail);
            if (adminUser != null) {
                reportIfError(userManager.AddToRole(adminUser.Id, adminRole));
            } else {
                throw new Exception("Couldn't find user with email:" + adminEmail);
            }


        }
    }
}
