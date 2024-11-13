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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        

        private void AdminBtn_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();  
            obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");

        public static string User;
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(UNameTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Both UserName and Password");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Seller where SName='"+UNameTb.Text+"' and SPass='"+PasswordTb.Text+"'",Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    User = UNameTb.Text;
                    Seller Obj = new Seller();
                    Obj.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong UserName Or Password");
                }
            }
        }

        private void UNameTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
