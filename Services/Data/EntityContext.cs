using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services.Data
{
   
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new AppConfiguration().ConnectionString;
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Post>().ToTable("Post");
            //  modelBuilder.Entity<Student>().ToTable("Student");
        }

    }
}
