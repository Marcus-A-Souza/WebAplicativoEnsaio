using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAplicativoEnsaio.Models;

namespace WebAplicativoEnsaio.Data
{
    public class MusicsDbContext : IdentityDbContext
    {
        public DbSet<Musics> Musics { get; set; }
        public DbSet<Ensaio> Ensaios { get; set; }
        public DbSet<Musico> Musicos { get; set; }
        public MusicsDbContext(DbContextOptions<MusicsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Chama a configuração base do Identity

            // Configuração do relacionamento entre Ensaio e Music
            modelBuilder.Entity<Ensaio>()
                .HasMany(e => e.Musics)
                .WithOne(m => m.Ensaio)
                .HasForeignKey(m => m.EnsaioId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
