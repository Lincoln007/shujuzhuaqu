using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using CsvHelper;
using System.Threading;
using SufeiUtil;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ScrapySharp.Extensions;

namespace 数据抓取
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            panel2.Controls.Add(webBrowser1);
            webBrowser1.Dock = DockStyle.Fill;

            InitBrowser();

        }
        ChromiumWebBrowser webBrowser1 = new ChromiumWebBrowser("");

        public void InitBrowser()
        {
            try
            {
                BrowserSettings bset = new BrowserSettings();
                bset.Plugins = CefState.Enabled;
                //关于跨域限制
                bset.WebSecurity = CefState.Disabled;
                webBrowser1.LifeSpanHandler = new OpenPageSelf(this);
                webBrowser1.BrowserSettings = bset;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                string url_tmp = this.textBox1.Text;
                if (url_tmp != "")
                {
                    //this.Controls.Add(webBrowser1);
                    //webBrowser1.Navigate(url_tmp);
                    webBrowser1.Load(url_tmp);
                    //this.Controls.Add(webBrowser1);
                    //webBrowser1.Dock = DockStyle.Fill;
                    webBrowser1.Update();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void SetURL(string url)
        {
            textBox1.Text = url;
        }
        public static string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.Default);//注意编码
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        private async void BtnTest_Click(object sender, EventArgs e)
        {
            InitWebDriver();
            根据网址抓取商品("C:\\Users\\jiayp\\Desktop\\测试下载链接.csv", new List<string> { "//item.taobao.com/item.htm?id=549540454057&amp;ns=1&amp;abbucket=11#detail" });
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
        private void 获取连接(string uri, CsvWriter csv)
        {
            var arr = uri.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
            string id = "";
            id = GetIdByUri(arr);
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

            List<string> lst分类 = new List<string>();
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
                    lst分类.Add(string.Format("{0}|{1}|{2}", price, sku, pic));
                    //sb.AppendLine(string.Format("{0}|{1}|{2}", price, sku, pic));

                }
            }
            //csv.WriteField(sb.ToString().TrimEnd());

            //csv.WriteField(sb.ToString());
            //var sku_elements = driver.FindElements(By.XPath("//*[@id='J_isku']/div/dl[1]/dd/ul/li/a"));
            //foreach (var item in sku_elements)
            //{
            //    item.Click();
            //    string txt = "";
            //    string price = "";
            //    string picurl = "";
            //    var txtele = item.FindElement(By.XPath("./span"));
            //    if (txtele != null)
            //    {
            //        txt = txtele.Text;
            //        picurl = item.GetAttribute("style");
            //    }
            //    var priceele = driver.FindElement(By.XPath("//*[@id='J_PromoPriceNum']"));
            //    if (priceele != null)
            //    {
            //        price = priceele.Text;
            //    }
            //    Thread.Sleep(100);
            //}


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
            WriteCsv(csv, id, uri, strtitle, comment, lst标题图片, 快递, lst分类, lst详情);
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

        private void WriteCsv(CsvWriter csv, string id, string uri, string title, string commentnum, List<string> 所有标题图片, string 快递情况, List<string> price_sku_tupians, List<string> 详情)
        {
            csv.WriteField(id);
            csv.WriteField(uri);
            csv.WriteField(title);
            csv.WriteField(commentnum);
            StringBuilder sb = new StringBuilder();
            foreach (var item in 所有标题图片)
            {
                sb.AppendLine(item);
            }
            csv.WriteField(sb.ToString());
            csv.WriteField(快递情况);
            sb.Clear();
            foreach (var item in price_sku_tupians)
            {
                sb.AppendLine(item);
            }
            csv.WriteField(sb.ToString());
            sb.Clear();
            foreach (var item in 详情)
            {
                sb.AppendLine(item);
            }
            csv.WriteField(sb.ToString());
            csv.NextRecord();

        }
        private void 获取天猫连接(string uri, CsvWriter csv)
        {
            var arr = uri.Split(new char[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries);
            string id = "";
            id = GetIdByUri(arr);
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
            List<string> lst分类 = new List<string>();
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
                lst分类.Add(string.Format("{0}|{1}|{2}", price, sku, tupian));
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
            WriteCsv(csv, id, uri, strtitle, comment, lst标题图片, 快递, lst分类, lst详情);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        ChromeDriverService service = null;
        IWebDriver driver = null;
        WebDriverWait wait = null;
        private void Btngetall_Click(object sender, EventArgs e)
        {
            string keyword = textBox2.Text;
            if (string.IsNullOrEmpty(keyword)) return;
            SaveFileDialog form = new SaveFileDialog();
            form.Filter = "csv(*.csv)|*.csv";
            if (form.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string filename = form.FileName;
            Thread th = new Thread(new ThreadStart(() =>
            {
                获取商品信息(keyword, filename);
            }));
            th.IsBackground = true;
            th.Start();
        }

        private void 获取商品信息(string keyword, string filename)
        {
            InitWebDriver();
            //所有商品
            driver.Url = "https://www.taobao.com";
            var input = wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#q")); });
            var submit = wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#J_TSearchForm > div.search-button > button")); });
            input.SendKeys(keyword);
            submit.Click();
            //var total = wait.Until<IWebElement>((d) => { return d.FindElement(By.CssSelector("#mainsrp-pager > div > div > div > div.total ")); });
            var btnsort = wait.Until<IWebElement>((d) => { return d.FindElement(By.XPath("//*[@id='J_relative']/div[1]/div/ul/li[3]/a")); });
            btnsort.Click();

            var pagenum = nubpagenum.Value;
            List<string> lst = new List<string>();
            for (int i = 1; i <= pagenum; i++)
            {
                var pagenuminput = wait.Until<IWebElement>((d) => { return d.FindElement(By.XPath("//*[@id='mainsrp-pager']/div/div/div/div[2]/input")); });
                if(pagenuminput==null)
                {
                    i--;
                    continue;
                }
                pagenuminput.Clear();
                if (pagenuminput != null)
                {
                    pagenuminput.SendKeys(i.ToString());
                }
                var btnenter = wait.Until<IWebElement>((d) => { return d.FindElement(By.XPath("//*[@id='mainsrp-pager']/div/div/div/div[2]/span[3]")); });
                btnenter.Click();
                Thread.Sleep(1000);

                btnenter = wait.Until<IWebElement>((d) => { return d.FindElement(By.XPath("//*[@id='mainsrp-pager']/div/div/div/div[2]/span[3]")); });
                string strsource = driver.PageSource;
                var lsttmp = 从淘宝搜索页获取商品链接(strsource);
                lst.AddRange(lsttmp);
            }


            this.Invoke(new MethodInvoker(() =>
            {
                //dataGridView1.DataSource = lst.ToList();
                foreach (var item in lst)
                {
                    dataGridView1.Rows.Add(item);
                }
            }));
            根据网址抓取商品(filename, lst);
        }

        private void 根据网址抓取商品(string filename, List<string> lst)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            using (TextWriter writer = new StreamWriter(fs, Encoding.GetEncoding("GB2312")))
            using (var csv = new CsvWriter(writer))
            {
                foreach (var item in lst)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            string uri = row.Cells[0].Value?.ToString();
                            if (item == uri)
                            {
                                dataGridView1.CurrentCell = dataGridView1[0, row.Index];
                            }
                        }
                    }));

                    //解析淘宝商品搜索页
                    //webBrowser2.Load(item);
                    //webBrowser2.Update();
                    //httpitem.URL = item;
                    //var html = httphelper.GetHtml(httpitem);
                    //driver.Url = item;
                    LoadProductUri(item);
                    if (item.Contains("taobao"))
                    {
                        获取连接(item, csv);
                    }
                    else if (item.Contains("tmall"))
                    {
                        获取天猫连接(item, csv);
                    }
                }
            }
        }

        private void LoadProductUri(string uri)
        {
            driver.Navigate().GoToUrl(string.Format("https://{0}", uri));
            //将页面滚动条拖到底部
            for (int i = 0; i < 10; i++)
            {
                int row = (i + 1) * 2000;
                ((IJavaScriptExecutor)driver).ExecuteScript(string.Format("window.scrollTo(500,{0});", row));
                Thread.Sleep(100);
            }
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

        private List<string> 从淘宝搜索页获取商品链接(string strsource)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(strsource);
            var html = htmlDocument.DocumentNode;
            //*[@id="mainsrp-itemlist"]/div/div/div[1]
            //*[@id="mainsrp-itemlist"]/div/div/div[1]
            var nodes = html.SelectNodes("//*[@id='mainsrp-itemlist']/div/div/div/div");
            List<string> lst = new List<string>();
            foreach (var item in nodes)
            {
                var a = item.SelectSingleNode("./div/div/div/a");
                if (a != null)
                {
                    string uri = a.GetAttributeValue("href");
                    if (!uri.Contains("click.simba"))
                    {
                        lst.Add(a.GetAttributeValue("href"));
                    }
                }
            }
            return lst;
        }

    }







    /// <summary>
    /// 在自己窗口打开链接
    /// </summary>
    internal class OpenPageSelf : ILifeSpanHandler
    {
        Form1 form = null;
        public OpenPageSelf(Form1 mainform)
        {
            form = mainform;
        }
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl,
string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures,
IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;
            var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
            chromiumWebBrowser.Load(targetUrl);
            form.SetURL(targetUrl);
            return true; //Return true to cancel the popup creation copyright by codebye.com.
        }
    }
}
