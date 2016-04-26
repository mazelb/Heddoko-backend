﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HDContext : DbContext
    {
        public HDContext()
            : base(Constants.ConnectionStringName)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void DisableChangedAndValidation()
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public void ForceUpdate(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public DbSet<AccessToken> AccessTokens { get; set; }

        public DbSet<ComplexEquipment> ComplexEquipments { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<MaterialType> MaterialTypes { get; set; }

        public DbSet<Movement> Movements { get; set; }

        public DbSet<MovementEvent> MovementEvents { get; set; }

        public DbSet<MovementFrame> MovementFrames { get; set; }

        public DbSet<MovementMarker> MovementMarkers { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Screening> Screenings { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Profile>()
               .HasMany<Tag>(s => s.Tags)
               .WithMany(c => c.Profiles);

            modelBuilder.Entity<Group>()
               .HasMany<Tag>(s => s.Tags)
               .WithMany(c => c.Groups);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetUpdated();
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                DebugEntityValidationErrors(ex);
                throw;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            SetUpdated();
            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                DebugEntityValidationErrors(ex);
                throw;
            }
        }

        private void DebugEntityValidationErrors(DbEntityValidationException ex)
        {
            foreach (var eve in ex.EntityValidationErrors)
            {
                Debug.Write($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                foreach (var ve in eve.ValidationErrors)
                {
                    Debug.Write($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                }
            }
        }

        private void SetUpdated()
        {
            var updatedEntities = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Modified)
                .Where(i => i.Entity is BaseModel);

            foreach (var entity in updatedEntities)
            {
                ((BaseModel)entity.Entity).Updated = DateTime.UtcNow;
            }
        }
    }
}
