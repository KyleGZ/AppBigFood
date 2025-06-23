using System;
using System.Windows.Forms;
using DAL;

namespace AppBigFood.Views.Usuario
{
    public partial class FrmAgregarUsuario : Form
    {
        private BLL.Usuario user = null;
        private APIUsuario ApiUser = null;

        public FrmAgregarUsuario()
        {
            InitializeComponent();
            ApiUser = new APIUsuario();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
              


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
