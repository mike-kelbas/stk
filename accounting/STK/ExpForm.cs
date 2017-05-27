using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace STK
{
    public partial class ExpForm : Form
    {
        public ExpForm()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ClearSelection();
        }
        private void ExpForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            
            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT  t.NameType, COUNT(*), t.Expected FROM STK s LEFT JOIN TypeSTK t ON t.ID=s.TypeSTK GROUP BY t.NameType, t.Expected", Static.Con);
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;

            dataGridView1.Columns[0].HeaderText = "Наименование";
            dataGridView1.Columns[1].HeaderText = "В наличии";
            dataGridView1.Columns[2].HeaderText = "Положенно";
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }
        private Excel.Range excelcells;

       
        private void ExpForm_Activated(object sender, EventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)ExcelApp.Sheets[1];

            excelcells = worksheet.get_Range("A1", "C1");
            excelcells.Merge(Type.Missing);
            excelcells = worksheet.get_Range("A2", "C2");
            excelcells.Merge(Type.Missing);

            ExcelApp.Cells[1, 1] = "Журнал положенности";


            ExcelApp.Cells[3, 1] = "Наименование";
            ExcelApp.Cells[3, 2] = "В наличии";
            ExcelApp.Cells[3, 3] = "Положено";



            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    ExcelApp.Cells[j + 4, i + 1] = (dataGridView1[i, j].Value).ToString();

                }
            }
            worksheet.get_Range("A3", "C" + (dataGridView1.RowCount + 3)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ExcelApp.Visible = true;
            worksheet.get_Range("A1", "C3").Font.Bold = true;
            worksheet.Columns.ColumnWidth = 15;

            worksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }
    }
}
