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
    public partial class ctlParam : UserControl
    {
        public ctlParam()
        {
            InitializeComponent();
        }
        private string curid = "";
        internal void SetParam(string id, List<ParamInfo> lstinfo)
        {
            curid = id;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lstinfo;
        }
    }
    internal class ParamInfo
    {
        public ParamInfo(string name,string _value)
        {
            Paramname = name;
            Value = _value;
        }
        private string paramname = "";
        private string value;

        public string Paramname { get => paramname; set => paramname = value; }
        public string Value { get => value; set => this.value = value; }
    }
}
