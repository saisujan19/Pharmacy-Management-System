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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountMed();
            CountCustomers();
            CountSellers();
            SumAmt();
            GetSeller();
            GetBestSeller();
            GetBestCustomer();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");
        private void CountMed()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Medicines", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MedNum.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountSellers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Seller", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellerLb1.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void CountCustomers()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Customer", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CustLb1.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmt()
        {
            Con.Open();
            SqlDataAdapter sda =  new SqlDataAdapter("Select Sum(BAmount) from BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellAmtTb.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmtBySeller()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum(BAmount) from BillTbl where SName='" + SellerCb.SelectedValue.ToString() + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellsBySeller.Text = "Rs " + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void GetBestSeller()
        {
            try
            {
                Con.Open();
                string InnerQuery = " select Max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
                sda1.Fill(dt1);
                String Query = "select SName from BillTbl where BAmount ='" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BestSellerLb2.Text = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception ex)
            {
                
               Con.Close();
                
            }
        }

        private void GetBestCustomer()
        {
            try
            {
                Con.Open();
                string InnerQuery = " select Max(BAmount) from BillTbl";
                DataTable dt1 = new DataTable();
                SqlDataAdapter sda1 = new SqlDataAdapter(InnerQuery, Con);
                sda1.Fill(dt1);
                String Query = "select SName from BillTbl where BAmount ='" + dt1.Rows[0][0].ToString() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BestSellerLb2.Text = dt.Rows[0][0].ToString();
                Con.Close();
            }
            catch (Exception ex)
            {

                Con.Close();

            }
        }
        private void GetSeller()
            {

                Con.Open();
                SqlCommand cmd = new SqlCommand("select SName from Seller", Con);
                SqlDataReader Rdr;
                Rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("SName", typeof(String));
                dt.Load(Rdr);
                SellerCb.ValueMember = "SName";
                SellerCb.DataSource = dt;
                Con.Close();

            }

        private void SellsBySeller_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Manufacturer obj =new Manufacturer();
            obj.Show();
            this.Close();
        }

        private void label16_Click_1(object sender, EventArgs e)
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

        private void label17_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Close();
        }
    } 
}
 