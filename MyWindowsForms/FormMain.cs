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
                if(textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || pictureBox1.Image == null)
                {
                    MessageBox.Show("Enter all");
                    return;
                }
                string strcheck;
                if (File.Exists("Data.txt"))
                {
                    StreamReader sr = new StreamReader("Data.txt");
                    strcheck = sr.ReadToEnd();
                    sr.Close();
                }
                else
                {
                    strcheck = "";
                }
                
                if (strcheck.Contains("ID : " + textBox2.Text + "\n"))
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
                    if (!Directory.Exists("img"))
                    {
                        Directory.CreateDirectory("img");
                    }
                    pictureBox1.Image.Save("img/" + textBox2.Text + ".jpg");
                    MessageBox.Show("Person Is Added");
                    foreach(Control c in this.Controls)
                    {
                        if (c is TextBox)
                            c.Text = "";
                     }
                    pictureBox1.Image = new PictureBox().Image;
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
                                if(File.Exists("img/" + arrData[0] + ".jpg"))
                                {
                                    pictureBox1.Image = Image.FromFile("img/" + arrData[0] + ".jpg");
                                }
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

        private void button6_Click(object sender, EventArgs e)
        {
             OpenFileDialog xxx = new OpenFileDialog();
            if(xxx.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(xxx.FileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Font = this.Font;
            frm.Icon = this.Icon;
            frm.Size = this.Size;
            frm.Height += 300;
            frm.Text = "All Data Images";
            frm.AutoScroll = true;


            try
            {
                StreamReader sr = new StreamReader("Data.txt");
                string data = "";
                string tmp = sr.ReadLine();
                int myTop = 10;
                while (tmp != null)
                {
                    data = tmp;
                    data += "\r\n" + sr.ReadLine();
                    data += "\r\n" + sr.ReadLine();
                    sr.ReadLine();
                    TextBox txt = new TextBox();
                    PictureBox pic = new PictureBox();
                    txt.Width = 300;
                    txt.Height = 100;
                    txt.Top = myTop;
                    txt.Multiline = true;
                    txt.Text = data;
                    pic.Left = 305;
                    pic.Top = myTop;
                    pic.Size = new Size(100, 100);
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    myTop += 150;                    
                    tmp = tmp.Substring(5);
                    if(File.Exists("img/" + tmp + ".jpg"))
                        pic.Image = Image.FromFile("img/" + tmp + ".jpg");
                    frm.Controls.Add(txt);
                    frm.Controls.Add(pic);

                    tmp = sr.ReadLine();
                    data = "";

                }
                sr.Close();
            }
            catch (Exception ex)
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

        // Numeric up dow 
        // xxx.Value;

        // PictureBox
        // pictureBox1.Image = Properties.Resources.nameOfPic
        // pictureBox1.Image = Image.FromFile("")
        // Bitmap yyy = new Bimtmap("name file") ;
        // xxxx.Image = yyy
        // File.Open("name of file",FileMode.Open);
        // Bitmap yyy = new Bimtmap(File.Open("name of file",FileMode.Open)) ;
        // FileStream xxx =  File.Open("name of file",FileMode.Open);
        // xxx.Close();
        // pictureBox1.Image = new PictureBox().Image;


        // File 
        // xxx.InitialDirectory = "C:\\";
        // xxx.InitialDirectory = Enviroment.GetFolderPath(Enviroment..SpecialFolder.YYYY);
        // xxx.FileName;
        // Path.getFileName(xxx.FileName);
        // Path.getExtension(xxx.FileName);
        // xxx.Multiselect = true or false
        // OpenFileDialog xxx = new OpenFileDialog();
        // xxx.ShowDialog()
        // DialogResult.OK
        // SaveFileDialog xxx = new SaveFileDialog();
        // xxx.Filter = "Description (JPG Images)|*.png"
        // xxx.Filter = "JPG & PNG Images)*.png;*.jpg"
        // xxx.FilterIndex = index;
        // File.Exists()
        // File.Copy(From,To);
        // File.Move(,)

        // Remove or Delete Controls
        //this.Controls[name or index].dispose


    }
}
