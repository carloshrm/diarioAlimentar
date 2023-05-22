using System.Reflection.Emit;
using System.Reflection.Metadata;

using diarioAlimentar.Server.Models;
using diarioAlimentar.Shared;

using Duende.IdentityServer.EntityFramework.Options;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace diarioAlimentar.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions) 
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Diario> Diarios { get; set; }
        public DbSet<Refeicao> Refeicoes { get; set; }
        public DbSet<Porcao> Porcoes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Diario>()
                .HasMany(r => r.Refeicoes)
                .WithOne()
                .HasForeignKey(r => r.diarioID)
                .IsRequired();

            builder.Entity<Refeicao>()
                .HasMany(p => p.Porcoes)
                .WithOne()
                .HasForeignKey(p => p.refeicaoID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}