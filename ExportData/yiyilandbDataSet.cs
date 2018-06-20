using System.Collections.Generic;
using System.Data;

namespace ExportData
{


    partial class yiyilandbDataSet
    {
    }
}

namespace ExportData.yiyilandbDataSetTableAdapters {
    
    
    public partial class productsTableAdapter {


        public virtual int FillBySql(yiyilandbDataSet.productsDataTable dataTable, string whereyuju)
        {
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
            command.Connection = this.Connection;
            command.CommandText= "SELECT   id, title, description, params, current_price_min, " +
                "current_price_max, original_price_min, original_price_max, month_sales_count," +
                " stock, skunumber, shipping_address, shop_id, category_id, keyword, comments_count," +
                " stores_count, score, uri FROM products ";
            command.CommandType = global::System.Data.CommandType.Text;

            if (!string.IsNullOrEmpty(whereyuju))
            {
                command.CommandText += whereyuju;
            }
            if ((this.ClearBeforeFill == true))
            {
                dataTable.Clear();
            }
            this.Adapter.SelectCommand = command;

            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        public virtual List<string> GetAllKeyWords()
        {
            //SELECT DISTINCT keyword FROM products
            System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
            command.Connection = this.Connection;
            command.CommandText = "SELECT DISTINCT keyword FROM products";
            //command.CommandText = "SELECT keyword FROM products";
            command.CommandType = global::System.Data.CommandType.Text;
            this.Adapter.SelectCommand = command;
            DataTable dataTable = new DataTable();
            this.Adapter.Fill(dataTable);
            List<string> lstrtn = new List<string>();
            foreach (DataRow item in dataTable.Rows)
            {
                lstrtn.Add(item[0].ToString().Trim());
            }
            return lstrtn;
        }
    }
}
