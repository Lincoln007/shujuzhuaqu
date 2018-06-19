using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace 根据sku抓取数据
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(string xmlpath)
        {
            InitializeComponent();
            txtpath.Text = Path.GetDirectoryName(xmlpath);
            btnOpen.Enabled = false;
            this.Text = string.Format("下载图片 - {0} ", Path.GetFileName(xmlpath));
            Loadxml(xmlpath);
        }
        HashSet<string> hashsetkeyword = new HashSet<string>();
        int totalcount = 0;

        private void btnOpen_Click(object sender, EventArgs e)
        {

            idcolindex = int.Parse(textBox1.Text);
            skuscolindex = int.Parse(textBox2.Text);
            imagescolindex = int.Parse(textBox3.Text);
            detailscolindex = int.Parse(textBox4.Text);
            keywordcolindex = int.Parse(textBox5.Text);


            string path = Path.Combine(Application.StartupPath, "config.ini");
            Ini ini = new Ini(path);
            ini.WriteValue("colindex", "idcolindex", idcolindex.ToString());
            ini.WriteValue("skuscolindex", "colindex", skuscolindex.ToString());
            ini.WriteValue("imagescolindex", "colindex", imagescolindex.ToString());
            ini.WriteValue("detailscolindex", "colindex", detailscolindex.ToString());
            ini.WriteValue("keywordcolindex", "colindex", keywordcolindex.ToString());
            ini.Save();
            OpenFileDialog form = new OpenFileDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var stream = form.OpenFile())
                {

                    string extension = Path.GetExtension(form.FileName);
                    if(extension==".xml")
                    {
                        Loadxml(form.FileName);
                    }
                    else
                    {
                        IExcelDataReader excelReader = null;
                        if (extension == ".xls")
                        {
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (extension == ".xlsx")
                        {
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        //excelReader.IsFirstRowAsColumnNames = true;
                        DataSet result = excelReader.AsDataSet();
                        curtable = result.Tables[0];
                        //DownImage(result.Tables[0]);
                    }
                }
            }
            form.Dispose();
        }
        DataTable curtable = null;
        private void SetStartDownload()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(SetStartDownload));
            }
            else
            {
                this.panel1.Enabled = false;
            }
        }
        private void SetEndDownload()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(SetEndDownload));
            }
            else
            {
                this.panel1.Enabled = true;
                this.Close();
            }
        }
        public void DownImage(DataTable result)
        {
            if (curtable == null)
            {
                MessageBox.Show("请导入商品");
                return;
            }
            totalcount = 0;
            int count = result.Rows.Count;
            Thread th = new Thread(() =>
            {
                SetStartDownload();
                Parallel.ForEach<DataRow>(result.AsEnumerable(), row =>
                {
                    Parse(row);
                    lock (this)
                    {
                        totalcount++;
                        this.BeginInvoke(new MethodInvoker(() =>
                        {
                            labelcount.Text = string.Format("{0}/{1}", totalcount, count);
                        }));
                    }
                });
                SetEndDownload();
            });
            th.IsBackground = true;
            th.Start();
        }

        public int idcolindex = 0;
        public int skuscolindex = 11;
        public int imagescolindex = 16;
        public int detailscolindex = 17;
        public int keywordcolindex = 18;
        private void Parse(DataRow row)
        {
            string id = row[idcolindex].ToString();
            string skus = row[skuscolindex].ToString();
            string images = row[imagescolindex].ToString();
            string details = row[detailscolindex].ToString();
            string keyword = row[keywordcolindex].ToString();

            string strroot = txtpath.Text; //Path.Combine(Application.StartupPath, "数据");
            string strpath = Path.Combine(strroot, keyword);
            string stridpath = Path.Combine(strpath, id);
            string strskupath = Path.Combine(stridpath, "sku");
            string strimagespath = Path.Combine(stridpath, "标题图片");
            string strdetailspath = Path.Combine(stridpath, "详情图片");
            var directory = Directory.CreateDirectory(strpath);
            Directory.CreateDirectory(stridpath);
            Directory.CreateDirectory(strskupath);
            Directory.CreateDirectory(strimagespath);
            Directory.CreateDirectory(strdetailspath);

            try
            {
                JArray jo = (JArray)JsonConvert.DeserializeObject(skus);
                //string str = jo["image"].ToString();
                foreach (var item in jo)
                {
                    foreach (var value in item["values"])
                    {
                        string strdesc = value["desc"].ToString();
                        string imageuri = value["image"]?.ToString();
                        if (!string.IsNullOrEmpty(imageuri))
                        {
                            try
                            {
                                //下载图片
                                //DownloadOneFileByURL(strdesc, imageuri, strskupath, 8000000);
                                DownloadOneFileByURLWithWebClient(strdesc, imageuri, strskupath);
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            } //下载sku图片

            try
            {
                JArray ja = (JArray)JsonConvert.DeserializeObject(images);
                int count = 1;
                foreach (var item in ja)
                {
                    string struri = item["image_url"].ToString();
                    if (!string.IsNullOrEmpty(struri))
                    {
                        //下载图片
                        DownloadOneFileByURLWithWebClient(count.ToString(), struri, strimagespath);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {

            }//下载商品图片
            try
            {
                string[] strarr = details.Split(new char[] { ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                int count = 1;
                foreach (var item in strarr)
                {
                    if (item.Contains("src=\""))
                    {
                        string strimage = item.Substring(5, item.Length - 6);
                        if (!string.IsNullOrEmpty(strimage))
                        {
                            //下载图片
                            DownloadOneFileByURLWithWebClient(count.ToString(), strimage, strdetailspath);
                            count++;

                        }
                    }
                }
            }
            catch (Exception)
            {

            }//下载详情
        }
        public static void DownloadOneFileByURL(string fileName, string url, string localPath, int timeout)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;
            request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            request.Timeout = timeout;//8000 Not work ?
            response = (System.Net.HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();
            BinaryReader br = new BinaryReader(s);
            //int length2 = Int32.TryParse(response.ContentLength.ToString(), out 0);
            int length2 = Int32.Parse(response.ContentLength.ToString());
            byte[] byteArr = new byte[length2];
            s.Read(byteArr, 0, length2);
            fileName = fileName.Replace("*", "乘");
            string filepath = Path.Combine(localPath, fileName + ".jpg");
            if (File.Exists(filepath)) { File.Delete(filepath); }
            if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
            FileStream fs = File.Create(filepath);
            fs.Write(byteArr, 0, length2);
            fs.Close();
            br.Close();
        }

        public static void DownloadOneFileByURLWithWebClient(string fileName, string url, string localPath)
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            idcolindex = int.Parse(textBox1.Text);
            skuscolindex = int.Parse(textBox2.Text);
            imagescolindex = int.Parse(textBox3.Text);
            detailscolindex = int.Parse(textBox4.Text);
            keywordcolindex = int.Parse(textBox5.Text);

            try
            {
                string path = Path.Combine(Application.StartupPath, "config.ini");
                if (File.Exists(path))
                {
                    Ini ini = new Ini(path);
                    textBox1.Text = ini.GetValue("idcolindex", "colindex", idcolindex.ToString());
                    textBox2.Text = ini.GetValue("skuscolindex", "colindex", skuscolindex.ToString());
                    textBox3.Text = ini.GetValue("imagescolindex", "colindex", imagescolindex.ToString());
                    textBox4.Text = ini.GetValue("detailscolindex", "colindex", detailscolindex.ToString());
                    textBox5.Text = ini.GetValue("keywordcolindex", "colindex", keywordcolindex.ToString());
                }
            }
            catch (Exception ex)
            {
            }

            if (string.IsNullOrWhiteSpace(txtpath.Text))
            {
                txtpath.Text = Path.Combine(Application.StartupPath, "数据");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog form = new FolderBrowserDialog())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    txtpath.Text = form.SelectedPath;
                }
            }
        }
        /// <summary>  
        /// 序列化DataTable  
        /// </summary>  
        /// <param name="pDt">包含数据的DataTable</param>  
        /// <returns>序列化的DataTable</returns>  
        private static void SerializeDataTableXml(string path, DataTable pDt)
        {
            // 序列化DataTable  
            XmlWriterSettings xmlsetting = new XmlWriterSettings { CheckCharacters = false };
            using (XmlWriter writer = XmlWriter.Create(path, xmlsetting))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                serializer.Serialize(writer, pDt);
            }
        }

        /// <summary>  
        /// 反序列化DataTable  
        /// </summary>  
        /// <param name="pXml">序列化的DataTable</param>  
        /// <returns>DataTable</returns>  
        public static DataTable DeserializeDataTable(string path)
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings { CheckCharacters = false };
            using (var strReader = new StreamReader(path))
            {
                XmlReader xmlReader = XmlReader.Create(strReader, xmlReaderSettings);
                XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
                DataTable dt = serializer.Deserialize(xmlReader) as DataTable;
                return dt;
            }
        }
        public void Loadxml(string xmlpath)
        {
            curtable = DeserializeDataTable(xmlpath);
            //curtable = new DataTable();
            //curtable.ReadXml(xmlpath);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            idcolindex = int.Parse(textBox1.Text);
            skuscolindex = int.Parse(textBox2.Text);
            imagescolindex = int.Parse(textBox3.Text);
            detailscolindex = int.Parse(textBox4.Text);
            keywordcolindex = int.Parse(textBox5.Text);
            if (string.IsNullOrWhiteSpace(txtpath.Text))
            {
                MessageBox.Show("请设置下载路径");
                return;
            }
            DownImage(curtable);
        }
    }
}
