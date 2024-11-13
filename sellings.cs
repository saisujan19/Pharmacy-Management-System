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
    public partial class sellings : Form
    {
        public sellings()
        {
            InitializeComponent();
            ShowMed();
            ShowBill();
            SnameLb1.Text = Login.User;
            GetCustomer();
            GetCustName();
            
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAI\SQLEXPRESS;Initial Catalog=PharmacyCdb;Integrated Security=True;Pooling=False;Encrypt=False;");

        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select CustNum from Customer", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustNum", typeof(int));
            dt.Load(Rdr);
            CustIdcd.ValueMember = "CustNum";
            CustIdcd.DataSource = dt;
            Con.Close();
        }
        private void GetCustName()
        {
            Con.Open();
            String Query = "Select * from Customer where CustNum='" + CustIdcd.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }

        private void UpdateQty()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(MedQtyTb.Text);
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update Medicines set MedQty=@MQ where MedNum=@MKey", Con);
                cmd.Parameters.AddWithValue("@MQ", NewQty);
                
                cmd.Parameters.AddWithValue("@MKey", Key);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Medicine Updated Successfully...");
                Con.Close();
                ShowMed();
                //Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       

        private void ShowBill()
        {
            Con.Open();
            string Query = "select * from BillTbl where SName='"+SnameLb1.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransactionDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ShowMed()
        {
            Con.Open();
            string Query = "select * from Medicines";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            MedicinesDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        int n = 0,GrdTotal=0;
        private void AddToBill_Click(object sender, EventArgs e)
        {
            if (MedQtyTb.Text == "" || Convert.ToInt32(MedQtyTb.Text) > Stock)
            {
                MessageBox.Show("Enter Correct Quality");
            }
            else
            {
                int total =Convert.ToInt32(MedQtyTb.Text)* Convert.ToInt32(MedPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = MedNameTb.Text;
                newRow.Cells[2].Value = MedQtyTb.Text;
                newRow.Cells[3].Value = MedPriceTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);
                GrdTotal = GrdTotal + total;
                totalLb1.Text = "Rs    " + GrdTotal;
                n++;
                UpdateQty();
                


            }
        }

        private void InsertBill()
        {
            if (CustNameTb.Text == "")
            {

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(SName,CustNum,CustName,BDate,BAmount)values(@SN,@CN,@CNA,@BD,@BA)", Con);
                    cmd.Parameters.AddWithValue("@SN", SnameLb1.Text);
                    cmd.Parameters.AddWithValue("@CN", CustIdcd.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CNA", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("BD", DateTime.Today.Date);
                    cmd.Parameters.AddWithValue("@BA", GrdTotal);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved");
                    Con.Close();
                    ShowBill();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }


        }
        int Key = 0, Stock;
        int MedId,MedPrice,MedQty,MedTot;

        private void CustIdcd_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void TransactionDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
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

        int pos = 60;

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            


        }
        string MedName;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("HealthCare Pharmacy", new Font("Century Gothic", 12, FontStyle.Regular), Brushes.Red,new Point(65));
            e.Graphics.DrawString("ID MEDICINE PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                MedId = Convert.ToInt32(row.Cells["Column1"].Value);
                MedName = "" + row.Cells["Column2"].Value;
                MedPrice = Convert.ToInt32(row.Cells["Column3"].Value);
                MedQty = Convert.ToInt32(row.Cells["Column4"].Value);
                MedTot = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + MedId, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + MedName, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + MedPrice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + MedQty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + MedTot, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total:Rs " + GrdTotal, new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString(" ****************PHARMACY*************", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Crimson, new Point(15, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;

        }

           
        private void MedicinesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MedNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[1].Value.ToString();
            //MedTypeTb.Text = MedicinesDGV.SelectedRows[0].Cells[2].Value.ToString();
            Stock =Convert.ToInt32( MedicinesDGV.SelectedRows[0].Cells[3].Value.ToString());
            MedPriceTb.Text = MedicinesDGV.SelectedRows[0].Cells[4].Value.ToString();
            //MedManTb.SelectedValue = MedicinesDGV.SelectedRows[0].Cells[5].Value.ToString();
            //MedManNameTb.Text = MedicinesDGV.SelectedRows[0].Cells[6].Value.ToString();
            if (MedNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(MedicinesDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
