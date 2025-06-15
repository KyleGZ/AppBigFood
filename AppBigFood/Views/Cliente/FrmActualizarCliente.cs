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
        private BLL.Cliente varObjCliente = null;
        private APIClientes varObjApiCliente = null;
        private int tempID;
        public FrmActualizarCliente()
        {
            InitializeComponent();
            varObjApiCliente=new APIClientes();
        }
        public void PasarDatos(BLL.Cliente pTemp)
        {
            try
            {
                this.tempID = pTemp.Id;
                this.txtCedula.Text = pTemp.cedula;
                this.cbTipoCedula.Items.Add(pTemp.TipoCedula);
                this.txtFullName.Text = pTemp.NombreCompleto;
                this.txtEmail.Text = pTemp.Email;
                this.txtEstado.Text = pTemp.Estado;
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
                this.varObjCliente = new BLL.Cliente();
                this.varObjCliente.Id = this.tempID;
                this.varObjCliente.TipoCedula = this.cbTipoCedula.SelectedItem.ToString();
                this.varObjCliente.cedula = this.txtCedula.Text.Trim();
                this.varObjCliente.NombreCompleto = this.txtFullName.Text;
                this.varObjCliente.Email = this.txtEmail.Text.Trim();
                this.varObjCliente.Estado = this.txtEstado.Text.Trim();

                this.varObjApiCliente.ActualizarCliente(this.tempID, this.varObjCliente);

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
