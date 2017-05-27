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
    public partial class PostEdit : Form
    {
        public static int ID;

        public PostEdit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Zap;
            if (ID != 0)
            {
                Zap = string.Format("UPDATE Post SET " +
                "NamePost='{0}' WHERE ID='{1}'",
                TBNamePost.Text, ID);
            }
            else
            {
                Zap = string.Format("INSERT INTO Post " +
                "(NamePost) Values('{0}')",
                TBNamePost.Text);

            }
            SqlCommand cmd = new SqlCommand(Zap, Static.Con);
            cmd.ExecuteNonQuery();
            this.Close();
        }

        private void PostEdit_Load(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                DataTable Tabl = new DataTable();
                string sql = string.Format("SELECT * FROM Post WHERE ID={0}", ID);
                SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
                Adap.Fill(Tabl);
                TBNamePost.Text = Tabl.Rows[0]["NamePost"].ToString();
            }
        }

       
    }
}
