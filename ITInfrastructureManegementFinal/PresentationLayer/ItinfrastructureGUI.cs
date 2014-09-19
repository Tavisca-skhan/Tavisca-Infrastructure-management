using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using ItemObject;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;
using BusinessLogicLayer;
using System.IO;

namespace Final1
{
    public partial class Form1 : Form
    {
        enum ProductName { Dongle3G = 1, Laptop, HeadPhone, OS_License, Antivirus, Application_License };
        enum Dongle3GBrands {Airtel,Iball,Dlink,Idea};
        enum LaptopBrand { Dell, Lenova,HP,Toshiba,Acer};
        enum HeadPhoneBrands { Artis,Sony,Iball };
        enum OS_LicenseBrands { Linux,Mac,Windows7};
        enum AntivirusBrands { NPAV,Kaspersky };
        enum Application_LicenseBrands { VisualStudio, Eclipse };
        enum  SearchProductBy{ Name = 1, Brand };
        enum SearchEmployeeBy {Name=1,EmpID };

        string PID, EID;
        string assigndate;
        ItemHelper itemhelper;

        string productFilePath = ConfigurationManager.AppSettings["productFilePath"];
        string employeeFilePath = ConfigurationManager.AppSettings["employeeFilePath"];

        public Form1()
        {
            itemhelper = new ItemHelper();
            InitializeComponent();
            
            cmbSearchEmployee.SelectedIndex = 0;
            cmbSearchProduct.SelectedIndex = 0;

             ItemObjects.XmlFiles x = new ItemObjects.XmlFiles();
          x.ProductFilePath = ConfigurationManager.AppSettings["productFilePath"];
         x.EmployeeFilePath = ConfigurationManager.AppSettings["employeeFilePath"];
        
        // myDataGridView.DataSource = myDataSet.Tables[0].DefaultView;
          PID = "";
          EID = "";
          assigndate = "";

        }
        private void ItInfrastructureManagementLoad(object sender, EventArgs e)
        {
            DataSet myDataSet = new DataSet();

            itemhelper = new ItemHelper();
           
            //ih = new ItemHelper();
            dataGridProductView1.DataSource = itemhelper.ReadGridviewValues("False");
            this.dataGridProductView1.Columns["AssignedTo"].Visible = false;
            this.dataGridProductView1.Columns["DateOfExpiryOfAssignment"].Visible = false;
            this.dataGridProductView1.Columns["AssignedDate"].Visible = false;
            this.dataGridProductView1.Columns["IsAssigned"].Visible = false;
            this.dataGridProductView1.Columns["WarrantyExists"].Visible = false;
            this.dataGridProductView1.Columns["ActualExpiryDate"].Visible = false;


            dataGridAssignView.DataSource = itemhelper.ReadGridviewValues("True");
            dataGridAssignView.Columns["IsAssigned"].Visible = false;
            dataGridAssignView.Columns["DateOfExpiryOfAssignment"].Visible = true;

            DataSet my1DataSet = new DataSet();
            my1DataSet.ReadXml(ConfigurationManager.AppSettings["employeeFilePath"]);
           dgvEmployee.DataSource=my1DataSet.Tables[0].DefaultView;

           DataSet my2DataSet = new DataSet();
           my2DataSet.ReadXml(ConfigurationManager.AppSettings["productFilePath"]);
           dgvDatailView.DataSource = my2DataSet.Tables[0].DefaultView;

           // dgvDatailView

           dataGridAssignView.DataSource = itemhelper.ReadGridviewValues("True");
            splitContainer1.Panel2Collapsed = true;

            //dataGridAssignView.Rows[0].Cells[0].Selected = false;


        }
        
        private void btnUnassign_Click(object sender, EventArgs e)
        {
            itemhelper.Assign(PID, EID, DateTime.Now, false);
             ItInfrastructureManagementLoad(null, null);
            
        }

      
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (PID == "")
            {
                MessageBox.Show("please select item");
            }
            else
            {
               splitContainer1.Panel1Collapsed = true;
               splitContainer1.Panel2Collapsed = false;
            }
           // button_OK.Visible = false;
            buttonBack.Visible = true;
        }

        
        private void textProductSearchBox_TextChanged_1(object sender, EventArgs e)
        {
            string searchByValue = cmbSearchProduct.SelectedItem.ToString();

            string searchvalue = textProductSearchBox.Text;
            dataGridProductView1.DataSource = itemhelper.ReadGridviewValues(searchByValue, searchvalue, "False");
            //MessageBox.Show(searchByValue);

        }

        private void textEmployeeSearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchBy = cmbSearchEmployee.SelectedItem.ToString();
            string searchvalue = textEmployeeSearchBox.Text;
            dgvEmployee.DataSource = itemhelper.ReadGridviewEmpValues(searchBy, searchvalue);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime expiredDate = DTPExpireDate.Value;
            
