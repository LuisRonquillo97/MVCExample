using Modelos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modelos.ConfiguracionEntidades
{
    public class EcArticulo : IEntityTypeConfiguration<EArticulo>
    {
        //aquí se relacionan las propiedades del modelo a los campos de base de datos
        public void Configure(EntityTypeBuilder<EArticulo> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired().HasColumnName("IDArticulo");
            builder.Property(x => x.Descripcion).IsRequired().HasColumnName("descripción");
            builder.Property(x => x.Precio).IsRequired().HasColumnName("precioVenta");
            builder.Property(x => x.Eliminado).IsRequired().HasColumnName("eliminado");
            builder.ToTable("articulo");
        }
    }
}
