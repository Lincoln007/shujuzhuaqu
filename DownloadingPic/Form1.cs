using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ScrapySharp.Extensions;
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

namespace DownloadingPic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            linkLabel1.Text = AppConfigContext.Instance.WorkingPath;
        }
        private void InitWebDriver()
        {
            if (service == null)
            {
                service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true; //隐藏 命令窗口  
                var option = new ChromeOptions();
                //option.Proxy = proxy;

                //option.AddArgument("disable-infobars"); //隐藏 自动化标题  
                //option.AddArgument("headless"); //隐藏 chorme浏览器  
                option.AddArgument("--incognito");//隐身模式  
                driver = new OpenQA.Selenium.Chrome.ChromeDriver(service, option, TimeSpan.FromSeconds(40));
                wait = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
            }
        }
        private void UninitWebDriver()
        {
            if(service!=null)
            {
                driver.Close();
                driver.Dispose();
                service.Dispose();
                service = null;
            }
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
        public void DownloadOneFileByURLWithWebClient(string fileName, string url, string localPath)
        {
            try
            {
                string strurl = "https:" + url;
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    fileName = fileName.Replace("*", "乘");
                    string filepath = Path.Combine(localPath, fileName + ".jpg");
                    if (File.Exists(filepath)) { File.Delete(filepath); }
                    if (Directory.Exists(localPath) == false) { Directory.CreateDirectory(localPath); }
                    wc.DownloadFile(strurl, filepath);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void btndownload_Click(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            InitWebDriver();
            await 根据网址抓取商品(textBox1.Text);
            UninitWebDriver();
            panel2.Enabled = true;
        }
        ChromeDriverService service = null;
        IWebDriver driver = null;
        WebDriverWait wait = null;
        private void LoadProductUri(string uri)
        {
            driver.Navigate().GoToUrl(uri);

            //driver.Navigate().GoToUrl(string.Format("https://{0}", uri));
            //将页面滚动条拖到底部
            for (int i = 0; i < 10; i++)
            {
                int row = (i + 1) * 2000;
                ((IJavaScriptExecutor)driver).ExecuteScript(string.Format("window.scrollTo(500,{0});", row));
                Thread.Sleep(100);
            }
        }
        private async Task<bool> 根据网址抓取商品(string uri)
        {
            var task = Task.Run<bool>(
                 () =>
                 {
                     bool rtn = false;
                     //解析淘宝商品搜索页
                     //webBrowser2.Load(item);
                     //webBrowser2.Update();
                     //httpitem.URL = item;
                     //var html = httphelper.GetHtml(httpitem);
                     //driver.Url = item;
                     LoadProductUri(uri);
                     if (uri.Contains("taobao"))
                     {
                         rtn = 获取连接(uri);
                     }
                     else if (uri.Contains("tmall"))
                     {
                         rtn = 获取天猫连接(uri);
                     }
                     return rtn;
                 });
            await task;
            return task.Result;
        }
        void SetText(string path)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    SetText(path);
                }));
            }
            else
            {
                linkLabel2.Text = path;
            }
        }
        IWebElement FindElement(IWebDriver _driver, string xpath)
        {
            try
            {
                return _driver.FindElement(By.XPath(xpath));
            }
            catch (Exception)
            {
            }
            return null;
        }

        private bool 获取连接(string uri)
        {
            bool rtn = false;
            var arr = uri.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
            string id = "";
            id = GetIdByUri(arr);
            rtn = true;
            try
            {
                string strsource = driver.PageSource;
                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(strsource);

                var html = htmlDocument.DocumentNode;
                //标题
                var title = html.SelectSingleNode("//*[@id='J_Title']/h3");
                string strtitle = title.GetAttributeValue("data-title", "");

                //累计评论
                var comment = html.SelectSingleNode("//*[@id='J_RateCounter']").InnerText;
                Console.WriteLine("累计评论 {0}", comment);
                //csv.WriteField(comment);

                var 标题图片 = html.SelectNodes("//*[@id='J_UlThumb']/li/div/a/img");
                List<string> lst标题图片 = new List<string>();
                StringBuilder sb = new StringBuilder();
                foreach (var item in 标题图片)
                {
                    //Console.WriteLine(item.InnerText);
                    lst标题图片.Add(item.GetAttributeValue("src"));
                    //sb.AppendLine(item.GetAttributeValue("src", ""));
                }
                //csv.WriteField(sb.ToString().TrimEnd());
                var 快递 = html.SelectSingleNode("//*[@id='J_WlServiceTitle']").InnerText;
                Console.WriteLine(快递);
                //csv.WriteField(快递);

                List<Tuple<string, string, string>> lst分类 = new List<Tuple<string, string, string>>();
                sb.Clear();
                //var sku_elements = driver.FindElements(By.CssSelector("#J_isku > div.tb-skin > dl.J_Prop tb-prop tb-clear  J_Prop_Color > dd > ui.J_TSaleProp tb-img tb-clearfix > li"));
                var 分类 = html.SelectNodes("//*[@id='J_isku']/div/dl[1]/dd/ul/li/a");
                if (分类 != null)
                {
                    foreach (var item in 分类)
                    {
                        //item.XPath
                        //var skus= wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#J_isku > div.tb-skin > dl.J_Prop tb-prop tb-clear  J_Prop_Color > dd > ui.J_TSaleProp tb-img tb-clearfix > li")); });
                        var pic = item.GetAttributeValue("style");
                        if (!string.IsNullOrEmpty(pic))
                        {
                            var tmparr = pic.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tmparr.Length >= 2)
                            {
                                pic = tmparr[1];
                            }
                        }
                        var sku = item.SelectSingleNode("./span").InnerText;
                        var sku_element = driver.FindElement(By.XPath(item.XPath));
                        string price = "";
                        if (sku_element != null)
                        {
                            sku_element.Click();
                            Thread.Sleep(100);
                            var priceele = FindElement(driver, "//*[@id='J_PromoPriceNum']");
                            if (priceele == null)
                            {
                                priceele = FindElement(driver, "//*[@id='J_StrPrice']/em[2]");
                            }
                            if (priceele != null)
                            {
                                price = priceele.Text;
                            }
                        }
                        lst分类.Add(new Tuple<string, string, string>(price, sku, pic));
                        //sb.AppendLine(string.Format("{0}|{1}|{2}", price, sku, pic));

                    }
                }
                var 详情 = html.SelectNodes("//*[@id='J_DivItemDesc']/p/img");
                List<string> lst详情 = new List<string>();
                if (详情 == null)
                {
                    详情 = html.SelectNodes("//*[@id='J_DivItemDesc']/div/div/img");
                }
                if (详情 != null)
                {
                    //var 图片 = html.SelectNodes("//*[@id='J_DivItemDesc']/p/strong/img");
                    sb.Clear();
                    //*[@id="J_DivItemDesc"]/p[2]/img[1]
                    //*[@id="J_DivItemDesc"]/p[5]/strong/img[1]
                    foreach (var item in 详情)
                    {
                        var pics = item.CssSelect("img");
                        foreach (var pic in pics)
                        {
                            var tp = pic.GetAttributeValue("src");
                            Console.WriteLine(tp);
                            sb.AppendLine(tp);
                            lst详情.Add(tp);
                        }
                    }
                }
                //csv.WriteField(sb.ToString().TrimEnd());
                rtn = WriteCsv(id, uri, strtitle, comment, lst标题图片, 快递, lst分类, lst详情);
            }
            catch (Exception)
            {
                rtn = false;
            }
            return rtn;
        }
        private bool WriteCsv(string id, string uri, string title, string commentnum, List<string> 所有标题图片, string 快递情况, List<Tuple<string, string, string>> price_sku_tupians, List<string> 详情)
        {
            string path = Path.Combine(AppConfigContext.Instance.WorkingPath, id);
            if (Directory.Exists(path) == false) { Directory.CreateDirectory(path); }
            string csvpath = Path.Combine(path, "product.csv");
            using (FileStream fs = new FileStream(csvpath, FileMode.Create))
            using (TextWriter writer = new StreamWriter(fs, Encoding.GetEncoding("GB2312")))
            using (var csv = new CsvWriter(writer))
            {
                try
                {
                    SetText(path);
                    csv.WriteField(id);
                    csv.WriteField(uri);
                    csv.WriteField(title);
                    csv.WriteField(commentnum);

                    StringBuilder sb = new StringBuilder();
                    int count = 0;
                    foreach (var item in 所有标题图片)
                    {
                        string strpath = Path.Combine(path, "标题图片");
                        count++;
                        sb.AppendLine(item);
                        string struri = item;
                        int index = struri.IndexOf(".jpg");
                        if (index > 0)
                        {
                            struri = struri.Substring(0, index + 4);
                        }
                        DownloadOneFileByURLWithWebClient(count.ToString(), struri, strpath);
                    }
                    csv.WriteField(sb.ToString());
                    csv.WriteField(快递情况);
                    sb.Clear();
                    foreach (var item in price_sku_tupians)
                    {
                        string strpath = Path.Combine(path, "sku");
                        string struri = item.Item3;
                        int index = struri.IndexOf(".jpg");
                        if (index>0)
                        {
                            struri = struri.Substring(0, index + 4);
                        }
                        DownloadOneFileByURLWithWebClient(item.Item2, struri, strpath);
                        sb.AppendLine(string.Format("{0}|{1}|{2}", item.Item1, item.Item2, item.Item3));
                    }
                    csv.WriteField(sb.ToString());
                    sb.Clear();
                    count = 0;
                    foreach (var item in 详情)
                    {
                        string strpath = Path.Combine(path, "详情");
                        count++;
                        sb.AppendLine(item);
                        DownloadOneFileByURLWithWebClient(count.ToString(), item, strpath);
                    }
                    csv.WriteField(sb.ToString());
                    csv.NextRecord();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        private bool 获取天猫连接(string uri)
        {
            bool rtn = false;
            var arr = uri.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
            string id = "";
            id = GetIdByUri(arr);

            rtn = true;
            try
            {
                string strsource = driver.PageSource;
                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(strsource);
                var html = htmlDocument.DocumentNode;
                //标题
                var title = html.SelectSingleNode("//*[@id='J_DetailMeta']/div[1]/div[1]/div/div[1]/h1");
                string strtitle = title?.InnerText?.Trim();
                //csv.WriteField(title.InnerText.Trim());
                //string strtitle = title.GetAttributeValue("data-title");
                //csv.WriteField(strtitle);
                //累计评论
                var comment = html.SelectSingleNode("//*[@id='J_ItemRates']/div/span[2]").InnerText;
                Console.WriteLine("累计评论 {0}", comment);
                //csv.WriteField(comment);
                //*[@id="J_UlThumb"]/li[2]/a/img
                var 标题图片 = html.SelectNodes("//*[@id='J_UlThumb']/li/a/img");
                List<string> lst标题图片 = new List<string>();
                StringBuilder sb = new StringBuilder();
                foreach (var item in 标题图片)
                {
                    Console.WriteLine(item.InnerText);
                    lst标题图片.Add(item.GetAttributeValue("src"));
                    //sb.AppendLine(item.GetAttributeValue("src"));
                }
                //csv.WriteField(sb.ToString().TrimEnd());
                var 快递 = html.SelectSingleNode("//*[@id='J_PostageToggleCont']/p").InnerText;
                //Console.WriteLine(快递);
                //csv.WriteField(快递);

                var 分类 = html.SelectNodes("//*[@id='J_DetailMeta']/div[1]/div[1]/div/div[4]/div/div/dl[1]/dd/ul/li");
                List<Tuple<string, string, string>> lst分类 = new List<Tuple<string, string, string>>();
                sb.Clear();
                foreach (var item in 分类)
                {
                    var sku = item.GetAttributeValue("title");
                    var tupian = item.SelectSingleNode("./a").GetAttributeValue("style");
                    if (!string.IsNullOrEmpty(tupian))
                    {
                        var tmparr = tupian.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                        if (tmparr.Length >= 2)
                        {
                            tupian = tmparr[1];
                        }

                    }
                    var sku_element = driver.FindElement(By.XPath(item.XPath));
                    string price = "";
                    if (sku_element != null)
                    {
                        try
                        {

                            sku_element.Click();
                            Thread.Sleep(100);
                            var priceele = FindElement(driver, "//*[@id='J_PromoPrice']/dd/div/span");
                            if (priceele == null)
                            {
                                priceele = FindElement(driver, "//*[@id='J_StrPrice']/em[2]");
                            }
                            if (priceele != null)
                            {
                                price = priceele.Text;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    lst分类.Add(new Tuple<string, string, string>(price, sku, tupian));
                    //sku = string.Format("{0} | {1}", sku, tupian);

                    //Console.WriteLine(sku);
                    //sb.AppendLine(sku);
                }
                //csv.WriteField(sb.ToString().TrimEnd());

                //*[@id="description"]/div/p/img[1]
                //*[@id="description"]/div/img[3]
                var 详情 = html.SelectNodes("//*[@id='description']/div/p/img");
                if (详情 == null)
                {
                    详情 = html.SelectNodes("//*[@id='description']/div/img");
                }
                //var 图片 = html.SelectNodes("//*[@id='J_DivItemDesc']/p/strong/img");
                sb.Clear();
                //*[@id="J_DivItemDesc"]/p[2]/img[1]
                //*[@id="J_DivItemDesc"]/p[5]/strong/img[1]
                List<string> lst详情 = new List<string>();
                foreach (var item in 详情)
                {
                    var tp = item.GetAttributeValue("src");
                    Console.WriteLine(tp);
                    sb.AppendLine(tp);
                    lst详情.Add(tp);
                }
                //csv.WriteField(sb.ToString().TrimEnd());
                //csv.NextRecord();
                rtn = WriteCsv(id, uri, strtitle, comment, lst标题图片, 快递, lst分类, lst详情);
            }
            catch (Exception ex)
            {
                rtn = false;
            }

            return rtn;
        }
        private string GetIdByUri(string[] arr)
        {
            string id = "";
            foreach (var item in arr)
            {
                if (item.IndexOf("id=") == 0)
                {
                    id = item.Substring(3);
                }
            }
            return id;
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                //psi.Arguments = "/e,/select," + AppConfigContext.Instance.WorkingPath+"\\";
                //System.Diagnostics.Process.Start(psi);
                System.Diagnostics.Process.Start("Explorer.exe", linkLabel2.Text);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
