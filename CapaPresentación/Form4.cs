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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private bool Editarse = false;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            loadUserData();
            initializePassEditControls();
        }

        private void loadUserData()
        {
            lblUser.Text = UserLoginCache.Username;
            lblName.Text = UserLoginCache.FirstName;
            lblLastName.Text = UserLoginCache.LastName;
            lblEmail.Text = UserLoginCache.Email;
            lblUsertype.Text = UserLoginCache.Position;

            txtUser.Text = UserLoginCache.Username;
            txtName.Text = UserLoginCache.FirstName;
            txtLast.Text = UserLoginCache.LastName;
            txtEmail.Text = UserLoginCache.Email;
            txtPass.Text = UserLoginCache.Password;
            txtUsertype.Text = UserLoginCache.Position;
            //dtDate.Value = UserLoginCache.Date_Birth;
        }

        private void initializePassEditControls()
        {
            txtPass.UseSystemPasswordChar = true;
        }

        private void Reset()
        {
            loadUserData();
            initializePassEditControls();
        }

        private void lblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = true;
            loadUserData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Editarse == false)
            {
                try
                {
                    var userModel = new N(
                    username: txtUser.Text,
                    password: txtPass.Text,
                    firstName: txtName.Text,
                    lastName: txtLast.Text,
                    usertype: txtUsertype.Text,
                    email: txtEmail.Text);
                    var result = userModel.insertUserProfile();
                    MessageBox.Show(result);
                    Reset();
                    panel1.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo guardar el usuario. " + ex);
                }
            }
            if (Editarse == true)
            {
                try
                {
                    var userModel = new N(idUser: UserLoginCache.IdUser,
                    username: txtUser.Text,
                    password: txtPass.Text,
                    firstName: txtName.Text,
                    lastName: txtLast.Text,
                    usertype: txtUsertype.Text,
                    email: txtEmail.Text);
                    var result = userModel.editUserProfile();
                    MessageBox.Show(result);
                    Reset();
                    panel1.Visible = false;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Reset();
        }

    }
}
