using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportdataprocessing
{
    delegate void dgtprogress(int curindex, int total);
    class ImportDbContext
    {
        public ImportDbContext()
        {
        }
        public async Task<bool> UpdateFromDataTable(DataTable dt, dgtprogress func,string remark,string remarkid)
        {
            await Task.Run(() =>
            {
                int total = dt.Rows.Count;
                int curindex = 0;
                foreach (DataRow row in dt.Rows)
                {
                    curindex++;
                    func?.Invoke(curindex, total);
                    try
                    {
                        using (YIyilanDatabaseContainer dbcontext = new YIyilanDatabaseContainer())
                        {
                            string id = row[parserow.id].ToString();
                            if(!UpdateProductTable(dbcontext, row, remark,remarkid))
                            {
                                continue;
                            }
                            
                            var lst = parserow.ParamParamInfo(row);
                            //this.Database.ExecuteSqlCommand("delete from yiyilandb.dbo.productparams where id={0}", id);

                            foreach (var param in dbcontext.productparams.Where(e => e.id == id))
                            {
                                dbcontext.productparams.Remove(param);
                            }

                            foreach (var paraminfo in lst)
                            {
                                var tmp = new productparams();
                                tmp.id = id;
                                tmp.paramlabel = paraminfo.Paramname;
                                tmp.paramvalue = paraminfo.Value;
                                dbcontext.productparams.Add(tmp);
                            }

                            var lstsku = parserow.ParamSkuInfo(row);

                            foreach (var param in dbcontext.productskus.Where(e => e.id == id))
                            {
                                dbcontext.productskus.Remove(param);
                            }
                            foreach (var item in lstsku)
                            {
                                var tmp = new productskus();
                                tmp.id = id;
                                tmp.skname = item.Paramname;
                                tmp.imageurl = item.Value;
                                dbcontext.productskus.Add(tmp);
                            }

                            List<string> lstimages;
                            List<string> lstdetails;
                            parserow.ParamImage(row, out lstimages, out lstdetails);
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in lstimages)
                            {
                                sb.AppendFormat("{0}|", item);
                            }
                            string images = sb.ToString();
                            sb.Clear();
                            foreach (var item in lstdetails)
                            {
                                sb.AppendFormat("{0}|", item);
                            }
                            string details = sb.ToString();
                            foreach (var param in dbcontext.imagedetail.Where(e => e.id == id))
                            {
                                dbcontext.imagedetail.Remove(param);
                            }
                            var imagedetailinfo = new imagedetail();
                            imagedetailinfo.id = id;
                            imagedetailinfo.images = images;
                            imagedetailinfo.details = details;
                            dbcontext.imagedetail.Add(imagedetailinfo);
                            dbcontext.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            });
            return true;
        }
        public bool ParsePrice(string strprice, out decimal minprice, out decimal maxprice)
        {
            minprice = 0;
            maxprice = 0;
            try
            {
                var arr = strprice.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    minprice = maxprice = decimal.Parse(arr[0]);
                }
                if (arr.Length > 1)
                {
                    maxprice = decimal.Parse(arr[1]);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool UpdateProductTable(YIyilanDatabaseContainer dbcontext, DataRow row, string remark, string remarkid)
        {
            try
            {
                string id = row[parserow.id].ToString();
                string title = row[parserow.title].ToString();
                string description = row[parserow.description].ToString();
                string strparams = row[parserow.paramindex].ToString();
                string current_price = row[parserow.current_price].ToString();
                string original_price = row[parserow.original_price].ToString();
                string month_sales_count = row[parserow.month_sales_count].ToString();
                string stock = row[parserow.stock].ToString();
                string sku = row[parserow.sku].ToString();
                string shipping_address = row[parserow.shipping_address].ToString();
                string shop_id = row[parserow.shop_id].ToString();
                string shop_name = row[parserow.shop_name].ToString();
                string category_id = row[parserow.category_id].ToString();
                string keyword = row[parserow.keyword].ToString();
                string comments_count = row[parserow.comments_count].ToString();
                string url = row[parserow.url].ToString();
                string images = row[parserow.images].ToString();
                string detail = row[parserow.detail].ToString();
                string stores_count = row[parserow.stores_count].ToString();
                string score = row[parserow.score].ToString();

                bool isnew = false;
                products pro = dbcontext.products.Find(id);
                if (pro == null)
                {
                    pro = new products();
                    pro.id = id;
                    isnew = true;
                }

                //pro.id = id;
                pro.title = title;
                pro.description = description;
                pro.@params = strparams;
                pro.month_sales_count = int.Parse(month_sales_count);
                pro.stock = int.Parse(stock);
                pro.skunumber = parserow.ParamSkuInfo(row).Count;
                pro.shipping_address = shipping_address;
                pro.shop_id = shop_id;
                pro.category_id = category_id;
                pro.comments_count = int.Parse(comments_count);
                pro.keyword = keyword;
                pro.uri = url;
                decimal minprice = 0;
                decimal maxprice = 0;
                ParsePrice(current_price, out minprice, out maxprice);
                pro.current_price_min = minprice;
                pro.current_price_max = maxprice;
                ParsePrice(original_price, out minprice, out maxprice);
                pro.original_price_min = minprice;
                pro.original_price_max = maxprice;
                pro.stores_count = int.Parse(stores_count);
                pro.score = decimal.Parse(score);
                pro.remark = remark;
                pro.remarkid = remarkid;
                if (isnew)
                {
                    dbcontext.products.Add(pro);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
