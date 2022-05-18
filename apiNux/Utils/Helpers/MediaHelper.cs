using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;

namespace SeguroViagemApi.Application.Utils
{
    public static class MediaHelper
    {
        static public string SaveMedia(string path, string media)
        {
            try {
                int index = media.IndexOf(",");

                media = media.Replace(media.Substring(0, index + 1), "");
                byte[] bytes = Convert.FromBase64String(media);

                string filePath;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    filePath = path + "/image_1.png";
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    FileInfo[] files = di.GetFiles();
                    int imageNum;
                    if (files.Length > 0)
                    {
                        FileInfo file = files[0];

                        var file_splitted = file.Name.Split('_');

                        if (file_splitted.Length > 1)
                        {
                            imageNum = Convert.ToInt32(file_splitted[1].Split('.')[0]);
                        }
                        else
                        {
                            imageNum = 0;
                        }

                        filePath = path + "/image_" + (imageNum + 1).ToString() + ".png";

                        file.Delete();
                    }
                    else
                    {
                        filePath = path + "/image_1.png";
                    }
                }

                using (FileStream stream = new System.IO.FileStream(filePath, FileMode.Create))
                {
                    Stream mstream = new MemoryStream(bytes);
                    mstream.CopyTo(stream);
                    stream.Close();
                }
                index = filePath.IndexOf("Upload");
                filePath = filePath.Substring(index);
                return filePath;
            }
            catch(Exception e) {
                return null;
            }
        }

        static public string GenerateExcel(dynamic list)
        {
            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(list), (typeof(DataTable)));
            string FileName = $@"wwwroot/Export/Aniversarios";
            
            if (!Directory.Exists(FileName))
            {
                Directory.CreateDirectory(FileName);
                FileName = FileName + $"/Aniverarios-export{DateTime.Now.ToString("fff")}.xlsx";
            }

            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Sheet1");

            List<String> columns = new List<string>();
            IRow row = excelSheet.CreateRow(0);
            int columnIndex = 0;

            foreach (System.Data.DataColumn column in table.Columns)
            {
                columns.Add(column.ColumnName);
                row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                columnIndex++;
            }

            int rowIndex = 1;
            foreach (DataRow dsrow in table.Rows)
            {
                row = excelSheet.CreateRow(rowIndex);
                int cellIndex = 0;
                foreach (String col in columns)
                {
                    row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                    cellIndex++;
                }

                rowIndex++;
            }

            ByteArrayOutputStream bos = new ByteArrayOutputStream();
            try
            {
                workbook.Write(bos);
            }
            finally
            {
                bos.Close();
            }
            byte[] bytes = bos.ToByteArray();
            return Convert.ToBase64String(bytes);
        }
    }
}