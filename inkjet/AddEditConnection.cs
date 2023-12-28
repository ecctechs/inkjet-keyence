using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper.Configuration;
using CsvHelper;
using inkjet.Class;
using inkjet.UserControls;
using Guna.UI2.WinForms;

namespace inkjet
{
    public partial class AddEditConnection : Form
    {
        public AddEditConnection(Inkjet obj)
        {
            InitializeComponent();
            bindingSource1.DataSource = obj;
            //Console.WriteLine(obj.InkJetName);

            if (obj.IPAdress != null)
            {
                var ip = obj.IPAdress.Split('.');
                //string ip1 = obj.IPAdress.Substring(0, 3); // substr1 will contain "Hello"
                //string ip2 = obj.IPAdress.Substring(4, 3);
                //string ip3 = obj.IPAdress.Substring(8, 3);
                //string ip4 = obj.IPAdress.Substring(10, 3);
                //string ip_conbine = ip1 + "." + ip2 + "." + ip3 + "." + ip4;
                //Console.WriteLine(ip_conbine + "full :"+ obj.IPAdress);

                txtIP1.Text = ip[0];
                txtIP2.Text = ip[1];
                txtIP3.Text = ip[2];
                txtIP4.Text = ip[3];
                txtInkjetName.Text = obj.InkJetName;

                string inkjet_id = Convert.ToString(obj.InkJetID);
                txtEditID.Text = inkjet_id;
                
            }
        }

        private void btnCancal_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Inkjet> list_inkjet = new List<Inkjet>();
            //Console.WriteLine(id_edit);
            if (txtIP1.Text.Trim() != "" && txtIP2.Text.Trim() != "" && txtIP3.Text.Trim() != "" && txtIP4.Text.Trim() != ""
                //&& txtIP1.Text.Length == 3 && txtIP2.Text.Length == 3 && txtIP3.Text.Length == 3 && txtIP4.Text.Length == 3 &&
                && txtInkjetName.Text.Trim() != "")
            {
                var full_ip = txtIP1.Text + "." + txtIP2.Text + "." + txtIP3.Text + "." + txtIP4.Text;
                var chk_duplicate = Inkjet.Duplicate_Inkjet(full_ip, txtEditID.Text);
                Console.WriteLine("----"+txtInkjetName.Text);

                if (chk_duplicate == true && txtEditID.Text == "") //ถ้าไม่มีซํ้า และ ไม่มี id เก่า ให้ add ใหม่
                {
                    list_inkjet.Add(new Inkjet{ InkJetID = Inkjet.running_id , InkJetName = txtInkjetName.Text, IPAdress = full_ip});
                    Inkjet.Add_Inkjet(list_inkjet);                    
                    DialogResult = DialogResult.OK;
                    List<Inkjet> inkjet_list2 = Inkjet.ListInkjet();
                    Inkjet.Update_Inkjet(inkjet_list2);
                    //MessageBox.Show(this, "Add Data Success");
                }

                else if (chk_duplicate == true && txtEditID.Text != null) //ถ้าไม่มีซํ้า และ มี id เก่า ให้ไป update อันเดิม
                {

                    
                    string program_name_running = ucCSVmarking.inkjet_name;
                    Console.WriteLine("---->>"+ program_name_running + "+++"+ txtInkjetName.Text);
                    List<Inkjet> inkjet_list = Inkjet.ListInkjet();
                    //if (program_name_running == txtInkjetName.Text)
                    //{
                    //    MessageBox.Show(this, "Inkjet is Running !!");
                    //}
                    //else
                    //{
                    string gg = ucCSVmarking.inkjet_name;
                    if (gg != "")
                    {
                        MessageBox.Show(this, "Cant edit because Inkjet is Running !!");
                    }
                    else
                    {
                        for (int i = 0; i < inkjet_list.Count; i++)
                        {

                            if (txtEditID.Text == inkjet_list[i].InkJetID.ToString())
                            {
                                inkjet_list[i].InkJetID = Convert.ToInt32(txtEditID.Text);
                                inkjet_list[i].InkJetName = txtInkjetName.Text;
                                inkjet_list[i].IPAdress = full_ip;
                            }
                            Inkjet.Update_Inkjet(inkjet_list);
                        }
                        DialogResult = DialogResult.OK;
                    }
                    //for (int i = 0; i < inkjet_list.Count; i++)
                    //    {

                    //        if (txtEditID.Text == inkjet_list[i].InkJetID.ToString())
                    //        {
                    //            inkjet_list[i].InkJetID = Convert.ToInt32(txtEditID.Text);
                    //            inkjet_list[i].InkJetName = txtInkjetName.Text;
                    //            inkjet_list[i].IPAdress = full_ip;
                    //        } 
                    //        Inkjet.Update_Inkjet(inkjet_list);
                    //    }
                    //    DialogResult = DialogResult.OK;
                    

                        //for (int i = 0; i < inkjet_list.Count; i++)
                        //{

                        //    if (txtEditID.Text == inkjet_list[i].InkJetID.ToString())
                        //    {
                        //        inkjet_list[i].InkJetID = Convert.ToInt32(txtEditID.Text);
                        //        inkjet_list[i].InkJetName = txtInkjetName.Text;
                        //        inkjet_list[i].IPAdress = full_ip;
                        //    }
                        //        Inkjet.Update_Inkjet(inkjet_list);                           
                        //}
                        //DialogResult = DialogResult.OK;
                        //}
                    
                    //MessageBox.Show(this, "Update Data Success");
                }
                else
                {
                    MessageBox.Show(this, "Data is Already Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(this, "Please Fill All Data");
            }  
        }

        private void txtIP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void txtIP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIP3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIP4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIP1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIP1.Text != "")
            {
                int txt = Int32.Parse(txtIP1.Text);
                if (txt > 255)
                {
                    MessageBox.Show("IPAddress is out of rang ! more then 255");
                    txtIP1.Text = "";
                }
            }
        }

        private void txtIP2_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIP2.Text != "")
            {
                int txt = Int32.Parse(txtIP2.Text);
                if (txt > 255)
                {
                    MessageBox.Show("IPAddress is out of rang ! more then 255");
                    txtIP2.Text = "";
                }
            }
        }

        private void txtIP3_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIP3.Text != "")
            {
                int txt = Int32.Parse(txtIP3.Text);
                if (txt > 255)
                {
                    MessageBox.Show("IPAddress is out of rang ! more then 255");
                    txtIP3.Text = "";
                }
            }
        }

        private void txtIP4_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtIP4.Text != "")
            {
                int txt = Int32.Parse(txtIP4.Text);
                if (txt > 255)
                {
                    MessageBox.Show("IPAddress is out of rang ! more then 255");
                    txtIP4.Text = "";
                }
            }
        }
    }
}
