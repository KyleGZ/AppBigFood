using System;
using System.Windows.Forms;
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

                BLL.Cliente temp = new BLL.Cliente();

                temp.cedulaLegal = this.dgvTablaClientes.SelectedRows[0].Cells[0].Value.ToString();
                temp.tipoCedula = this.dgvTablaClientes.SelectedRows[0].Cells[1].Value.ToString();
                temp.NombreCompleto = this.dgvTablaClientes.SelectedRows[0].Cells[2].Value.ToString();
                temp.Email = this.dgvTablaClientes.SelectedRows[0].Cells[3].Value.ToString();
                temp.fechaRegistro = DateTime.Parse(this.dgvTablaClientes.SelectedRows[0].Cells[4].Value.ToString());
                temp.estado = char.Parse(this.dgvTablaClientes.SelectedRows[0].Cells[5].Value.ToString());
                temp.Usuario = int.Parse(this.dgvTablaClientes.SelectedRows[0].Cells[6].Value.ToString());

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
                if (this.dgvTablaClientes.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del usuario que desea eliminar");
                }

                if (MessageBox.Show("Desea eliminar el cliente seleccionado", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.EjecutarEliminar(int.Parse(this.dgvTablaClientes.SelectedRows[0].Cells[0].Value.ToString()));

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
                this.varObjClientes.EliminarCliente(pCliente);
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
                if (this.dgvTablaClientes.SelectedRows.Count <= 0)
                {
                    throw new Exception("Seleccione la fila del cliente que desea seleccionar");
                }

                BLL.Cliente cliente = new BLL.Cliente();
                cliente.cedulaLegal = this.dgvTablaClientes.SelectedRows[0].Cells[0].Value.ToString();
                cliente.NombreCompleto = this.dgvTablaClientes.SelectedRows[0].Cells[2].Value.ToString();

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
                this.ConsultarPorNombre();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
