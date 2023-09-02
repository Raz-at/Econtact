using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        contactClass c = new contactClass();

        private void txtboxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the vakue from the input field
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = comboBoxGender.Text;

            //insert data into database ..
            bool success = c.Insert(c);
            if (success == true)
            {
                //succefully inserted
                MessageBox.Show("New Contact inserted");
                //call the crear method
                clear();
            }
            else
            {
                //faild to insert
                MessageBox.Show("Failed to add contact. Try Again....");
            }

            //load data in dataview box
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            //load data in dataview box
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //method to clear all fields
        public void clear()
        {
            txtboxFirstName.Text="";
            txtboxLastName.Text = "";
            txtboxContactNo.Text = "";
            txtboxAddress.Text = "";
            txtboxContactID.Text = "";
            comboBoxGender.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the data from text boxes 
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = comboBoxGender.Text;

            //update data in database...
            bool success = c.update(c);
            if(success == true)
            {
                //updated succesfully
                MessageBox.Show("contact has been updated succefully");
                //load data in dataview box
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                clear();
            }
            else
            {
                MessageBox.Show("Failed to update the contact");
            }

        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // get the data from data grid view to thr text boxess..
            //identify the row on which the mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString(); 
            txtboxLastName.Text= dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNo.Text= dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text= dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text= dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get data from the text box
            c.ContactID = int.Parse(txtboxContactID.Text);
            bool success = c.delete(c);
            if(success==true)
            {
                MessageBox.Show("Contact deleted succefully");
                clear();
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Contact is unable to deleted");
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        static string myconnstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //get the value from textbox 
            string keyword = txtboxSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstring);
            
            SqlDataAdapter sda = new SqlDataAdapter("select * from tbl_contact where FirstName like '%"+keyword+"%' or lastName like '%"+keyword+ "%' or Address like '%" + keyword + "%' ", conn);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;


        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }
    }


}
