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
    public partial class ComingConsumption : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        public ComingConsumption(Autorization x, MainMenu m, Boolean r)
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
            this.root = r;
        }

        private void ComingConsumption_Load(object sender, EventArgs e)
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

        private void ComingConsumption_FormClosing(object sender, FormClosingEventArgs e)
        {
            autorization.Close();
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
            dataGridView_com.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_articul.Text == "" || textBox_pallet_number.Text == "" || (textBox_id_supplier.Text == "" && textBox_id_client.Text == "") || textBox_date_operation.Text == "" || textBox_count.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command;
                if(textBox_id_supplier.Text!=""&&textBox_id_client.Text!="")
                {
                    command = new SqlCommand(
                    $"INSERT INTO СomingConsumption (articul, pallet_number, id_supplier, id_client, date_operation, count) VALUES (@articul, @pallet_number, @id_supplier, @id_client, @date_operation, @count)",
                    sqlConnection);
                    command.Parameters.AddWithValue("id_supplier", textBox_id_supplier.Text);
                    command.Parameters.AddWithValue("id_client", textBox_id_client.Text);
                }
                else if(textBox_id_supplier.Text != "")
                {
                    command = new SqlCommand(
                    $"INSERT INTO СomingConsumption (articul, pallet_number, id_supplier, date_operation, count) VALUES (@articul, @pallet_number, @id_supplier, @date_operation, @count)",
                    sqlConnection);
                    command.Parameters.AddWithValue("id_supplier", textBox_id_supplier.Text);
                }
                else if(textBox_id_client.Text != "")
                {
                    command = new SqlCommand(
                    $"INSERT INTO СomingConsumption (articul, pallet_number, id_client, date_operation, count) VALUES (@articul, @pallet_number, @id_client, @date_operation, @count)",
                    sqlConnection);
                    command.Parameters.AddWithValue("id_client", textBox_id_client.Text);
                }
                else
                {
                    command = new SqlCommand();
                    MessageBox.Show("Введите id поставщика или id клиента");
                }
                

                DateTime date_operation = DateTime.Parse(textBox_date_operation.Text);

                command.Parameters.AddWithValue("articul", textBox_articul.Text);
                command.Parameters.AddWithValue("pallet_number", textBox_pallet_number.Text);
                command.Parameters.AddWithValue("date_operation", $"{date_operation.Month}/{date_operation.Day}/{date_operation.Year}");
                command.Parameters.AddWithValue("count", textBox_count.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id_document.Text == "")
            {
                MessageBox.Show("Введите id документа");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM СomingConsumption WHERE Id_document=N'{textBox_id_document.Text}'",
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
            if (textBox_id_document.Text == "")
            {
                MessageBox.Show("Введите id документа");
            }
            else
            {
                if (textBox_articul.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE СomingConsumption SET articul=@articul WHERE Id_document=@id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
                if (textBox_pallet_number.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE СomingConsumption SET pallet_number=@pallet_number WHERE Id_document=@id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    cmd.Parameters.AddWithValue("@pallet_number", $"{textBox_pallet_number.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
                if (textBox_id_supplier.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE СomingConsumption SET id_supplier=@id_supplier WHERE Id_document=@id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
                if (textBox_id_client.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE СomingConsumption SET id_client=@id_client WHERE Id_document=@id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
                if (textBox_date_operation.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE СomingConsumption SET date_operation=@date_operation WHERE Id_document=@id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    DateTime date_operation = DateTime.Parse(textBox_date_operation.Text);
                    cmd.Parameters.AddWithValue("@date_operation", $"{date_operation.Month}/{date_operation.Day}/{date_operation.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }

                if (textBox_count.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE СomingConsumption SET count=@count WHERE Id_document=@id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    cmd.Parameters.AddWithValue("@count", $"{textBox_count.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM СomingConsumption", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_com.DataSource = dataset.Tables[0];
        }

        private void textBox_id_document_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_document.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from ComingConsumption where id_document = @id_document", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_document", $"{textBox_id_document.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_articul_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_com.DataSource as DataTable).DefaultView.RowFilter = $"articul LIKE '%{textBox_articul.Text}%'";
        }

        private void textBox_pallet_number_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_com.DataSource as DataTable).DefaultView.RowFilter = $"pallet_number LIKE '%{textBox_pallet_number.Text}%'";
        }

        private void textBox_id_supplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_supplier.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from ComingConsumption where id_supplier = @id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
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
                    SqlCommand cmd = new SqlCommand("Select * from ComingConsumption where id_client = @id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_clientt", $"{textBox_id_client.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_date_operation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_date_operation.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from ComingConsumption where date_operation = @date_operation", sqlConnection);
                    DateTime date_operation = DateTime.Parse(textBox_date_operation.Text);
                    cmd.Parameters.AddWithValue("@date_operation", $"{date_operation.Month}/{date_operation.Day}/{date_operation.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }

        private void textBox_count_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_count.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from ComingConsumption where count = @count", sqlConnection);
                    cmd.Parameters.AddWithValue("@count", $"{textBox_count.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_com.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }
    }
}
