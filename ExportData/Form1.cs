using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnsetpath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog form = new FolderBrowserDialog();
                form.SelectedPath = AppConfigContext.Instance.WorkingPath;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AppConfigContext.Instance.WorkingPath = form.SelectedPath;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
