using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportdataprocessing
{
    static class parserow
    {

        public static  int id { get; set; }//	

        public static  int title { get; set; }//	标题

        public static  int description { get; set; }//	描述

        public static  int paramindex { get; set; }//	参数详情

        public static  int current_price { get; set; }//	当前价格

        public static  int original_price { get; set; }//	原价

        public static  int month_sales_count { get; set; }//	月销量

        public static  int stock { get; set; }//	库存

        public static  int sku { get; set; }//	规格详情

        public static  int shipping_address { get; set; }//	发货地址

        public static  int shop_id { get; set; }//	商店ID

        public static  int shop_name { get; set; }//	商店名字

        public static  int category_id { get; set; }//	商品分类ID

        public static  int keyword { get; set; }//	关键字

        public static  int comments_count { get; set; }//	

        public static  int url { get; set; }//	商品链接

        public static  int images { get; set; }//	商品图片

        public static  int detail { get; set; }//	商品详情

        public static  int stores_count { get; set; }//	收藏数

        public static  int score { get; set; }//	评分

        public static List<ParamInfo> ParamParamInfo(DataRow row)
        {
            List<ParamInfo> lstrtn = new List<ParamInfo>();
            try
            {
                string str = row[paramindex].ToString();
                JArray ja = (JArray)JsonConvert.DeserializeObject(str);
                foreach (var item in ja)
                {
                    if(lstrtn.Find(e=> e.Paramname == item["label"].ToString())==null)
                    {
                        lstrtn.Add(new ParamInfo(item["label"].ToString(), item["value"].ToString()));
                    }
                }
            }
            catch (Exception)
            {
            }
            return lstrtn;
        }

        public static List<SKUInfo> ParamSkuInfo(DataRow row)
        {
            List<SKUInfo> lstrtn = new List<SKUInfo>();
            try
            {
                string str = row[sku].ToString();
                JArray ja = (JArray)JsonConvert.DeserializeObject(str);
                foreach (var item in ja)
                {
                    foreach (var value in item["values"])
                    {
                        string strdesc = value["desc"].ToString();
                        string imageuri = value["image"]?.ToString();
                        if (lstrtn.Find(e => e.Paramname == strdesc) == null)
                        {
                            if (!string.IsNullOrEmpty(strdesc))
                            {
                                lstrtn.Add(new SKUInfo(strdesc, imageuri));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return lstrtn;
        }

        public static bool ParamImage(DataRow row ,out List<string> lstimage, out List<string> lstdetail)
        {
            lstimage = new List<string>();
            lstdetail = new List<string>();
            try
            {
                string strimage = row[images].ToString();
                JArray ja = (JArray)JsonConvert.DeserializeObject(strimage);
                foreach (var item in ja)
                {
                    string struri = item["image_url"].ToString();
                    if (!string.IsNullOrEmpty(struri))
                    {
                        lstimage.Add(struri);
                    }
                }

                string strdetail = row[detail].ToString();
                string[] strarr = strdetail.Split(new char[] { ' ', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in strarr)
                {
                    if (item.Contains("src=\""))
                    {
                        string struri = item.Substring(5, item.Length - 6);
                        if (!string.IsNullOrEmpty(struri))
                        {
                            lstdetail.Add(struri);

                        }
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
        
    }
}
