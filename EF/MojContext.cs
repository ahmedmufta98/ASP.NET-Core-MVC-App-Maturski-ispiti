using Microsoft.EntityFrameworkCore;
using RS1_Ispit_asp.net_core.EntityModels;

namespace RS1_Ispit_asp.net_core.EF
{
    public class MojContext : DbContext
    {
        public MojContext(DbContextOptions<MojContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Odjeljenje>().HasOne(x => x.SkolskaGodina)
               .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Odjeljenje>().HasOne(x => x.Skola)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PredajePredmet>().HasOne(x => x.Odjeljenje)
                .WithMany().OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<DodjeljenPredmet> DodjeljenPredmet { get; set; }
        public DbSet<Ucenik> Ucenik { get; set; }
        public DbSet<OdjeljenjeStavka> OdjeljenjeStavka { get; set; }
        public DbSet<Odjeljenje> Odjeljenje { get; set; }
   
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<PredajePredmet> PredajePredmet { get; set; }
        public DbSet<Skola> Skola { get; set; }
        public DbSet<SkolskaGodina> SkolskaGodina { get; set; }
        public DbSet<MaturskiIspit> MaturskiIspit { get; set; }
        public DbSet<MaturskiIspitStavke> MaturskiIspitStavke { get; set; }

    }
}
