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

namespace AppBigFood.Views.Auditoria
{
    public partial class FrmCuentasXCobrar : Form
    {
        private APIFacturas varObjApiFactura = null;

        public FrmCuentasXCobrar()
        {
            InitializeComponent();
            varObjApiFactura = new APIFacturas();
            this.ConsultarPorNombre();
        }

        private void ConsultarPorNombre()
        {
            try
            {
                this.dgvCuentas.DataSource = this.varObjApiFactura.CuentasPorCobrar();
                this.dgvCuentas.AutoResizeColumns();
                this.dgvCuentas.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
