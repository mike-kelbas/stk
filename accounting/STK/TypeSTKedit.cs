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
    public partial class TypeSTKedit : Form
    {
        public static int ID;

        public TypeSTKedit()
        {
            InitializeComponent();
        }


        private void TypeSTKedit_Load(object sender, EventArgs e)
        {

            if (ID != 0)
            {
                DataTable Tabl = new DataTable();
                string sql = string.Format("SELECT * FROM TypeSTK WHERE ID={0}", ID);
                SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
                Adap.Fill(Tabl);
                TBNameType.Text = Tabl.Rows[0]["NameType"].ToString();
                TBExpected.Text = Tabl.Rows[0]["Expected"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Zap;
            if (ID != 0)
            {
                Zap = string.Format("UPDATE TypeSTK SET " +
                "NameType='{0}',Expected='{1}' WHERE ID='{2}'",
                TBNameType.Text, TBExpected.Text, ID);

            }
            else
            {
                Zap = string.Format("INSERT INTO TypeSTK " +
                "(NameType,Expected) Values('{0}','{1}')",
                TBNameType.Text, TBExpected.Text);

            }
            SqlCommand cmd = new SqlCommand(Zap, Static.Con);
            cmd.ExecuteNonQuery();
            this.Close();
        }
    }
}
