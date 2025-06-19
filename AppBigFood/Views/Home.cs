using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
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
        public static string token = null;
        private APIClientes apiClientes = null;
        private APIProducto apiProductos = null;
        private APIUsuario user = null;
        private APIFacturas factura = null;
        private Email email = null;

        private decimal impuesto;
        private decimal descuento;
        private decimal total;

        public Home()
        {
            InitializeComponent();
            this.user = new APIUsuario();
            this.factura = new APIFacturas();
            this.apiClientes = new APIClientes();
            this.apiProductos = new APIProducto();
            this.email = new Email();
            this.total = 0;
        }

        //private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //this.varObjUser.Logout();
        //        this.MostrarLogin();

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

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
            this.MostrarLogin();
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

                this.txtCedula.Text = frm.PasarClientes().cedulaLegal;
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

            this.txtCodigoProducto.Text = frm.PasarProductos().CodigoInterno.ToString();
            this.txtNombreProducto.Text = frm.PasarProductos().Descripcion;
            this.txtPrecio.Text = frm.PasarProductos().PrecioVenta.ToString();
            this.txtDisponible.Text = frm.PasarProductos().Existencia.ToString();
            this.impuesto = (decimal)frm.PasarProductos().Impuesto;
            this.descuento = (decimal)frm.PasarProductos().Descuento;
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
                int precio = int.Parse(this.txtPrecio.Text.Trim());
                int cantidad = int.Parse(this.txtCantidad.Text.Trim());
                decimal montoDescuento = precio * (descuento / 100);
                decimal montoImpuesto = precio * (impuesto / 100);        
                var subtotal =  ((precio + montoImpuesto) - montoDescuento) * cantidad;
                total = total+subtotal;
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
                decimal precio = decimal.Parse(this.dgvCarrito.Rows[0].Cells[2].Value.ToString());
                int cantidad = int.Parse(this.dgvCarrito.Rows[0].Cells[3].Value.ToString());
                decimal montoDescuento = precio * (descuento / 100);
                decimal montoImpuesto = precio * (impuesto / 100);
                var subtotal = ((precio + montoImpuesto) - montoDescuento) * cantidad;
                total = total + subtotal;
                int numeroFactura = this.factura.GetFacturas().Count()+1;
                var temp = this.apiClientes.GetCliente(int.Parse(this.txtCedula.Text.Trim()));
                Factura factura = new Factura()
                {
                    numero = numeroFactura,
                    Fecha = DateTime.Now,
                    codCliente = this.txtCedula.Text.Trim(),
                    SubTotal = subtotal,
                    MontoDescuento = montoDescuento,
                    MontoImpuesto = montoImpuesto,
                    Total = decimal.Parse(this.txtTotalPagar.Text.Trim()),
                    estado = temp.estado,
                    Usuario = temp.Usuario,
                    TipoPago = this.cbTipoPago.Text.Substring(0,1),
                    Condicion = this.cbCondicion.Text.Substring(0,1),
                };
                this.factura.CrearFactura(factura);
                //if (this.cbCondicion.Text.Substring(0,1)=="B")
                //{
                //    Bitacora bitacora = new Bitacora()
                //    {
                //        Tabla = "Bitacora",
                //        Usuario = temp.Usuario,
                //        Maquina = "Home",
                //        Fecha = DateTime.Now,
                //        TipoMov = "Compra",
                //        Registro = temp.NombreCompleto
                //    };
                //    this.factura.CrearBitacora(bitacora);
                //}
                var producto = this.apiProductos.GetProducto(int.Parse(this.dgvCarrito.Rows[0].Cells[0].Value.ToString()));
                //BLL.Producto product = new BLL.Producto()
                //{
                //    CodigoInterno = int.Parse(this.txtCodigoProducto.Text.Trim()),
                //    CodigoBarra = producto.CodigoBarra,
                //    Descripcion = producto.Descripcion,
                //    PrecioVenta = (decimal)double.Parse(this.txtPrecio.Text.Trim()),
                //    Descuento = producto.Descuento,
                //    Impuesto = producto.Impuesto,
                //    UnidadMedida = producto.UnidadMedida,
                //    PrecioCompra = producto.PrecioCompra,
                //    Usuario = producto.Usuario,
                //    Existencia = producto.Existencia-int.Parse(this.txtCantidad.Text.Trim())
                //};
                //this.apiProductos.ActualizarProducto(product);
                this.email.GenerarPDF(factura,temp,producto);
                this.email.Enviar(factura, temp, producto);
                // Crear una lista para almacenar las filas que se eliminarán
                List<DataGridViewRow> filasEliminar = new List<DataGridViewRow>();
                DetalleFactura detalle = new DetalleFactura();

                // Recorrer las filas del DataGridView
                for (int i = 0; i < dgvCarrito.Rows.Count-1; i++)
                {
                    DataGridViewRow fila = dgvCarrito.Rows[i];

                    // Obtener los datos de la fila
                    detalle.numFactura = numeroFactura;
                    detalle.codInterno = int.Parse(fila.Cells[0].Value.ToString());
                    detalle.cantidad = (int.Parse(fila.Cells[3].Value.ToString()));
                    detalle.PrecioUnitario = (int.Parse(fila.Cells[2].Value.ToString()));
                    detalle.Subtotal = subtotal;
                    detalle.PorImp = (int.Parse(fila.Cells[4].Value.ToString()));
                    detalle.PorDescuento = (int.Parse(fila.Cells[5].Value.ToString()));

                    // Agregar la fila a la lista de filas a eliminar
                    filasEliminar.Add(fila);
                    this.factura.CrearFacturaDetalle(detalle);
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
                //this.ActualizarTotal();
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

        //private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (dgvCarrito.SelectedRows.Count > 0)
        //    {
        //        // Confirmación opcional
        //        DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar esta fila?",
        //                                              "Confirmar eliminación",
        //                                              MessageBoxButtons.YesNo,
        //                                              MessageBoxIcon.Warning);

        //        if (result == DialogResult.Yes)
        //        {
        //            // Elimina la fila seleccionada
        //            dgvCarrito.Rows.RemoveAt(dgvCarrito.SelectedRows[0].Index);
        //            this.ActualizarTotal();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Seleccione una fila para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}
        //private void ActualizarTotal()
        //{
        //    decimal total = 0;

        //    foreach (DataGridViewRow fila in dgvCarrito.Rows)
        //    {
        //        if (fila.Cells["SubTotal"].Value != null)
        //        {
        //            decimal precio;
        //            if (decimal.TryParse(fila.Cells["subTotal"].Value.ToString(), out precio))
        //            {
        //                total += precio;
        //            }
        //        }
        //    }

        //    txtTotalPagar.Text = total.ToString("");
        //}
    }//
}//
