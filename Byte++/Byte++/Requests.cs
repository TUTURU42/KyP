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

namespace Byte__
{
    public partial class Requests : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        public Requests(Autorization a, MainMenu m, Boolean r)
        {
            InitializeComponent();
            this.mainMenu = m;
            this.autorization = a;
            this.root = r;
        }

        private void button_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.mainMenu.Show();
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(textBox_select.Text, sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_requests.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_id_client.Text == "" || textBox_articul.Text == "" || textBox_count_prod.Text == "" || textBox_date_request.Text == "" || textBox_status_request.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO Requests (id_client, articul, count_products, date_request, status_request) VALUES (@id_client, @articul, @count_products, @date_request, @status_request)",
                    sqlConnection);

                DateTime date_request = DateTime.Parse(textBox_date_request.Text);

                command.Parameters.AddWithValue("id_client", textBox_id_client.Text);
                command.Parameters.AddWithValue("articul", textBox_articul.Text);
                command.Parameters.AddWithValue("count_products", textBox_count_prod.Text);
                command.Parameters.AddWithValue("date_request", $"{date_request.Month}/{date_request.Day}/{date_request.Year}");
                command.Parameters.AddWithValue("status_request", textBox_status_request.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id_request.Text == "")
            {
                MessageBox.Show("Введите id заявки");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Requests WHERE id_request=N'{textBox_id_request.Text}'",
                    sqlConnection);
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка удалена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_change_Click(object sender, EventArgs e)
        {
            if (textBox_id_request.Text == "")
            {
                MessageBox.Show("Введите id заявки");
            }
            else
            {
                if (textBox_id_client.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Requests SET id_client=@id_client WHERE Id_request=@id_request", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_request", $"{textBox_id_request.Text}");
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
                if (textBox_articul.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Requests SET articul=@articul WHERE Id_request=@id_request", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_request", $"{textBox_id_request.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
                if (textBox_count_prod.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Requests SET count_products=@count_prod WHERE Id_request=@id_request", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_request", $"{textBox_id_request.Text}");
                    cmd.Parameters.AddWithValue("@count_prod", $"{textBox_count_prod.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
                if (textBox_date_request.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Requests SET date_request=@date_request WHERE Id_request=@id_request", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_request", $"{textBox_id_request.Text}");
                    DateTime date_request = DateTime.Parse(textBox_date_request.Text);
                    cmd.Parameters.AddWithValue("@date_request", $"{date_request.Month}/{date_request.Day}/{date_request.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
                if (textBox_status_request.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Requests SET status_request=@status_request WHERE Id_request=@id_request", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_request", $"{textBox_id_request.Text}");
                    cmd.Parameters.AddWithValue("@status_request", $"{textBox_status_request.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Requests", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_requests.DataSource = dataset.Tables[0];
        }

        private void Requests_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBByte++"].ConnectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            button_renew_Click(sender, e);
            if (!root)
            {
                textBox_select.Visible = root;
                button_select.Visible = root;
                button_create.Visible = root;
                button_delete.Visible = root;
                button_change.Visible = root;

            }
        }

        private void Requests_FormClosing(object sender, FormClosingEventArgs e)
        {
            autorization.Close();
        }

        private void textBox_id_request_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_request.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Requests where Id_request = @id_request", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_request", $"{textBox_id_request.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_id_client_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_client.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Requests where id_client = @id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_articul_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_requests.DataSource as DataTable).DefaultView.RowFilter = $"articul LIKE '%{textBox_articul.Text}%'";
        }

        private void textBox_count_prod_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_count_prod.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Requests where count_products = @count_prod", sqlConnection);
                    cmd.Parameters.AddWithValue("@count_prod", $"{textBox_count_prod.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_date_request_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_request.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Requests where date_request = @date_request", sqlConnection);
                    DateTime date_request = DateTime.Parse(textBox_date_request.Text);
                    cmd.Parameters.AddWithValue("@date_request", $"{date_request.Month}/{date_request.Day}/{date_request.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_requests.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }

        private void textBox_status_request_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_requests.DataSource as DataTable).DefaultView.RowFilter = $"status_request LIKE '%{textBox_status_request.Text}%'";
        }
    }
}
