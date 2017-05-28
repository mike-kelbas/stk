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
        public static int UserId;

        public AutorizeForm()
        {
            InitializeComponent();
            Vhod = false;
            Exit = false;
            ConnectDB();
        }

        private int ConnectDB()
        {
            var strCon = "Data Source=WIN-9G0RUIFL9IK;Initial Catalog=STK;Trusted_Connection=Yes;";

            try
            {
                Static.Con = new SqlConnection();
                Static.Con.ConnectionString = strCon;
                Static.Con.Open();

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL сервер не запущен!");

                return 0;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (TBLogin.Text == "" || MTBPassword.Text == "")
            {
                MessageBox.Show("Введите имя пользователя и пароль!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка существования пользователя
            var cmd = new SqlCommand("PravPolz", Static.Con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.Char));
            cmd.Parameters.Add(new SqlParameter("@Pas", SqlDbType.Char));
            cmd.Parameters["@Pas"].Value = Static.GetHashString(MTBPassword.Text);
            cmd.Parameters["@Name"].Value = TBLogin.Text;
            //cmd.Parameters["@Pas"].Value = MTBPassword.Text;

            //if (File.Exists($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt"))
            //{
            //    var stream = new FileStream($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt", FileMode.Open);
            //    var sw = new StreamReader(stream);
            //    sw.ReadLine();
            //    cmd.Parameters["@Pas"].Value = sw.ReadLine();
            //    sw.Close();
            //    stream.Close();
            //}
            //else
            //{
            //    cmd.Parameters["@Pas"].Value = Static.GetHashString(MTBPassword.Text);
            //}

            var autorAdap = new SqlDataAdapter(cmd);
            var users = new DataTable();

            autorAdap.Fill(users);

            if (users.Rows.Count == 0)
            {
                MessageBox.Show("Вы не зарегистрированы!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Admin = users.Rows[0]["NamePriveleg"].ToString().Contains("Администратор");
                UserId = int.Parse(users.Rows[0]["ID"].ToString());
                Vhod = true;

                //if (checkBox1.Checked)
                //{
                //    var stream = new FileStream($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt", FileMode.Create);
                //    var sw = new StreamWriter(stream);
                //    sw.WriteLine(users.Rows[0]["Login"].ToString());
                //    sw.WriteLine(users.Rows[0]["Password"].ToString());
                //    sw.Close();
                //    stream.Close();
                //}
                //else
                //{
                //    if (File.Exists($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt"))
                //    {
                //        File.Delete($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt");
                //    }
                //}

                this.Close();
            }
        }

        //private void AutorizeForm_Load(object sender, EventArgs e)
        //{
        //    if (File.Exists($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt"))
        //    {
        //        var stream = new FileStream($"{Path.GetDirectoryName(Application.ExecutablePath)}\\pwd.txt", FileMode.Open);
        //        var sw = new StreamReader(stream);
        //        TBLogin.Text = sw.ReadLine();
        //        checkBox1.Checked = true;
        //        MTBPassword.Text = "1234";
        //        sw.Close();
        //        stream.Close();
        //    }
        //}

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