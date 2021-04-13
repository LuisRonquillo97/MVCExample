using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Controladores;
using System.Windows.Forms;

namespace Vista
{
    public partial class FrmArticulos : Form
    {
        //llamamos a la clase cArticulo de la capa controladores.
        public CArticulos Articulos;
        //en el constructor, inicializamos la clase y llenamos el datagrid.
        public FrmArticulos()
        {
            InitializeComponent();
            Articulos = new CArticulos();
            SetDgv();
        }
        //los método auxiliares nos ayudan con los procesos que involucran comportamientos en la forma, pero que no pertenecen a ella.
        #region MetodosAuxiliares
        //método que llena el datagrid con datos. También omitimos el campo "eliminado" de la lista.
        //tiene un parámetro opcional, que, en el caso de que se genere una lista (para búsqueda), utilice esa lista filtrada, en vez de usar la lista tal cual.
        void SetDgv(List<MArticuloController> listado=null)
        {
            //para refrescar el datagrid, a fueza debemos cambiar su datasource, por eso lo asigno a null.
            dgvArticulos.DataSource = null;
            //si la variable opcional tiene datos, la utilizamos, si no, hacemos la llamada del método listar.
            if (listado is null)
            {
                dgvArticulos.DataSource = Articulos.Listar();
            }
            else
            {
                dgvArticulos.DataSource = listado;
            }
            //ocultamos la columna eliminado.
            dgvArticulos.Columns[3].Visible=false;
            dgvArticulos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvArticulos.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        }
        //método que activa los botones originales, al completar una modificacion/eliminación de un registro, o se cancela una acción.
        void ActivarBotones()
        {
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = false;
            txtID.Enabled = true;
        }
        //método que activa botones de modificar/eliminar/cancelar/ cuando se selecciona un registro.
        void DesactivarBotones()
        {
            btnGuardar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = true;
            txtID.Enabled = false;
        }
        //método que limpia los campos del formulario.
        void LimpiarCampos()
        {
            txtPrecio.Text = "";
            rchDescripcion.Text = "";
            txtID.Text = "";
        }
        #endregion MetodosAuxiliares
        //métodos de formulario son los métodos que trabajan en base al comportamiento de algún elemento del form.
        #region MetodosDeFormulario
        //acción del botón guardar. las acciones modificar y eliminar funcionan de manera similar a este.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //comprobamos que no haya campos vacíos
            if (!string.IsNullOrEmpty(rchDescripcion.Text) && double.TryParse(txtPrecio.Text, out double precio))
            {
                //guardamos y obtenemos el resultado
                string resultado = Articulos.Guardar(rchDescripcion.Text, precio);
                //si el resultado no contiene un texto diciendo Error, significa que el resultado fue correcto.
                if (!resultado.Contains("Error:"))
                {
                    //mostramos el mensaje de confirmación, limpiamos campos, reasignamos los botones activos y refrescamos el datagrid.
                    MessageBox.Show(resultado, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActivarBotones();
                    LimpiarCampos();
                    SetDgv();
                }
                //si ocurre un problema, mostramos el error.
                else
                {
                    MessageBox.Show("Ocurrió un error al guardar, intente de nuevo.\n"+resultado, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //si algún campo está incompleto, mostramos un error.
            else
            {
                MessageBox.Show("Usted dejó algún campo incompleto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rchDescripcion.Text) && double.TryParse(txtPrecio.Text, out double precio) && int.TryParse(txtID.Text,out int id))
            {
                string resultado=Articulos.Modificar(id, rchDescripcion.Text, precio);
                if (!resultado.Contains("Error:"))
                {
                    MessageBox.Show(resultado, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActivarBotones();
                    LimpiarCampos();
                    SetDgv();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al modificar, intente de nuevo.\n" + resultado, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Usted dejó algún campo incompleto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtID.Text, out int id))
            {
                string resultado=Articulos.Desactivar(id);
                if (!resultado.Contains("Error:"))
                {
                    MessageBox.Show(resultado, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActivarBotones();
                    LimpiarCampos();
                    SetDgv();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al eliminar, intente de nuevo.\n" + resultado, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Usted dejó algún campo incompleto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //el método buscar funciona en base a lo que hayamos escrito en los campos de búsqueda
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //primero obtengo la lista de artículos.
            List<MArticuloController> articulosList = Articulos.Listar();
            //después, comprobamos campo por campo del formulario, si alguno tiene datos, y utilizando linq, vamos filtrando.
            if (int.TryParse(txtID.Text, out int id)) articulosList=articulosList.Where(x=>x.ID==id).ToList();
            if (!string.IsNullOrEmpty(rchDescripcion.Text)) articulosList=articulosList.Where(x=>x.Descripcion.ToLower().Contains(rchDescripcion.Text.ToLower())).ToList();
            if (double.TryParse(txtPrecio.Text, out double precio)) articulosList=articulosList.Where(x=>x.Precio==precio).ToList();
            //al final, utilizamos el método para actualizar el datagrid, con la lista filtrada.
            SetDgv(articulosList);
        }
        //el botón cancelar limpia los campos y activa los botones correspondientes.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActivarBotones();
            LimpiarCampos();
        }
        //esta acción es el doble clic a una celda del datagrid, es la manera en la que seleccionamos un registro.
        //con esto, obtenemos los datos del datagrid, los colocamos en los textbox y activamos los botones para modificar/eliminar/cancelar.
        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvArticulos.Rows[e.RowIndex].Cells[0].Value.ToString();
            rchDescripcion.Text = dgvArticulos.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPrecio.Text = dgvArticulos.Rows[e.RowIndex].Cells[2].Value.ToString();
            DesactivarBotones();
        }
        #endregion MetodosDeFormulario
    }
}
