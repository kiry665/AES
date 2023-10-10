using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AES
{
    public partial class Decrypt : Form
    {
        MainMenu mm;
        byte[] key, iv;
        public Decrypt(MainMenu mm)
        {
            InitializeComponent();
            this.mm = mm;
        }

        private void Decrypt_FormClosing(object sender, FormClosingEventArgs e)
        {
            mm.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag1 = false, flag2 = false;
            try
            {
                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Введите хотя бы один символ в поле Ключ 1");
                    return;
                }
                else
                {
                    flag1 = true;
                    if (textBox1.Text.Length != 32)
                    {
                        string s = Key_generator.key1(textBox1.Text);
                        key = Encoding.ASCII.GetBytes(s);
                    }
                    else
                    {
                        key = Encoding.ASCII.GetBytes(textBox1.Text);
                    }
                }
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Введите хотя бы один символ в поле Ключ 2");
                    return;
                }
                else
                {
                    flag2 = true;
                    if (textBox2.Text.Length != 16)
                    {
                        string s = Key_generator.key2(textBox2.Text);
                        iv = Encoding.ASCII.GetBytes(s);
                    }
                    else
                    {
                        iv = Encoding.ASCII.GetBytes(textBox2.Text);
                    }
                }

                if (flag1 && flag2)
                {
                    openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                    if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    string filename = openFileDialog1.FileName;
                    byte[] enc = File.ReadAllBytes(filename);

                    AES_Code aEs = new AES_Code();
                    textBox3.Text = aEs.DecryptStringFromByte(enc, key, iv);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
