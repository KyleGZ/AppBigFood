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
    public partial class FrmAgregarProducto : Form
    {
        private BLL.Producto varObjProducto = null;
        private APIProducto varObjApiProductos = null;
        public FrmAgregarProducto()
        {
            InitializeComponent();
            varObjApiProductos = new APIProducto();
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
                this.varObjProducto = new BLL.Producto();
                this.varObjProducto.CodiInterno = int.Parse(this.txtCodigoInterno.Text.Trim());
                this.varObjProducto.CodiBarra = this.txtCodigoBarras.Text.Trim();
                this.varObjProducto.PrecioVenta = double.Parse(this.txtPrecioVenta.Text.Trim());
                this.varObjProducto.Descuento = double.Parse(this.txtDescuento.Text.Trim());
                this.varObjProducto.Impuesto = double.Parse(this.txtImpuesto.Text.Trim());
                this.varObjProducto.UnidadMedida = this.txtUnidadMedida.Text.Trim();
                this.varObjProducto.PrecioCompra = double.Parse(this.txtPrecioCompra.Text.Trim());
                this.varObjProducto.Existencia = int.Parse(this.txtExistencias.Text.Trim());
                this.varObjProducto.Descripcion = this.rtxtDescripcion.Text;

                this.varObjApiProductos.CrearProducto(this.varObjProducto);
                MessageBox.Show("Producto registrado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }//
}//
