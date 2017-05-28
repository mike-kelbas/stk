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

namespace STK
{
    public partial class TypeSTK : Form
    {
        public static int ID;

        public TypeSTK()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void TypeSTK_Load(object sender, EventArgs e)
        {
            ShowTypeSTK();
            dataGridView1.TopLeftHeaderCell.Value = "№ п/п";
        }

        public void ShowTypeSTK ()
        {
            toolTip1.SetToolTip(button3, "Добавить");
            toolTip1.SetToolTip(button1, "Изменить");
            toolTip1.SetToolTip(button2, "Удалить");
   
            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT *  FROM TypeSTK", Static.Con);
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;

            dataGridView1.Columns[0].HeaderText = "Инд. №";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Потробность, шт.";
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

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void TypeSTK_Activated(object sender, EventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TypeSTKedit1.ID = 0;
            TypeSTKedit1 typeSTKEdit = new TypeSTKedit1();
            typeSTKEdit.ShowDialog();
            ShowTypeSTK();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TypeSTKedit1.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            TypeSTKedit1 typeSTKEdit = new TypeSTKedit1();
            typeSTKEdit.ShowDialog();
            ShowTypeSTK(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                DataTable Tabl = new DataTable();
                string Zap = string.Format("DELETE from TypeSTK where ID={0}", ID);
                SqlCommand cmd = new SqlCommand(Zap, Static.Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись успешно удалена");
                ShowTypeSTK();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления!");
            }
        }
    }
}