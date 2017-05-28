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

namespace STK.Resources
{
    public partial class STKedit : Form
    {
        public static int ID;

        public STKedit()
        {
            InitializeComponent();
        }

        private void STKedit_Load(object sender, EventArgs e)
        {
            DataTable Tablvf = new DataTable();
            SqlDataAdapter Adapvf = new SqlDataAdapter("SELECT * FROM TypeSTK", Static.Con);
            Adapvf.Fill(Tablvf);
            CBTypeSTK.DataSource = Tablvf.DefaultView;
            CBTypeSTK.DisplayMember = "NameType";
            CBTypeSTK.ValueMember = "ID";
                    
            if (ID != 0)
            {
                DataTable Tabl = new DataTable();
                string sql = string.Format("SELECT * FROM STK WHERE ID={0}", ID);
                SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
                Adap.Fill(Tabl);
                TBNameSTK.Text = Tabl.Rows[0]["NameSTK"].ToString();
                TBfactnum.Text = Tabl.Rows[0]["FactNum"].ToString();
                TByearman.Text = Tabl.Rows[0]["YearMan"].ToString();
                TBlocation.Text = Tabl.Rows[0]["Location"].ToString();
                TBnote.Text = Tabl.Rows[0]["Note"].ToString();
                textBox1.Text = Tabl.Rows[0]["Param1"].ToString();
                textBox2.Text = Tabl.Rows[0]["Param2"].ToString();
                textBox3.Text = Tabl.Rows[0]["Param3"].ToString();
                textBox4.Text = Tabl.Rows[0]["Param4"].ToString();
                textBox5.Text = Tabl.Rows[0]["Param5"].ToString();
                textBox6.Text = Tabl.Rows[0]["Param6"].ToString();
             

                CBTypeSTK.SelectedValue = int.Parse(Tabl.Rows[0]["TypeSTK"].ToString());
            }

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string Zap;
            if (ID != 0)
            {
                Zap = string.Format("UPDATE STK SET " +
                "NameSTK='{0}',  TypeSTK='{1}', FactNum='{2}', YearMan='{3}', Location='{4}', Note='{5}', Param1='{6}', Param2='{7}',Param3='{8}',Param4='{9}',Param5='{10}',Param6='{11}' WHERE ID='{12}'",
                TBNameSTK.Text, CBTypeSTK.SelectedValue, TBfactnum.Text, Convert.ToInt32(TByearman.Text), TBlocation.Text, TBnote.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text,   ID);

            }
            else
            {
                Zap = string.Format("INSERT INTO STK " +
                "(NameSTK,TypeSTK,FactNum,YearMan,Location,Note,Param1,Param2,Param3,Param4,Param5,Param6) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')",
                TBNameSTK.Text, CBTypeSTK.SelectedValue, TBfactnum.Text, Convert.ToInt32(TByearman.Text), TBlocation.Text, TBnote.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);

            }
            SqlCommand cmd = new SqlCommand(Zap, Static.Con);
            cmd.ExecuteNonQuery();
            this.Close();  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnSave_Click_1(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnSave_Click_1(sender, e);
        }
    }
}