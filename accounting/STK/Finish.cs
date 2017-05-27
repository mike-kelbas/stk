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
    public partial class Finish : Form
    {
        public static int ID;

        public Finish()
        {
            InitializeComponent();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void Finish_Load(object sender, EventArgs e)
        {
            DataTable TablNum = new DataTable();
            SqlDataAdapter AdapNum = new SqlDataAdapter("SELECT DISTINCT ID,NumCom FROM Commission ORDER BY NumCom", Static.Con);
            AdapNum.Fill(TablNum);
            comboBox6.DisplayMember = "NumCom";
            comboBox6.ValueMember = "ID";
            comboBox6.DataSource = TablNum.DefaultView;

            if (ID != 0)
            {
                DataTable Tabl = new DataTable();
                string sql = string.Format("SELECT t.NameType,* FROM STK  s JOIN TypeSTK t  ON s.TypeSTK = t.ID  WHERE s.ID={0}", ID);
                SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
                Adap.Fill(Tabl);
                textBox18.Text = Tabl.Rows[0]["NameType"].ToString();
                textBox19.Text = Tabl.Rows[0]["NameSTK"].ToString();
                label5.Text = Tabl.Rows[0]["factNum"].ToString();
                label8.Text = Tabl.Rows[0]["yearMan"].ToString();
                label10.Text = Tabl.Rows[0]["invNum"].ToString();
                label9.Text = DateTime.Parse(Tabl.Rows[0]["dateCom"].ToString()).ToShortDateString();

            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ID = comboBox6.SelectedValue.ToString();
            DataTable Tabl = new DataTable();
            string sql = string.Format("SELECT c.*, p.NamePers as pName, d.NamePost as pPost, p1.NamePers as p1Name, d1.NamePost  as p1Post, p2.NamePers as p2Name, d2.NamePost as p2Post, p3.NamePers as p3Name, d3.NamePost as p3Post FROM Commission c JOIN Personal p ON p.ID=c.ComChair JOIN Personal p1 ON p1.ID=c.Member1 JOIN Personal p2 ON p2.ID=c.Member2 JOIN Personal p3 ON p3.ID=c.Member3 JOIN Post d ON d.ID =p.Post JOIN Post d1 ON d1.ID=p1.Post JOIN Post d2 ON d2.ID=p2.Post JOIN Post d3 ON d3.ID=p3.Post WHERE c.ID={0}", ID);
            SqlDataAdapter Adap = new SqlDataAdapter(sql, Static.Con);
            Adap.Fill(Tabl);
            textBox9.Text = Tabl.Rows[0]["pName"].ToString();
            textBox10.Text = Tabl.Rows[0]["p1Name"].ToString();
            textBox11.Text = Tabl.Rows[0]["p2Name"].ToString();
            textBox13.Text = Tabl.Rows[0]["pPost"].ToString();
            textBox14.Text = Tabl.Rows[0]["p1Post"].ToString();
            textBox15.Text = Tabl.Rows[0]["p2Post"].ToString();
            textBox1.Text = DateTime.Parse(Tabl.Rows[0]["DateCreat"].ToString()).ToShortDateString();
        }

        private readonly string TempleteFileName = Application.StartupPath + @"\Шаблоны\spisanie.docx";

        private void button19_Click(object sender, EventArgs e)
        {
            var numAct = textBox17.Text;
            var typeSTK = textBox18.Text;
            var nameSTK = textBox19.Text;
            var numCom = comboBox6.Text;
            var dateCreate = textBox1.Text;
            var factNum = label5.Text;
            var yearMan = label8.Text;
            var dateCom = label9.Text;
            var invNum = label10.Text;
            var date = dateTimePicker1.Text;
            var name1 = textBox9.Text;
            var name2 = textBox10.Text;
            var name3 = textBox11.Text;
            var post1 = textBox13.Text;
            var post2 = textBox14.Text;
            var post3 = textBox15.Text;
            var osn = textBox12.Text;

            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                var wordDocument = wordApp.Documents.Open(TempleteFileName);

                RepalceWordStub("{numAct}", numAct, wordDocument);
                RepalceWordStub("{typeSTK}", typeSTK, wordDocument);
                RepalceWordStub("{nameSTK}", nameSTK, wordDocument);
                RepalceWordStub("{numCom}", numCom, wordDocument);
                RepalceWordStub("{dateCreate}", dateCreate, wordDocument);
                RepalceWordStub("{factNum}", factNum, wordDocument);
                RepalceWordStub("{invNum}", invNum, wordDocument);
                RepalceWordStub("{yearMan}", yearMan, wordDocument);
                RepalceWordStub("{dateCom}", dateCom, wordDocument);
                RepalceWordStub("{date}", date, wordDocument);
                RepalceWordStub("{name1}", name1, wordDocument);
                RepalceWordStub("{name2}", name2, wordDocument);
                RepalceWordStub("{name3}", name3, wordDocument);
                RepalceWordStub("{post1}", post1, wordDocument);
                RepalceWordStub("{post2}", post2, wordDocument);
                RepalceWordStub("{post3}", post3, wordDocument);
                RepalceWordStub("{osn}", osn, wordDocument);
                RepalceWordStub("{name1}", name1, wordDocument);
                RepalceWordStub("{name2}", name2, wordDocument);
                RepalceWordStub("{name3}", name3, wordDocument);
                RepalceWordStub("{name11}", name1, wordDocument);
                RepalceWordStub("{name22}", name2, wordDocument);
                RepalceWordStub("{name33}", name3, wordDocument);
                RepalceWordStub("{typeSTK2}", typeSTK, wordDocument);
                RepalceWordStub("{nameSTK2}", nameSTK, wordDocument);

                wordDocument.SaveAs(Application.StartupPath + @"\Акты\Списания\Акт списания №" + textBox17.Text + ".docx");
                wordApp.Visible = true;
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения файла");
            }

            string ZapAct;
            ZapAct = string.Format("INSERT INTO JournalTech " + "(NumAct,NameAct,DateCreat,NumInst,Adress,IdSTK) Values('{0}','{1}','{2}','{3}','{4}','{5}')", textBox17.Text, "Акт списания", DateTime.Parse(dateTimePicker1.Text).ToString(), "2", "ФЭС/ОСиСО", ID);
            SqlCommand cmdAct = new SqlCommand(ZapAct, Static.Con);
            cmdAct.ExecuteNonQuery();

            string Zap;
            Zap = string.Format("UPDATE STK SET " +  "WriteOff='{0}' WHERE ID='{1}'", DateTime.Parse(dateTimePicker1.Text).ToString(), ID);
            SqlCommand cmd = new SqlCommand(Zap, Static.Con);
            cmd.ExecuteNonQuery();



            this.Close();




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
