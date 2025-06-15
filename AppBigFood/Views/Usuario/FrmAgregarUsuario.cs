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

namespace AppBigFood.Views.Usuario
{
    public partial class FrmAgregarUsuario : Form
    {
        private UsuarioRegistro varObjUsuario = null;
        private APIUsuario varObjApiUsuario = null;

        public FrmAgregarUsuario()
        {
            InitializeComponent();
            varObjApiUsuario = new APIUsuario();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.RegistrarUsuario();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void RegistrarUsuario()
        {
            try
            {
                this.varObjUsuario = new UsuarioRegistro();
                this.varObjUsuario.nombre = this.txtFullName.Text;
                this.varObjUsuario.NombreUsuario = this.txtUsuario.Text.Trim();
                this.varObjUsuario.Email = this.txtEmail.Text.Trim();
                this.varObjUsuario.Password = this.txtPassword.Text.Trim();

                await this.varObjApiUsuario.CrearUsuario(this.varObjUsuario);
                MessageBox.Show("Usuario registrado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }//
}//
