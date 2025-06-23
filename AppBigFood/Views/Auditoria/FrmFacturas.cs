using System;
using System.Windows.Forms;
using DAL;

namespace AppBigFood.Views.Auditoria
{
    public partial class FrmFacturas : Form
    {
        private APIFacturas varObjApiFactura = null;

        public FrmFacturas()
        {
            InitializeComponent();
            varObjApiFactura = new APIFacturas();
            this.ConsultarPorNombre();
        }

        private void ConsultarPorNombre()
        {
            try
            {
                this.dgvFacturas.DataSource = this.varObjApiFactura.GetFacturas();
                this.dgvFacturas.AutoResizeColumns();
                this.dgvFacturas.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
