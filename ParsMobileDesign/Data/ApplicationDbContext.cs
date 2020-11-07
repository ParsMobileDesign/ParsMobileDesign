using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParsMobileDesign.Models;

namespace ParsMobileDesign.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<PortfolioImage> PortfolioImage { get; set; }
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeSpecialty> EmployeeSpecialty { get; set; }
        public DbSet<EmployeeExperience> EmployeeExperience { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
