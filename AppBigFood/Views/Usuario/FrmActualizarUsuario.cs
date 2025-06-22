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
        public void PasarDatos(BLL.Usuario pTemp)
        {
            try
            {
                this.txtId.Text = pTemp.Id.ToString();
                this.txtUsuario.Text = pTemp.login;
                this.txtPassword.Text = pTemp.password;
                switch (pTemp.estado)
                {
                    case 'A':
                        this.cbEstado.SelectedIndex = 0;
                        break;
                    case 'I':
                        this.cbEstado.SelectedIndex = 1;
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void ModificarUsuario()
        {
            try
            {
                user = new BLL.Usuario()
                {
                    Id = int.Parse(this.txtId.Text.Trim()),
                    login = this.txtUsuario.Text.Trim(),
                    password = this.txtPassword.Text.Trim(),
                    fechaRegistro = DateTime.Now,
                    estado = char.Parse(this.cbEstado.Text.Substring(0,1)),
                };

                this.apiUsuario.ActualizarUsuario(user);

                MessageBox.Show("Usuario modificado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.ModificarUsuario();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
