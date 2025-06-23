using System;
using System.Net.Http;
using System.Windows.Forms;
using BLL;
using DAL;
using Newtonsoft.Json;

namespace AppBigFood.Views.Cliente
{
    public partial class FrmAgregarCliente : Form
    {
        private BLL.Cliente clientes = null;
        private APIClientes apiClientes = null;
        private HttpAPI _api;
        private ClienteGometa datosCliente = null;

        public FrmAgregarCliente()
        {
            InitializeComponent();
            this.apiClientes = new APIClientes();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.RegistrarCliente();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RegistrarCliente()
        {
            try
            {
                    clientes = new BLL.Cliente()
                    {
                        cedulaLegal = txtCedula.Text.Trim(),
                        tipoCedula = this.cbTipoCedula.SelectedItem.ToString(),
                        NombreCompleto = txtFullName.Text,
                        Email = txtEmail.Text.Trim(),
                        fechaRegistro = DateTime.Now,
                        estado = char.Parse(this.cbEstado.Text.Substring(0, 1)),
                        Usuario = int.Parse(this.txtUsuario.Text.Trim())
                    };
                this.apiClientes.CrearCliente(clientes);
                MessageBox.Show("Cliente registrado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async void extraerDatos(string cedula)
        {
            try
            {
                this._api = new HttpAPI();
                HttpClient client = this._api.Gometa();

                HttpResponseMessage response = client.GetAsync("/cedulas/" + cedula).Result;

                if (response.IsSuccessStatusCode)
                {
                    var contenido = response.Content.ReadAsStringAsync().Result;
                    datosCliente = JsonConvert.DeserializeObject<ClienteGometa>(contenido);
                    this.txtFullName.Text = datosCliente.nombre;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la API: " + ex.Message);
            }
        }
        private void txtCedula_Leave(object sender, EventArgs e)
        {
            string cedula = txtCedula.Text.Trim();

            if (!string.IsNullOrEmpty(cedula))
            {
                extraerDatos(cedula);
            }
        }
    }
}
