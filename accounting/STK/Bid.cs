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
    public partial class Bid : Form
    {
        public static int userID=0;
        public Bid()
        {
            InitializeComponent();
        }

        public void ShowBid()
        {
            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT Date, Text FROM BIDS WHERE Person=" + userID, Static.Con); //SELECT * FROM Personal
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;

        }


        private void Bid_Load(object sender, EventArgs e)
        {
            ShowBid();
            

            dataGridView1.Columns[0].HeaderText = "Дата отправки";
            dataGridView1.Columns[1].HeaderText = "Сообщение";
        }
    }
}
