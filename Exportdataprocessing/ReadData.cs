using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exportdataprocessing
{
    internal class ReadData
    {
        DataTable datatable = null;
        public ReadData()
        {

        }
        public DataTable ProductData
        {
            get
            {
                return datatable;
            }
        }
        public bool ReadExcel(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            try
            {
                using (FileStream s = new FileStream(path, FileMode.Open))
                {
                    IExcelDataReader excelReader = null;
                    string extension = Path.GetExtension(path);
                    if (extension == ".xls")
                    {
                        excelReader = ExcelReaderFactory.CreateBinaryReader(s);
                    }
                    else if (extension == ".xlsx")
                    {
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(s);
                    }
                    if (excelReader != null)
                    {
                        using (excelReader)
                        {
                            datatable = excelReader.AsDataSet().Tables[0];
                            datatable.Rows.RemoveAt(0);
                            return datatable != null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public async Task<bool> ReadExcelAsync(string path)
        {
            Task<bool> task = new Task<bool>((_path) => { return ReadExcel((string)_path); }, path);
            task.Start();
            bool rtn = await task;
            return rtn;
        }

    }
}
