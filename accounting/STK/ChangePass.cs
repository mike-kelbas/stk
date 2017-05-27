using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STK.Resources
{
    public partial class ChangePass : Form
    {
        public static int userID = 0;
        public ChangePass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable Autor = new DataTable();
            var oldPwd = Static.GetHashString(maskedTextBox1.Text);
            var cmdold = new SqlCommand(string.Format("SELECT * FROM Personal WHERE ID={0} AND Password='{1}'", userID, oldPwd), Static.Con);
            SqlDataAdapter AutorAdap = new SqlDataAdapter(cmdold);
            AutorAdap.Fill(Autor);
            if (Autor.Rows.Count == 0)
            {
                MessageBox.Show("Старый пароль указан неверно", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                if (maskedTextBox2.Text == maskedTextBox3.Text)
                {
                    var newPwd = maskedTextBox3.Text;
                    var cmd = new SqlCommand(string.Format("UPDATE Personal SET Password='{0}' WHERE ID={1}", Static.GetHashString(newPwd), userID), Static.Con);
                    cmd.ExecuteNonQuery();
                    if (File.Exists(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath))))
                        File.Delete(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath)));
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            


        }
    }
}
