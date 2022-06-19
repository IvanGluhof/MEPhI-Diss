using System;
using System.IO;
using System.Data;
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;
//-----------------------------------------------------------------------КОНВЕНЦИЯ СТИЛЯ--------------------------------------------------------------------------------------
//---------------------------------------------------------------ВСЕ ПЕРЕМЕННЫЕ - С МАЛЕНЬКОЙ БУКВЫ!--------------------------------------------------------------------------
//-------------------------------------------------------------------МЕТОДЫ И КЛАССЫ - С БОЛЬШОЙ!-----------------------------------------------------------------------------
namespace ConverterLibrary
{
    /// <summary>
    /// Класс для работы с файлами
    /// </summary>
    public class File_Converter
    {
        private string input_path;
        private string output_path;

        private string file_name;

        public string Input_path { get { return input_path; } }
        public string Output_path { get { return output_path; } }

        public string File_name { get { return file_name; } }

        public File_Converter()
        {
            Set_Paths();
        }

        /// <summary>
        /// Пути для входных/выходных файлов
        /// </summary>
        private void Set_Paths()
        {
            input_path = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\input_files");
            output_path = (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\output_files");
        }        

        /// <summary>
        /// Загрузка Excel файла с помощью OpenFileDialog
        /// </summary>
        /// <returns></returns>
        public DataTable Load_Excel_Table_From_File()
        {
            DataTable excel_data_to_grid = new DataTable();// Для временного хранения данных Excel в виде таблицы и передачи их в метод   

            //Загрузка Excel в область данных.
            Microsoft.Win32.OpenFileDialog open_excel = new Microsoft.Win32.OpenFileDialog();

            open_excel.DefaultExt = "*.xls;*.xlsx"; //Расширение файла по умолчанию
            open_excel.Filter = "Excel Sheet(*.xlsx)|*.xlsx"; //Фильтр имен файлов
            open_excel.Title = "Выберите файл Excel для загрузки данных"; //Заголовок диалогового окна
            open_excel.InitialDirectory = input_path;

            ExcelObj.Application excel_application = new ExcelObj.Application();
            ExcelObj.Workbook excel_workbook;
            ExcelObj.Worksheet excel_sheet;
            ExcelObj.Range excel_sheet_range;

            Nullable<bool> result = open_excel.ShowDialog(); //для WPF - разобраться побольше

            if (result == true)
            {
                excel_data_to_grid.TableName = open_excel.SafeFileName;

                excel_workbook = excel_application.Workbooks.Open(open_excel.FileName,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                excel_sheet = (ExcelObj.Worksheet)excel_workbook.Sheets.get_Item(1);
                excel_sheet_range = excel_sheet.UsedRange;

                for (int column_num = 1; column_num <= excel_sheet_range.Columns.Count; column_num++)
                {
                    excel_data_to_grid.Columns.Add(new DataColumn((excel_sheet_range.Cells[1, column_num] as ExcelObj.Range).Value2.ToString()));
                }

                for (int row_num = 2; row_num <= excel_sheet_range.Rows.Count; row_num++)
                {
                    DataRow _datarow = excel_data_to_grid.NewRow();
                    for (int _column_num = 1; _column_num <= excel_sheet_range.Columns.Count; _column_num++)
                    {
                        if ((excel_sheet_range.Cells[row_num, _column_num] as ExcelObj.Range).Value2 != null)
                        {
                            _datarow[_column_num - 1] =
                            (excel_sheet_range.Cells[row_num, _column_num] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    excel_data_to_grid.Rows.Add(_datarow);
                    excel_data_to_grid.AcceptChanges();
                }

                file_name = open_excel.SafeFileName;
                excel_workbook.Close(false);
                excel_application.Quit();


                return excel_data_to_grid;
            }
            else
            {
                excel_application.Quit(); //Excel остается в диспетчере (решить)   
                return excel_data_to_grid = null;
            }
        }
        /// <summary>
        /// Загрузка файла без OpenFileDialog
        /// </summary>
        /// <param name="file_name"></param>
        /// <returns>Имя файла</returns>
        public DataTable Load_Excel_Table_From_File(string file_name)
        {
            DataTable excel_data = new DataTable();// Для временного хранения данных Excel в виде таблицы и передачи их в метод   
            string file_path = output_path + "\\" + file_name;
            ExcelObj.Application excel_application = new ExcelObj.Application();
            ExcelObj.Workbook excel_workbook;
            ExcelObj.Worksheet excel_sheet;
            ExcelObj.Range excel_sheet_range;

            if (file_path != null)
            {
                excel_data.TableName = file_name;

                excel_workbook = excel_application.Workbooks.Open(file_path,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                excel_sheet = (ExcelObj.Worksheet)excel_workbook.Sheets.get_Item(1);
                excel_sheet_range = excel_sheet.UsedRange;

                for (int column_num = 1; column_num <= excel_sheet_range.Columns.Count; column_num++)
                {
                    excel_data.Columns.Add(new DataColumn((excel_sheet_range.Cells[1, column_num] as ExcelObj.Range).Value2.ToString()));
                }

                for (int row_num = 2; row_num <= excel_sheet_range.Rows.Count; row_num++)
                {
                    DataRow _datarow = excel_data.NewRow();
                    for (int _column_num = 1; _column_num <= excel_sheet_range.Columns.Count; _column_num++)
                    {
                        if ((excel_sheet_range.Cells[row_num, _column_num] as ExcelObj.Range).Value2 != null)
                        {
                            _datarow[_column_num - 1] =
                            (excel_sheet_range.Cells[row_num, _column_num] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    excel_data.Rows.Add(_datarow);
                    excel_data.AcceptChanges();
                }

                excel_workbook.Close(false);
                excel_application.Quit();

                return excel_data;
            }
            else
            {
                excel_application.Quit(); //Excel остается в диспетчере (решить)   
                return excel_data = null;
            }
        }
    }
}
