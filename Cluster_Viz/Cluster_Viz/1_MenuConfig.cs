using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//для Excel
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;

//для OxyPlot
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace Cluster_Viz
{
    public class Menu_Toolbar
    {

    }

    partial class main_form
    {
        public void Load_Menu() //Загрузка меню. Переделать в класс(?)
        {
            main_menu_file_open.Click += (s, e) =>
            {
                Load_Excel_Table_From_File(excel_data);
            };

            main_menu_file_exit.Click += (s, e) =>
            {
                Exit_App();
            };
        }
        
        public DataTable Load_Excel_Table_From_File(DataTable excel_table)
        {
            //Загрузка Excel в область данных. Нужно доделать разделение
            OpenFileDialog open_excel = new OpenFileDialog();

            open_excel.DefaultExt = "*.xls;*.xlsx"; //Расширение файла по умолчанию
            open_excel.Filter = "Excel Sheet(*.xlsx)|*.xlsx"; //Фильтр имен файлов
            open_excel.Title = "Выберите документ для загрузки данных"; //Заголовок диалогового окна

            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet NewSheet;
            ExcelObj.Range SheetRange;

            //Чистка - иначе ошибка
            excel_table.Columns.Clear();
            excel_table.Rows.Clear();

            if (open_excel.ShowDialog() == DialogResult.OK)
            {
                excel_table.TableName = open_excel.SafeFileName;
                workbook = app.Workbooks.Open(open_excel.FileName, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

                NewSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);
                SheetRange = NewSheet.UsedRange;

                for (int ClmNum = 1; ClmNum <= SheetRange.Columns.Count; ClmNum++)
                {
                    excel_table.Columns.Add(new DataColumn((SheetRange.Cells[1, ClmNum] as ExcelObj.Range).Value2.ToString()));
                }

                for (int Rnum = 2; Rnum <= SheetRange.Rows.Count; Rnum++)
                {
                    DataRow _datarow = excel_table.NewRow();
                    for (int Cnum = 1; Cnum <= SheetRange.Columns.Count; Cnum++)
                    {
                        if ((SheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                        {
                            _datarow[Cnum - 1] =
                            (SheetRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    excel_table.Rows.Add(_datarow);
                    excel_table.AcceptChanges();
                }
                excel_dataGridView.DataSource = excel_table;
                excel_loaded = true;
                app.Quit(); //Excel остается в диспетчере (решить)
                this.tab_control.SelectedTab = tab_page_data;
            }
            return excel_table;
        }

        public void Exit_App()
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти из приложения?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
