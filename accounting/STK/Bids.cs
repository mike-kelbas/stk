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
    public partial class Bids : Form
    {
        public Bids()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
         
        }

        private void Bids_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT Personal.NamePers, Bids.Date,Bids.Text  FROM Bids JOIN Personal  ON Bids.Person = Personal.ID", Static.Con); //SELECT * FROM Personal
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;



            dataGridView1.Columns[0].HeaderText = "Сотрудник";
            dataGridView1.Columns[1].HeaderText = "Дата отправки";
            dataGridView1.Columns[2].HeaderText = "Текст сообщения";
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr; 
        }
    }
}
