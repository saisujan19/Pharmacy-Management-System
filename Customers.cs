using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagement_System
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCust();
            Reset();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");

        private void ShowCust()
        {
            Con.Open();
            string Query = "select * from Customer";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
            CustGenderTb.SelectedIndex = 0;
            Key = 0;
            

        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "" || CustGenderTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Customer(CustName,CustAdd,CustPhone,CustDOB,CustGen)values(@CN,@CA,@CP,@CDOB,@CG)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenderTb.SelectedText.ToString());
                    cmd.Parameters.AddWithValue("@CDOB", CustDOBTb.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully...");
                    Con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        int Key = 0;
        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustAddTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhoneTb.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            CustDOBTb.Text = CustomersDGV.SelectedRows[0].Cells[4].Value.ToString();
            CustGenderTb.SelectedItem = CustomersDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Customer where CustNum = @CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted Successfully...");
                    Con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "" || CustGenderTb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update Customer set CustName=@CN,CustAdd=@CA,CustPhone=@CP,CustDOB=@CDOB,CustGen=@CG where CustNum=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenderTb.SelectedText.ToString());
                    cmd.Parameters.AddWithValue("@CDOB", CustDOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated Successfully...");
                    Con.Close();
                    ShowCust();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
           Manufacturer obj = new Manufacturer();
            obj.Show(); 
            this.Hide();    
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Medicines obj = new Medicines();
            obj.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();    
            obj.Show(); 
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Seller obj = new Seller();  
            obj.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
           sellings obj = new sellings();
            obj.Show();
            this.Hide();
        }
    }
}
