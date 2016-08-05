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

namespace LapTrinhCoSoDuLieu
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void qLNHANSUDataSetBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
           // MessageBox.Show("ME", "Có xóa nó không ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            MetroFramework.MetroMessageBox.Show(this,"Có Xóa nóa ko ?", "Hỏi cho chắc", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 100);
            
        }
    }
}
