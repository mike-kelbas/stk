using System;
using System.Data;
using System.Data.SqlClient;
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

        private void ConnectDB()
        {
            var strCon = "Data Source=WIN-9G0RUIFL9IK;Initial Catalog=STK;Trusted_Connection=Yes;";

            try
            {
                Static.Con = new SqlConnection {ConnectionString = strCon};
                Static.Con.Open();
            }
            catch
            {
                MessageBox.Show("SQL сервер не запущен!");
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

                this.Close();
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