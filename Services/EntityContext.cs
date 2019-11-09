using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Models.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class EntityContext : IdentityDbContext<AppUser>
{
    public EntityContext(DbContextOptions<EntityContext> options)
       : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        //optionsBuilder.UseMySQL("server=localhost;database=allinone;user=sa;password=P@ssw0rd159");

        //var connection = new AppConfiguration().ConnectionString;
        //optionsBuilder.UseSqlServer(connection);

        optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }


    public DbSet<Employee> Employees { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }

    //public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        var allEntities = modelBuilder.Model.GetEntityTypes();

        foreach (var entity in allEntities)
        {
            entity.AddProperty("Created", typeof(DateTime));
            entity.AddProperty("Modified", typeof(DateTime));
           // entity.AddProperty("Message", typeof(string));


            entity.AddProperty("CreatedDate", typeof(DateTime));
            entity.AddProperty("UpdatedDate", typeof(DateTime));
        }



        //modelBuilder.Entity<Bookmark>().HasData(new Bookmark()
        //{
        //    CreatedOn = DateTime.UtcNow,
        //    Description = "OG description of the URL",
        //    Title = "OG Title of the URL",
        //    ImageURL = "https://example.com/sample.png"
        //});

        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Bookmark>().ToTable("Bookmarks");

        //  modelBuilder.Entity<Student>().ToTable("Student");
    }


    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is ITrackable trackable)
            {
                var now = DateTime.UtcNow;
                var user = GetCurrentUser();
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.LastUpdatedAt = now;
                        trackable.LastUpdatedBy = user;
                        break;

                    case EntityState.Added:
                        trackable.CreatedAt = now;
                        trackable.CreatedBy = user;
                        trackable.LastUpdatedAt = now;
                        trackable.LastUpdatedBy = user;
                        break;
                }

                entry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }
        }


    }

    private string GetCurrentUser()
    {
        return "BaGaLaTy"; // TODO implement your own logic

        // If you are using ASP.NET Core, you should look at this answer on StackOverflow
        // https://stackoverflow.com/a/48554738/2996339
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
                //      ((BaseModel)entry.Entity).Created = DateTime.UtcNow;
            }
            // ((BaseModel)entry.Entity).Modified = DateTime.UtcNow;
        }
    }

}