                if (PID == "")
                {
                     MessageBox.Show("please Select product");
                 }
                else
                 if (EID == "")
                 {
                MessageBox.Show("please select Employee");
                }
                 else
                     if (expiredDate == DateTime.Now)
                     {
                         MessageBox.Show("please select proper date");
                     }
                     else
                     {
                         itemhelper.Assign(PID, EID, expiredDate, true);
                         ItInfrastructureManagementLoad(null, null);
                         splitContainer1.Panel1Collapsed = false;
                        
                         buttonSelect.Visible = true;
                         EmailSender.EmailSend es = new EmailSender.EmailSend();

                          string Emailid = es.GetEmail(EID);//BusinessLogicLayer.Helper.GetEmail(empid);

                          MessageBox.Show("Item Assigned");

                         es.SendMail(Emailid,false);
                         PID = "";
                         EID = "";
                     }
        }

       

        private void btnExtend_Click(object sender, EventArgs e)
        {
            dTPExtendUpto.Visible = true;
            buttonSubmit.Visible = true;
            labelExtend.Visible = true;
           
        }


        private void buttonBack_Click(object sender, EventArgs e)
        {
            buttonSelect.Visible = true;
            buttonBack.Visible = false;
            splitContainer1.Panel1Collapsed = false;
            splitContainer1.Panel2Collapsed = true;
            

        }



        private void cmbProductNameSelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSearchEmployee.Items.Clear();
            string Itemname = cmbSearchProduct.SelectedItem.ToString();
            List<string> BrandList = itemhelper.ReadBrand(Itemname);

            foreach (var item in BrandList)
            {
                cmbSearchEmployee.Items.Add(item);
            }
        }
        private void cmbBrandSelectedIndexChanged(object sender, EventArgs e)
        {
            // cmbProductId.Items.Clear();
            List<string> ids = itemhelper.ReadItemId(cmbSearchProduct.SelectedItem.ToString(), cmbSearchEmployee.SelectedItem.ToString());
            foreach (var item in ids)
            {
                //cmbProductId.Items.Add(item);
            }
        }
        
       

        
        private void SaveItembutton_Click(object sender, EventArgs e)
        {


             if (cmbItem.SelectedItem == null)
            {
                MessageBox.Show("Select Item!!");
            }
            else if (cmbBrandName.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Write Brand");
            }
            else
            {
                Item d = null;
                string Itemname = cmbItem.SelectedItem.ToString();
                //MessageBox.Show(Itemname);
                if (Itemname == ProductName.Dongle3G.ToString())
                {
                    d = new Dongle3G();
                }
                else if (Itemname == ProductName.Laptop.ToString())
                {
                    d = new Laptop();
                }
                else if (Itemname == ProductName.HeadPhone.ToString())
                {
                    d = new HeadPhone();
                }
                else if (Itemname == ProductName.OS_License.ToString())
                {
                    d = new OS_License();
                }
                else if (Itemname == ProductName.Antivirus.ToString())
                {
                    d = new AntiVirus();
                }
                else if (Itemname == ProductName.Application_License.ToString())
                {
                    d = new ApplicationLicense();
                }
                try
                {
                    d.Brand = cmbBrandName.SelectedItem.ToString();
                    d.CreationDate = CreationDateTimePicker.Value;

                    d.ActualExpiryDate = ActualExpiryTimePicker.Value;

                    d.WarrantyExpiration = WarrantyExpiryTimePicker.Value;
                    d.WarrantyExists = "True";
                    d.IsAssigned = "False";
                    itemhelper.Save(d, productFilePath);
                    MessageBox.Show("Item Added");
                   // MessageBox.Show();
                   
                    ItInfrastructureManagementLoad(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        
        }

        private void textBoxUnassignText_TextChanged(object sender, EventArgs e)
        {
            string searchByValue = "";
            if (comboBoxAsgnSearch.SelectedItem.ToString() == "Product Name")
            {
                searchByValue = "Name";
            }
            else if (comboBoxAsgnSearch.SelectedItem.ToString() == "Product Id")
            {
                searchByValue = "UniqueID";
            }
            else if (comboBoxAsgnSearch.SelectedItem.ToString() == "Product Expire Date")
            {
                searchByValue = "ActualExpiryDate";
            }
            else if (comboBoxAsgnSearch.SelectedItem.ToString() == "Employee Id")
            {
                searchByValue = "AssignedTo";
            }
            string searchvalue = textBoxUnassignText.Text;
            dataGridAssignView.DataSource = itemhelper.ReadGridviewValues(searchByValue, searchvalue, "True");

        }

       
        public void writeCSV(DataGridView gridIn, string outputFile)
        {
            //test to see if the DataGridView has any rows
            if (gridIn.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(outputFile);

                //write header rows to csv
                for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(",");
                    }
                    swOut.Write(gridIn.Columns[i].HeaderText);
                }

                swOut.WriteLine();

                //write DataGridView rows to csv
                for (int j = 0; j <= gridIn.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = gridIn.Rows[j];

                    for (int i = 0; i <= gridIn.Columns.Count - 1; i++)
                    {
                        if (i > 0)
                        {
                            swOut.Write(",");
                        }

                        value = dr.Cells[i].Value.ToString();
                        //replace comma's with spaces
                        value = value.Replace(',', ' ');
                        //replace embedded newlines with spaces
                        value = value.Replace(Environment.NewLine, " ");

                        swOut.Write(value);
                    }
                }
                swOut.Close();
            }
        }

       
        private void txtSearchDetail_TextChanged(object sender, EventArgs e)
        {
            string searchByValue = "";
            if (cmbSearchDetail.SelectedItem.ToString() == "Product Name")
            {
                searchByValue = "Name";
            }
            else if (cmbSearchDetail.SelectedItem.ToString() == "Product Id")
            {
                searchByValue = "UniqueID";
            }
            else if (cmbSearchDetail.SelectedItem.ToString() == "Product Expire Date")
            {
                searchByValue = "ActualExpiryDate";
            }
            else if (cmbSearchDetail.SelectedItem.ToString() == "Employee Id")
            {
                searchByValue = "AssignedTo";
            }
            string searchvalue = txtSearchDetail.Text;
            dgvDatailView.DataSource = itemhelper.ReadGridviewValues(searchByValue, searchvalue, "All");
        }

        private void cmbSearchDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearchDetail.SelectedItem.ToString() == "Employee Id")
            {
                txtSearchDetail.Text = "EMP";
            }
            else
            {
                txtSearchDetail.Text = "";
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                writeCSV(dgvDatailView, @"C:\Users\ssakurde\Desktop\New\sushant.cvs");
                MessageBox.Show("Written in cvs file successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            string Itemname = cmbItem.SelectedItem.ToString();
            if (Itemname == ProductName.Dongle3G.ToString())
            {
                cmbBrandName.DataSource = Enum.GetValues(typeof(Dongle3GBrands));
            }
            else if (Itemname == ProductName.Laptop.ToString())
            {
                cmbBrandName.DataSource = Enum.GetValues(typeof(LaptopBrand));
            }
            else if (Itemname == ProductName.HeadPhone.ToString())
            {
                cmbBrandName.DataSource = Enum.GetValues(typeof(HeadPhoneBrands));
            }
            else if (Itemname == ProductName.OS_License.ToString())
            {
                cmbBrandName.DataSource = Enum.GetValues(typeof(OS_License));
            }
            else if (Itemname == ProductName.Antivirus.ToString())
            {
                cmbBrandName.DataSource = Enum.GetValues(typeof(AntivirusBrands));
            }
            else if (Itemname == ProductName.Application_License.ToString())
            {
                cmbBrandName.DataSource = Enum.GetValues(typeof(Application_LicenseBrands));
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (PID == "")
            {
                MessageBox.Show("please select item");
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                splitContainer1.Panel2Collapsed = false;
                buttonSelect.Visible = false;
                buttonBack.Visible = true;
            }
          
        }

       

        private void dgvDatailView_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip abc = new System.Windows.Forms.ToolTip();
            abc.SetToolTip(this.dgvDatailView, "hello there");
        }

        private void dataGridProductView1_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip abc1 = new System.Windows.Forms.ToolTip();
            abc1.SetToolTip(this.dataGridProductView1 , "Click on row to select product");
        }

        private void dataGridProductView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridProductView1.Rows[e.RowIndex];
                PID = row.Cells["UniqueID"].Value.ToString();
                MessageBox.Show(PID);

            }
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvEmployee.Rows[e.RowIndex];
                EID = row.Cells["EmployeeId"].Value.ToString();
                MessageBox.Show(EID);


            }
        }

        private void dataGridAssignView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridAssignView.Rows[e.RowIndex];
                // if (cell != null)
                // id_txt.Text = row.Cells["ID"].Value.ToString();
                PID = row.Cells["UniqueID"].Value.ToString();
                EID = row.Cells["AssignedTo"].Value.ToString();
                assigndate = row.Cells["AssignedDate"].Value.ToString();
                MessageBox.Show(assigndate);
                MessageBox.Show(EID);
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvEmployee_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip abc2 = new System.Windows.Forms.ToolTip();
            abc2.SetToolTip(this.dgvEmployee, "Click on row to select product");
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            TimeSpan ts = Convert.ToDateTime(dTPExtendUpto.Value) - Convert.ToDateTime(assigndate);
            if (ts.Days < 1)
            {
                MessageBox.Show("please select proper date");
            }
            else
            {
                itemhelper.Assign(PID, EID, Convert.ToDateTime(dTPExtendUpto.Value), true);
                ItInfrastructureManagementLoad(null, null);
            }
        }

        private void dgvDatailView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridAssignView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
 
        
    }
}
     

