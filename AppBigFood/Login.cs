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

namespace AppBigFood
{
    public partial class Login : Form
    {
        private UsuarioLogin varObjUsuario = null;
        private APIUsuario varObjApiUsuario = null;
        public Login()
        {
            InitializeComponent();
            varObjApiUsuario = new APIUsuario();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Auntenticarse();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Auntenticarse()
        {
            try
            {
                this.varObjUsuario = new UsuarioLogin();
                this.varObjUsuario.NombreUsuario = this.txtUsuario.Text.Trim();
                this.varObjUsuario.Password = this.txtPassword.Text.Trim();

                if (await this.varObjApiUsuario.Login(varObjUsuario))
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
