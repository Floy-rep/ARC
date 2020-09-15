using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;

namespace Сoursework
{
    public partial class Form2 : Form
    {
        SqlConnection SqlCon;
        Int32 last_id = new int();
        public bool can_change_combo_box = true;

        public Form2()
        {
            InitializeComponent();
        }

        private void sFFFFToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                SqlCommand SqlCom = new SqlCommand("INSERT INTO [Cars] (car_name)VALUES(@car_name)", SqlCon);

                //SqlCom.Parameters.AddWithValue("Id", textBox8.Text);
                SqlCom.Parameters.AddWithValue("car_name", textBox1.Text);

                await SqlCom.ExecuteNonQueryAsync();

                Update_data();
            }
            else
            {
                MessageBox.Show("Введите название машины", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox8.Text))
            {
                SqlCommand SqlCom = new SqlCommand("UPDATE [Cars] SET [car_name]=@car_name WHERE [Id]=@Id", SqlCon);

                SqlCom.Parameters.AddWithValue("Id", textBox8.Text);
                SqlCom.Parameters.AddWithValue("car_name", textBox1.Text);

                await SqlCom.ExecuteNonQueryAsync();

                Update_data();
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox8.Text))
                    MessageBox.Show("Введите название машины", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else if (!string.IsNullOrEmpty(textBox1.Text))
                    MessageBox.Show("Введите ID машины", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                if (Int32.TryParse(textBox9.Text, out int id_for_bd))
                {
                    SqlDataReader SqlReader = null;
                    SqlCommand SqlCom = new SqlCommand("SELECT * FROM [Cars] WHERE [Id]=@Id", SqlCon);
                    SqlCom.Parameters.AddWithValue("Id", id_for_bd);

                    try
                    {
                        SqlReader = await SqlCom.ExecuteReaderAsync();
                        while (await SqlReader.ReadAsync())
                        {
                            listBox5.Items.Clear();
                            listBox5.Items.Add("Удаление " + Convert.ToString(SqlReader["car_name"]));
                            listBox5.Items.Add("прошло успешно");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    finally
                    {
                        if (SqlReader != null)
                            SqlReader.Close();
                    }

                    SqlCom = new SqlCommand("DELETE FROM [Cars] WHERE [Id]=@Id", SqlCon);
                    SqlCom.Parameters.AddWithValue("Id", id_for_bd);
                    await SqlCom.ExecuteNonQueryAsync();

                }
                else
                    MessageBox.Show("Введите корректные данные", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Введите ID машины", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Update_data();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Update_data();
        }

        private async void Update_data()
        {
            string SqlWayconnect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Сoursework\Сoursework\Database1.mdf;Integrated Security=True"; // путь до БД
            SqlCon = new SqlConnection(SqlWayconnect); // Подключение БД
            await SqlCon.OpenAsync();

            // CARS_UPDATE

            SqlDataReader SqlReader = null;
            SqlCommand SqlCom = new SqlCommand("SELECT * FROM [Cars]", SqlCon);

            try
            {
                SqlReader = await SqlCom.ExecuteReaderAsync(); //Сбор данных с таблиц исходы из запроса SqlCom
                listBox1.Items.Clear();
                while (await SqlReader.ReadAsync())
                {
                    listBox1.Items.Add("ID: " + Convert.ToString(SqlReader["Id"]) + ", название машины: " + Convert.ToString(SqlReader["car_name"]));
                    comboBox2.Items.Add(Convert.ToString(SqlReader["car_name"]));
                    last_id = Convert.ToInt32(SqlReader["Id"]);
                }
                textBox8.Text = Convert.ToString(last_id + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (SqlReader != null)
                    SqlReader.Close();
            }

            listBox2.Items.Clear();
            
            // RADIATOR_UPDATE

            SqlCom = new SqlCommand("SELECT * FROM [Radiator]", SqlCon);
            try
            {
                SqlReader = await SqlCom.ExecuteReaderAsync(); //Сбор данных с таблиц исходы из запроса SqlCom
                while (await SqlReader.ReadAsync())
                    listBox2.Items.Add("ID: " + Convert.ToString(SqlReader["Id"]) + ", деталь: Радиатор, цена: " + Convert.ToString(SqlReader["cost"]) + ", для машины: " + Convert.ToString(SqlReader["vihicle"]) + ", бренд: " + Convert.ToString(SqlReader["brand"]));
                listBox2.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (SqlReader != null)
                    SqlReader.Close();
            }

            // BATTERY_UPDATE

            SqlCom = new SqlCommand("SELECT * FROM [Battery]", SqlCon);
            try
            {
                SqlReader = await SqlCom.ExecuteReaderAsync(); //Сбор данных с таблиц исходы из запроса SqlCom
                while (await SqlReader.ReadAsync())
                    listBox2.Items.Add("ID: " + Convert.ToString(SqlReader["Id"]) + ", деталь: Батарея, цена: " + Convert.ToString(SqlReader["cost"]) + ", для машины: " + Convert.ToString(SqlReader["vihicle"]) + ", бренд: " + Convert.ToString(SqlReader["brand"]));
                listBox2.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (SqlReader != null)
                    SqlReader.Close();
            }

            // Air_filter

            SqlCom = new SqlCommand("SELECT * FROM [Air filter]", SqlCon);
            try
            {
                SqlReader = await SqlCom.ExecuteReaderAsync(); //Сбор данных с таблиц исходы из запроса SqlCom
                while (await SqlReader.ReadAsync())
                    listBox2.Items.Add("ID: " + Convert.ToString(SqlReader["Id"]) + ", деталь: Воздушный фильтр, цена: " + Convert.ToString(SqlReader["cost"]) + ", для машины: " + Convert.ToString(SqlReader["vihicle"]) + ", бренд: " + Convert.ToString(SqlReader["brand"]));
                listBox2.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (SqlReader != null)
                    SqlReader.Close();
            }

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            bool data_is_true = false;
            SqlCommand SqlCom = new SqlCommand();

            if (!string.IsNullOrEmpty(textBox11.Text) && Int32.TryParse(textBox11.Text, out int id_for_bd))
            {
                SqlCom = new SqlCommand("SELECT * FROM [Cars] WHERE [Id]=@Id", SqlCon);
                SqlCom.Parameters.AddWithValue("Id", id_for_bd);
                data_is_true = true;
            }
            else if (!string.IsNullOrEmpty(textBox12.Text) && data_is_true == false)
            {
                SqlCom = new SqlCommand("SELECT * FROM [Cars] WHERE [car_name]=@car_name", SqlCon);
                SqlCom.Parameters.AddWithValue("car_name", textBox12.Text);
                data_is_true = true;
            }

            if (data_is_true)
            {
                SqlDataReader SqlReader = null;
                try
                {
                    SqlReader = await SqlCom.ExecuteReaderAsync(); //Сбор данных с таблиц исходы из запроса SqlCom

                    while (await SqlReader.ReadAsync())
                    {
                        listBox3.Items.Clear();
                        listBox3.Items.Add("Найдено:");
                        listBox3.Items.Add(Convert.ToString(SqlReader["Id"]) + "  " + Convert.ToString(SqlReader["car_name"]));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                finally
                {
                    if (SqlReader != null)
                        SqlReader.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите данные", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string ChooseDetail(string detail) 
        {
            switch (detail)
            {
                case "Радиатор":
                    detail = "Radiator";
                    break;

                case "Батарея":
                    detail = "Battery";
                    break;

                case "Воздушный фильтр":
                    detail = "Air filter";
                    break;
            }

            return detail;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                if (Int32.TryParse(textBox7.Text, out int id_for_bd))
                {
                    SqlCommand SqlCom = new SqlCommand("SELECT * FROM [Cars] WHERE [Id]=@Id", SqlCon);
                    SqlCom.Parameters.AddWithValue("Id", id_for_bd);

                    bool can_change = false;
                    if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrEmpty(textBox5.Text))
                        can_change = true;
                    else
                        MessageBox.Show("Введите все данные", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    try
                    {
                        if (can_change == true)
                        {
                            SqlCom = new SqlCommand("UPDATE [" + ChooseDetail(comboBox1.Text) + "] SET [cost]=@cost, [vihicle]=@vihicle, [brand]=@brand  WHERE [Id]=@Id", SqlCon);

                            SqlCom.Parameters.AddWithValue("Id", id_for_bd);
                            SqlCom.Parameters.AddWithValue("cost", textBox3.Text);
                            SqlCom.Parameters.AddWithValue("vihicle", comboBox2.Text);
                            SqlCom.Parameters.AddWithValue("brand", textBox5.Text);

                            await SqlCom.ExecuteNonQueryAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
                else
                    MessageBox.Show("Введите корректные данные", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Введите ID детали", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Update_data();
        }

        private List<string> ArrayDeleteByIndexs(string[] elem, int[] ind ) 
        {
            List<string> answer = new List<string>();

            for (int i = 0; i < elem.Length; i++) 
            {
                if (!ind.Contains(i))
                    answer.Add(elem[i]);
            }
            return answer;
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(listBox2.Text))
            {
                string[] data = listBox2.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                textBox7.Text = string.Join(" ", ArrayDeleteByIndexs(data[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0}).ToArray()); // id 
                comboBox1.Text = string.Join(" ", ArrayDeleteByIndexs(data[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0}).ToArray()); // detail name
                textBox3.Text = string.Join(" ", ArrayDeleteByIndexs(data[2].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0}).ToArray()); // cost
                comboBox2.Text = string.Join(" ", ArrayDeleteByIndexs(data[3].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0,1}).ToArray()); // vihicle
                textBox5.Text = string.Join(" ", ArrayDeleteByIndexs(data[4].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0}).ToArray()); // brand

                tabControl1.SelectedTab = tabPage2;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] data = listBox1.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            textBox8.Text = string.Join(" ", ArrayDeleteByIndexs(data[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0}).ToArray());
            textBox1.Text = string.Join(" ", ArrayDeleteByIndexs(data[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), new int[] {0,1}).ToArray());

            tabControl1.SelectedTab = tabPage1;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrEmpty(textBox3.Text)) 
            {
                try
                {
                    SqlCommand SqlCom = new SqlCommand("INSERT INTO [" + ChooseDetail(comboBox1.Text) + "] (cost, vihicle, brand)VALUES(@cost, @vihicle, @brand)", SqlCon);

                    SqlCom.Parameters.AddWithValue("cost", textBox3.Text);
                    SqlCom.Parameters.AddWithValue("vihicle", comboBox2.Text);
                    SqlCom.Parameters.AddWithValue("brand", textBox5.Text);

                    await SqlCom.ExecuteNonQueryAsync();

                    Update_data();
                }

                catch (Exception ex)
                { 
                        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            bool data_is_true = false;
            SqlCommand SqlCom = new SqlCommand();

            if (!string.IsNullOrEmpty(comboBox3.Text) && Int32.TryParse(textBox4.Text, out int id_for_bd))
            {
                SqlCom = new SqlCommand("SELECT * FROM [" + ChooseDetail(comboBox3.Text) + "] WHERE [Id]=@Id", SqlCon);
                SqlCom.Parameters.AddWithValue("Id", id_for_bd);
                data_is_true = true;
            }

            if (data_is_true)
            {
                SqlDataReader SqlReader = null;
                try
                {
                    SqlReader = await SqlCom.ExecuteReaderAsync(); //Сбор данных с таблиц исходы из запроса SqlCom

                    while (await SqlReader.ReadAsync())
                    {
                        listBox6.Items.Clear();
                        listBox6.Items.Add("Найдено:");
                        listBox6.Items.Add("ID: " + Convert.ToString(SqlReader["Id"]));
                        listBox6.Items.Add("Деталь: " + comboBox3.Text );
                        listBox6.Items.Add("Для машины: " + Convert.ToString(SqlReader["vihicle"]));
                        listBox6.Items.Add("Стоимость: " + Convert.ToString(SqlReader["cost"]));
                        listBox6.Items.Add("Марка: " + Convert.ToString(SqlReader["brand"]));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (SqlReader != null)
                        SqlReader.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите данные", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
