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
using Word = Microsoft.Office.Interop.Word;

namespace STK
{
    public partial class Repair : Form
    {
        public static int ID;

        public Repair()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Repair_Load(object sender, EventArgs e)
        {
            DataTable Tablvf = new DataTable();
            SqlDataAdapter Adapvf = new SqlDataAdapter("SELECT * FROM Personal", Static.Con);
            Adapvf.Fill(Tablvf);
            comboBox1.DataSource = Tablvf.DefaultView;
            comboBox1.DisplayMember = "NamePers";
            comboBox1.ValueMember = "ID";


            if (ID != 0)
            {
                DataTable Tabl = new DataTable();
                string sql = string.Format("SELECT t.NameType,* FROM STK  s JOIN TypeSTK t  ON s.TypeSTK = t.ID  WHERE s.ID={0}", ID);
                SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
                Adap.Fill(Tabl);

                textBox1.Text = Tabl.Rows[0]["NameType"].ToString();
                textBox2.Text = Tabl.Rows[0]["NameSTK"].ToString();
                label1.Text = Tabl.Rows[0]["factNum"].ToString();

            }
        }

        private readonly string TempleteFileName = Application.StartupPath + @"\Шаблоны\reklamaciy.docx";

        private void button17_Click(object sender, EventArgs e)
        {
            var typeSTK = textBox1.Text;
            var nameSTK = textBox2.Text;
            var factNum = label1.Text;
            var date = dateTimePicker3.Text;
            var date1 = dateTimePicker2.Text;
            var date2 = dateTimePicker1.Text;
            var numAct = textBox17.Text;
            var def = textBox3.Text;
            var prich = textBox4.Text;
            var podlej = textBox5.Text;
            var zamena = textBox6.Text;



            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                var wordDocument = wordApp.Documents.Open(TempleteFileName);
                RepalceWordStub("{typeSTK}", typeSTK, wordDocument);
                RepalceWordStub("{nameSTK}", nameSTK, wordDocument);
                RepalceWordStub("{factNum}", factNum, wordDocument);
                RepalceWordStub("{date}", date, wordDocument);
                RepalceWordStub("{date1}", date1, wordDocument);
                RepalceWordStub("{date2}", date2, wordDocument);
                RepalceWordStub("{numAct}", numAct, wordDocument);
                RepalceWordStub("{def}", def, wordDocument);
                RepalceWordStub("{prich}", prich, wordDocument);
                RepalceWordStub("{podlej}", podlej, wordDocument);
                RepalceWordStub("{zamena}", zamena, wordDocument);


                wordDocument.SaveAs(Application.StartupPath + @"\Акты\Рекламации\Акт рекламациию №" + textBox17.Text + ".docx");
                wordApp.Visible = true;
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения файла");
            }
            
            string ZapAct;
                ZapAct = string.Format("INSERT INTO JournalTech " +
                   "(NumAct,NameAct,DateCreat,NumInst,Adress,IdSTK) Values('{0}','{1}','{2}','{3}','{4}','{5}')",
                   textBox17.Text, "Акт рекламации", DateTime.Parse(dateTimePicker1.Text).ToString(), "2", "Мастерская ВОУДО/ОСиСО", ID);
            SqlCommand cmdAct = new SqlCommand(ZapAct, Static.Con);
            cmdAct.ExecuteNonQuery();

            string Zap;
            Zap = string.Format("INSERT INTO JournalRep" +
                   "(IdSTK,DateDisk,Defect,PersonDisc,DateSub,DateRet) Values('{0}','{1}','{2}','{3}','{4}','{5}')",
                  ID, DateTime.Parse(dateTimePicker2.Text).ToString(), textBox3.Text, comboBox1.SelectedValue, DateTime.Parse(dateTimePicker1.Text).ToString(), DateTime.Parse(dateTimePicker3.Text).ToString());
            SqlCommand cmd = new SqlCommand(Zap, Static.Con);
            cmd.ExecuteNonQuery();

            this.Close();  
        }


        private void RepalceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }


    }
}
