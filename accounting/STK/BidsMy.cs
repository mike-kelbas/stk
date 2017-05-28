using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STK
{
    public partial class BidsMy : Form
    {
        public BidsMy()
        {
            InitializeComponent();
        }

        public void ShowBid()
        {
            var tabl = new DataTable();
            var adap = new SqlDataAdapter("SELECT ID, Date, Text FROM BIDS WHERE Person=" + AutorizeForm.UserId, Static.Con); //SELECT * FROM Personal

            adap.Fill(tabl);
            dataGridView1.DataSource = tabl;
        }

        private void Bid_Load(object sender, EventArgs e)
        {
            ShowBid();

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Дата отправки";
            dataGridView1.Columns[2].HeaderText = "Сообщение";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowBidForm(null, string.Empty);
        }

        private void EditBid_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                return;
            }

            var firstSelectedRow = selectedRows[0];
            var currentBidId = (int)firstSelectedRow.Cells["ID"].Value;
            var currentBidText = (string)firstSelectedRow.Cells["Text"].Value;

            this.ShowBidForm(currentBidId, currentBidText);
        }

        private void DeleteBid_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                return;
            }

            var currnentBid = (int)selectedRows[0].Cells["ID"].Value;
            this.DeleteBid(currnentBid);
            this.ShowBid();
        }

        private void DeleteBid(int id)
        {
            var query = $"delete from Bids where ID = { id } and Person = { AutorizeForm.UserId }";

            var cmd = new SqlCommand(query, Static.Con);
            cmd.ExecuteNonQuery();
        }

        private void ShowBidForm(int? bidId, string bidText)
        {
            using (var bid = new BidEdit(bidId, bidText))
            {
                bid.Closed += (o, ea) => { this.ShowBid(); };
                bid.ShowDialog();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.CurrentRow.Selected = true;
            }
        }
    }
}