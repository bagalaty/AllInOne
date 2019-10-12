using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Services.Models;


public partial class allinoneContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// 
        /// </summary>
        public allinoneContext()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public allinoneContext(DbContextOptions<allinoneContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=P@ssw0rd159;database=allinone");
            }
        }




        public DbSet<Employee> Employees { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

        modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
        modelBuilder.Entity<IdentityUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));

        modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));

        modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));

        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Bookmark>().ToTable("Bookmarks");

        modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
        }
    }
 
