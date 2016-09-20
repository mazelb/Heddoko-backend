using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;
using Z.EntityFramework.Plus;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class HDContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public HDContext()
            : base(Constants.ConnectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<AccessToken> AccessTokens { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Brainpack> Brainpacks { get; set; }

        public DbSet<Component> Components { get; set; }

        public DbSet<Databoard> Databoards { get; set; }

        public DbSet<Kit> Kits { get; set; }

        public DbSet<PantsOctopi> PantsOctopuses { get; set; }

        public DbSet<Pants> PantsPair { get; set; }

        public DbSet<Powerboard> Powerboards { get; set; }

        public DbSet<Sensor> Sensors { get; set; }

        public DbSet<SensorSet> SensorSets { get; set; }

        public DbSet<ShirtOctopi> ShirtOctopuses { get; set; }

        public DbSet<Shirt> Shirts { get; set; }

        public DbSet<Firmware> Firmware { get; set; }

        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        public void DisableChangedAndValidation()
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public void ForceUpdate(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Organization>()
            // .HasMany<User>(s => s.Users)
            // .WithOptional(c => c.Organization);

            AuditManager.DefaultConfiguration.Exclude(x => true);
            AuditManager.DefaultConfiguration.Include<IAuditable>();
            AuditManager.DefaultConfiguration.SoftDeleted<ISoftDelete>(x => x.IsDeleted);

            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            {
                foreach (AuditEntry entiry in audit.Entries)
                {
                    entiry.CreatedBy = audit.CreatedBy;
                    if (entiry.EntityTypeName.Contains("_"))
                    {
                        entiry.EntityTypeName = entiry.EntityTypeName.Substring(0, entiry.EntityTypeName.IndexOf("_"));
                    }
                }
                (context as HDContext)?.AuditEntries.AddRange(audit.Entries);
            };

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            Audit audit = new Audit();
            audit.PreSaveChanges(this);

            string currentUser = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            audit.CreatedBy = string.IsNullOrEmpty(currentUser) ? Constants.SystemUser : currentUser;

            SetUpdated();
            int rowAffecteds = base.SaveChanges();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(this, audit);
                base.SaveChanges();
            }

            return rowAffecteds;
            }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            Audit audit = new Audit();
            audit.PreSaveChanges(this);
            SetUpdated();

            string currentUser = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            audit.CreatedBy = string.IsNullOrEmpty(currentUser) ? Constants.SystemUser : currentUser;

            int rowAffecteds = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(this, audit);
                await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return rowAffecteds;
        }

        public override Task<int> SaveChangesAsync()
            {
            return SaveChangesAsync(CancellationToken.None);
            }

        private static void DebugEntityValidationErrors(DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult eve in ex.EntityValidationErrors)
            {
                Debug.Write($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                foreach (DbValidationError ve in eve.ValidationErrors)
                {
                    Debug.Write($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                }
            }
        }

        private void SetUpdated()
        {
            IEnumerable<DbEntityEntry> updatedEntities = ChangeTracker.Entries()
                                                                      .Where(i => i.State == EntityState.Modified)
                                                                      .Where(i => i.Entity is BaseModel);

            foreach (DbEntityEntry entity in updatedEntities)
            {
                ((BaseModel)entity.Entity).Updated = DateTime.UtcNow;
            }
        }


        public static HDContext Create()
        {
            return new HDContext();
        }
    }
}