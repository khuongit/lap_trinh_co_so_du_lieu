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
            GetTable();
            //
            

            DataTable temp = new DataTable();
            temp = GetSectionTable();
            drlpb.DataSource = temp;
            drlpb.DisplayMember = "Name";
            drlpb.ValueMember = "SectionID";
            
            //GetRegencyTable
            DataTable Regency_temp = new DataTable();
            Regency_temp = GetRegencyTable();
            drlcv.DataSource = Regency_temp;
            drlcv.DisplayMember = "Name";
            drlcv.ValueMember = "RegencyID";

        }

        private void qLNHANSUDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        private DataSet getStaff()
        {
            string sql = @"SELECT StaffID AS Mã_NV, CONCAT (First_Name ,' ', Last_Name) AS Tên_Đầy_Đủ, Email , Phone AS Số_Điện_Thoại, Address AS Địa_Chỉ ,GovernmentID AS CMND , Regency.Name AS Chức_Vụ, Section.Name AS Bộ_Phận
                            FROM Staff
                            LEFT JOIN Section ON (Staff.SectionID = Section.SectionID)
                            LEFT JOIN Regency ON (Staff.RegencyID = Regency.RegencyID)";
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
            newRow["SectionID"] = int.Parse(drlpb.SelectedValue.ToString());
            newRow["RegencyID"] = int.Parse(drlcv.SelectedValue.ToString());
            dt.Rows.Add(newRow);
            string ins = "INSERT INTO Staff(First_Name, Last_Name, Email,Phone, Address, GovernmentID, SectionID, RegencyID) VALUES(@First_Name, @Last_name,@Email, @Phone, @Address,@GovernmentID, @SectionID, @RegencyID)";
            SqlCommand cmd = new SqlCommand(ins, cn);
            //cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 4, "MaKH"); CSDL Tu Dong Tang
            cmd.Parameters.Add("@First_Name", SqlDbType.NVarChar, 30, "First_Name");
            cmd.Parameters.Add("@Last_Name", SqlDbType.NVarChar, 20, "Last_Name");
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 20, "Phone");
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 20, "Address");
            cmd.Parameters.Add("@GovernmentID", SqlDbType.NVarChar, 20, "GovernmentID");
            cmd.Parameters.Add("@SectionID", SqlDbType.Int, 4, "SectionID");
            cmd.Parameters.Add("@RegencyID", SqlDbType.Int, 4, "RegencyID");
            SqlDataAdapter da = new SqlDataAdapter();
            da.InsertCommand = cmd;

            da.Update(dt);
            clear_txt();
            try
            {
                MetroFramework.MetroMessageBox.Show(this, "Bạn đã thêm mới thành công một nhân viên", "Thông Báo Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information, 100);
                ClearDataSet(ds);
                clear_drv();
                //dgvnhanvien.DataSource = GetTable();
                dgvnhanvien.DataSource = getStaff().Tables["staff"];
            }
            catch
            {

            }
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
                int id = int.Parse(dgv.SelectedRows[0].Cells[0].Value.ToString());
                //
                /*txtfname.Text = dgv.SelectedRows[0].Cells[1].Value.ToString();
                txtlname.Text = dgv.SelectedRows[0].Cells[2].Value.ToString();
                txtEmail.Text = dgv.SelectedRows[0].Cells[3].Value.ToString();
                txtphone.Text = dgv.SelectedRows[0].Cells[4].Value.ToString();
                txtdiachi.Text = dgv.SelectedRows[0].Cells[5].Value.ToString();
                txtpeolpeid.Text = dgv.SelectedRows[0].Cells[6].Value.ToString();
                txtDateOfBirth.Text = dgv.SelectedRows[0].Cells[7].Value.ToString();*/
                ////getoneStaff
                DataTable onestaff = getoneStaff(id).Tables["onestaff"];
                if(onestaff.Rows.Count >0 )
                {
                    
                    foreach (DataRow dtRow in onestaff.Rows)
                    {
                        txtfname.Text = dtRow["First_Name"].ToString();
                        txtlname.Text = dtRow["Last_Name"].ToString();
                        txtEmail.Text = dtRow["Email"].ToString();
                        txtphone.Text = dtRow["Phone"].ToString();
                        txtdiachi.Text = dtRow["Address"].ToString();
                        txtpeolpeid.Text = dtRow["GovernmentID"].ToString();
                        //txtDateOfBirth.Text = dtRow["DateOfBirth"].ToString();
                        drlcv.SelectedValue = dtRow["RegencyID"].ToString();
                        drlpb.SelectedValue = dtRow["SectionID"].ToString();
                        string day = dtRow["DateOfBirth"].ToString() != "" ? dtRow["DateOfBirth"].ToString() : DateTime.Today.ToString();
                        // dayofbirth.Value = new DateTime(day);
                        dayofbirth.Value = DateTime.Parse(day);
                        //MessageBox.Show(day);
                    }
                }


            }
            //MessageBox.Show(dgv.SelectedRows[0].Cells[0].Value.ToString());

        }

        private void btndong_Click(object sender, EventArgs e)
        {
            btnreset.Visible = true;
            btnthem.Visible = true;
            btnsua.Visible = false;
            btndong.Visible = false;

            clear_txt();
        }
        private void clear_txt()
        {
            txtfname.Text = "";
            txtlname.Text = "";
            txtEmail.Text = "";
            txtphone.Text = "";
            txtpeolpeid.Text = "";
            txtdiachi.Text = "";
            //txtDateOfBirth.Text = "";
        }
        private DataSet GetSection()
        {
            string sql = @"SELECT * FROM Section"; 
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "Section");
            return ds;
        }
        private DataTable GetSectionTable()
        {
            DataTable sec = new DataTable();
            string sql = @"SELECT SectionID,Name FROM Section";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(sec);
            return sec;
        }
        private DataTable GetRegencyTable()
        {
            DataTable reg = new DataTable();
            string sql = @"SELECT RegencyID,Name FROM Regency";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(reg);
            return reg;
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
            //btnsearch.Enabled = false;
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private DataSet SearchStaff(string key)
        {
            ClearDataSet(ds);
            string sql = @"Select StaffID AS Mã_NV, CONCAT (First_Name ,' ', Last_Name) AS Tên_Đầy_Đủ, Email , Phone AS Số_Điện_Thoại, Address AS Địa_Chỉ ,GovernmentID AS CMND , Regency.Name AS Chức_Vụ, Section.Name AS Bộ_Phận 
                                From staff 
                                LEFT JOIN Section ON (Staff.SectionID = Section.SectionID)
                                LEFT JOIN Regency ON (Staff.RegencyID = Regency.RegencyID) 
                                Where Last_Name LIKE N'%" + @key + "%'";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "staffsearch");
            return ds;
        }
        
        private DataSet getoneStaff(int id)
        {
            //ClearDataSet(ds);
            string sql = @"SELECT *
                             FROM Staff
                             LEFT JOIN Section ON (Staff.SectionID = Section.SectionID)
                             LEFT JOIN Regency ON (Staff.RegencyID = Regency.RegencyID)
                             Where StaffID = " + id + "";
            da = new SqlDataAdapter(sql, cn);
            da.Fill(ds, "onestaff");
            return ds;
        }
        private void txtsearch_KeyUp(object sender, KeyEventArgs e)
        {

            clear_drv();
            dgvnhanvien.DataSource = null;
            dgvnhanvien.DataSource = SearchStaff(txtsearch.Text).Tables["staffsearch"];
        }
        private void clear_drv()
        {
            try
            {
                this.dgvnhanvien.DataSource = null;
                this.dgvnhanvien.Rows.Clear();
            }
            catch
            {

            }
            finally
            {
                while (this.dgvnhanvien.Rows.Count > 0)
                {
                    this.dgvnhanvien.Rows.RemoveAt(0);
                }
            }
        }
        private void ClearDataSet(DataSet dataSet)
        {
            // To test, print the number rows in each table.
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine(table.TableName + "Rows.Count = "
                    + table.Rows.Count.ToString());
            }
            // Clear all rows of each table.
            dataSet.Clear();

            // Print the number of rows again.
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine(table.TableName + "Rows.Count = "
                    + table.Rows.Count.ToString());
            }
        }
        
    }

}
