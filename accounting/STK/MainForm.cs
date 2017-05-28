using STK.Resources;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace STK
{
    public partial class MainForm : Form
    {
        public static int ID;
        public static int userID = 0;
        public static bool isAdmin = false;

        public MainForm()
        {
            InitializeComponent();
            ShowSTK();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void ShowSTK()
        {
            if (!isAdmin)
            {
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button7.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button11.Visible = false;
                btnAdd.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
                button17.Visible = false;
                button18.Visible = false;
                button19.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = false;
                JournalsToolStripMenuItem.Visible = false;
                SpravToolStripMenuItem.Visible = false;
                FuncToolStripMenuItem.Visible = false;
                contextMenuStrip1.Enabled = false;
            }
            
            dataGridView1.TopLeftHeaderCell.Value = "№ п/п";

            if (isAdmin)
            {
                DataTable Tabl = new DataTable();
                SqlDataAdapter Adap = new SqlDataAdapter("SELECT STK.ID, TypeSTK.NameType, STK.NameSTK, STK.InvNum, STK.FactNum, STK.YearMan, STK.DateCom, STK.Location,  Personal.NamePers, STK.WriteOff, STK.Note   FROM STK JOIN TypeSTK ON STK.TypeSTK = TypeSTK.ID LEFT JOIN Personal  ON STK.RespPerson = Personal.ID", Static.Con);
                Adap.Fill(Tabl);
                dataGridView1.DataSource = Tabl;
            }
            else
            {
                DataTable Tabl = new DataTable();
                SqlDataAdapter Adap = new SqlDataAdapter("SELECT STK.ID, TypeSTK.NameType, STK.NameSTK, STK.InvNum, STK.FactNum, STK.YearMan, STK.DateCom, STK.Location,  Personal.NamePers, STK.WriteOff, STK.Note   FROM STK JOIN TypeSTK ON STK.TypeSTK = TypeSTK.ID LEFT JOIN Personal  ON STK.RespPerson = Personal.ID WHERE STK.RespPerson=" + userID, Static.Con);
                Adap.Fill(Tabl);
                dataGridView1.DataSource = Tabl;
            }

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Тип техники";
            dataGridView1.Columns[2].HeaderText = "Наименование";
            dataGridView1.Columns[3].HeaderText = "Инвентарный номер";
            dataGridView1.Columns[4].HeaderText = "Заводской номер";
            dataGridView1.Columns[5].HeaderText = "Год выпуска";
            dataGridView1.Columns[6].HeaderText = "Дата ввода в эксплуатацию";
            dataGridView1.Columns[7].HeaderText = "Место нахождения";
            dataGridView1.Columns[8].HeaderText = "Материально ответственное лицо";
            dataGridView1.Columns[9].HeaderText = "Дата списания";
            dataGridView1.Columns[10].HeaderText = "Примечание";

            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowSTK();

            toolTip1.SetToolTip(button2, "Журнал учета техники");
            toolTip1.SetToolTip(button3, "Журнал отправки в ремонт");
            toolTip1.SetToolTip(button4, "Журнал технической эксплуатации");
            toolTip1.SetToolTip(button5, "Табель положенности");
            toolTip1.SetToolTip(button7, "Заявки");
            toolTip1.SetToolTip(button8, "Типы техники");
            toolTip1.SetToolTip(button9, "Сотрудники");
            toolTip1.SetToolTip(button11, "Должности");
            toolTip1.SetToolTip(btnAdd, "Добавить запись");
            toolTip1.SetToolTip(button13, "Изменить запись");
            toolTip1.SetToolTip(button14, "Удалить запись");
            toolTip1.SetToolTip(button15, "Ввести в эксплуатацию");
            toolTip1.SetToolTip(button16, "Назначить категорию");
            toolTip1.SetToolTip(button17, "Отправить в ремонт");
            toolTip1.SetToolTip(button18, "Модернизировать");
            toolTip1.SetToolTip(button19, "Списть");
            toolTip1.SetToolTip(button21, "Отправить заявку");
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button9_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы действительно хотите выйти?", "Закрытие программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes) this.Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var personalForm = new PersonalForm();
            personalForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var journalSTK = new JournalSTK();
            journalSTK.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            STKedit.ID = 0;
            STKedit STKRdit = new STKedit();
            STKRdit.ShowDialog();
            ShowSTK();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            STKedit.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            STKedit STKRdit = new STKedit();
            STKRdit.ShowDialog();
            ShowSTK();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                dataGridView1.CurrentRow.Selected = true;
            }
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (isAdmin)
            {
                STKedit.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                STKedit STKRdit = new STKedit();
                STKRdit.ShowDialog();
                ShowSTK();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow.Index + 1).ToString();
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = dataGridView1.RowCount.ToString();
            label6.Text = (dataGridView1.CurrentRow?.Index + 1)?.ToString() ?? string.Empty;
            
            var selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            label4.Text = selectedRowCount.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
                DataTable Tabl = new DataTable();
                string Zap = string.Format("DELETE from STK where ID={0}", ID);
                SqlCommand cmd = new SqlCommand(Zap, Static.Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись успешно удалена");
                ShowSTK();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления!");
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button13_Click(sender, e);
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button14_Click(sender, e);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button13_Click(sender, e);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button14_Click(sender, e);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var typeSTK = new TypeSTK();
            typeSTK.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var postForm = new PostForm();
            postForm.ShowDialog();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ChangePass.userID = userID;
            var changePass = new ChangePass();
            changePass.ShowDialog();
        }

        private void типыСТКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button11_Click(sender, e);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var journalRem = new JournalRem();
            journalRem.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var journalTech = new JournalTech();
            journalTech.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var expForm = new ExpForm();
            expForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var plan = new Plan();
            plan.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var bids = new BidsAll();
            bids.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            StartExp.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            var startExp = new StartExp();
            startExp.ShowDialog();
            ShowSTK();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var bid = new BidsMy();
            bid.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            Repair.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            var repair = new Repair();
            repair.ShowDialog();
            ShowSTK();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Modern.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            var modern = new Modern();
            modern.ShowDialog();
            ShowSTK();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Techcon.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            var techcon = new Techcon();
            techcon.ShowDialog();
            ShowSTK();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Finish.ID = (int)dataGridView1.SelectedRows[0].Cells["ID"].Value;
            var finish = new Finish();
            finish.ShowDialog();
            ShowSTK();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var info = new info();
            info.ShowDialog();
        }

        private void журналУчетаСТКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void журналОтправкиВРемонтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click_1(sender, e);
        }

        private void журналТехническойЭксплуатацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void табельПоложенностиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e);
        }

        private void планЭксплуатацииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button6_Click(sender, e);
        }

        private void заявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void ввестиВЭксплуатациюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button15_Click(sender, e);
        }

        private void назначитьКатегориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button16_Click(sender, e);
        }

        private void отправитьВРемонтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button17_Click(sender, e);
        }

        private void модернизироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button18_Click(sender, e);
        }

        private void списатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button19_Click(sender, e);
        }

        private void сменитьПарольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button20_Click(sender, e);
        }

        private void отправитьЗаявкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button21_Click(sender, e);
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button10_Click(sender, e);
        }
    }
}