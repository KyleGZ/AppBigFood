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
                switch (char.Parse(pTemp.tipoCedula))
                {
                    case 'N':
                        this.cbTipoCedula.SelectedIndex=0;
                        break;
                    case 'E':
                        this.cbTipoCedula.SelectedIndex = 1;
                        break;
                } 
                this.txtFullName.Text = pTemp.NombreCompleto;
                this.txtEmail.Text = pTemp.Email;
                //this.cbEstado.Items.Add(pTemp.estado);
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
                    tipoCedula = this.cbTipoCedula.Text.Substring(0, 1),
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

        //private void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cliente = this.apiCliente.GetCliente(int.Parse(this.txtCedula.Text.Trim()));
        //        PasarDatos(cliente);
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message, "Confirmado", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }//
}//
