using System;
using System.Data.SqlClient; // Выгрузка данных из SQL
using System.Windows.Forms;

namespace Сoursework
{
    public partial class Form1 : Form
    {

        SqlConnection SqlCon; 

        public Form1()
        {
            InitializeComponent();
        }

        
        private  void Form1_Load(object sender, EventArgs e) // ассинхронное выполнение кода
        {

            //comboBox1.SelectedItem;


            string SqlWayconnect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Сoursework\Сoursework\Database1.mdf;Integrated Security=True"; // путь до БД
            SqlCon = new SqlConnection(SqlWayconnect); // Подключение БД
            SqlCon.Open();

            SqlDataReader SqlReader = null;
            SqlCommand SqlCom = new SqlCommand("SELECT * FROM [Cars]", SqlCon);

            try
            {
                SqlReader = SqlCom.ExecuteReader(); //Сбор данных с таблиц исходы из запроса SqlCom
                while (SqlReader.Read()) 
                {
                    comboBox1.Items.Add(Convert.ToString(SqlReader["car_name"]));
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 form2 = new Form2();
            Form1 form1 = new Form1();
            form2.Close();
            form1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}