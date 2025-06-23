using System;
using System.Windows.Forms;
using DAL;

namespace AppBigFood.Views.Usuario
{
    public partial class FrmActualizarUsuario : Form
    {
        private BLL.Usuario user = null;
        private APIUsuario apiUsuario = null;
        private int tempID;
        public FrmActualizarUsuario()
        {
            InitializeComponent();
            apiUsuario = new APIUsuario();
        }
        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
