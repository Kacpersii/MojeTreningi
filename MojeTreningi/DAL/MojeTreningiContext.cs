using MojeTreningi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MojeTreningi.DAL
{
    public class MojeTreningiContext : DbContext
    {
        public MojeTreningiContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Profil> Profile { get; set; }

        public DbSet<Pomiar> Pomiary { get; set; }
        public DbSet<PartiaCiala> PartieCiala { get; set; }

        public DbSet<Cwiczenie> Cwiczenia { get; set; }

        public DbSet<PlanTreningowy> PlanyTreningowe { get; set; }
        public DbSet<Zestaw> Zestawy { get; set; }
        public DbSet<CwiczenieWPlanie> CwiczeniaWPlanie { get; set; }

        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Temat> Tematy { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Temat>().HasRequired<Profil>(t => t.Profil)
                .WithMany(p => p.Tematy).HasForeignKey(t => t.ProfilID)
                .WillCascadeOnDelete(false);
        }


    }
}