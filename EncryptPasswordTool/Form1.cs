using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptPasswordTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtPepper.Text) && string.IsNullOrWhiteSpace(this.txtPlainText.Text))
            {
                MessageBox.Show("Plain text and Pepper cannot be null or empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // var salt = SaltHelper.GenerateSalt(16);
                string salt;
                if (!string.IsNullOrWhiteSpace(this.txtSalt.Text)) salt = this.txtSalt.Text;
                else salt = SaltHelper.GenerateSalt(16);
                var cipher = AESHelper.Encrypt(this.txtPlainText.Text, this.txtPepper.Text, salt);
                this.txtSalt.Text = salt;
                this.txtCipher.Text = cipher;
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtPepper.Text) && string.IsNullOrWhiteSpace(this.txtCipher.Text) && string.IsNullOrWhiteSpace(this.txtSalt.Text))
            {
                MessageBox.Show("Cipher, Pepper and Salt cannot be null or empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var password = AESHelper.Decrypt(this.txtCipher.Text, this.txtPepper.Text, this.txtSalt.Text);
                this.txtPlainText.Text = password;
            }
        }
    }
}