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
    public partial class Seller : Form
    {
        public Seller()
        {
            InitializeComponent();
            ShowSell();
            Reset();

        }

        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");

        private void ShowSell()
        {
            Con.Open();
            string Query = "select * from Seller";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            SellNameTb.Text = "";
            SellAddTb.Text = "";
            SellPassTb.Text = "";
            SellPhoneTb.Text = "";
            SellGenTb.SelectedIndex = 0;
            Key = 0;

        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SellNameTb.Text == "" || SellAddTb.Text == "" || SellPhoneTb.Text == "" || SellGenTb.SelectedIndex== -1 || SellPassTb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Seller(SName,SAdd,SPhone,SDOB,SGen,SPass)values(@SN,@SA,@SP,@SDOB,@SG,@SPA)", Con);
                    cmd.Parameters.AddWithValue("@SN", SellNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SellAddTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SellPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SellGenTb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDOB", SellDOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@SPA", SellPassTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Added Successfully...");
                    Con.Close();
                    ShowSell();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }
        int Key = 0;

        private void SellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SellNameTb.Text = SellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            SellAddTb.Text = SellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            SellPhoneTb.Text = SellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            SellGenTb.SelectedItem = SellerDGV.SelectedRows[0].Cells[4].Value.ToString();
            SellPassTb.Text = SellerDGV.SelectedRows[0].Cells[5].Value.ToString();
            SellDOBTb.Text = SellerDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (SellNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(SellerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Seller");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Seller where SNum = @SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfully...");
                    Con.Close();
                    ShowSell();
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
            if (SellNameTb.Text == "" || SellAddTb.Text == "" || SellPhoneTb.Text == "" || SellGenTb.SelectedIndex == -1 || SellPassTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update Seller set SName=@SN,SAdd=@SA,SPhone=@SP,SDOB=@SDOB,SPass=@SPA,SGen=@SG where SNum=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", SellNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SellAddTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SellPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SPA", SellPassTb.Text);
                    cmd.Parameters.AddWithValue("@SG", SellGenTb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDOB", SellDOBTb.Value.Date);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Updated Successfully...");
                    Con.Close();
                    ShowSell();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void Seller_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {
            sellings obj = new sellings();  
            obj.Show();
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Manufacturer obj = new Manufacturer();
            obj.Show();
            this.Close();
        }

        private void label17_Click(object sender, EventArgs e)
        {

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
