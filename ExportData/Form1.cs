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
            linkLabel1.Text = AppConfigContext.Instance.WorkingPath;
            this.dataGridView1.SetDoubleBuffered(true);
            this.dataGridView2.SetDoubleBuffered(true);

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
                linkLabel1.Text = AppConfigContext.Instance.WorkingPath;
            }
            catch (Exception)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                //psi.Arguments = "/e,/select," + AppConfigContext.Instance.WorkingPath+"\\";
                //System.Diagnostics.Process.Start(psi);

                System.Diagnostics.Process.Start("Explorer.exe", AppConfigContext.Instance.WorkingPath);
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (yiyilandbEntities dbcontext = new yiyilandbEntities())
            {
                //dbcontext.products
                //this.productsTableAdapter.Fill(this.yiyilandbDataSet.products);
                this.productsTableAdapter.FillBySql(this.yiyilandbDataSet.products, textBox1.Text);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“yiyilandbDataSet.products”中。您可以根据需要移动或删除它。
            //this.productsTableAdapter.Fill(this.yiyilandbDataSet.products);
        }

        private void btnsearchkeyword_Click(object sender, EventArgs e)
        {
            //var tmp = this.productsTableAdapter.GetDataBy1();
            var lst = this.productsTableAdapter.GetAllKeyWords();
            StringBuilder sb = new StringBuilder();
            foreach (var item in lst)
            {
                sb.AppendFormat("{0} , ", item);
            }
            textBox2.Text = sb.ToString();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex==colstatus.Index)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                yiyilandbDataSet.productsRow switchrow = ((DataRowView)row.DataBoundItem).Row as yiyilandbDataSet.productsRow;
                yiyilandbDataSet1.products.ImportRow(switchrow);
                yiyilandbDataSet.products.RemoveproductsRow(switchrow);
            }
        }
    }
}
