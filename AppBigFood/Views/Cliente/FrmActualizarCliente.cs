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

namespace AppBigFood.Views.Cliente
{
    public partial class FrmActualizarCliente : Form
    {
        private BLL.Cliente cliente = null;
        private APIClientes apiCliente = null;
        private int tempID;
        public FrmActualizarCliente()
        {
            InitializeComponent();
            apiCliente = new APIClientes();
        }
        public void PasarDatos(BLL.Cliente pTemp)
        {
            try
            {
                this.txtCedula.Text = pTemp.cedulaLegal;
                this.cbTipoCedula.Text = pTemp.tipoCedula;
                this.txtFullName.Text = pTemp.NombreCompleto;
                this.txtEmail.Text = pTemp.Email;
                switch (pTemp.estado)
                {
                    case 'A':
                        this.cbEstado.SelectedIndex = 0;
                        break;
                    case 'I':
                        this.cbEstado.SelectedIndex = 1;
                        break;
                }
                this.txtUsuario.Text = pTemp.Usuario.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                this.ModificarCliente();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ModificarCliente()
        {
            try
            {
                cliente = new BLL.Cliente()
                {
                    cedulaLegal = this.txtCedula.Text.Trim(),
                    tipoCedula = this.cbTipoCedula.SelectedItem.ToString(),
                    NombreCompleto = this.txtFullName.Text.Trim(),
                    Email = this.txtEmail.Text.Trim(),
                    fechaRegistro = DateTime.Now,
                    estado = char.Parse(this.cbEstado.Text.Substring(0, 1)),
                    Usuario = int.Parse(txtUsuario.Text.Trim())
                };
                this.apiCliente.ActualizarCliente(cliente);
                MessageBox.Show("Cliente modificado correctamente", "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }//
}//
