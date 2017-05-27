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
    public partial class Techcon : Form
    {
        public static int ID;
        public Techcon()
        {
            InitializeComponent();
        }

        private void Techcon_Load(object sender, EventArgs e)
        {
            DataTable Tablvf = new DataTable();
            SqlDataAdapter Adapvf = new SqlDataAdapter("SELECT * FROM Personal", Static.Con);
            Adapvf.Fill(Tablvf);
            comboBox1.DataSource = Tablvf.DefaultView;
            comboBox1.DisplayMember = "NamePers";
            comboBox1.ValueMember = "ID";

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
                textBox3.Text = Tabl.Rows[0]["NameType"].ToString();
                textBox4.Text = Tabl.Rows[0]["FactNum"].ToString();
                textBox5.Text = Tabl.Rows[0]["InvNum"].ToString();
                textBox6.Text = Tabl.Rows[0]["YearMan"].ToString();
                textBox16.Text = Tabl.Rows[0]["NameSTK"].ToString();
                textBox8.Text = Tabl.Rows[0]["Category"].ToString();
                label19.Text = Tabl.Rows[0]["param1"].ToString();
                label20.Text = Tabl.Rows[0]["param2"].ToString();
                label21.Text = Tabl.Rows[0]["param3"].ToString();
                label22.Text = Tabl.Rows[0]["param4"].ToString();
                label23.Text = Tabl.Rows[0]["param5"].ToString();
                label24.Text = Tabl.Rows[0]["param6"].ToString();
                comboBox1.SelectedValue = int.Parse(Tabl.Rows[0]["RespPerson"].ToString());
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



        private readonly string TempleteFileName = Application.StartupPath + @"\Шаблоны\tehocenka.docx";

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            var typeSTK = textBox3.Text;
            var nameSTK = textBox16.Text;
            var numact = textBox17.Text;
            var numcom = comboBox6.Text;
            var name1 = textBox9.Text;
            var name2 = textBox10.Text;
            var name3 = textBox11.Text;
            var post1 = textBox13.Text;
            var post2 = textBox14.Text;
            var post3 = textBox15.Text;
            var proverka = textBox2.Text;
            var zak = textBox12.Text;
            var datecom = textBox1.Text;
            var factnum = textBox4.Text;
            var invnum = textBox5.Text;
            var yearman = textBox6.Text;
            var category = textBox8.Text;
            var name44 = comboBox1.Text;
            var dateact = dateTimePicker3.Text;
            var zak1 = textBox7.Text;
            var sog = comboBox2.Text;

            var param1 = label19.Text;
            var param2 = label20.Text;
            var param3 = label21.Text;
            var param4 = label22.Text;
            var param5 = label23.Text;
            var param6 = label24.Text;




            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                var wordDocument = wordApp.Documents.Open(TempleteFileName);
                RepalceWordStub("{nameSTK}", nameSTK, wordDocument);
                RepalceWordStub("{typeSTK}", typeSTK, wordDocument);
                RepalceWordStub("{numact}", numact, wordDocument);
                RepalceWordStub("{numcom}", numcom, wordDocument);
                RepalceWordStub("{name1}", name1, wordDocument);
                RepalceWordStub("{name2}", name2, wordDocument);
                RepalceWordStub("{name3}", name3, wordDocument);
                RepalceWordStub("{name11}", name1, wordDocument);
                RepalceWordStub("{name22}", name2, wordDocument);
                RepalceWordStub("{name33}", name3, wordDocument);
                RepalceWordStub("{name44}", name44, wordDocument);
                RepalceWordStub("{post1}", post1, wordDocument);
                RepalceWordStub("{post2}", post2, wordDocument);
                RepalceWordStub("{post3}", post3, wordDocument);
                RepalceWordStub("{nameSTK1}", nameSTK, wordDocument);
                RepalceWordStub("{typeSTK1}", typeSTK, wordDocument);
                RepalceWordStub("{datecom}", datecom, wordDocument);
                RepalceWordStub("{typeSTK2}", typeSTK, wordDocument);
                RepalceWordStub("{nameSTK2}", nameSTK, wordDocument);
                RepalceWordStub("{typeSTK3}", typeSTK, wordDocument);
                RepalceWordStub("{nameSTK3}", nameSTK, wordDocument);
                RepalceWordStub("{yearman}", yearman, wordDocument);
                RepalceWordStub("{factnum}", factnum, wordDocument);
                RepalceWordStub("{dateact}", dateact, wordDocument);
                RepalceWordStub("{dateact2}", dateact, wordDocument);
                RepalceWordStub("{zak1}", zak1, wordDocument);
                RepalceWordStub("{zak}", zak, wordDocument);
                RepalceWordStub("{category}", category, wordDocument);
                RepalceWordStub("{invnum}", invnum, wordDocument);
                RepalceWordStub("{sog}", sog, wordDocument);
                RepalceWordStub("{proverka}", proverka, wordDocument);
                RepalceWordStub("{param1}", param1, wordDocument);
                RepalceWordStub("{param2}", param2, wordDocument);
                RepalceWordStub("{param3}", param3, wordDocument);
                RepalceWordStub("{param4}", param4, wordDocument);
                RepalceWordStub("{param5}", param5, wordDocument);
                RepalceWordStub("{param6}", param6, wordDocument);





                wordDocument.SaveAs(Application.StartupPath + @"\Акты\Оценки технического состояния\Акт оценки технического состояния №" + textBox17.Text + ".docx");
                wordApp.Visible = true;
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения файла");
            }

            string ZapAct;
            {
                ZapAct = string.Format("INSERT INTO JournalTech " +
                   "(NumAct,NameAct,DateCreat,NumInst,Adress,IdSTK) Values('{0}','{1}','{2}','{3}','{4}','{5}')",
                   textBox17.Text, "Акт оценки технического состояния", DateTime.Parse(dateTimePicker3.Text).ToString(), "2", "ФЭС/ОСиСО", ID);
            }

            SqlCommand cmdAct = new SqlCommand(ZapAct, Static.Con);
            cmdAct.ExecuteNonQuery();


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
