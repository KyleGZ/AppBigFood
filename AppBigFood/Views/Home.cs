using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBigFood.Views.Auditoria;
using AppBigFood.Views.Cliente;
using AppBigFood.Views.Producto;
using AppBigFood.Views.Usuario;
using BLL;
using DAL;

namespace AppBigFood.Views
{
    public partial class Home : Form
    {
        private APIClientes varObjApiClientes = null;
        private APIProducto varObjApiProductos = null;
        private APIUsuario varObjUser = null;
        private APIFacturas varObjFactura = null;

        private double impuesto;
        private double descuento;
        private double total;

        public Home()
        {
            InitializeComponent();
            this.varObjUser = new APIUsuario();
            this.varObjFactura = new APIFacturas();
            this.total = 0;
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.varObjUser.Logout();
                this.MostrarLogin();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void informacionDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTablaClientes frm = new FrmTablaClientes();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void informacionDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTablaProductos frm = new FrmTablaProductos();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void agregarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAgregarUsuario frm = new FrmAgregarUsuario();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                this.MostrarLogin();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Método para mostrar login
        private void MostrarLogin()
        {
            try
            {
                Login frm = new Login();
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTablaClientes frm = new FrmTablaClientes();
                frm.ShowDialog();

                this.txtCedula.Text = frm.PasarClientes().cedula;
                this.txtFullName.Text = frm.PasarClientes().NombreCompleto;
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            FrmTablaProductos frm = new FrmTablaProductos();
            frm.ShowDialog();

            this.txtCodigoProducto.Text = frm.PasarProductos().CodiInterno.ToString();
            this.txtNombreProducto.Text = frm.PasarProductos().Descripcion;
            this.txtPrecio.Text = frm.PasarProductos().PrecioVenta.ToString();
            this.txtCantidad.Text = frm.PasarProductos().Existencia.ToString();

            this.impuesto = frm.PasarProductos().Impuesto;
            this.descuento = frm.PasarProductos().Descuento;
            frm.Dispose();
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarFacturas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarFacturas()
        {
            try
            {
                FrmFacturas frm = new FrmFacturas();
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void cuentasXCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarCuentasXCobrar();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarCuentasXCobrar()
        {
            try
            {
                FrmCuentasXCobrar frm = new FrmCuentasXCobrar();
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarBitacora();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarBitacora()
        {
            try
            {
                FrmBitacora frm = new FrmBitacora();
                frm.ShowDialog();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LlenarTabla()
        {
            try
            {
                var subtotal = double.Parse(this.txtPrecio.Text.ToString()) * int.Parse(this.txtCantidad.Text);
                total = total + ((double.Parse(this.txtPrecio.Text.ToString()) * 
                    int.Parse(this.txtCantidad.Text)) + 
                    (double.Parse(this.txtPrecio.Text.ToString()) * impuesto / 100)) - 
                    (double.Parse(this.txtPrecio.Text.ToString()) * (descuento / 100));

                this.dgvCarrito.Rows.Add(this.txtCodigoProducto.Text, 
                    this.txtNombreProducto.Text, this.txtPrecio.Text, this.txtCantidad.Text, 
                    this.impuesto, this.descuento, subtotal);
                this.dgvCarrito.AutoResizeColumns();
                this.txtTotalPagar.Text = total.ToString();

                this.txtCodigoProducto.Text = "";
                this.txtNombreProducto.Text = "";
                this.txtPrecio.Text = "";
                this.txtDisponible.Text = "";
                this.txtCantidad.Text = "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                this.LlenarTabla();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.RealizarFactura();

                MessageBox.Show("Factura realizada exitosamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.txtCedula.Text = "";
                this.txtFullName.Text = "";
                this.txtCodigoProducto.Text = "";
                this.txtNombreProducto.Text = "";
                this.txtPrecio.Text = "";
                this.txtDisponible.Text = "";
                this.txtCantidad.Text = "";
                this.cbTipoPago.SelectedIndex = -1;
                this.cbCondicion.SelectedIndex = -1;
                this.txtTotalPagar.Text = "";
                this.total = 0;
                this.impuesto = 0;
                this.descuento = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RealizarFactura()
        {
            try
            {
                Compra compra = new Compra();
                compra.ClienteId = int.Parse(txtCedula.Text);
                compra.TipoPa = cbTipoPago.SelectedItem.ToString();
                compra.Condicion = cbCondicion.SelectedItem.ToString();
                compra.Fecha = DateTime.Now;

                this.varObjFactura.CrearFactura(compra);

                // Crear una lista para almacenar las filas que se eliminarán
                List<DataGridViewRow> filasEliminar = new List<DataGridViewRow>();
                DetalleCompra detCompra = new DetalleCompra();

                // Recorrer las filas del DataGridView
                for (int i = 0; i < dgvCarrito.Rows.Count; i++)
                {
                    DataGridViewRow fila = dgvCarrito.Rows[i];

                    // Obtener los datos de la fila
                    detCompra.CodInterno = int.Parse(fila.Cells[0].Value.ToString());
                    detCompra.Cantidad = (int.Parse(fila.Cells[3].Value.ToString()));

                    // Agregar la fila a la lista de filas a eliminar
                    filasEliminar.Add(fila);
                    this.varObjFactura.CrearFacturaDetalle(detCompra);
                }

                // Eliminar las filas de la lista
                foreach (DataGridViewRow filaEliminar in filasEliminar)
                {
                    dgvCarrito.Rows.Remove(filaEliminar);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.CancelarCompra();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CancelarCompra()
        {
            try
            {
                List<DataGridViewRow> filasEliminar = new List<DataGridViewRow>();

                // Recorrer las filas del DataGridView
                for (int i = 0; i < dgvCarrito.Rows.Count; i++)
                {
                    DataGridViewRow fila = dgvCarrito.Rows[i];

                    // Agregar la fila a la lista de filas a eliminar
                    filasEliminar.Add(fila);
                }

                // Eliminar las filas de la lista
                foreach (DataGridViewRow filaEliminar in filasEliminar)
                {
                    dgvCarrito.Rows.Remove(filaEliminar);
                }

                this.txtTotalPagar.Text = "";
                this.total = 0;
                this.impuesto = 0;
                this.descuento = 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }//
}//
