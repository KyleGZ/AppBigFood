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
    public partial class FrmTablaClientes : Form
    {
        private APIClientes varObjClientes = null;

        public FrmTablaClientes()
        {
            InitializeComponent();
            varObjClientes = new APIClientes();
            ConsultarPorNombre();
        }

        private void ConsultarPorNombre()
        {
            try
            {
                this.dgvTablaClientes.DataSource = this.varObjClientes.GetClientes();
                this.dgvTablaClientes.AutoResizeColumns();
                this.dgvTablaClientes.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.PasarClientes();
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarActualizarCliente()
        {
            try
            {
                FrmActualizarCliente frm = new FrmActualizarCliente();

                //Se crea la instancia el objeto
                BLL.Cliente temp = new BLL.Cliente();

                //Se rellenan los campos del objeto con la fila seleccionada
                temp.Id = int.Parse(this.dgvTablaClientes.SelectedRows[0].Cells[0].Value.ToString());
                temp.TipoCedula = this.dgvTablaClientes.SelectedRows[0].Cells[2].Value.ToString();
                temp.cedula = this.dgvTablaClientes.SelectedRows[0].Cells[1].Value.ToString();
                temp.NombreCompleto = this.dgvTablaClientes.SelectedRows[0].Cells[3].Value.ToString();
                temp.Email = this.dgvTablaClientes.SelectedRows[0].Cells[4].Value.ToString();
                temp.Estado = this.dgvTablaClientes.SelectedRows[0].Cells[5].Value.ToString();

                //Se pasa el objeto al formulario
                frm.PasarDatos(temp);

                frm.ShowDialog();
                frm.Dispose();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Se valida que el usuario tenga una fila seleccionada
                if (this.dgvTablaClientes.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del usuario que desea eliminar");
                }

                //Solicitamos confirmacion para eliminar
                if (MessageBox.Show("Desea eliminar el cliente seleccionado", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Se toma la fila seleccionada y se extrae el login en la celda 0
                    this.EjecutarEliminar(this.dgvTablaClientes.SelectedRows[0].Cells[0].Value.ToString());

                    //Se actualiza la lista
                    this.ConsultarPorNombre();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EjecutarEliminar(string pCliente)
        {
            try
            {
                this.varObjClientes.EliminarCliente(int.Parse(pCliente));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarAgregarCliente();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarAgregarCliente()
        {
            try
            {
                FrmAgregarCliente frm = new FrmAgregarCliente();
                frm.ShowDialog();
                this.ConsultarPorNombre();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BLL.Cliente PasarClientes()
        {
            try
            {
                //Se valida que el destino tenga una fila seleccionada
                if (this.dgvTablaClientes.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del cliente que desea seleccionar");
                }

                BLL.Cliente cliente = new BLL.Cliente();
                cliente.cedula = this.dgvTablaClientes.SelectedRows[0].Cells[1].Value.ToString();
                cliente.NombreCompleto = this.dgvTablaClientes.SelectedRows[0].Cells[3].Value.ToString();

                return cliente;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.MostrarActualizarCliente();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }//
}//
