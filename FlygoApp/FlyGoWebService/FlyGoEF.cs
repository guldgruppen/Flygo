namespace FlyGoWebService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FlyGoEF : DbContext
    {
        public FlyGoEF()
            : base("name=FlyGoEF")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<BrugerLogIn> BrugerLogIn { get; set; }
        public virtual DbSet<Destination> Destination { get; set; }
        public virtual DbSet<Fly> Fly { get; set; }
        public virtual DbSet<FlyRute> FlyRute { get; set; }
        public virtual DbSet<Hangar> Hangar { get; set; }
        public virtual DbSet<OpgaveArkiv> OpgaveArkiv { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>()
                .HasMany(e => e.FlyRute)
                .WithRequired(e => e.Destination)
                .HasForeignKey(e => e.DestinationFraId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Destination>()
                .HasMany(e => e.FlyRute1)
                .WithRequired(e => e.Destination1)
                .HasForeignKey(e => e.DestinationTilId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fly>()
                .HasMany(e => e.FlyRute)
                .WithRequired(e => e.Fly)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FlyRute>()
                .HasMany(e => e.OpgaveArkiv)
                .WithRequired(e => e.FlyRute)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hangar>()
                .HasMany(e => e.FlyRute)
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
