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
    public partial class PersonalEdit : Form
    {
        public static int ID;

        public PersonalEdit()
        {
            InitializeComponent();

        }

        private void PersonalEdit_Load(object sender, EventArgs e)
        {
            DataTable Tablvf = new DataTable();
            SqlDataAdapter Adapvf = new SqlDataAdapter("SELECT * FROM Priveleg", Static.Con);
            Adapvf.Fill(Tablvf);
            CBPriveleg.DataSource = Tablvf.DefaultView;
            CBPriveleg.DisplayMember = "NamePriveleg";
            CBPriveleg.ValueMember = "ID";


            DataTable Tablvfv = new DataTable();
            SqlDataAdapter Adapvfv = new SqlDataAdapter("SELECT * FROM Post", Static.Con);
            Adapvfv.Fill(Tablvfv);
            CBPost.DataSource = Tablvfv.DefaultView;
            CBPost.DisplayMember = "NamePost";
            CBPost.ValueMember = "ID";





            if (ID != 0)
            {
                DataTable Tabl = new DataTable();
                string sql = string.Format("SELECT * FROM Personal WHERE ID={0}", ID);
                SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
                Adap.Fill(Tabl);
                TBNamePers.Text = Tabl.Rows[0]["NamePers"].ToString();
                
                TBLogin.Text = Tabl.Rows[0]["Login"].ToString();
                MTBPassword.Text = Tabl.Rows[0]["Password"].ToString();
                CBPriveleg.SelectedValue = int.Parse(Tabl.Rows[0]["Priveleg"].ToString());
                CBPost.SelectedValue = int.Parse(Tabl.Rows[0]["Post"].ToString());
            }
        }

 

        private void btnSave_Click(object sender, EventArgs e)
        {
            string Zap;
            if (ID != 0)
            {
                Zap = string.Format("UPDATE Personal SET " +
                "NamePers='{0}',Post='{1}',Login='{2}',Password='{3}',Priveleg='{4}' WHERE ID={5}",
                TBNamePers.Text, CBPost.SelectedValue,  TBLogin.Text, Static.GetHashString(MTBPassword.Text), CBPriveleg.SelectedValue, ID);
          
            }
            else
            {
                Zap = string.Format("INSERT INTO Personal " +
                "(NamePers,Post,Login,Password,Priveleg) Values('{0}','{1}','{2}','{3}','{4}')",
                TBNamePers.Text, CBPost.SelectedValue,  TBLogin.Text, Static.GetHashString(MTBPassword.Text), CBPriveleg.SelectedValue);
                    
            }
            SqlCommand cmd = new SqlCommand(Zap, Static.Con);
            cmd.ExecuteNonQuery();
            this.Close();
            }
    }
}
