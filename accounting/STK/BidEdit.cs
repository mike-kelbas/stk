using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STK
{
    public partial class BidEdit : Form
    {
        private readonly int? _id;

        private bool IsAddMode => _id == null;

        public BidEdit(): this(null, string.Empty) { }

        public BidEdit(int? bidId, string text)
        {
            InitializeComponent();

            _id = bidId;
            this.BidText.Text = text;
        }

        private void save_Click(object sender, EventArgs e)
        {
            var bidText = this.BidText.Text;
            if (string.IsNullOrEmpty(bidText))
            {
                MessageBox.Show("Введите пожалуйста текст заявки.");
                return;
            }

            this.SaveBid(_id, bidText);
            this.Close();
        }

        private void SaveBid(int? id, string text)
        {
            var query = this.IsAddMode == false
                ? "update Bids set " +
                  $"Text = '{ text }' " +
                  $"where ID = { id } and Person = { AutorizeForm.UserId };"
                : "insert into Bids " + "(Person, Date, Text) " +
                  $"Values({ AutorizeForm.UserId }, cast(getdate() as date), '{ text }')";

            var cmd = new SqlCommand(query, Static.Con);
            cmd.ExecuteNonQuery();
        }
    }
}