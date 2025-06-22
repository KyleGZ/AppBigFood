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
                //this.RegistrarUsuario();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private async void RegistrarUsuario()
        //{
        //    try
        //    {
        //        if (await this.ApiUser.GetUsuario(int.Parse(this.txtId.Text.Trim())) != null)
        //        {
        //            MessageBox.Show("Ya existe un usuario con el id ingresado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        user = new BLL.Usuario()
        //        {
        //            Id = int.Parse(this.txtId.Text.Trim()),
        //            login = this.txtUsuario.Text.Trim(),
        //            password = this.txtPassword.Text.Trim(),
        //            fechaRegistro = DateTime.Now,
        //            estado = char.Parse(this.cbEstado.Text.Substring(0, 1))
        //        };
        //        await this.ApiUser.CrearUsuario(user);
        //        MessageBox.Show("Usuario registrado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        this.Close();
                    
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}


    }//
}//
