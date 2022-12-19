using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad.Cache;
using CapaDominio;

namespace CapaPresentación
{
    public partial class FormVisitantes : Form
    {
        public FormVisitantes()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                var userModel = new N(
                    name: txtNombre.Text,
                    last: txtApellido.Text,
                    career: txtCarrera.Text,
                    mail: txtCorreo.Text,
                    entrytime: dtpEntrada.Value,
                    outtime: dtpSalida.Value,
                    reason: txtMotivo.Text,
                    building: Convert.ToInt32(txtEdificio.Text),
                    place: Convert.ToInt32(txtLugar.Text));
                var result = userModel.insertVisitor();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el usuario. " + ex);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }
    }
}
