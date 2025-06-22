using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppBigFood.Views.Cliente;
using DAL;

namespace AppBigFood.Views.Usuario
{
    public partial class FrmTablaUsuarios : Form
    {
        private APIUsuario apiUser = null;

        public FrmTablaUsuarios()
        {
            InitializeComponent();
            apiUser = new APIUsuario();
            ConsultarPorNombre();
        }

        private void ConsultarPorNombre()
        {
            try
            {
                this.dgvTablaUsuario.DataSource = this.apiUser.GetUsuarios();
                this.dgvTablaUsuario.AutoResizeColumns();
                this.dgvTablaUsuario.ReadOnly = true;
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
                this.MostrarActualizarUsuario();
                this.ConsultarPorNombre();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MostrarActualizarUsuario()
        {
            try
            {
                FrmActualizarUsuario frm = new FrmActualizarUsuario();

                //Se crea la instancia el objeto
                BLL.Usuario temp = new BLL.Usuario();

                //Se rellenan los campos del objeto con la fila seleccionada
                temp.Id = int.Parse(this.dgvTablaUsuario.SelectedRows[0].Cells[0].Value.ToString());
                temp.login = this.dgvTablaUsuario.SelectedRows[0].Cells[1].Value.ToString();
                temp.password = this.dgvTablaUsuario.SelectedRows[0].Cells[2].Value.ToString();
                temp.estado = char.Parse(this.dgvTablaUsuario.SelectedRows[0].Cells[4].Value.ToString());

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
                if (this.dgvTablaUsuario.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del usuario que desea eliminar");
                }

                //Solicitamos confirmacion para eliminar
                if (MessageBox.Show("Desea eliminar el cliente seleccionado", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Se toma la fila seleccionada y se extrae el login en la celda 0
                    this.EjecutarEliminar(int.Parse(this.dgvTablaUsuario.SelectedRows[0].Cells[0].Value.ToString()));

                    //Se actualiza la lista
                    this.ConsultarPorNombre();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EjecutarEliminar(int pCliente)
        {
            try
            {
                this.apiUser.EliminarUsuario(pCliente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void MostrarAgregarUsuario()
        {
            try
            {
                FrmAgregarUsuario frm = new FrmAgregarUsuario();
                frm.ShowDialog();
                this.ConsultarPorNombre();
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.MostrarAgregarUsuario();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
