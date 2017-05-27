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
    public partial class JournalRem : Form
    {
        public JournalRem()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void JournalRem_Load(object sender, EventArgs e)
        {
            ShowJournalRem();
            dataGridView1.ClearSelection();

        }

        public void ShowJournalRem()
        {
            dataGridView1.TopLeftHeaderCell.Value = "№ п/п";
            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("Select NameType,NameSTK, DateDisk, Defect,NamePers, DateSub, DateRet From JournalRep Join STK On STK.id = JournalRep.IdSTK Join Personal on Personal.ID = JournalRep.PersonDisc Join TypeSTK on STK.TypeSTK=TypeSTK.ID", Static.Con);
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;

            dataGridView1.Columns[0].HeaderText = "Тип техники";
            dataGridView1.Columns[1].HeaderText = "Наименование техники";
            dataGridView1.Columns[2].HeaderText = "Дата выхода из строя";
            dataGridView1.Columns[3].HeaderText = "Неисправность";
            dataGridView1.Columns[4].HeaderText = "Лицо обнаружевшее неисправность";
            dataGridView1.Columns[5].HeaderText = "Дата отправки в ремонт";
            dataGridView1.Columns[6].HeaderText = "Дата возврата";

        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr; 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private Excel.Range excelcells;

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)ExcelApp.Sheets[1];

            excelcells = worksheet.get_Range("A1", "G1");
            excelcells.Merge(Type.Missing);
            excelcells = worksheet.get_Range("A2", "G2");
            excelcells.Merge(Type.Missing);

            ExcelApp.Cells[1, 1] = "Журнал отправки техники в ремонт";


            ExcelApp.Cells[3, 1] = "Тип";
            ExcelApp.Cells[3, 2] = "Наименование";
            ExcelApp.Cells[3, 3] = "Дата выхода из строя";
            ExcelApp.Cells[3, 4] = "Неисправность";
            ExcelApp.Cells[3, 5] = "Лицо обнаружевшее деффект";
            ExcelApp.Cells[3, 6] = "Дата отправки в мастерскую";
            ExcelApp.Cells[3, 7] = "Дата возвращенияиз мастерской";


            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    ExcelApp.Cells[j + 4, i + 1] = (dataGridView1[i, j].Value).ToString();

                }
            }
            worksheet.get_Range("A3", "G" + (dataGridView1.RowCount + 3)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ExcelApp.Visible = true;
            worksheet.get_Range("A1", "G3").Font.Bold = true;
            worksheet.Columns.ColumnWidth = 15;

            worksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        private void JournalRem_Activated(object sender, EventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }
    }
}
