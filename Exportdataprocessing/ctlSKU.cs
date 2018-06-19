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
    public partial class ctlSKU : UserControl
    {
        public ctlSKU()
        {
            InitializeComponent();
        }
        private string curid = "";
        internal void SetParam(string id, List<SKUInfo> lstinfo)
        {
            curid = id;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lstinfo;
        }
    }
    internal class SKUInfo
    {
        public SKUInfo(string skuname, string url)
        {
            this.skuname = skuname;
            strUrl = url;
        }
        private string skuname = "";
        private string strUrl;

        public string Paramname { get => skuname; set => skuname = value; }
        public string Value { get => strUrl; set => this.strUrl = value; }
    }
}
