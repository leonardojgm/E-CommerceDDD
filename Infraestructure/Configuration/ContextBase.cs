﻿using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Configuration
{
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        #region Contrutores

        public ContextBase (DbContextOptions <ContextBase> options) : base (options) { }

        #endregion

        #region Propriedades

        public DbSet<Produto> Produto { get; set; }

        public DbSet<CompraUsuario> CompraUsuario { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        #endregion

        #region Métodos

        #region EntityFrameworkCore

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetStringConectionConfig());

                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        #endregion

        private string GetStringConectionConfig()
        {
            return "Data Source=DESKTOP-RPGUSJ3;Initial Catalog=DDD_ECOMMERCE;Integrated Security=False;User ID=sa;Password=1234;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        }

        #endregion
    }
}