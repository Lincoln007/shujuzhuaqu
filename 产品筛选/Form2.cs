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

namespace 产品筛选
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            textBox1.Text = idcolindex.ToString();
            textBox2.Text = titlecolindex.ToString();
            textBox3.Text = skuscolindex.ToString();
            textBox4.Text = pricecolindex.ToString();
            textBox5.Text = chengjiaocolindex.ToString();
            textBox6.Text = yishoucolindex.ToString();
            textBox7.Text = pinluncolindex.ToString();
            textBox8.Text = keywordcolindex.ToString();

            try
            {
                string path = Path.Combine(Application.StartupPath, "config.ini");
                if (File.Exists(path))
                {
                    Ini ini = new Ini(path);
                    textBox1.Text = ini.GetValue("idcolindex", "colindex", idcolindex.ToString());
                    textBox2.Text = ini.GetValue("titlecolindex", "colindex", titlecolindex.ToString());
                    textBox3.Text = ini.GetValue("skuscolindex", "colindex", skuscolindex.ToString());
                    textBox4.Text = ini.GetValue("pricecolindex", "colindex", pricecolindex.ToString());
                    textBox5.Text = ini.GetValue("chengjiaocolindex", "colindex", chengjiaocolindex.ToString());
                    textBox6.Text = ini.GetValue("yishoucolindex", "colindex", yishoucolindex.ToString());
                    textBox7.Text = ini.GetValue("pinluncolindex", "colindex", pinluncolindex.ToString());
                    textBox8.Text = ini.GetValue("keywordcolindex", "colindex", keywordcolindex.ToString());
                    textBox9.Text= ini.GetValue("imagescolindex", "colindex", imagescolindex.ToString());
                    textBox10.Text=ini.GetValue("detailscolindex", "colindex", detailscolindex.ToString());
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static int idcolindex = 0;
        public static int titlecolindex = 1;
        public static int skuscolindex = 7;
        public static int pricecolindex = 3;
        public static int chengjiaocolindex = 5;
        public static int yishoucolindex = 6;
        public static int pinluncolindex = 8;
        public static int keywordcolindex = 18;
        public static int imagescolindex = 18;
        public static int detailscolindex = 18;


        private void button1_Click(object sender, EventArgs e)
        {
            idcolindex = int.Parse(textBox1.Text);
            titlecolindex = int.Parse(textBox2.Text);
            skuscolindex = int.Parse(textBox3.Text);
            pricecolindex = int.Parse(textBox4.Text);
            chengjiaocolindex = int.Parse(textBox5.Text);
            yishoucolindex = int.Parse(textBox6.Text);
            pinluncolindex = int.Parse(textBox7.Text);
            keywordcolindex = int.Parse(textBox8.Text);
            imagescolindex = int.Parse(textBox9.Text);
            detailscolindex = int.Parse(textBox10.Text);

            Ini ini = new Ini(Path.Combine(Application.StartupPath, "config.ini"));
            ini.WriteValue("idcolindex", "colindex", idcolindex.ToString());
            ini.WriteValue("titlecolindex", "colindex", titlecolindex.ToString());
            ini.WriteValue("skuscolindex", "colindex", skuscolindex.ToString());
            ini.WriteValue("pricecolindex", "colindex", pricecolindex.ToString());
            ini.WriteValue("chengjiaocolindex", "colindex", chengjiaocolindex.ToString());
            ini.WriteValue("yishoucolindex", "colindex", yishoucolindex.ToString());
            ini.WriteValue("pinluncolindex", "colindex", pinluncolindex.ToString());
            ini.WriteValue("keywordcolindex", "colindex", keywordcolindex.ToString());
            ini.WriteValue("imagescolindex", "colindex", imagescolindex.ToString());
            ini.WriteValue("detailscolindex", "colindex", detailscolindex.ToString());


            ini.Save();
            DialogResult = DialogResult.OK;
        }
    }
}
