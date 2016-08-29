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

        private void button2_Click(object sender, EventArgs e)
        {

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
        }
    }
}
