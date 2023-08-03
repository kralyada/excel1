using ExcelDataReader;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace excel
{
    public partial class ѕростои : Form
    {



        List<analysis> list = new List<analysis>();
        ApplicationDbContext db = new ApplicationDbContext();

        public ѕростои()
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

            OpenFileDialog openFileDialog = new OpenFileDialog(); //открытие диалогового окна

            openFileDialog.InitialDirectory = filePath;//присвоение переменной

            openFileDialog.Filter = "Excel File (*.xls)|*.xls";//фильтр типа файлов
            openFileDialog.FilterIndex = 1; //количество возможного выбора файлов
            openFileDialog.ShowDialog(); //диалоговое окно
            if (!string.IsNullOrEmpty(openFileDialog.FileName)) // условие, если файл выбран
            {
                textBox1.Text = openFileDialog.FileName; // вывод пути файла в текстовом поле
                filePath = openFileDialog.FileName; // присвоение выбранного файла переменной
            }
            else // иначе пустое текстовое поле
            {
                textBox1.Text = "‘айл не выбран";
                return; // выход из метода, если файл не выбран
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
                        period = ConvertPeriodFormat(ParseSt(row[5])),
                        condition = Parse(row[6]),
                        region = Parse(row[7]),
                        device = ParseSt(row[8]),
                        category = ParseSt(row[11]),
                        reason = ParseSt(row[12]),
                        coefficient = ParseSt(row[14]),
                        note = ParseSt(row[15]),

                    };

                    list.Add(validRows);



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
                return "категори€ не определена";
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
                return "нет данных";
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
            if (TimeSpan.TryParse(a_2.Replace("1 дн.", "1 day"), out b))
                return b;
            else
                return TimeSpan.Zero;
        }

        public static string ConvertPeriodFormat(string period)
        {
            {
                // ѕровер€ем, соответствует ли поле region формату "число дней часы:минуты"
                Regex regex = new Regex(@"(\d+)дн\. (\d{2}):(\d{2})");
                Match match = regex.Match(period);
                Regex minSec = new Regex(@"(\d{2}):(\d{2})");
                Match minMatch = minSec.Match(period);


                if (minMatch.Success)
                {
                    int hours = int.Parse(minMatch.Groups[1].Value);
                    int minutes = int.Parse((minMatch.Groups[2].Value));
                    int totalMinutes = hours * 60 + minutes;
                    return totalMinutes.ToString();
                }

                if (match.Success)
                {
                    int days = int.Parse(match.Groups[1].Value);
                    int hours = int.Parse(match.Groups[2].Value);
                    int minutes = int.Parse(match.Groups[3].Value);

                    // ѕреобразуем в формат "часы:минуты" формата типа 1дн. 08:49
                    int totalMinutes = days * 24 * 60 + hours * 60 + minutes;
                    return totalMinutes.ToString();
                }

                return period.ToString(); // ≈сли формат region не соответствует ожидаемому, возвращаем исходное значение
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            WriteExcel();
        }

        public void WriteExcel()
        {

            int g = 0;
            int updateCount = 0;
            int failedRowsCountAdd = 0;
            foreach (analysis excel in list)
            {

                //excel.period= ConvertPeriodFormat(excel.period);

                var res = db.Analysis.AsNoTracking().FirstOrDefault(x => x.Date_start == excel.Date_start && x.region == excel.region);

                if (res == null)
                {
                    //if (!String.IsNullOrEmpty(excel.period))
                    //{
                    //    excel.period = ConvertPeriodFormat(excel.period); 
                    //}

                    db.Analysis.Add(excel);
                    g++;
                }
                else
                {
                    if (String.IsNullOrEmpty(res.Date_finish) == true)
                    {

                        res.Date_finish = excel.Date_finish;
                        db.Analysis.Update(res);
                        //textBox3.Text = res.Date_finish;
                        updateCount++;
                    }
                    failedRowsCountAdd++;
                    excel.Date_start = res.Date_start;
                    excel.region = res.region;
                }

            }

            db.SaveChanges();

            MessageBox.Show($"¬ыгружено {g} строк, не выгружено в таблицу {failedRowsCountAdd}, обновлено {updateCount}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

               private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}