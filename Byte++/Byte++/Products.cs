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
    
    public partial class Products : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        public Products(Autorization a, MainMenu m, Boolean r)
        {
            InitializeComponent();
            this.autorization = a;
            this.mainMenu = m;
            this.root = r;
        }

        private void Products_Load(object sender, EventArgs e)
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

        private void Products_FormClosing(object sender, FormClosingEventArgs e)
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
            dataGridView_products.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_name_prod.Text == "" || textBox_id_position.Text == "" || textBox_articul.Text == "" || textBox_pallet_numb.Text == "" || textBox_quality_cond.Text == "" || textBox_count.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command;
                if (textBox_date_manufacture.Text != "" && textBox_date_expir.Text != "")
                {
                   command = new SqlCommand(
                   $"INSERT INTO Products (name_product, id_position, articul, pallet_number, quality_condition, count, date_expiration, date_manufacture) VALUES (@name_product, @id_position, @articul, @pallet_number, @quality_condition, @count, @date_expiration, @date_manufacture)",
                   sqlConnection);
                    DateTime date_manufacture = DateTime.Parse(textBox_date_manufacture.Text);
                    command.Parameters.AddWithValue("date_manufacture", $"{date_manufacture.Month}/{date_manufacture.Day}/{date_manufacture.Year}");
                    DateTime date_expiration = DateTime.Parse(textBox_date_expir.Text);
                }
                else if(textBox_date_manufacture.Text != "")
                {
                   command = new SqlCommand(
                   $"INSERT INTO Products (name_product, id_position, articul, pallet_number, quality_condition, count, date_manufacture) VALUES (@name_product, @id_position, @articul, @pallet_number, @quality_condition, @count, @date_manufacture)",
                   sqlConnection);
                    DateTime date_manufacture = DateTime.Parse(textBox_date_manufacture.Text);
                    command.Parameters.AddWithValue("date_manufacture", $"{date_manufacture.Month}/{date_manufacture.Day}/{date_manufacture.Year}");
                }
                else if(textBox_date_expir.Text != "")
                {
                   command = new SqlCommand(
                   $"INSERT INTO Products (name_product, id_position, articul, pallet_number, quality_condition, count, date_expiration) VALUES (@name_product, @id_position, @articul, @pallet_number, @quality_condition, @count, @date_expiration)",
                   sqlConnection);
                    DateTime date_expiration = DateTime.Parse(textBox_date_expir.Text);
                    command.Parameters.AddWithValue("date_expiration", $"{date_expiration.Month}/{date_expiration.Day}/{date_expiration.Year}");
                }
                else
                {
                   command = new SqlCommand(
                   $"INSERT INTO Products (name_product, id_position, articul, pallet_number, quality_condition, count) VALUES (@name_product, @id_position, @articul, @pallet_number, @quality_condition, @count)",
                   sqlConnection);
                }
                command.Parameters.AddWithValue("name_product", textBox_name_prod.Text);
                command.Parameters.AddWithValue("id_position", textBox_id_position.Text);
                command.Parameters.AddWithValue("articul", textBox_articul.Text);
                command.Parameters.AddWithValue("pallet_number", textBox_pallet_numb.Text);
                command.Parameters.AddWithValue("count", textBox_count.Text);
                command.Parameters.AddWithValue("quality_condition", textBox_quality_cond.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);

            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_articul.Text == ""||textBox_id_position.Text=="")
            {
                MessageBox.Show("Введите артикул и id позиции");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Products WHERE id_position=N'{textBox_id_position.Text}' AND articul=N'{textBox_articul}'",
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
            if (textBox_articul.Text == "" || textBox_id_position.Text == "")
            {
                MessageBox.Show("Введите артикул и id позиции");
            }
            else
            {
                if (textBox_name_prod.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET name_product=@name_product WHERE id_position=@id_position AND articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@name_product", $"{textBox_name_prod.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
                if (textBox_pallet_numb.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET pallet_number=@pallet_number WHERE id_position=@id_position AND articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@pallet_number", $"{textBox_pallet_numb.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
                if (textBox_quality_cond.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET quality_condition=@quality_condition WHERE id_position=@id_position AND articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@quality_condition", $"{textBox_quality_cond.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
                if (textBox_count.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET count=@count WHERE id_position=@id_position AND articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@count", $"{textBox_count.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
                if (textBox_date_manufacture.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET date_manufacture=@date_manufacture WHERE id_position=@id_position AND articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    DateTime date_manufacture = DateTime.Parse(textBox_date_manufacture.Text);
                    cmd.Parameters.AddWithValue("@date_manufacture", $"{date_manufacture.Month}/{date_manufacture.Day}/{date_manufacture.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
                if (textBox_date_expir.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Products SET date_expiration=@date_expiration WHERE id_position=@id_position AND articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    DateTime date_expiration = DateTime.Parse(textBox_date_manufacture.Text);
                    cmd.Parameters.AddWithValue("@date_expiration", $"{date_expiration.Month}/{date_expiration.Day}/{date_expiration.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Products", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_products.DataSource = dataset.Tables[0];
        }

        private void textBox_name_prod_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_products.DataSource as DataTable).DefaultView.RowFilter = $"name_product LIKE '%{textBox_name_prod.Text}%'";
        }

        private void textBox_id_position_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Products where id_position = @id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_articul_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_products.DataSource as DataTable).DefaultView.RowFilter = $"articul LIKE '%{textBox_articul.Text}%'";
        }

        private void textBox_pallet_numb_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_products.DataSource as DataTable).DefaultView.RowFilter = $"pallet_number LIKE '%{textBox_pallet_numb.Text}%'";
        }

        private void textBox_quality_cond_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_products.DataSource as DataTable).DefaultView.RowFilter = $"quality_condition LIKE '%{textBox_quality_cond.Text}%'";
        }

        private void textBox_count_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_count.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Products where count = @count", sqlConnection);
                    cmd.Parameters.AddWithValue("@count", $"{textBox_count.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_date_expir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_expir.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Product where date_expiration = @date_expiration", sqlConnection);
                    DateTime date_expiration = DateTime.Parse(textBox_date_expir.Text);
                    cmd.Parameters.AddWithValue("@date_expiration", $"{date_expiration.Month}/{date_expiration.Day}/{date_expiration.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }


        private void textBox_date_manufacture_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_manufacture.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Product where date_manufacture = @date_manufacture", sqlConnection);
                    DateTime date_manufacture = DateTime.Parse(textBox_date_manufacture.Text);
                    cmd.Parameters.AddWithValue("@date_manufacture", $"{date_manufacture.Month}/{date_manufacture.Day}/{date_manufacture.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_products.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }
    }
}
