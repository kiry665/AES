using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Runtime;
using System.IO;

namespace AES
{
    public partial class Encrypt : Form
    {
        MainMenu mm = new MainMenu();
        byte[] key, iv;
        string text;
        public Encrypt(MainMenu f)
        {
            InitializeComponent();
            mm = f;
        }

        private void Encrypt_FormClosing(object sender, FormClosingEventArgs e)
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
                }
                else
                {
                    flag1 = true;
                    if (textBox1.Text.Length != 32)
                    {
                        string s = Key_generator.key1(textBox1.Text);
                        key = Encoding.ASCII.GetBytes(s);
                        if (checkBox1.Checked) textBox1.Text = s;
                    }
                    else
                    {
                        key = Encoding.ASCII.GetBytes(textBox1.Text);
                    }
                }
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Введите хотя бы один символ в поле Ключ 2");
                }
                else
                {
                    flag2 = true;
                    if (textBox2.Text.Length != 16)
                    {
                        string s = Key_generator.key2(textBox2.Text);
                        iv = Encoding.ASCII.GetBytes(s);
                        if (checkBox1.Checked) textBox2.Text = s;
                    }
                    else
                    {
                        iv = Encoding.ASCII.GetBytes(textBox2.Text);
                    }
                }
                
                if(flag1 && flag2)
                {
                    if (textBox3.Text.Length != 0)
                    {
                        AES_Code aES = new AES_Code();
                        byte[] enc = aES.EncryptStringToByte(textBox3.Text, key, iv);

                        saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
                        if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        {
                            return;
                        }
                        string filename = saveFileDialog1.FileName;
                        File.WriteAllBytes(filename, enc);
                        MessageBox.Show("Файл сохранен");
                    }
                    else
                    {
                        MessageBox.Show("Введите текст для шифрования");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
