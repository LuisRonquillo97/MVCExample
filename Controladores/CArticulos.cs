using System.Collections.Generic;
using Modelos;
using Modelos.Entidades;

namespace Controladores
{
    //en esta clase tenemos todos los métodos que utilizará la Vista.
    public class CArticulos
    {
        //guardar: solicita una descripción y un precio, devuelve un texto con el mensaje de si logró guardar, o si tuvo un error al hacerlo.
        public string Guardar(string descripcion, double precio)
        {
            //inicializamos los objetos con los que vamos a trabajar, y asignamos sus datos.
            EArticulo articulo = new EArticulo { Descripcion = descripcion, Precio = precio };
            MArticulos mArticulo = new MArticulos();
            //el método Guardar del modelo, devuelve un true, o un false, así que podemos comparar directamente en el if.
            //todos los demás metodos de esta clase funcionan de una manera similar a este.
            if (mArticulo.Guardar(articulo))
            {
                return "Artículo guardado correctamente";
            }
            //si por alguna razón devuelve false, devolvemos el mensaje de error
            else
            {
                return "Error:" + mArticulo.Error;
            }
            
        }
        public string Desactivar(int id)
        {
            EArticulo articulo = new EArticulo { ID=id };
            MArticulos mArticulo = new MArticulos();
            if (mArticulo.Desactivar(articulo))
            {
                return "Artículo desactivado.";
            }
            else
            {
                return "Error:" + mArticulo.Error;
            }
        }
        public string Modificar(int id, string descripcion, double precio)
        {
            MArticulos mArticulo = new MArticulos();
            EArticulo articulo = new EArticulo { ID = id, Descripcion=descripcion, Precio=precio };
            if (mArticulo.Modificar(articulo))
            {
                return "Artículo modificado correctamente";
            }
            else
            {
                return "Error:" + mArticulo.Error;
            }
        }
        /*el método listar es el que nos devuelve el listado de artículos. como no podemos directamente conectar
         * vista con modelo, utilizamos la clase MArticuloController para que pase los datos a la capa de vista.
         * utilizamos una extensión llamada Automapper que permite pasar parámetros de un objeto A(en este ejemplo, sería la clase Earticulo de Modelos.Entidades)
         * a un objeto B(la clase MArticuloController de Controladores)
        */
        public List<MArticuloController> Listar()
        {
            //creamos un nuevo MArticuloController y llamamos a su método MapList, para recibir una lista con los artículos.
            return new MArticuloController().MapList( new MArticulos().Listar());
        }
    }
}
