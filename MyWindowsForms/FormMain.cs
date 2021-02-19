using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyWindowsForms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
                if(textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" )
                {
                    MessageBox.Show("Enter all");
                    return;
                }

                StreamReader sr = new StreamReader("Data.txt");
                string strcheck = sr.ReadToEnd();
                sr.Close();
                if(strcheck.Contains("ID : " + textBox2.Text + "\n"))
                {
                    MessageBox.Show("This ID Exist");
                    textBox2.Focus();
                    textBox2.SelectAll();
                }
                else
                {
                    StreamWriter sw = new StreamWriter("Data.txt", true);
                    string strPerson = "ID : " +  textBox2.Text + "\n"
                                      + "Name : " + textBox3.Text + "\n"
                                       + "Address : " + textBox4.Text + "\n";
                    sw.WriteLine(strPerson);
                    sw.Close();
                    MessageBox.Show("Person Is Added");
                    foreach(Control c in this.Controls)
                    {
                        if (c is TextBox)
                            c.Text = "";
                    }
                    textBox2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("Enter id");
                    return;
                }
                else
                {
                    StreamReader sr = new StreamReader("Data.txt");
                    string line = "";
                    bool found = false;
                    do
                    {
                        line = sr.ReadLine() + '\n';
                        line += sr.ReadLine() + '\n';
                        line += sr.ReadLine();
                        if (line != "\n\n")
                        {
                            string[] arrData = line.Split('\n');
                            arrData[0] = arrData[0].Substring(5);
                            arrData[1] = arrData[1].Substring(7);
                            arrData[2] = arrData[2].Substring(10);
                            if (textBox2.Text == arrData[0])
                            {
                                found = true;
                                textBox3.Text = arrData[1];
                                textBox4.Text = arrData[2];
                                break;
                            }
                        }
                        line += sr.ReadLine();
                    } while (line != null && line != "\n\n");
                    sr.Close();
                    if (found == false)
                    {
                        MessageBox.Show("not Found");
                        textBox2.Focus();
                        textBox2.SelectAll();
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Font = this.Font;
            frm.Icon = this.Icon;
            frm.Size = this.Size;
            frm.Text = "All Data";
            TextBox txt = new TextBox();
            txt.Multiline = true;
            txt.Dock = DockStyle.Fill;
            frm.Controls.Add(txt);

            try
            {
                StreamReader sr = new StreamReader("Data.txt");
                string data = "";
                string tmp = sr.ReadLine();
                while (tmp != null)
                {
                    data += tmp + "\r\n";
                    tmp = sr.ReadLine();

                }
                sr.Close();
                txt.Text = data;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            frm.ShowDialog();
        }





        // Show Message
        //MessageBox.Show("Hello");

        // Get/Set text
        // XXX.Text;

        // Hide and Show
        // XXX.visible = true or false;

        // Location
        // XXX.Top = YYY;
        // XXX.Left = YYY;
        // XXX.Location = new Point(x,y);

        // Events 1
        // _Load
        // _Resize
        // _Move 
        // _MouseHover
        // _MouseLeave

        // Events 2
        // xxx.Focus()

        // Width or Height
        // Screen.PrimaryScreen.Bounds.Width / Height
        // this.Width / Height

        // Sive
        // this.size = new Size(w,h);

        // switch between forms
        // formName xxx = new formName();
        //  Application.OpenForms[0].Show();
        // this.show() / ShowDialog() / Hide() / Close()

        // Create form using code
        // Form name = new Form()
        // xxx.StartPosition 
        // FormStartPosition.CenterScreen

        // Create new component
        // Component xxx = new Component();
        // Foemname.Controls.Add(xxx);

        // Check Box
        // xxx.Checked / ToString()
        // xxx.ChekedItems
        // xxx.Items.Add();
        // xxx.Items.AddRange();

        // Icon
        // this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);

        // Exit 
        //  Application.Exit();
    }
}
