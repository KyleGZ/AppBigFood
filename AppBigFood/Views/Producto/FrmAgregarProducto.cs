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
    public partial class FrmAgregarProducto : Form
    {
        private BLL.Producto producto = null;
        private APIProducto apiProductos = null;
        private APIFacturas apiFactura = null;
        public FrmAgregarProducto()
        {
            InitializeComponent();
            apiProductos = new APIProducto();
            this.apiFactura = new APIFacturas();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.RegistrarProducto();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void RegistrarProducto()
        {
            try
            {
                if (this.apiProductos.GetProducto(int.Parse(this.txtCodigoInterno.Text.Trim())) == null)
                {
                    producto = new BLL.Producto()
                    {
                        CodigoInterno = int.Parse(this.txtCodigoInterno.Text.Trim()),
                        CodigoBarra = this.txtCodigoBarras.Text.Trim(),
                        PrecioVenta = (decimal)double.Parse(this.txtPrecioVenta.Text.Trim()),
                        Descuento = (decimal)double.Parse(this.txtDescuento.Text.Trim()),
                        Impuesto = (decimal)double.Parse(this.txtImpuesto.Text.Trim()),
                        UnidadMedida = this.txtUnidadMedida.Text.Trim(),
                        PrecioCompra = (decimal)double.Parse(this.txtPrecioCompra.Text.Trim()),
                        Existencia = int.Parse(this.txtExistencias.Text.Trim()),
                        Usuario = int.Parse(this.txtUsuario.Text.Trim()),
                        Descripcion = this.rtxtDescripcion.Text
                    };
                    this.apiProductos.CrearProducto(producto);
                    Bitacora bitacora = new Bitacora()
                    {
                        Tabla = "FrmAgregarProducto",
                        Usuario = producto.Usuario,
                        Maquina = "Home",
                        Fecha = DateTime.Now,
                        TipoMov = "Agregado",
                        Registro = producto.Descripcion
                    };
                    MessageBox.Show("Producto registrado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ya existe un producto con el codigo ingresado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
