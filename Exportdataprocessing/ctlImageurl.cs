using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exportdataprocessing
{
    public partial class ctlImageurl : UserControl
    {
        public ctlImageurl()
        {
            InitializeComponent();
        }
        private string curid = "";
        internal void SetParam(string id, List<string> lstimage,List<string> lstdetail)
        {
            curid = id;
            dataGridView1.Rows.Clear();
            foreach (var item in lstimage)
            {
                dataGridView1.Rows.Add(item);
            }
            dataGridView2.Rows.Clear();
            foreach (var item in lstdetail)
            {
                dataGridView2.Rows.Add(item);
            }
        }
    }
   
}
