using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace LapTrinhCoSoDuLieu
{
    public partial class quanlynhansu : MetroFramework.Forms.MetroForm
    {
        string cnstr;
        SqlConnection cn;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        public quanlynhansu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            cnstr = ConfigurationManager.ConnectionStrings["QLNHANSU_ConnectionString"].ConnectionString;
            cn = new SqlConnection(cnstr);
            dgvnhanvien.DataSource = getStaff().Tables["staff"];
            btnsua.Visible=false;
            btndong.Visible = false;
            dgvchucvu.DataSource = GetSection().Tables["Section"];
            dgvnhanvien.DataSource = GetTable();
            int[] numbers = new int[5];

            // Multidimensional array
            string[,] names = new string[5, 4];

            // Array-of-arrays (jagged array)
            byte[][] scores = new byte[5][];

            // Create the jagged array
            for (int i = 0; i < scores.Length; i++)
            {
                scores[i] = new byte[i + 3];
            }

            dataGridView2.DataSource = scores;
        }

        private void qLNHANSUDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        private DataSet getStaff()
        {
            string sql = @"SELECT * FROM Staff";
            // DataSet ds = new DataSet();
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "staff");
            return ds;
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("ME", "Có xóa nó không ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MetroFramework.MetroMessageBox.Show(this,"Có Xóa nóa ko ?", "Hỏi cho chắc", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 100);
            
        }
        private DataTable GetTable()
        {
            string sql = "SELECT * FROM Staff";
            SqlDataAdapter da = new SqlDataAdapter(sql, cn);
            da.Fill(dt);
            return dt;
        }
        private void button2_Click(object sender, EventArgs e)//Button them
        {
            DataRow newRow = dt.NewRow();
            //newRow["MaNV"] = txtID;
            newRow["First_Name"] = txtfname.Text;
            newRow["Last_Name"] = txtlname.Text;
            newRow["Email"] = txtEmail.Text;
            newRow["Phone"] = txtphone.Text;
            newRow["Address"] = txtdiachi.Text;
            newRow["GovernmentID"] = txtpeolpeid.Text;
            dt.Rows.Add(newRow);
            string ins = "INSERT INTO Staff(First_Name, Last_Name, Email,Phone, Address, GovernmentID) VALUES(@First_Name, @Last_name,@Email, @Phone, @Address,@GovernmentID)";
            SqlCommand cmd = new SqlCommand(ins, cn);
            //cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 4, "MaKH"); CSDL Tu Dong Tang
            cmd.Parameters.Add("@First_Name", SqlDbType.NVarChar, 30, "First_Name");
            cmd.Parameters.Add("@Last_Name", SqlDbType.NVarChar, 20, "Last_Name");
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20, "Phone");
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20, "Address");
            cmd.Parameters.Add("@GovernmentID", SqlDbType.NVarChar, 20, "GovernmentID");
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = cmd;

            da.Update(dt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void dgvnhanvien_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            
            //User selected WHOLE ROW (by clicking in the margin)
            if (dgv.SelectedRows.Count > 0)
            {
                btnreset.Visible = false;
                btnthem.Visible = false;
                btnsua.Visible = true;
                btndong.Visible = true;
                txtfname.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
                txtlname.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
                txtEmail.Text = dgv.SelectedRows[0].Cells[3].Value.ToString();
                txtphone.Text = dgv.SelectedRows[0].Cells[4].Value.ToString();
                txtdiachi.Text = dgv.SelectedRows[0].Cells[5].Value.ToString();
                txtpeolpeid.Text = dgv.SelectedRows[0].Cells[6].Value.ToString();
                

            }
                //MessageBox.Show(dgv.SelectedRows[0].Cells[0].Value.ToString());
            
        }

        private void btndong_Click(object sender, EventArgs e)
        {
            btnreset.Visible = true;
            btnthem.Visible = true;
            btnsua.Visible = false;
            btndong.Visible = false;
            txtfname.Text = "";
            txtlname.Text = "";
            txtEmail.Text = "";
            txtphone.Text = "";
            txtpeolpeid.Text = "";
            txtdiachi.Text = "";
            
        }
        
        private DataSet GetSection()
        {
            string sql = @"SELECT * FROM Section"; 
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "Section");
            return ds;
        }
        private void mtrchucvu_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader = cmd.ExecuteReader();
            List<string[]> Section = new List<string[]>();
            while (reader.Read())
            {
                string[] fields = new string[2];
                fields[0] = reader["SectionID"].ToString();
                fields[1] = reader["SecName"].ToString();
                Section.Add(fields);
            }
            // Now you have a list of arrays that you can iterate over
            foreach (string[] fields in Section)
            {
                string id = fields[0];
                string name = fields[1];
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            txtsearch.Show();
            btnsearch.Enabled = false;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //dt = new DataTable();
            string sql = "Select * From staff Where Last_Name LIKE '%'" + txtsearch.Text + '%';
            //OleDbDataAdapter da = new OleDbDataAdapter(sql, cn);
            da.Fill(dt);
            dgvnhanvien.DataSource = dt;
        }
    }
}
