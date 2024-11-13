using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PharmacyManagement_System
{
    public partial class Medicines : Form
    {
        public Medicines()
        {
            InitializeComponent();
            ShowMed();
            GetManufacturer();
            GetManName();
        }
         
        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");
        private void ShowMed()
        {
            Con.Open();
            string Query = "select * from Medicines";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder();
            var ds = new DataSet();
            sda.Fill(ds);
            MedicinesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            MedManTb.Text = "";
            MedNameTb.Text = "";
            MedPriceTb.Text = "";
            MedQtyTb.Text = "";
            MedTypeTb.SelectedIndex = 0;
            Key = 0; 
        }
        private void GetManufacturer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select ManId from Manufacturer", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ManId", typeof(int));
            dt.Load(Rdr);
            MedManTb.ValueMember = "ManId";
            MedManTb.DataSource = dt; 
            Con.Close() ;
        }
        private void GetManName()
        {
            Con.Open();
            String Query ="Select * from Manufacturer where ManId='" + MedManTb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda =new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                 MedManNameTb.Text = dr["ManName"].ToString();
            }
            Con.Close( );
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (MedNameTb.Text == "" || MedTypeTb.SelectedIndex == -1 || MedQtyTb.Text == ""|| MedPriceTb.Text == ""|| MedManTb.Text == "" || MedManNameTb.Text=="")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into Medicines(MedName,MedType,MedQty,MedPrice,MedManId,MedManufact)values(@MN,@MT,@MQ,@MP,@MM,@MMN)", Con);
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeTb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MM", MedManTb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MMN", MedManNameTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicines Added Successfully...");
                    Con.Close(); 
                    ShowMed();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void MedManTb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetManName();
        }
        int Key = 0;

        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
            MedTypeTb.Text = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            MedQtyTb.Text = MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString();
            MedPriceTb.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();
            MedManTb.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            MedManNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Medicines");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From Medicines  where MedNum = @MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicines Deleted Successfully...");
                    Con.Close();
                    ShowMed();
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
            if (MedNameTb.Text == "" || MedTypeTb.SelectedIndex == -1 || MedQtyTb.Text == "" || MedPriceTb.Text == "" || MedManTb.Text == "" || MedManNameTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            } 
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update Medicines set MedName=@MN,MedType=@MT,MedQty=@MQ,MedPrice=@MP,MedManId=@MM,MedManufact=@MMN where MedNum=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", MedNameTb.Text);
                    cmd.Parameters.AddWithValue("@MT", MedTypeTb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@MQ", MedQtyTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MedPriceTb.Text);
                    cmd.Parameters.AddWithValue("@MM", MedManTb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@MMN", MedManNameTb.Text);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Medicine Updated Successfully...");
                    Con.Close();
                    ShowMed();
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
    }
}
