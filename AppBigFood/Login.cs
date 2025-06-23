using System;
using System.Net.Http;
using System.Windows.Forms;
using AppBigFood.Views;
using BLL;
using DAL;

namespace AppBigFood
{
    public partial class Login : Form
    {
        private Usuario user = null;

        private HttpAPI _client = null;

        private HttpClient _api = null;
        public Home HomeForm { get; set; }

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
                this.Autenticarse();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void Autenticarse()
        {
            try
            {
                var usuario = new Usuario
                {
                    NombreUsuario = this.txtUsuario.Text.Trim(),
                    Contrasena = this.txtPassword.Text.Trim()
                };

                AutorizacionResponse autorizacionResponse = await this._apiUsuario.Login(usuario);

                if (autorizacionResponse != null && !string.IsNullOrEmpty(autorizacionResponse.Token))
                {
                    Home.token = autorizacionResponse.Token;

                    var httpAPI = new HttpAPI();
                    Sesion.Permisos = await httpAPI.ObtenerPermisos(usuario.NombreUsuario, "BigFOOD");

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private bool visible = false;
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            
            if (visible)
            {
                this.txtPassword.PasswordChar = '*';
            }
            else
            {
                this.txtPassword.PasswordChar = '\0';
            }
            visible = !visible;
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Home.token == null)
            {
                Application.Exit();
            }
        }
    }
}
