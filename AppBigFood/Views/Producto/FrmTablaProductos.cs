using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace AppBigFood.Views.Producto
{
    public partial class FrmTablaProductos : Form
    {
        private APIProducto varObjProductos = null;

        public FrmTablaProductos()
        {
            InitializeComponent();
            varObjProductos = new APIProducto();
            ConsultarPorNombre();
        }

        private void ConsultarPorNombre()
        {
            try
            {
                this.dgvTablaProductos.DataSource = this.varObjProductos.GetProductos();
                this.dgvTablaProductos.AutoResizeColumns();
                this.dgvTablaProductos.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public BLL.Producto PasarProductos()
        {
            try
            {
                if (this.dgvTablaProductos.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del producto que desea seleccionar");
                }

                BLL.Producto producto = new BLL.Producto();
                producto.CodigoInterno = int.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[0].Value.ToString());
                producto.Descripcion = this.dgvTablaProductos.SelectedRows[0].Cells[2].Value.ToString();
                producto.PrecioVenta = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[3].Value.ToString());
                producto.Existencia = int.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[9].Value.ToString());
                producto.Descuento = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[4].Value.ToString());
                producto.Impuesto = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[5].Value.ToString());

                return producto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvTablaProductos.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del producto que desea eliminar");
                }

                if (MessageBox.Show("Desea eliminar el producto seleccionado", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.EjecutarEliminar(int.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[0].Value.ToString()));

                    this.ConsultarPorNombre();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EjecutarEliminar(int pCodigoInterno)
        {
            try
            {
                this.varObjProductos.EliminarProducto(pCodigoInterno);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarActualizarProducto();
                this.ConsultarPorNombre();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarActualizarProducto()
        {
            try
            {
                FrmActualizarProducto frm = new FrmActualizarProducto();

                BLL.Producto temp = new BLL.Producto();

                temp.CodigoInterno = int.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[0].Value.ToString());
                temp.CodigoBarra = this.dgvTablaProductos.SelectedRows[0].Cells[1].Value.ToString();
                temp.PrecioVenta = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[3].Value.ToString());
                temp.Descuento = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[4].Value.ToString());
                temp.Impuesto = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[5].Value.ToString());
                temp.UnidadMedida = this.dgvTablaProductos.SelectedRows[0].Cells[6].Value.ToString();
                temp.PrecioCompra = (decimal)double.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[7].Value.ToString());
                temp.Usuario = int.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[8].Value.ToString());
                temp.Existencia = int.Parse(this.dgvTablaProductos.SelectedRows[0].Cells[9].Value.ToString());
                temp.Descripcion = this.dgvTablaProductos.SelectedRows[0].Cells[2].Value.ToString();

                frm.PasarDatos(temp);

                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarAgregarProducto();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarAgregarProducto()
        {
            try
            {
                FrmAgregarProducto frm = new FrmAgregarProducto();
                frm.ShowDialog();
                this.ConsultarPorNombre();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PasarProductos();
            this.Close();
        }
    }
}
