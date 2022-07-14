﻿using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hackathon_CV_Portal.Persistence.Context
{
    public class CvPortalDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public CvPortalDbContext(DbContextOptions<CvPortalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvPortalDbContext).Assembly);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(item => item.Role).WithMany(item => item.UserRoles).HasForeignKey(item => item.RoleId);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(item => item.User).WithMany(item => item.UserRoles).HasForeignKey(item => item.UserId);

        }
    }
}
