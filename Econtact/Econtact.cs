using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Econtact.econtactClasses;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.select();
            dataGridViewContactList.DataSource = dt;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNo.Text;
                c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;
            bool success = c.Insert(c);
            if(success==true)
            {
                MessageBox.Show("New Contact Insterted");
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Add");
            }

            DataTable dt =c.select();
            dataGridViewContactList.DataSource = dt;

        }


        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            textboxContactId.Text = "";
            textBoxFirstName.Text="";
           textBoxLastName.Text = "";
            textBoxContactNo.Text = "";
           textBoxAddress.Text = "";
            comboBoxGender.Text = "";
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridViewContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            textboxContactId.Text = dataGridViewContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridViewContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridViewContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNo.Text = dataGridViewContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dataGridViewContactList.Rows[rowIndex].Cells[4].Value.ToString();
            comboBoxGender.Text = dataGridViewContactList.Rows[rowIndex].Cells[5].Value.ToString(); ;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            c.ContactID = int.Parse(textboxContactId.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = comboBoxGender.Text;

            bool success = c.Update(c);
            if(success==true)
            {
                MessageBox.Show("Contact Updated");
                Clear();
                DataTable dt = c.select();
                dataGridViewContactList.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Failed to Update");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(textboxContactId.Text);
            if (c.ContactID != null)
            {
                bool success = c.Delete(c);
                if (success == true)
                {
                    MessageBox.Show("Contact Deleted");
                    Clear();
                    DataTable dt = c.select();
                    dataGridViewContactList.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Unable to delete");
                }
            }
            else
            {
                MessageBox.Show("Select Contact to delete");
            }
        }
        static string myconntstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconntstring);
            SqlDataAdapter da = new SqlDataAdapter("Select * from tbl_contact where FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridViewContactList.DataSource = dt;

        }
    }
}
