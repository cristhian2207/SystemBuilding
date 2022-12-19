using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad.Cache;

namespace CapaPresentación
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();
        }
        
        #region Funcionalidades del form
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(49, 66, 82));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }
        //Capturar posición y tamaño antes maximizar para restaurar
        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Width;
            sh = this.Height;
            btnMaximizar.Visible = false;
            btnRestaurar.Visible = true;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            btnMaximizar.Visible = true;
            btnRestaurar.Visible = false;
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void panelBarra_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit?", "Warning", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
                Application.Exit();
        }

        //Método para arrastrar el form 
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirForm<FormVisitantes>();
            btnVisitantes.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirForm<Form3>();
            btnConsultas.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirForm<Form4>();
            btnConfig.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirForm<Form5>();
            btnUsers.BackColor = Color.FromArgb(12, 61, 92);
        }

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to logout?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataUser();
            //Permisos
            if (UserLoginCache.Position == Users.General)
            {
                btnConfig.Enabled = false;
                btnUsers.Enabled = false;
            }
        }
        private void LoadDataUser()
        {
            lblNombre.Text = UserLoginCache.LastName+ ", " + UserLoginCache.FirstName;
            lblUsuario.Text = UserLoginCache.Position;
            lblEmail.Text = UserLoginCache.Email;
        }
        #endregion
        //Método para abrir un form dentro de un panel
        private void AbrirForm<MiForm>()where MiForm :Form, new()
        {
            Form formulario;
            formulario = panelFormularios.Controls.OfType<MiForm>().FirstOrDefault(); // Busca en la colección el formulario
            //Si el formulario no existe
            if(formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelFormularios.Controls.Add(formulario);
                panelFormularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms);
            }
            //Si el formulario existe
            else
            {
                formulario.BringToFront();
            }
        }
        public void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Form2"] == null)
                btnVisitantes.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form3"] == null)
                btnConsultas.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form4"] == null)
                btnConfig.BackColor = Color.FromArgb(4, 41, 68);
            if (Application.OpenForms["Form5"] == null)
                btnUsers.BackColor = Color.FromArgb(4, 41, 68);
        }
    }
}
