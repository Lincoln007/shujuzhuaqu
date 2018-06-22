using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exportdataprocessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvProduct.SetDoubleBuffered(true);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            textBox2.Text = Guid.NewGuid().ToString();
            string path = Path.Combine(Application.StartupPath, "config.ini");
            if (File.Exists(path))
            {
                Ini ini = new Ini(path);
                txtid.Text = ini.GetValue("id", "colindex", "0");
                txttitle.Text = ini.GetValue("title", "colindex", "1");
                txtdescription.Text = ini.GetValue("description", "colindex", "2");
                txtparams.Text = ini.GetValue("params", "colindex", "3");
                txtcurrent_price.Text = ini.GetValue("current_price", "colindex", "4");
                txtoriginal_price.Text = ini.GetValue("original_price", "colindex", "5");
                txtmonth_sales_count.Text = ini.GetValue("month_sales_count", "colindex", "6");
                txtstock.Text = ini.GetValue("stock", "colindex", "7");
                txtsku.Text = ini.GetValue("sku", "colindex", "8");
                txtshipping_address.Text = ini.GetValue("shipping_address", "colindex", "9");
                txtshop_id.Text = ini.GetValue("shop_id", "colindex", "10");
                txtshop_name.Text = ini.GetValue("shop_name", "colindex", "11");
                txtcategory_id.Text = ini.GetValue("category_id", "colindex", "12");
                txtkeyword.Text = ini.GetValue("keyword", "colindex", "13");
                txtcomments_count.Text = ini.GetValue("comments_count", "colindex", "14");
                txturl.Text = ini.GetValue("url", "colindex", "15");
                txtimages.Text = ini.GetValue("images", "colindex", "16");
                txtdetail.Text = ini.GetValue("detail", "colindex", "17");
                txtstores_count.Text = ini.GetValue("stores_count", "colindex", "18");
                txtscore.Text = ini.GetValue("score", "colindex", "19");
            }
        }

        private bool SetColindex()
        {
            try
            {
                parserow.id = int.Parse(txtid.Text);
                parserow.title = int.Parse(txttitle.Text);
                parserow.description = int.Parse(txtdescription.Text);
                parserow.paramindex = int.Parse(txtparams.Text);
                parserow.current_price = int.Parse(txtcurrent_price.Text);
                parserow.original_price = int.Parse(txtoriginal_price.Text);
                parserow.month_sales_count = int.Parse(txtmonth_sales_count.Text);
                parserow.stock = int.Parse(txtstock.Text);
                parserow.sku = int.Parse(txtsku.Text);
                parserow.shipping_address = int.Parse(txtshipping_address.Text);
                parserow.shop_id = int.Parse(txtshop_id.Text);
                parserow.shop_name = int.Parse(txtshop_name.Text);
                parserow.category_id = int.Parse(txtcategory_id.Text);
                parserow.keyword = int.Parse(txtkeyword.Text);
                parserow.comments_count = int.Parse(txtcomments_count.Text);
                parserow.url = int.Parse(txturl.Text);
                parserow.images = int.Parse(txtimages.Text);
                parserow.detail = int.Parse(txtdetail.Text);
                parserow.stores_count = int.Parse(txtstores_count.Text);
                parserow.score = int.Parse(txtscore.Text);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        ReadData m_readdata = new ReadData();
        private async void btnimport_Click(object sender, EventArgs e)
        {
            OpenFileDialog form = new OpenFileDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                bool rtn = await m_readdata.ReadExcelAsync(form.FileName);
                if (rtn)
                {
                    dgvProduct.DataSource = m_readdata.ProductData;
                }
                else
                {
                    MessageBox.Show("加载失败");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetColindex();
            Ini ini = new Ini(Path.Combine(Application.StartupPath, "config.ini"));
            ini.WriteValue("id", "colindex", txtid.Text);
            ini.WriteValue("title", "colindex", txttitle.Text);
            ini.WriteValue("description", "colindex", txtdescription.Text);
            ini.WriteValue("params", "colindex", txtparams.Text);
            ini.WriteValue("current_price", "colindex", txtcurrent_price.Text);
            ini.WriteValue("original_price", "colindex", txtoriginal_price.Text);
            ini.WriteValue("month_sales_count", "colindex", txtmonth_sales_count.Text);
            ini.WriteValue("stock", "colindex", txtstock.Text);
            ini.WriteValue("sku", "colindex", txtsku.Text);
            ini.WriteValue("shipping_address", "colindex", txtshipping_address.Text);
            ini.WriteValue("shop_id", "colindex", txtshop_id.Text);
            ini.WriteValue("shop_name", "colindex", txtshop_name.Text);
            ini.WriteValue("category_id", "colindex", txtcategory_id.Text);
            ini.WriteValue("keyword", "colindex", txtkeyword.Text);
            ini.WriteValue("comments_count", "colindex", txtcomments_count.Text);
            ini.WriteValue("url", "colindex", txturl.Text);
            ini.WriteValue("images", "colindex", txtimages.Text);
            ini.WriteValue("detail", "colindex", txtdetail.Text);
            ini.WriteValue("stores_count", "colindex", txtstores_count.Text);
            ini.WriteValue("score", "colindex", txtscore.Text);
            ini.Save();
        }

        private void dgvProduct_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if(!SetColindex())
                {
                    MessageBox.Show("请输入正确的列号");
                }
                DataGridViewRow row = dgvProduct.CurrentRow;
                DataRow datarow = ((DataRowView)row.DataBoundItem).Row;
                string id = datarow[parserow.id].ToString();
                var lst = parserow.ParamParamInfo(datarow);
                ctlParam1.SetParam(id, lst);

                ctlSKU1.SetParam(id, parserow.ParamSkuInfo(datarow));

                List<string> lstimages;
                List<string> lstdetails;
                parserow.ParamImage(datarow, out lstimages, out lstdetails);
                ctlImageurl1.SetParam(id, lstimages, lstdetails);

            }
            catch (Exception ex)
            {
            }

        }

        private void UpdateProcess(int curindex,int total)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    UpdateProcess(curindex, total);
                }));
            }
            else
            {
                this.Text = string.Format("上传数据 {0}/{1}", curindex, total);
            }
        }
        private async void btnupdate_Click(object sender, EventArgs e)
        {
            if (m_readdata.ProductData==null)
            {
                MessageBox.Show("请先导入数据");
                return;
            }
            panel1.Enabled = false;
            ImportDbContext dbContext = new ImportDbContext();
            await dbContext.UpdateFromDataTable(m_readdata.ProductData, UpdateProcess,textBox1.Text,textBox2.Text);
            panel1.Enabled= true;
        }
    }
}
