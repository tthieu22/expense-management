using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExpenseManagement.Core
{
    internal class ExcelExporter
    {
        public static void ExportToExcel(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    Excel.Application excelApp = new Excel.Application();
                    if (excelApp == null)
                    {
                        MessageBox.Show("Microsoft Excel is not installed.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Excel.Workbook workbook = excelApp.Workbooks.Add();
                    Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

                    for (int col = 0; col < dataGridView.Columns.Count; col++)
                    {
                        worksheet.Cells[1, col + 1] = dataGridView.Columns[col].HeaderText;
                    }

                    for (int row = 0; row < dataGridView.Rows.Count; row++)
                    {
                        for (int col = 0; col < dataGridView.Columns.Count; col++)
                        {
                            object cellValue = dataGridView.Rows[row].Cells[col].Value;
                            worksheet.Cells[row + 2, col + 1] = cellValue != null ? cellValue.ToString() : "";
                        }
                    }

                    worksheet.Columns.AutoFit();
                    workbook.SaveAs(filePath);
                    workbook.Close();
                    excelApp.Quit();

                    ReleaseObject(worksheet);
                    ReleaseObject(workbook);
                    ReleaseObject(excelApp);

                    MessageBox.Show($"Exported successfully to: {filePath}", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
