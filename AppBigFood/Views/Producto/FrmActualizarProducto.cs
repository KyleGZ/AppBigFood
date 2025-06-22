using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;

namespace AppBigFood.Views.Producto
{
    public partial class FrmActualizarProducto : Form
    {
        private BLL.Producto producto = null;
        private APIProducto apiProductos = null;
        private APIFacturas apiFactura = null;
        public FrmActualizarProducto()
        {
            InitializeComponent();
            this.apiProductos = new APIProducto();
            this.apiFactura = new APIFacturas();
        }

        public void PasarDatos(BLL.Producto pTemp)
        {
            try
            {
                this.txtCodigoInterno.Text = pTemp.CodigoInterno.ToString();
                this.txtCodigoBarras.Text = pTemp.CodigoBarra;
                this.txtPrecioVenta.Text = pTemp.PrecioVenta.ToString();
                this.txtDescuento.Text = pTemp.Descuento.ToString();
                this.txtImpuesto.Text = pTemp.Impuesto.ToString();
                this.txtUnidadMedida.Text = pTemp.UnidadMedida;
                this.txtPrecioCompra.Text = pTemp.PrecioCompra.ToString();
                this.txtExistencias.Text = pTemp.Existencia.ToString();
                this.txtUsuario.Text = pTemp.Usuario.ToString();    
                this.rtxtDescripcion.Text = pTemp.Descripcion;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ModificarProducto();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ModificarProducto()
        {
            try
            {
                producto = new BLL.Producto()
                {
                    CodigoInterno = int.Parse(this.txtCodigoInterno.Text.Trim()),
                    CodigoBarra = this.txtCodigoBarras.Text.Trim(),
                    Descripcion = this.rtxtDescripcion.Text.Trim(),
                    PrecioVenta = (decimal)double.Parse(this.txtPrecioVenta.Text.Trim()),
                    Descuento = (decimal)double.Parse(this.txtDescuento.Text.Trim()),
                    Impuesto = (decimal)double.Parse(this.txtImpuesto.Text.Trim()),
                    UnidadMedida = this.txtUnidadMedida.Text.Trim(),
                    PrecioCompra = (decimal)double.Parse(this.txtPrecioCompra.Text.Trim()),
                    Usuario = int.Parse(this.txtUsuario.Text.Trim()),
                    Existencia = int.Parse(this.txtExistencias.Text.Trim())     
                };
                this.apiProductos.ActualizarProducto(producto);
                Bitacora bitacora = new Bitacora()
                {
                    Tabla = "FrmActualizarProducto",
                    Usuario = producto.Usuario,
                    Maquina = "Home",
                    Fecha = DateTime.Now,
                    TipoMov = "Modificado",
                    Registro = producto.Descripcion
                };
                this.apiFactura.CrearBitacora(bitacora);
                MessageBox.Show("Producto modificado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }//
}//
