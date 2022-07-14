﻿using Hackathon_CV_Portal.Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Persistence.Context;
using System.Data.Entity;

namespace Hackathon_CV_Portal.Persistence.Seed
{
    public class CvPortalSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<CvPortalDbContext>();

            Seed(database);
        }

        private static void Seed(CvPortalDbContext context)
        {
            var seeded = false;

            SeedRoles(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedRoles(CvPortalDbContext context, ref bool seeded)
        {
            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole()
                {
                    Name = UserRole.User.ToString(),
                },
                new ApplicationRole()
                {
                    Name =  UserRole.Company.ToString(),
                },
            };

            foreach (var role in roles)
            {
                if (!context.Roles.Any(item => item.Name == role.Name))
                {
                    context.Roles.Add(role);
                    seeded = true;
                }
            }
        }
    }
}
