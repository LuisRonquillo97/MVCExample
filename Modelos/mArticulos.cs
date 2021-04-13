using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Modelos
{
    public class MArticulos : iMetodosCatalogo<MArticulos>
    {
        public string Error { get; set; }

        readonly MConexion Context = new MConexion();
        public bool Guardar(EArticulo articulo)
        {
            try
            {
                Context.Articulos.Add(articulo);
                Context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }
        public bool Desactivar(EArticulo articulo)
        {
            try
            {
                Context.Articulos.FirstOrDefault(x => x.ID == articulo.ID).Eliminado = true;
                Context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }
        }

        public bool Modificar(EArticulo articulo)
        {
            try
            {
                var rArticulo = Context.Articulos.FirstOrDefault(x => x.ID == articulo.ID);
                rArticulo.Descripcion = articulo.Descripcion;
                rArticulo.Precio = articulo.Precio;
                Context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Error = e.Message;
                return false;
            }

        }
        public List<EArticulo> Listar()
        {
            return Context.Articulos.Where(x=>!x.Eliminado).ToList();
        }
    }
}
