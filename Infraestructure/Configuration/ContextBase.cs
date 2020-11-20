using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        #region Propriedades

        public DbSet<Produto> Produto { get; set; }

        public DbSet<CompraUsuario> CompraUsuario { get; set; }

        #endregion

        #region Contrutores

        public ContextBase (DbContextOptions <ContextBase> options) : base (options) { }

        #endregion

        #region Métodos

        #region DbContext

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
                base.OnConfiguring(optionsBuilder);
            }
        }

        #endregion

        private string GetStringConectionConfig()
        {
            return "Data Source=DESKTOP-RPGUSJ3;Initial Catalog=DDD_ECOMMERCE;Integrated Security=False;User ID=sa;Password=1;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        }

        #endregion
    }
}