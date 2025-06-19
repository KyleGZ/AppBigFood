using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBigFood.Views;
using BLL;
using DAL;
using Newtonsoft.Json;

namespace AppBigFood
{
    public partial class Login : Form
    {
        //Variable para almacenar los datos del usuario
        private Usuario user = null;

        //Variable para manejar la referencia para la web api
        private HttpAPI _client = null;

        //Variable para usar los metodos en la api
        private HttpClient _api = null;

        private APIUsuario _apiUsuario = null;

        public Login()
        {
            InitializeComponent();
            _client = new HttpAPI();
            _api = _client.Seguridad();
            _apiUsuario = new APIUsuario();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
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
                this.user = new Usuario();
                this.user.login = this.txtUsuario.Text.Trim();
                this.user.password = this.txtPassword.Text.Trim();

                AutorizacionResponse autorizacionResponse =
                await this._apiUsuario.Login(this.user);
                if (autorizacionResponse != null)
                {
                    Home.token = autorizacionResponse.Token;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email o password incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
