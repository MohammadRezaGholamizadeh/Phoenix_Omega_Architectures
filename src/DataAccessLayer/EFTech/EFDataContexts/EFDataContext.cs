using DomainLayer.Entities.Color;
using IdentityLayer.AspDotNetIdentity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccessLayer.EFTech.EFDataContexts
{
    public partial class EFDataContext : IdentityDbContext<ApplicationUser,
                                         ApplicationRole,
                                         string,
                                         ApplicationUserClaim,
                                         ApplicationUserRole,
                                         ApplicationUserLogin,
                                         ApplicationRoleClaim,
                                         ApplicationUserToken>
    {
        public DbSet<Color> Colors { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public EFDataContext(
            DbContextOptions<EFDataContext> options)
            : base(options)
        {
        }

        public EFDataContext(
            string connectionString) : this(
                new DbContextOptionsBuilder<EFDataContext>()
                .UseSqlServer(connectionString).Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override ChangeTracker ChangeTracker
        {
            get
            {
                var tracker = base.ChangeTracker;
                tracker.LazyLoadingEnabled = false;
                tracker.AutoDetectChangesEnabled = true;
                tracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
                return tracker;
            }
        }
    }
}
