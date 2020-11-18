using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Configuration
{
    public class ContextBase : DbContext
    {
        #region Propriedades
        public DbSet<Product> Products { get; set; }

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
            return "Data Source=DESKTOP-HVNTI80\\DESENVOLVIMENTO;Initial Catalog=DDD_ECOMMERCE;Integrated Security=False;User ID=sa;Password=1234;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        }

        #endregion
    }
}