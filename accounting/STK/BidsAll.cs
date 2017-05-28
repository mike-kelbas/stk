using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STK
{
    public partial class BidsAll : Form
    {
        public BidsAll()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Bids_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            var tabl = new DataTable();
            var adap = new SqlDataAdapter("SELECT Personal.NamePers, Bids.Date,Bids.Text, Bids.ID  FROM Bids JOIN Personal ON Bids.Person = Personal.ID", Static.Con); //SELECT * FROM Personal
            adap.Fill(tabl);

            dataGridView1.DataSource = tabl;
            dataGridView1.Columns[0].HeaderText = "Сотрудник";
            dataGridView1.Columns[1].HeaderText = "Дата отправки";
            dataGridView1.Columns[2].HeaderText = "Текст сообщения";
            dataGridView1.Columns[3].Visible = false;
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;

            if (header == null || !header.Equals(indexStr))
            {
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
            } 
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var currentBidId = this.GetCurrentBidId();
            if (currentBidId != null)
            {
                this.DeleteBid(currentBidId.Value);
                this.Bids_Load(null, null);
            }
        }

        private void DeleteBid(int id)
        {
            var query = $"delete from Bids where ID = { id };";
            var cmd = new SqlCommand(query, Static.Con);

            cmd.ExecuteNonQuery();
        }

        private int? GetCurrentBidId()
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                return null;
            }

            return (int)selectedRows[0].Cells["ID"].Value;
        }
    }
}