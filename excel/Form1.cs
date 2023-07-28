using ExcelDataReader;
using System.Globalization;
using System.Text;

namespace excel
{
    public partial class Простои : Form
    {

        List<analysis> list = new List<analysis>();
        ApplicationDbContext db = new ApplicationDbContext();

        public Простои()
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
            if (openFileDialog.FileName != null) //условие если файл выбран
            {
                textBox1.Text = openFileDialog.FileName;//вывод пути файла в текст бокс
                filePath = openFileDialog.FileName; //присвоение переменной выбранный файл
            }
            else  //иначе пустой текстбокс
            {
                textBox1.Text = "Файл не выбран";
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
                    list.Add(new analysis()
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

                    });
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
                return "категория не определена";
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


        private void button2_Click(object sender, EventArgs e)
        {
            WriteExcel();
        }

        public void WriteExcel()
        {
            int g = 0;
            foreach (analysis excel in list)
            {
                db.Analysis.Add(excel);
                g++;
            }
            db.SaveChanges();

            MessageBox.Show($"Выгружено строк {g}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}