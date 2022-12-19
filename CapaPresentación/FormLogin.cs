using CapaDominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentación
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "USUARIO")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.LightGray;
            }
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "USUARIO";
                txtUser.ForeColor = Color.DimGray;
            }
        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "CONTRASEÑA")
            {
                txtContraseña.Text = "";
                txtContraseña.ForeColor = Color.LightGray;
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        private void txtContraseña_Leave(object sender, EventArgs e)
        {
            if (txtContraseña.Text == "")
            {
                txtContraseña.Text = "CONTRASEÑA";
                txtContraseña.ForeColor = Color.DimGray;
                txtContraseña.UseSystemPasswordChar = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AutenticarUsuario();
        }

        private void AutenticarUsuario()
        {
            if (txtUser.Text != "USUARIO")
            {
                if (txtContraseña.Text != "CONTRASEÑA")
                {
                    N user = new N();
                    var validLogin = user.LoginUser(txtUser.Text, txtContraseña.Text);
                    if (validLogin == true)
                    {
                        this.Hide();
                        FormMainMenu form = new FormMainMenu();
                        WelcomeForm welcome = new WelcomeForm();
                        welcome.ShowDialog();
                        form.Show();
                        form.FormClosed += LogOut;
                    }
                    else
                    {
                        msgError("Incorrect username or password entered. \n  Please try again.");
                        txtContraseña.Text = "CONTRASEÑA";
                        txtUser.Focus();
                    }
                }
                else
                {
                    msgError("Please enter password");
                }
            }
            else
            {
                msgError("Please enter username");
            }
        }

        private void msgError(string msg)
        {
            lblErrorMessage.Text = "    " + msg;
            lblErrorMessage.Visible = true;
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutenticarUsuario();
            }
        }
        private void LogOut(object sender, FormClosedEventArgs e)
        {
            txtUser.Text = "USUARIO";
            txtContraseña.Text = "CONTRASEÑA";
            txtContraseña.UseSystemPasswordChar = false;
            lblErrorMessage.Visible = false;
            this.Show();
            txtUser.Focus();
        }

    }
}
