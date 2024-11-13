using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace PharmacyManagement_System
{
    public partial class Manufacturer : Form
    {
        public Manufacturer()
        {
            InitializeComponent();
            ShowMan();
            Reset();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");

        private void ShowMan()
        {
            Con.Open();
            string Query = "select * from Manufacturer";
            SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds =new DataSet();
            sda.Fill(ds);
            ManufacturerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ManAddTb.Text == "" || ManNameTb.Text == "" || ManPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Manufacturer(ManName,ManAdd,ManPhone,ManJDate)values(@MN,@MA,@MP,@MJD)", Con);
                    cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@MJD", ManJDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Added Successfully...");
                    Con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        int key = 0;
        private void ManufacturerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ManNameTb.Text = ManufacturerDGV.SelectedRows[0].Cells[1].Value.ToString();
            ManAddTb.Text = ManufacturerDGV.SelectedRows[0].Cells[2].Value.ToString();
            ManPhoneTb.Text = ManufacturerDGV.SelectedRows[0].Cells[3].Value.ToString();
            ManJDate.Text = ManufacturerDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ManNameTb.Text=="")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ManufacturerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

       

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Manufacturer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Manufacturer where ManId = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", key);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Deleted Successfully...");
                    Con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        private void Reset()
        {
            ManNameTb.Text="";
            ManAddTb.Text="";
            ManPhoneTb.Text="";
            key = 0;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ManAddTb.Text == "" || ManNameTb.Text == "" || ManPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update Manufacturer set ManName=@MN,ManAdd=@MA,ManPhone=@MP,ManJDate=@MJD where ManId=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", ManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MA", ManAddTb.Text);
                    cmd.Parameters.AddWithValue("@MP", ManPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@MJD", ManJDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Manufacturer Updated Successfully...");
                    Con.Close();
                    ShowMan();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Seller obj = new Seller();  
            obj.Show();
            this.Close();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            sellings obj = new sellings();
            obj.Show();
            this.Close();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}