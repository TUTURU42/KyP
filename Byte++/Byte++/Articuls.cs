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
    public partial class Articuls : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        public Articuls(Autorization x, MainMenu m,Boolean r)
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
            this.root = r;
        }

        private void Articuls_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBByte++"].ConnectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            button_renew_Click(sender, e);
            if(!root)
            {
                textBox_select.Visible = root;
                button_select.Visible=root;
                button_create.Visible=root;
                button_delete.Visible=root;
                button_change.Visible=root;

            }

        }

        private void Articuls_FormClosing(object sender, FormClosingEventArgs e)
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
            dataGridView_articuls.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_articul.Text == "" || textBox_height.Text == "" || textBox_width.Text == "" || textBox_depth.Text == "" || textBox_weight.Text == "" || textBox_existence.Text == "" || textBox_id_supplier.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO Articuls (articul, height, width, depth, weight, existence, id_supplier) VALUES (@articul, @height, @width, @depth, @weight, @existence, @id_supplier)",
                    sqlConnection);


                command.Parameters.AddWithValue("articul", textBox_articul.Text);
                command.Parameters.AddWithValue("height", textBox_height.Text);
                command.Parameters.AddWithValue("width", textBox_width.Text);
                command.Parameters.AddWithValue("depth", textBox_depth.Text);
                command.Parameters.AddWithValue("weight", textBox_weight.Text);
                command.Parameters.AddWithValue("existence", textBox_existence.Text);
                command.Parameters.AddWithValue("id_supplier", textBox_id_supplier.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_articul.Text == "")
            {
                MessageBox.Show("Введите articul");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Articuls WHERE articul=N'{textBox_articul.Text}'",
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
            if (textBox_articul.Text == "")
            {
                MessageBox.Show("Введите articul");
            }
            else
            {
                if (textBox_height.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Articuls SET height=@height WHERE articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@height", $"{textBox_height.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
                if (textBox_width.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Articuls SET width=@width WHERE articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@width", $"{textBox_width.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
                if (textBox_depth.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Articuls SET depth=@depth WHERE articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@depth", $"{textBox_depth.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
                if (textBox_weight.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Articuls SET weight=@weight WHERE articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@weight", $"{textBox_weight.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
                if (textBox_existence.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Articuls SET existence=@existence WHERE articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@existence", $"{textBox_existence.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
                if (textBox_id_supplier.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Articuls SET id_supplier=@id_supplier WHERE articul=@articul", sqlConnection);
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Articuls", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_articuls.DataSource = dataset.Tables[0];
        }

        private void textBox_articul_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_articuls.DataSource as DataTable).DefaultView.RowFilter = $"articul LIKE '%{textBox_articul.Text}%'";
        }

        private void textBox_height_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_height.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Articuls where height = @height", sqlConnection);
                    cmd.Parameters.AddWithValue("@height", $"{textBox_height.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
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
                    SqlCommand cmd = new SqlCommand("Select * from Articuls where width = @width", sqlConnection);
                    cmd.Parameters.AddWithValue("@width", $"{textBox_width.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
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
                    SqlCommand cmd = new SqlCommand("Select * from Articuls where depth = @depth", sqlConnection);
                    cmd.Parameters.AddWithValue("@depth", $"{textBox_depth.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_weight_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_weight.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Articuls where weight = @weight", sqlConnection);
                    cmd.Parameters.AddWithValue("@weight", $"{textBox_weight.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_existence_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_existence.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Articuls where existence = @existence", sqlConnection);
                    cmd.Parameters.AddWithValue("@existence", $"{textBox_existence.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_id_supplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_supplier.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Articuls where id_supplier = @id_supplier", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_supplier", $"{textBox_id_supplier.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_articuls.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }
    }
}
