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
    public partial class FrmBitacora : Form
    {
        private APIFacturas varObjApiFactura = null;

        public FrmBitacora()
        {
            InitializeComponent();
            varObjApiFactura = new APIFacturas();
            this.ConsultarPorNombre();
        }

        private void ConsultarPorNombre()
        {
            try
            {
                this.dgvBitadora.DataSource = this.varObjApiFactura.GetBitacora();
                this.dgvBitadora.AutoResizeColumns();
                this.dgvBitadora.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
