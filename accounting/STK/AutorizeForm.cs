using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STK
{
    public partial class AutorizeForm : Form
    {
        public static bool Vhod;
        public static bool Exit;
        public static bool Admin;
        public static int userID = 0;
        public AutorizeForm()
        {
            InitializeComponent();
            Vhod = false;
            Exit = false;
            ConnectDB();
        }


        private int ConnectDB()
        {
            #region Построение строки подключения
            AppSettingsReader ar = new AppSettingsReader();
            string strCon = "Data Source=WIN-9G0RUIFL9IK;Initial Catalog=STK;Trusted_Connection=Yes;";
            #endregion
            #region Подключение к БД
            try
            {
                Static.Con = new SqlConnection();
                Static.Con.ConnectionString = strCon;
                Static.Con.Open();
                return 1;
            }
            catch (Exception)
            {
                MessageBox.Show("SQL сервер не запущен!");
                return 0;
            }
            #endregion
        }





        private void btnOk_Click(object sender, EventArgs e)
        {
            if (TBLogin.Text == "" || MTBPassword.Text == "")
            {
                MessageBox.Show("Введите имя пользователя и пароль!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region Проверка существования пользователя
            DataTable Autor = new DataTable();

            SqlCommand cmd = new SqlCommand("PravPolz", Static.Con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.Char));
            cmd.Parameters.Add(new SqlParameter("@Pas", SqlDbType.Char));
            cmd.Parameters["@Name"].Value = TBLogin.Text;
            if (File.Exists(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath))))
            {
                var stream = new FileStream(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath)), FileMode.Open);
                var sw = new StreamReader(stream);
                sw.ReadLine();
                cmd.Parameters["@Pas"].Value = sw.ReadLine();
                sw.Close();
                stream.Close();
            }
            else cmd.Parameters["@Pas"].Value = Static.GetHashString(MTBPassword.Text);  //
            SqlDataAdapter AutorAdap = new SqlDataAdapter(cmd);
            AutorAdap.Fill(Autor);

            if (Autor.Rows.Count == 0)
            {
                MessageBox.Show("Вы не зарегистрированы!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Autor.Rows[0]["NamePriveleg"].ToString().Contains("Администратор"))
                    Admin = true;
                else
                {
                    Admin = false;
                }
                userID = int.Parse(Autor.Rows[0]["ID"].ToString());
                Vhod = true;

                if (checkBox1.Checked)
                {
                    var stream = new FileStream(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath)), FileMode.Create);
                    var sw = new StreamWriter(stream);
                    sw.WriteLine(Autor.Rows[0]["Login"].ToString());
                    sw.WriteLine(Autor.Rows[0]["Password"].ToString());
                    sw.Close();
                    stream.Close();
                }
                else
                {
                    if (File.Exists(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath))))
                        File.Delete(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath)));
                }

                this.Close();

            }
            #endregion
        }



        private void AutorizeForm_Load(object sender, EventArgs e)
        {

            if (File.Exists(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath), userID)))
            {
                var stream = new FileStream(string.Format("{0}\\pwd.txt", Path.GetDirectoryName(Application.ExecutablePath), userID), FileMode.Open);
                var sw = new StreamReader(stream);
                TBLogin.Text = sw.ReadLine();
                checkBox1.Checked = true;
                MTBPassword.Text = "1234";
                sw.Close();
                stream.Close();
            }
        }

        private void MTBPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(this, EventArgs.Empty);
            }
        }

        private void TBLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(this, EventArgs.Empty);
            }
        }

        private void AutorizeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Exit = true;
        }




    }
}
