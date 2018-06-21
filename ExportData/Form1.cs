using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
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
            if (e.ColumnIndex == colstatus.Index)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                yiyilandbDataSet.productsRow switchrow = ((DataRowView)row.DataBoundItem).Row as yiyilandbDataSet.productsRow;
                yiyilandbDataSet1.products.ImportRow(switchrow);
                yiyilandbDataSet.products.RemoveproductsRow(switchrow);
            }
        }
        private bool isallselected = false;
        private bool Isallselected
        {
            get
            {
                return isallselected;
            }
            set
            {
                isallselected = value;
                button1.Text = isallselected ? "全不选" : "全选";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            isallselected = !isallselected;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Cells[0].Value = isallselected;
            }
        }

        HashSet<string> alreaddownload = new HashSet<string>();
        HashSet<string> downloading = new HashSet<string>();
        /// <summary>
        /// 下载选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button3_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {
                    lst.Add(row.Cells[1].Value.ToString());
                }
            }
            lst.Except(alreaddownload);
            lst.Except(downloading);
            foreach (var item in lst)
            {
                downloading.Add(item.Trim());
            }
            foreach (var item in lst)
            {
                string id = item.Trim();
                bool rtn= await DownLoadProduct(id);
                if(rtn)
                {
                    alreaddownload.Add(id);
                }
                downloading.Remove(id);
            }

        }
        private async Task<bool> DownLoadProduct(string id)
        {
            var t = Task.Run(() =>
             {
                 //先查询产品信息
                 using (yiyilandbEntities dbcontext = new yiyilandbEntities())
                 {
                     try
                     {
                         {
                             //下载商品标题图片和详情图片
                             var arrs = from imagedetail in dbcontext.imagedetail
                                        where id == imagedetail.id
                                        select imagedetail;
                             int count = 0;
                             foreach (var item in arrs)
                             {
                                 count = 0;
                                 string strimages = item.images;
                                 if (!string.IsNullOrEmpty(strimages))
                                 {
                                     var images = strimages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                     string path = Path.Combine(AppConfigContext.Instance.WorkingPath, id, "images");
                                     foreach (var uri in images)
                                     {
                                         count++;
                                         DownloadOneFileByURLWithWebClient(count.ToString(), uri, path);
                                     }
                                 }
                                 count = 0;
                                 string strdetails = item.details;
                                 if (!string.IsNullOrEmpty(strdetails))
                                 {
                                     var details = strdetails.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                                     string path = Path.Combine(AppConfigContext.Instance.WorkingPath, id, "details");
                                     foreach (var uri in details)
                                     {
                                         count++;
                                         DownloadOneFileByURLWithWebClient(count.ToString(), uri, path);
                                     }
                                 }
                             }
                         }

                         {
                             //下载sku图片
                             var arrs = from sku in dbcontext.productskus
                                        where sku.id == id
                                        select sku;
                             string path = Path.Combine(AppConfigContext.Instance.WorkingPath, id, "skus");
                             foreach (var item in arrs)
                             {
                                 if (!string.IsNullOrEmpty(item.skname))
                                 {
                                     DownloadOneFileByURLWithWebClient(item.skname, item.imageurl, path);
                                 }
                             }
                         }
                         return true;
                     }
                     catch (Exception ex)
                     {
                         return false;
                     }
                 }
             });
            await t;
            return t.Result;
        }
        public void DownloadOneFileByURLWithWebClient(string fileName, string url, string localPath)
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    fileName = fileName.Replace("*", "乘");
                    string filepath = Path.Combine(localPath, fileName + ".jpg");
                    if (File.Exists(filepath)) { File.Delete(filepath); }
                    if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
                    wc.DownloadFile(url, filepath);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
