using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
           : base(options)
        {
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
