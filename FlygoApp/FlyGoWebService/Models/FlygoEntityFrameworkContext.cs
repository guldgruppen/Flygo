namespace FlyGoWebService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FlygoEntityFrameworkContext : DbContext
    {
        public FlygoEntityFrameworkContext()
            : base("name=FlygoEntityFrameworkContext2")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<BrugerLogIn> BrugerLogIn { get; set; }
        public virtual DbSet<Fly> Fly { get; set; }
        public virtual DbSet<Flyopgave> Flyopgave { get; set; }
        public virtual DbSet<Hangar> Hangar { get; set; }
        public virtual DbSet<OpgaveArkiv> OpgaveArkiv { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fly>()
                .HasMany(e => e.Flyopgave)
                .WithRequired(e => e.Fly)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hangar>()
                .HasMany(e => e.Flyopgave)
                .WithRequired(e => e.Hangar)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.BrugerLogIn)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);
        }
    }
}
