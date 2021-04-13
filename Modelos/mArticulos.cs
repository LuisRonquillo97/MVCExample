using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Modelos
{
    public class MArticulos : IIMetodosCatalogo<MArticulos>
    {
        //parámetro donde almacenaremos el error si llega a existir alguno.
        public string Error { get; set; }
        //inicializamos la conexión a BD
        readonly MConexion Context = new MConexion();
        //método para guardar, necesita una entidad de artículo.
        public bool Guardar(EArticulo articulo)
        {
            //intenta guardar y retornar true si lo logra
            //si no, guardamos el error en el parámetro y devolvemos false.
            //el resto de métodos tienen un comportamiento similar a este.
            try
            {
                //utilizando EF, agregamoos la entidad artículo, y guardamos cambios.
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
