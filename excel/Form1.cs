using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text;

namespace excel
{
    public partial class ������� : Form
    {
        int failedRowsCount = 0;

        List<analysis> list = new List<analysis>();
        ApplicationDbContext db = new ApplicationDbContext();

        public �������()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readExcel();
        }

        private void readExcel()
        {
            string filePath = @"C:\";

            OpenFileDialog openFileDialog = new OpenFileDialog(); //�������� ����������� ����

            openFileDialog.InitialDirectory = filePath;//���������� ����������

            openFileDialog.Filter = "Excel File (*.xls)|*.xls";//������ ���� ������
            openFileDialog.FilterIndex = 1; //���������� ���������� ������ ������
            openFileDialog.ShowDialog(); //���������� ����
            if (openFileDialog.FileName != null) //������� ���� ���� ������
            {
                textBox1.Text = openFileDialog.FileName;//����� ���� ����� � ����� ����
                filePath = openFileDialog.FileName; //���������� ���������� ��������� ����
            }
            else  //����� ������ ���������
            {
                textBox1.Text = "���� �� ������";
            }

            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet();

                var table = result.Tables[0];
                int number = 0;
                
                for (int i = 4; i < table.Rows.Count - 1; i++)
                {
                    var row = table.Rows[i].ItemArray;
                    
                    analysis validRows = new analysis()
                    {
                        Date_start = ParseSt(row[0]),
                        Change_start = Parse(row[2]),
                        Date_finish = ParseSt(row[3]),
                        Change_finish = Parse(row[4]),
                        period = ParseSt(row[5]),
                        condition = Parse(row[6]),
                        region = Parse(row[7]),
                        device = ParseSt(row[8]),
                        category = ParseSt(row[11]),
                        reason = ParseSt(row[12]),
                        coefficient = ParseSt(row[14]),
                        note = ParseSt(row[15]),

                    };
                    if (String.IsNullOrEmpty(validRows.Date_finish) == false)
                    {
                        list.Add(validRows);
                    }
                    else
                    {
                        failedRowsCount++;
                    }

                    number++;
                }
                
                textBox2.Text = number.ToString();
            }

        }
        public static string ParseSt(object a)
        {
            try
            {
                string b = Convert.ToString(a);
                return b;
            }
            catch
            {
                return "��������� �� ����������";
            }
        }

        public static string Parse(object a)
        {
            try
            {
                string b = Convert.ToString(a);
                return b;
            }
            catch
            {
                return "��� ������";
            }
        }
        public static DateTime ParseDate(object a)
        {
            DateTime b;
            DateTime v = Convert.ToDateTime(null);
            if (DateTime.TryParse(Convert.ToString(a), out b))
                return b;
            else
                return v;

        }
        public static TimeSpan ParsePeriod(object a)
        {
            TimeSpan b;
            string a_2 = Convert.ToString(a);
            if (TimeSpan.TryParse(a_2.Replace("1 ��.", "1 day"), out b))
                return b;
            else
                return TimeSpan.Zero;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            WriteExcel();
        }

        public void WriteExcel()
        {
            int g = 0;
            foreach (analysis excel in list)
            {

                var res = db.Analysis.AsNoTracking().FirstOrDefault(x => x.Date_start == excel.Date_start && x.region == excel.region);
                if (res == null)
                {
                    db.Analysis.Add(excel);
                }
                else
                {
                    failedRowsCount++;
                    excel.Date_start = res.Date_start;
                    excel.region = res.region;
                }
                
                g++;
            }
            textBox3.Text = failedRowsCount.ToString();
            db.SaveChanges();

            MessageBox.Show($"��������� ����� {g}, �� ��������� {failedRowsCount}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}