namespace Exportdataprocessing
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.btnimport = new System.Windows.Forms.Button();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.txtshop_id = new System.Windows.Forms.TextBox();
            this.txtsku = new System.Windows.Forms.TextBox();
            this.txtshipping_address = new System.Windows.Forms.TextBox();
            this.txtstock = new System.Windows.Forms.TextBox();
            this.txtmonth_sales_count = new System.Windows.Forms.TextBox();
            this.txtoriginal_price = new System.Windows.Forms.TextBox();
            this.txtcurrent_price = new System.Windows.Forms.TextBox();
            this.txtparams = new System.Windows.Forms.TextBox();
            this.txtdescription = new System.Windows.Forms.TextBox();
            this.txttitle = new System.Windows.Forms.TextBox();
            this.txtid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtshop_name = new System.Windows.Forms.TextBox();
            this.txtcategory_id = new System.Windows.Forms.TextBox();
            this.txtkeyword = new System.Windows.Forms.TextBox();
            this.txtcomments_count = new System.Windows.Forms.TextBox();
            this.txturl = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.txtimages = new System.Windows.Forms.TextBox();
            this.txtdetail = new System.Windows.Forms.TextBox();
            this.txtstores_count = new System.Windows.Forms.TextBox();
            this.txtscore = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.ctlImageurl1 = new Exportdataprocessing.ctlImageurl();
            this.ctlSKU1 = new Exportdataprocessing.ctlSKU();
            this.ctlParam1 = new Exportdataprocessing.ctlParam();
            this.btnupdate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnupdate);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtscore);
            this.panel1.Controls.Add(this.txtstores_count);
            this.panel1.Controls.Add(this.txturl);
            this.panel1.Controls.Add(this.txtdetail);
            this.panel1.Controls.Add(this.txtcomments_count);
            this.panel1.Controls.Add(this.txtimages);
            this.panel1.Controls.Add(this.txtkeyword);
            this.panel1.Controls.Add(this.textBox17);
            this.panel1.Controls.Add(this.txtcategory_id);
            this.panel1.Controls.Add(this.txtshop_name);
            this.panel1.Controls.Add(this.txtshop_id);
            this.panel1.Controls.Add(this.txtsku);
            this.panel1.Controls.Add(this.txtshipping_address);
            this.panel1.Controls.Add(this.txtstock);
            this.panel1.Controls.Add(this.txtmonth_sales_count);
            this.panel1.Controls.Add(this.txtoriginal_price);
            this.panel1.Controls.Add(this.txtcurrent_price);
            this.panel1.Controls.Add(this.txtparams);
            this.panel1.Controls.Add(this.txtdescription);
            this.panel1.Controls.Add(this.txttitle);
            this.panel1.Controls.Add(this.txtid);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnimport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 748);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvProduct);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(254, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(481, 748);
            this.panel2.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(251, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 748);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ctlImageurl1);
            this.panel3.Controls.Add(this.splitter4);
            this.panel3.Controls.Add(this.ctlSKU1);
            this.panel3.Controls.Add(this.splitter3);
            this.panel3.Controls.Add(this.ctlParam1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(738, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(317, 748);
            this.panel3.TabIndex = 0;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(735, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 748);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // btnimport
            // 
            this.btnimport.Location = new System.Drawing.Point(12, 12);
            this.btnimport.Name = "btnimport";
            this.btnimport.Size = new System.Drawing.Size(75, 23);
            this.btnimport.TabIndex = 0;
            this.btnimport.Text = "导入";
            this.btnimport.UseVisualStyleBackColor = true;
            this.btnimport.Click += new System.EventHandler(this.btnimport_Click);
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToAddRows = false;
            this.dgvProduct.AllowUserToDeleteRows = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProduct.Location = new System.Drawing.Point(3, 3);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            this.dgvProduct.ReadOnly = true;
            this.dgvProduct.RowTemplate.Height = 23;
            this.dgvProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduct.Size = new System.Drawing.Size(475, 742);
            this.dgvProduct.TabIndex = 0;
            this.dgvProduct.CurrentCellChanged += new System.EventHandler(this.dgvProduct_CurrentCellChanged);
            // 
            // txtshop_id
            // 
            this.txtshop_id.Location = new System.Drawing.Point(173, 286);
            this.txtshop_id.Name = "txtshop_id";
            this.txtshop_id.Size = new System.Drawing.Size(40, 21);
            this.txtshop_id.TabIndex = 12;
            this.txtshop_id.Text = "18";
            // 
            // txtsku
            // 
            this.txtsku.Location = new System.Drawing.Point(173, 238);
            this.txtsku.Name = "txtsku";
            this.txtsku.Size = new System.Drawing.Size(40, 21);
            this.txtsku.TabIndex = 13;
            this.txtsku.Text = "18";
            // 
            // txtshipping_address
            // 
            this.txtshipping_address.Location = new System.Drawing.Point(173, 263);
            this.txtshipping_address.Name = "txtshipping_address";
            this.txtshipping_address.Size = new System.Drawing.Size(40, 21);
            this.txtshipping_address.TabIndex = 14;
            this.txtshipping_address.Text = "18";
            // 
            // txtstock
            // 
            this.txtstock.Location = new System.Drawing.Point(173, 213);
            this.txtstock.Name = "txtstock";
            this.txtstock.Size = new System.Drawing.Size(40, 21);
            this.txtstock.TabIndex = 15;
            this.txtstock.Text = "18";
            // 
            // txtmonth_sales_count
            // 
            this.txtmonth_sales_count.Location = new System.Drawing.Point(173, 189);
            this.txtmonth_sales_count.Name = "txtmonth_sales_count";
            this.txtmonth_sales_count.Size = new System.Drawing.Size(40, 21);
            this.txtmonth_sales_count.TabIndex = 16;
            this.txtmonth_sales_count.Text = "18";
            // 
            // txtoriginal_price
            // 
            this.txtoriginal_price.Location = new System.Drawing.Point(173, 166);
            this.txtoriginal_price.Name = "txtoriginal_price";
            this.txtoriginal_price.Size = new System.Drawing.Size(40, 21);
            this.txtoriginal_price.TabIndex = 17;
            this.txtoriginal_price.Text = "18";
            // 
            // txtcurrent_price
            // 
            this.txtcurrent_price.Location = new System.Drawing.Point(173, 144);
            this.txtcurrent_price.Name = "txtcurrent_price";
            this.txtcurrent_price.Size = new System.Drawing.Size(40, 21);
            this.txtcurrent_price.TabIndex = 18;
            this.txtcurrent_price.Text = "17";
            // 
            // txtparams
            // 
            this.txtparams.Location = new System.Drawing.Point(173, 121);
            this.txtparams.Name = "txtparams";
            this.txtparams.Size = new System.Drawing.Size(40, 21);
            this.txtparams.TabIndex = 19;
            this.txtparams.Text = "16";
            // 
            // txtdescription
            // 
            this.txtdescription.Location = new System.Drawing.Point(173, 97);
            this.txtdescription.Name = "txtdescription";
            this.txtdescription.Size = new System.Drawing.Size(40, 21);
            this.txtdescription.TabIndex = 20;
            this.txtdescription.Text = "11";
            // 
            // txttitle
            // 
            this.txttitle.Location = new System.Drawing.Point(173, 72);
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(40, 21);
            this.txttitle.TabIndex = 21;
            this.txttitle.Text = "1";
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(173, 47);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(40, 21);
            this.txtid.TabIndex = 22;
            this.txtid.Text = "0";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 502);
            this.label1.TabIndex = 11;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtshop_name
            // 
            this.txtshop_name.Location = new System.Drawing.Point(173, 309);
            this.txtshop_name.Name = "txtshop_name";
            this.txtshop_name.Size = new System.Drawing.Size(40, 21);
            this.txtshop_name.TabIndex = 12;
            this.txtshop_name.Text = "18";
            // 
            // txtcategory_id
            // 
            this.txtcategory_id.Location = new System.Drawing.Point(173, 333);
            this.txtcategory_id.Name = "txtcategory_id";
            this.txtcategory_id.Size = new System.Drawing.Size(40, 21);
            this.txtcategory_id.TabIndex = 12;
            this.txtcategory_id.Text = "18";
            // 
            // txtkeyword
            // 
            this.txtkeyword.Location = new System.Drawing.Point(173, 357);
            this.txtkeyword.Name = "txtkeyword";
            this.txtkeyword.Size = new System.Drawing.Size(40, 21);
            this.txtkeyword.TabIndex = 12;
            this.txtkeyword.Text = "18";
            // 
            // txtcomments_count
            // 
            this.txtcomments_count.Location = new System.Drawing.Point(173, 382);
            this.txtcomments_count.Name = "txtcomments_count";
            this.txtcomments_count.Size = new System.Drawing.Size(40, 21);
            this.txtcomments_count.TabIndex = 12;
            this.txtcomments_count.Text = "18";
            // 
            // txturl
            // 
            this.txturl.Location = new System.Drawing.Point(173, 407);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(40, 21);
            this.txturl.TabIndex = 12;
            this.txturl.Text = "18";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(173, 409);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(40, 21);
            this.textBox17.TabIndex = 12;
            this.textBox17.Text = "18";
            // 
            // txtimages
            // 
            this.txtimages.Location = new System.Drawing.Point(173, 433);
            this.txtimages.Name = "txtimages";
            this.txtimages.Size = new System.Drawing.Size(40, 21);
            this.txtimages.TabIndex = 12;
            this.txtimages.Text = "18";
            // 
            // txtdetail
            // 
            this.txtdetail.Location = new System.Drawing.Point(173, 458);
            this.txtdetail.Name = "txtdetail";
            this.txtdetail.Size = new System.Drawing.Size(40, 21);
            this.txtdetail.TabIndex = 12;
            this.txtdetail.Text = "18";
            // 
            // txtstores_count
            // 
            this.txtstores_count.Location = new System.Drawing.Point(173, 483);
            this.txtstores_count.Name = "txtstores_count";
            this.txtstores_count.Size = new System.Drawing.Size(40, 21);
            this.txtstores_count.TabIndex = 12;
            this.txtstores_count.Text = "18";
            // 
            // txtscore
            // 
            this.txtscore.Location = new System.Drawing.Point(173, 507);
            this.txtscore.Name = "txtscore";
            this.txtscore.Size = new System.Drawing.Size(40, 21);
            this.txtscore.TabIndex = 12;
            this.txtscore.Text = "18";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(109, 534);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "设置默认列号";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter3.Location = new System.Drawing.Point(3, 172);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(311, 3);
            this.splitter3.TabIndex = 1;
            this.splitter3.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter4.Location = new System.Drawing.Point(3, 363);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(311, 3);
            this.splitter4.TabIndex = 3;
            this.splitter4.TabStop = false;
            // 
            // ctlImageurl1
            // 
            this.ctlImageurl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlImageurl1.Location = new System.Drawing.Point(3, 366);
            this.ctlImageurl1.Name = "ctlImageurl1";
            this.ctlImageurl1.Size = new System.Drawing.Size(311, 379);
            this.ctlImageurl1.TabIndex = 4;
            // 
            // ctlSKU1
            // 
            this.ctlSKU1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlSKU1.Location = new System.Drawing.Point(3, 175);
            this.ctlSKU1.Name = "ctlSKU1";
            this.ctlSKU1.Size = new System.Drawing.Size(311, 188);
            this.ctlSKU1.TabIndex = 2;
            // 
            // ctlParam1
            // 
            this.ctlParam1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlParam1.Location = new System.Drawing.Point(3, 3);
            this.ctlParam1.Name = "ctlParam1";
            this.ctlParam1.Size = new System.Drawing.Size(311, 169);
            this.ctlParam1.TabIndex = 0;
            // 
            // btnupdate
            // 
            this.btnupdate.Location = new System.Drawing.Point(147, 12);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(75, 23);
            this.btnupdate.TabIndex = 24;
            this.btnupdate.Text = "更新到数据库";
            this.btnupdate.UseVisualStyleBackColor = true;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 754);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnimport;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.TextBox txtshop_id;
        private System.Windows.Forms.TextBox txtsku;
        private System.Windows.Forms.TextBox txtshipping_address;
        private System.Windows.Forms.TextBox txtstock;
        private System.Windows.Forms.TextBox txtmonth_sales_count;
        private System.Windows.Forms.TextBox txtoriginal_price;
        private System.Windows.Forms.TextBox txtcurrent_price;
        private System.Windows.Forms.TextBox txtparams;
        private System.Windows.Forms.TextBox txtdescription;
        private System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtscore;
        private System.Windows.Forms.TextBox txtstores_count;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.TextBox txtdetail;
        private System.Windows.Forms.TextBox txtcomments_count;
        private System.Windows.Forms.TextBox txtimages;
        private System.Windows.Forms.TextBox txtkeyword;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox txtcategory_id;
        private System.Windows.Forms.TextBox txtshop_name;
        private System.Windows.Forms.Button button1;
        private ctlParam ctlParam1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter4;
        private ctlSKU ctlSKU1;
        private ctlImageurl ctlImageurl1;
        private System.Windows.Forms.Button btnupdate;
    }
}

