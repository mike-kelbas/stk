using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace STK
{
    public partial class JournalSTK : Form
    {
        public JournalSTK()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void JournalSTK_Load(object sender, EventArgs e)
        {
            ShowJournalSTK();
            dataGridView1.ClearSelection();
        }

        public void ShowJournalSTK()
        {
            dataGridView1.TopLeftHeaderCell.Value = "№ п/п";

            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT TypeSTK.NameType, STK.NameSTK, STK.InvNum, STK.FactNum, STK.YearMan, STK.DateCom, STK.Location, Personal.NamePers, STK.WriteOff, STK.Note FROM STK JOIN TypeSTK  ON STK.TypeSTK = TypeSTK.ID LEFT JOIN Personal  ON STK.RespPerson = Personal.ID", Static.Con);
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;

            dataGridView1.Columns[0].HeaderText = "Тип техники";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Инвентарный номер";
            dataGridView1.Columns[3].HeaderText = "Заводской номер";
            dataGridView1.Columns[4].HeaderText = "Год выпуска";
            dataGridView1.Columns[5].HeaderText = "Дата ввода в эксплуатацию";
            dataGridView1.Columns[6].HeaderText = "Место нахождения";
            dataGridView1.Columns[7].HeaderText = "Материально ответственное лицо";
            dataGridView1.Columns[8].HeaderText = "Дата списания";
            dataGridView1.Columns[9].HeaderText = "Примечание";
        }

        private void dataGridView1_RowPrePaint_1(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
            {
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
            } 
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

        private void JournalSTK_Activated(object sender, EventArgs e)
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

            excelcells = worksheet.get_Range("A1", "M1");
            excelcells.Merge(Type.Missing);
            excelcells = worksheet.get_Range("A2", "M2");
            excelcells.Merge(Type.Missing);

            ExcelApp.Cells[1, 1] = "Журнал учета";
            ExcelApp.Cells[2, 1] = "техники на организации";

            ExcelApp.Cells[3, 1] = "Тип";
            ExcelApp.Cells[3, 2] = "Наименование";
            ExcelApp.Cells[3, 3] = "Инвентарный номер";
            ExcelApp.Cells[3, 4] = "Заводской номер";
            ExcelApp.Cells[3, 5] = "Год выпуска";
            ExcelApp.Cells[3, 6] = "Дата ввода в эксплуатацию";
            ExcelApp.Cells[3, 10] = "Место нахождения";
            ExcelApp.Cells[3, 11] = "Материально ответственное лицо";
            ExcelApp.Cells[3, 12] = "Дата списания";
            ExcelApp.Cells[3, 13] = "Примечание";

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    ExcelApp.Cells[j + 4, i + 1] = (dataGridView1[i, j].Value).ToString();
                    
                }
            }

            worksheet.get_Range("A3", "M" + (dataGridView1.RowCount+3)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous; 
            ExcelApp.Visible = true;
            worksheet.get_Range("A1", "M3").Font.Bold = true;
            worksheet.Columns.ColumnWidth = 15;

            worksheet.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.Cells.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }
    }
}