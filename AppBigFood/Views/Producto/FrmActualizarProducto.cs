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
    public partial class FrmActualizarProducto : Form
    {
        private BLL.Producto varObjProducto = null;
        private APIProducto varObjApiProductos = null;
        public FrmActualizarProducto()
        {
            InitializeComponent();
            this.varObjApiProductos = new APIProducto();
        }

        public void PasarDatos(BLL.Producto pTemp)
        {
            try
            {
                this.txtCodigoInterno.Text = pTemp.CodiInterno.ToString();
                this.txtCodigoBarras.Text = pTemp.CodiBarra;
                this.txtPrecioVenta.Text = pTemp.PrecioVenta.ToString();
                this.txtDescuento.Text = pTemp.Descuento.ToString();
                this.txtImpuesto.Text = pTemp.Impuesto.ToString();
                this.txtUnidadMedida.Text = pTemp.UnidadMedida;
                this.txtPrecioCompra.Text = pTemp.PrecioCompra.ToString();
                this.txtExistencias.Text = pTemp.Existencia.ToString();
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
                this.varObjProducto = new BLL.Producto();
                this.varObjProducto.CodiInterno = int.Parse(this.txtCodigoInterno.Text.Trim());
                this.varObjProducto.CodiBarra = this.txtCodigoBarras.Text.Trim();
                this.varObjProducto.PrecioVenta = double.Parse(this.txtPrecioVenta.Text.Trim());
                this.varObjProducto.Descuento = double.Parse(this.txtDescuento.Text.Trim());
                this.varObjProducto.Impuesto = double.Parse(this.txtImpuesto.Text.Trim());
                this.varObjProducto.UnidadMedida = this.txtUnidadMedida.Text.Trim();
                this.varObjProducto.PrecioCompra = double.Parse(this.txtPrecioCompra.Text.Trim());
                this.varObjProducto.Existencia = int.Parse(this.txtExistencias.Text.Trim());
                this.varObjProducto.Descripcion = this.rtxtDescripcion.Text.Trim();

                this.varObjApiProductos.ActualizarProducto(this.varObjProducto.CodiInterno, this.varObjProducto);
                MessageBox.Show("Producto modificado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }//
}//
