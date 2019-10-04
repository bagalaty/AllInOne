using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

 
   
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
           : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connection = new AppConfiguration().ConnectionString;
            //optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        //public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmark>().HasData(new Bookmark()
            {
                CreatedOn = DateTime.UtcNow,
                Description = "OG description of the URL",
                Title = "OG Title of the URL",
                ImageURL = "https://example.com/sample.png"
            });

            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Post>().ToTable("Post");
            //  modelBuilder.Entity<Student>().ToTable("Student");
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseModel
            && (x.State == EntityState.Added 
            || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseModel)entry.Entity).Created = DateTime.UtcNow;
                }
            ((BaseModel)entry.Entity).Modified = DateTime.UtcNow;
            }
        }

    }
 
