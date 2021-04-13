using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Modelos
{
    interface iMetodosCatalogo<T>
    {
        public bool Guardar(EArticulo articulo);
        public bool Modificar(EArticulo aritculo);
        public bool Desactivar(EArticulo aritculo);

        public List<EArticulo> Listar();
    }
}
