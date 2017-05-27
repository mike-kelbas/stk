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
    public partial class PersonalForm : Form
    {
        public static int ID;

        public PersonalForm()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void PersonalForm_Load(object sender, EventArgs e)
        {
            ShowPersonal();
            dataGridView1.TopLeftHeaderCell.Value = "№ п/п";
        }


        public void ShowPersonal()
        {

            toolTip1.SetToolTip(btnAdd, "Добавить");
            toolTip1.SetToolTip(BtnEdit, "Изменить");
            toolTip1.SetToolTip(btnDelete, "Удалить");
          

            DataTable Tabl = new DataTable();
            SqlDataAdapter Adap = new SqlDataAdapter("SELECT Personal.ID, Personal.NamePers, Post.NamePost, Personal.Login,  Priveleg.NamePriveleg FROM Personal JOIN Priveleg ON Personal.Priveleg = Priveleg.ID  JOIN Post ON Personal.Post = Post.ID", Static.Con); //SELECT * FROM Personal
            Adap.Fill(Tabl);
            dataGridView1.DataSource = Tabl;

            dataGridView1.Columns[0].HeaderText = "Инд. №";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Фамилия инициалы";
            dataGridView1.Columns[2].HeaderText = "Должность";
            dataGridView1.Columns[3].HeaderText = "Логин";
            dataGridView1.Columns[4].HeaderText = "Тип учетной записи";
        }

       
        

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr; 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void PersonalForm_Activated(object sender, EventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PersonalEdit.ID = 0;
            PersonalEdit personalRdit = new PersonalEdit();
            personalRdit.ShowDialog();
            ShowPersonal();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                DataTable Tabl = new DataTable();
                string Zap = string.Format("DELETE from Personal where ID={0}", ID);
                SqlCommand cmd = new SqlCommand(Zap, Static.Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись успешно удалена");
                ShowPersonal();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления!");
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            PersonalEdit.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            PersonalEdit personalRdit = new PersonalEdit();
            personalRdit.ShowDialog();
            ShowPersonal();
        }

            }
    }
