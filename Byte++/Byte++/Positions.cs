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
    public partial class Positions : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        public Positions(Autorization x, MainMenu m, Boolean r)
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
            this.root = r;
        }

        private void Positions_Load(object sender, EventArgs e)
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

        private void Positions_FormClosing(object sender, FormClosingEventArgs e)
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
            dataGridView_positions.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_alley.Text == "" || textBox_place.Text == "" || textBox_level.Text == "" || textBox_status_position.Text == "" || textBox_height.Text == "" || textBox_width.Text == "" || textBox_depth.Text == "" || textBox_lifting_capacity.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO Positions (alley, place, level, status_position, height, width, depth, lifting_capacity) VALUES (@alley, @place, @level, @status_position, @height, @width, @depth, @lifting_capacity)",
                    sqlConnection);


                command.Parameters.AddWithValue("alley", textBox_alley.Text);
                command.Parameters.AddWithValue("place", textBox_place.Text);
                command.Parameters.AddWithValue("level", textBox_level.Text);
                command.Parameters.AddWithValue("status_position", textBox_status_position.Text);
                command.Parameters.AddWithValue("height", textBox_height.Text);
                command.Parameters.AddWithValue("width", textBox_width.Text);
                command.Parameters.AddWithValue("depth", textBox_depth.Text);
                command.Parameters.AddWithValue("lifting_capacity", textBox_lifting_capacity.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id_position.Text == "")
            {
                MessageBox.Show("Введите id позиции");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Positions WHERE Id_position=N'{textBox_id_position.Text}'",
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
            if (textBox_id_position.Text == "")
            {
                MessageBox.Show("Введите id позиции");
            }
            else
            {
                if (textBox_alley.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET alley=@alley WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@alley", $"{textBox_alley.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_place.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET place=@place WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@place", $"{textBox_place.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_level.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET level=@level WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@level", $"{textBox_level.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_status_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET status_position=@status_position WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@status_position", $"{textBox_status_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_height.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET height=@height WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@height", $"{textBox_height.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_width.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET width=@width WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@width", $"{textBox_width.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_depth.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET depth=@depth WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@depth", $"{textBox_depth.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                if (textBox_lifting_capacity.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Positions SET lifting_capacity=@lifting_capacity WHERE Id_position=@id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    cmd.Parameters.AddWithValue("@lifting_capacity", $"{textBox_lifting_capacity.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Positions", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_positions.DataSource = dataset.Tables[0];
        }

        private void textBox_id_position_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where id_position = @id_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_position", $"{textBox_id_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_alley_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_alley.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where alley = @alley", sqlConnection);
                    cmd.Parameters.AddWithValue("@alley", $"{textBox_alley.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_place_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_place.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where place = @place", sqlConnection);
                    cmd.Parameters.AddWithValue("@place", $"{textBox_place.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_level_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_level.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where level = @level", sqlConnection);
                    cmd.Parameters.AddWithValue("@level", $"{textBox_level.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_status_position_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_positions.DataSource as DataTable).DefaultView.RowFilter = $"status_position LIKE '%{textBox_status_position.Text}%'";
        }

        private void textBox_height_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_height.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where height = @height", sqlConnection);
                    cmd.Parameters.AddWithValue("@height", $"{textBox_height.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_width_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_width.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where width = @width", sqlConnection);
                    cmd.Parameters.AddWithValue("@width", $"{textBox_width.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_depth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_depth.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where depth = @depth", sqlConnection);
                    cmd.Parameters.AddWithValue("@depth", $"{textBox_depth.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_lifting_capacity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_lifting_capacity.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Positions where lifting_capacity = @lifting_capacity", sqlConnection);
                    cmd.Parameters.AddWithValue("@lifting_capacity", $"{textBox_lifting_capacity.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_positions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }
    }
}
