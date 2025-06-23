using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBigFood.Views.Auditoria;
using AppBigFood.Views.Cliente;
using AppBigFood.Views.Producto;
using BLL;
using DAL;
using Newtonsoft.Json;

namespace AppBigFood.Views
{
    public partial class Home : Form
    {

        public static string token = null;
        private APIClientes apiClientes = null;
        private APIProducto apiProductos = null;
        private APIUsuario apiUser = null;
        private APIFacturas apiFactura = null;
        private HttpAPI gometa = null;
        private Email email = null;

        private decimal impuesto;
        private decimal descuento;
        private decimal total;
        private decimal totalDolares;



        public Home()
        {
            InitializeComponent();
            this.apiUser = new APIUsuario();
            this.apiFactura = new APIFacturas();
            this.apiClientes = new APIClientes();
            this.apiProductos = new APIProducto();
            this.email = new Email();
            this.total = 0;
            this.totalDolares = 0;
        }
        private void informacionDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTablaProductos frm = new FrmTablaProductos();
            frm.ShowDialog();
            frm.Dispose();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            this.MostrarLogin();
        }
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
                decimal tipoCambio = decimal.Parse(this.obtenerTipoCambio().ToString("0.00"));
                int precio = int.Parse(this.txtPrecio.Text.Trim());
                int cantidad = int.Parse(this.txtCantidad.Text.Trim());
                decimal montoDescuento = precio * (descuento / 100);
                decimal montoImpuesto = precio * (impuesto / 100);
                var subtotal = ((precio + montoImpuesto) - montoDescuento) * cantidad;
                total = total + subtotal;
                totalDolares = decimal.Parse(total.ToString("0.00")) / tipoCambio;
                this.dgvCarrito.Rows.Add(this.txtCodigoProducto.Text,
                    this.txtNombreProducto.Text, this.txtPrecio.Text, this.txtCantidad.Text,
                    this.impuesto, this.descuento, subtotal);
                this.dgvCarrito.AutoResizeColumns();
                this.txtTotalPagar.Text = total.ToString();
                this.txtTotalDolares.Text = totalDolares.ToString("0.00");

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
                int numeroFactura = this.apiFactura.GetFacturas().Count() + 1;
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
                    TipoPago = this.cbTipoPago.SelectedItem.ToString(),
                    Condicion = this.cbCondicion.SelectedItem.ToString(),
                };
                this.apiFactura.CrearFactura(factura);
                if (this.cbCondicion.SelectedItem.ToString() == "Credito")
                {
                    CuentasXCobrar cuentas = new CuentasXCobrar()
                    {
                        numFactura = numeroFactura,
                        codCliente = temp.cedulaLegal,
                        FechaFactura = factura.Fecha,
                        FechaRegistro = DateTime.Now,
                        montoFactura = decimal.Parse(this.txtTotalPagar.Text.Trim()),
                        usuario = temp.Usuario,
                        estado = temp.estado
                    };
                    this.apiFactura.CrearCuentasXCobarar(cuentas);
                    Bitacora bitaco = new Bitacora()
                    {
                        Tabla = "Home",
                        Usuario = temp.Usuario,
                        Maquina = "Home",
                        Fecha = DateTime.Now,
                        TipoMov = "CxC",
                        Registro = temp.NombreCompleto
                    };
                    this.apiFactura.CrearBitacora(bitaco);
                }
                Bitacora bitacora = new Bitacora()
                {
                    Tabla = "Home",
                    Usuario = temp.Usuario,
                    Maquina = "Home",
                    Fecha = DateTime.Now,
                    TipoMov = "Compra",
                    Registro = temp.NombreCompleto
                };
                this.apiFactura.CrearBitacora(bitacora);
                var producto = this.apiProductos.GetProducto(int.Parse(this.dgvCarrito.Rows[0].Cells[0].Value.ToString()));
                BLL.Producto product = new BLL.Producto()
                {
                    CodigoInterno = int.Parse(this.dgvCarrito.Rows[0].Cells[0].Value.ToString()),
                    CodigoBarra = producto.CodigoBarra,
                    Descripcion = producto.Descripcion,
                    PrecioVenta = (decimal)double.Parse(this.dgvCarrito.Rows[0].Cells[2].Value.ToString()),
                    Descuento = producto.Descuento,
                    Impuesto = producto.Impuesto,
                    UnidadMedida = producto.UnidadMedida,
                    PrecioCompra = producto.PrecioCompra,
                    Usuario = producto.Usuario,
                    Existencia = producto.Existencia - int.Parse(this.dgvCarrito.Rows[0].Cells[3].Value.ToString())
                };
                this.apiProductos.ActualizarProducto(product);

                List<DataGridViewRow> filasEliminar = new List<DataGridViewRow>();
                List<DetalleFactura> detallesFactura = new List<DetalleFactura>();

                for (int i = 0; i < dgvCarrito.Rows.Count - 1; i++)
                {
                    DataGridViewRow fila = dgvCarrito.Rows[i];
                    DetalleFactura detalle = new DetalleFactura();

                    detalle.numFactura = numeroFactura;
                    detalle.codInterno = int.Parse(fila.Cells[0].Value.ToString());
                    detalle.cantidad = (int.Parse(fila.Cells[3].Value.ToString()));
                    detalle.PrecioUnitario = (int.Parse(fila.Cells[2].Value.ToString()));
                    detalle.Subtotal = subtotal;
                    detalle.PorImp = (int.Parse(fila.Cells[4].Value.ToString()));
                    detalle.PorDescuento = (int.Parse(fila.Cells[5].Value.ToString()));


                    filasEliminar.Add(fila);
                    detallesFactura.Add(detalle);
                    this.apiFactura.CrearFacturaDetalle(detalle);
                }
                this.email.GenerarPDF(factura, temp, producto, detallesFactura);
                this.email.Enviar(factura, temp, producto, detallesFactura);

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

                for (int i = 0; i < dgvCarrito.Rows.Count; i++)
                {
                    DataGridViewRow fila = dgvCarrito.Rows[i];

                    if (fila.IsNewRow)
                    {
                        continue;
                    }

                    filasEliminar.Add(fila);
                }

                foreach (DataGridViewRow filaEliminar in filasEliminar)
                {
                    dgvCarrito.Rows.Remove(filaEliminar);
                }
                MessageBox.Show("Compra cancelada con exito", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txtTotalPagar.Text = "";
                this.txtTotalDolares.Text = "";
                this.total = 0;
                this.totalDolares = 0;
                this.impuesto = 0;
                this.descuento = 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCarrito.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar esta fila?",
                                                      "Confirmar eliminación",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    dgvCarrito.Rows.RemoveAt(dgvCarrito.SelectedRows[0].Index);
                    this.ActualizarTotal();
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ActualizarTotal()
        {
            decimal total = 0;
            decimal totalDolares = 0;
            decimal tipoCambio = decimal.Parse(this.obtenerTipoCambio().ToString("0.00"));

            foreach (DataGridViewRow fila in dgvCarrito.Rows)
            {
                if (fila.Cells["SubTotal"].Value != null)
                {
                    decimal precio;
                    if (decimal.TryParse(fila.Cells["subTotal"].Value.ToString(), out precio))
                    {
                        total += precio;
                        totalDolares += total / tipoCambio;
                    }
                }
            }
            Math.Round(totalDolares, 2);
            txtTotalPagar.Text = total.ToString("");
            txtTotalDolares.Text = totalDolares.ToString("0.00");
        }

        private decimal obtenerTipoCambio()
        {
            try
            {
                // Crear instancia y llamar la API
                this.gometa = new HttpAPI();
                HttpClient client = this.gometa.Gometa();

                // Llamar al endpoint del tipo de cambio
                HttpResponseMessage response = client.GetAsync("/tdc/tdc.json").Result;

                if (response.IsSuccessStatusCode)
                {
                    var contenido = response.Content.ReadAsStringAsync().Result;

                    // Deserializar contenido
                    var tipoCambio = JsonConvert.DeserializeObject<TipoCambioResponse>(contenido);

                    // Retornar valor (ajusta según la estructura real del JSON)
                    return (decimal)tipoCambio.Venta;
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el tipo de cambio. Código: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar el tipo de cambio: " + ex.Message);
            }

            return 0m; // Retorna 0 si hubo error
        }
        public class TipoCambioResponse
        {
            [JsonProperty("venta")]
            public decimal Venta { get; set; }
        }
        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private async Task CargarPermisosDesdeAPI()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7157/");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Sesion.Token);

                var response = await client.GetAsync($"/UsuarioPantallaPermiso/GetPermisosPorUsuario?login={Sesion.UsuarioLogin}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var permisos = JsonConvert.DeserializeObject<List<PermisoDTO>>(json);
                    Sesion.Permisos = permisos ?? new List<PermisoDTO>();
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los permisos del usuario.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Sesion.Permisos = new List<PermisoDTO>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar permisos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Sesion.Permisos = new List<PermisoDTO>();
            }
        }

        private void informacionDeClientesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmTablaClientes frm = new FrmTablaClientes();
            frm.ShowDialog();
            frm.Dispose();
        }
    }
}
