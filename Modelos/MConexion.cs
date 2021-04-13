using Microsoft.EntityFrameworkCore;
using Modelos.ConfiguracionEntidades;
using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    public class MConexion: DbContext
    {
        public static string CadenaConexion = @"Data Source=LRONQUILLO\SQLEXPRESS;Initial Catalog=GenericSalesStore;Integrated Security=True;User ID=sa;Password=nAxx*362";

        #region Attributes
        public DbSet<EArticulo> Articulos { get; set; }
        #endregion

        #region Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CadenaConexion);
        }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EcArticulo());
            base.OnModelCreating(builder);
        }
        #endregion Methods
    }
}
