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
        public CArticulos Articulos;
        public FrmArticulos()
        {
            InitializeComponent();
            Articulos = new CArticulos();
            SetDgv();
        }
        void SetDgv(List<MArticuloController> listado=null)
        {
            dgvArticulos.DataSource = null;
            if (listado is null)
            {
                dgvArticulos.DataSource = Articulos.Listar();
            }
            else
            {
                dgvArticulos.DataSource = listado;
            }
            dgvArticulos.Columns[3].Visible=false;
            dgvArticulos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            dgvArticulos.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        }
        void ActivarBotones()
        {
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = false;
            txtID.Enabled = true;
        }
        void DesactivarBotones()
        {
            btnGuardar.Enabled = false;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = true;
            txtID.Enabled = false;
        }
        void LimpiarCampos()
        {
            txtPrecio.Text = "";
            rchDescripcion.Text = "";
            txtID.Text = "";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rchDescripcion.Text) && double.TryParse(txtPrecio.Text, out double precio))
            {
                string resultado = Articulos.Guardar(rchDescripcion.Text, precio);
                if (!resultado.Contains("Error:"))
                {
                    MessageBox.Show(resultado, "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ActivarBotones();
                    LimpiarCampos();
                    SetDgv();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al guardar, intente de nuevo.\n"+resultado, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<MArticuloController> articulosList = Articulos.Listar();
            if (int.TryParse(txtID.Text, out int id)) articulosList=articulosList.Where(x=>x.ID==id).ToList();
            if (!string.IsNullOrEmpty(rchDescripcion.Text)) articulosList=articulosList.Where(x=>x.Descripcion.ToLower().Contains(rchDescripcion.Text.ToLower())).ToList();
            if (double.TryParse(txtPrecio.Text, out double precio)) articulosList=articulosList.Where(x=>x.Precio==precio).ToList();
            SetDgv(articulosList);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActivarBotones();
            LimpiarCampos();
        }
        private void dgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvArticulos.Rows[e.RowIndex].Cells[0].Value.ToString();
            rchDescripcion.Text = dgvArticulos.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPrecio.Text = dgvArticulos.Rows[e.RowIndex].Cells[2].Value.ToString();
            DesactivarBotones();
        }
    }
}
