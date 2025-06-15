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
using BLL;
using DAL;
using Newtonsoft.Json;

namespace AppBigFood.Views.Cliente
{
    public partial class FrmAgregarCliente : Form
    {
        private BLL.Cliente varObjClientes = null;
        private APIClientes varObjApiClientes = null;
        private HttpAPI _api;
        private ClienteGometa datosCliente = null;

        public FrmAgregarCliente()
        {
            InitializeComponent();
            this.varObjApiClientes = new APIClientes();
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
                this.varObjClientes = new BLL.Cliente();
                this.varObjClientes.Id = 0;
                this.varObjClientes.TipoCedula = cbTipoCedula.SelectedItem.ToString();
                this.varObjClientes.cedula = txtCedula.Text.Trim();
                this.varObjClientes.NombreCompleto = txtFullName.Text;
                this.varObjClientes.Email = txtEmail.Text.Trim();
                this.varObjClientes.Estado = "Activo";

                this.varObjApiClientes.CrearCliente(this.varObjClientes);

                MessageBox.Show("Cliente registrado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void extraerDatos(string cedula)
        {

            //Reglas de negocio
            try
            { //Intento de conexion con la API

                //Se crea instancia de la API
                this._api = new HttpAPI();

                //Se obtiene el objeto cliente para consumir la API
                HttpClient client = this._api.Gometa();

                //Se utiliza el metodo publico de la API Gometa
                HttpResponseMessage response = client.GetAsync("cedulas/" + cedula).Result;

                if (response.IsSuccessStatusCode)
                {
                    //Aqui lee los datos obtenidos del objeto JSON
                    var result = response.Content.ReadAsStringAsync().Result;

                    //Se convierte el objeto JSON al objeto TipoCambio del modelo
                    datosCliente = JsonConvert.DeserializeObject<ClienteGometa>(result);
                }

            }//En caso de error capturamos con la ex
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }//
}//
