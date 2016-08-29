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
    }
}
