using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Byte__    // название проекта
{
    public partial class Suppliers : Form
    {
        private SqlConnection sqlConnection = null;   // создаем ссылку для подключения к бд
        Autorization autorization;  // создаем ссылки для окна авторизации и меню, что можно было их закрыть при выходе из программы или вернуться к ним назад
        MainMenu mainMenu;
        Boolean root;   // переменная для определения роли пользователя (админ или нет)
        public Suppliers(Autorization x, MainMenu m, Boolean r) // конструктор, где инициализируем окно и присваиваем ранее созданным ссылкам объекты
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
            this.root = r;
        }

        private void button_menu_Click(object sender, EventArgs e) // кнопка меню, скрываем нынешнее окно и возвращаем окно Меню
        {
            this.Hide();
            this.mainMenu.Show();
        }

        private void Suppliers_FormClosing(object sender, FormClosingEventArgs e) // при закрытии окна, закрываем и окно авторизации
        {
            autorization.Close();
        }


        private void Suppliers_Load(object sender, EventArgs e) // метод при загрузки текущего окна, создаем подключение к бд и открываем его, если он не открыт
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBByte++"].ConnectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            button_renew_Click(sender, e); // вызываем метод обновления данных в таблице
            if (!root) // если пользователь не админ, то скрываем не нужные для него объекты
            {
                textBox_select.Visible = root;
                button_select.Visible = root;
                button_create.Visible = root;
                button_delete.Visible = root;
                button_change.Visible = root;

            }
        }

        private void button_select_Click(object sender, EventArgs e) // обработка события при нажатии на кнопку select, создаем запрос в бд и выводим его в окно
        {
            SqlDataAdapter adapter = new SqlDataAdapter(textBox_select.Text, sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_suppliers.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e) // обработка события при нажатии на кнопку Создать, проверяем ввел ли пользователь все необходимые данные, добавляем новую запись в бд, выводим уведомление об успехе и обновляем таблицу
        {
            if (textBox_name_org.Text == "" || textBox_contract_num.Text == "" || textBox_phone.Text == "" || textBox_adress_org.Text == "" || textBox_date_concl.Text == "" || textBox_date_exp.Text == "" || textBox_status.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO Suppliers (name_organization, contract_number, phone, adress_organization, date_conclusion, date_expiration, status) VALUES (@name_organization, @contract_number, @phone, @adress_organization, @date_conclusion, @date_expiration, @status)",
                    sqlConnection);

                DateTime date_concl = DateTime.Parse(textBox_date_concl.Text);
                DateTime date_exp = DateTime.Parse(textBox_date_exp.Text);

                command.Parameters.AddWithValue("name_organization", textBox_name_org.Text);
                command.Parameters.AddWithValue("contract_number", textBox_contract_num.Text);
                command.Parameters.AddWithValue("phone", textBox_phone.Text);
                command.Parameters.AddWithValue("adress_organization", textBox_adress_org.Text);
                command.Parameters.AddWithValue("date_conclusion", $"{date_concl.Month}/{date_concl.Day}/{date_concl.Year}");
                command.Parameters.AddWithValue("date_expiration", $"{date_exp.Month}/{date_exp.Day}/{date_exp.Year}");
                command.Parameters.AddWithValue("status", textBox_status.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e) // обработка события при нажатии на кнопку Обновить, создаем запрос на вывод всей таблицы в бд и отобраем это в окне
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Suppliers", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_suppliers.DataSource = dataset.Tables[0];
        }

        private void button_delete_Click(object sender, EventArgs e) // обработка события при нажатии на кнопку Удалить, просим ввести первичный ключ для однозначного удаления записи в таблице
        {
            if (textBox_id_supplier.Text == "")
            {
                MessageBox.Show("Введите id поставщика");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Suppliers WHERE id_supplier=N'{textBox_id_supplier.Text}'",
                    sqlConnection);
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка удалена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_change_Click(object sender, EventArgs e) // обработка события нажатия на кнопку Изменить, изменяем при необходимости каждый столбец по отдельности
        {
            if (textBox_id_supplier.Text == "")
            {
                MessageBox.Show("Введите id поставщика");
            }
            else
            {
                if (textBox_name_org.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET name_organization=@name_org WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    cmd.Parameters.AddWithValue("@name_org", $"{textBox_name_org.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                if (textBox_contract_num.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET contract_number=@contract_num WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    cmd.Parameters.AddWithValue("@contract_num", $"{textBox_contract_num.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                if (textBox_phone.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET phone=@phone WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    cmd.Parameters.AddWithValue("@phone", $"{textBox_phone.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                if (textBox_adress_org.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET adress_organization=@adress_org WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    cmd.Parameters.AddWithValue("@adress_org", $"{textBox_adress_org.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                if (textBox_date_concl.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET date_conclusion=@date_concl WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    DateTime date_concl = DateTime.Parse(textBox_date_concl.Text);
                    cmd.Parameters.AddWithValue("@date_concl", $"{date_concl.Month}/{date_concl.Day}/{date_concl.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                if (textBox_date_exp.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET date_expiration=@date_exp WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    DateTime date_exp = DateTime.Parse(textBox_date_exp.Text);
                    cmd.Parameters.AddWithValue("@date_exp", $"{date_exp.Month}/{date_exp.Day}/{date_exp.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                if (textBox_status.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Suppliers SET status=@status WHERE Id_supplier=@id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    cmd.Parameters.AddWithValue("@status", $"{textBox_status.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void textBox_name_org_TextChanged(object sender, EventArgs e) // реализуем фильтрацию(поиск) для каждого окошка данных 
        {
            (dataGridView_suppliers.DataSource as DataTable).DefaultView.RowFilter = $"name_organization LIKE '%{textBox_name_org.Text}%'";
        }

        private void textBox_contract_num_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_suppliers.DataSource as DataTable).DefaultView.RowFilter = $"contract_number LIKE '%{textBox_contract_num.Text}%'";
        }

        private void textBox_phone_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_suppliers.DataSource as DataTable).DefaultView.RowFilter = $"phone LIKE '%{textBox_phone.Text}%'";
        }

        private void textBox_adress_org_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_suppliers.DataSource as DataTable).DefaultView.RowFilter = $"adress_organization LIKE '%{textBox_adress_org.Text}%'";
        }

        private void textBox_status_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_suppliers.DataSource as DataTable).DefaultView.RowFilter = $"status LIKE '%{textBox_status.Text}%'";
        }

        private void textBox_date_concl_KeyDown(object sender, KeyEventArgs e) // та же фильтрация, только для нестрокогого элемента (и фильтруется не при каждом введеном символе, а после нажатия Enter)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_concl.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Suppliers where date_conclusion = @date_concl", sqlConnection);
                    DateTime date_concl = DateTime.Parse(textBox_date_concl.Text);
                    cmd.Parameters.AddWithValue("@date_concl", $"{date_concl.Month}/{date_concl.Day}/{date_concl.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }

        private void textBox_date_exp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_exp.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Suppliers where date_expiration = @date_exp", sqlConnection);
                    DateTime date_exp = DateTime.Parse(textBox_date_exp.Text);
                    cmd.Parameters.AddWithValue("@date_exp", $"{date_exp.Month}/{date_exp.Day}/{date_exp.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }

        private void textBox_id_supplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_id_supplier.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Suppliers where Id_supplier = @id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_suppliers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }
    }
}
