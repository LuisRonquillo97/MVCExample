using Microsoft.EntityFrameworkCore;
using Modelos.ConfiguracionEntidades;
using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos
{
    //clase que se encarga de conectar las entitades y su configuración a la BD.
    public class MConexion: DbContext
    {
        //CADENA DE CONEXIÓN
        public static string CadenaConexion = @"Data Source=LRONQUILLO\SQLEXPRESS;Initial Catalog=GenericSalesStore;Integrated Security=True;User ID=sa;Password=xxxxxxxx";

        #region Attributes
        //atributos: aquí se colocan las entitades que utilizaremos con Entity Framework
        public DbSet<EArticulo> Articulos { get; set; }
        #endregion

        #region Methods
        //método de configuración de EF, aquí asignamos la conexión
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CadenaConexion);
        }
        
        //se asignan las configuraciones de entidades en EF
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EcArticulo());
            base.OnModelCreating(builder);
        }
        #endregion Methods
    }
}
