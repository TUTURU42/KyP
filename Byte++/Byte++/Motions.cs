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
    public partial class Motions : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        public Motions(Autorization x, MainMenu m, Boolean r)
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
            this.root = r;
        }

        private void Motions_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBByte++"].ConnectionString);
            if (sqlConnection.State == ConnectionState.Closed)
             {
                 sqlConnection.Open();
             }
             using (SqlConnection cn = new SqlConnection())
             {
                 cn.ConnectionString = sqlConnection.ConnectionString;
                 try
                 {
                     //Открыть подключение
                     cn.Open();
                 }
                 catch (SqlException ex)
                 {
                     // Протоколировать исключение
                     Console.WriteLine(ex.Message);
                 }
                 finally
                 {
                      //Гарантировать освобождение подключения
                     cn.Close();
                 }
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

        private void Motions_FormClosing(object sender, FormClosingEventArgs e)
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
            dataGridView_motions.DataSource = dataset.Tables[0];
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_document_type.Text == "" || textBox_pallet_number.Text == "" || textBox_articul.Text == "" || textBox_type_motion.Text == "" || (textBox_id_start_position.Text == "" && textBox_id_finish_position.Text == "") || textBox_date_motion.Text == "")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command;
                if (textBox_id_start_position.Text != "" && textBox_id_finish_position.Text != "")
                {
                    command = new SqlCommand(
                        $"INSERT INTO Motions (document_type, pallet_number, articul, type_motion, id_start_position, id_finish_position, date_motion) VALUES (@document_type, @pallet_number, @articul, @type_motion, @id_start_position, @id_finish_position, @date_motion)",
                        sqlConnection);
                    command.Parameters.AddWithValue("id_start_position", textBox_id_start_position.Text);
                    command.Parameters.AddWithValue("id_finish_position", textBox_id_finish_position.Text);
                }
                else if (textBox_id_start_position.Text != "")
                {
                    command = new SqlCommand(
                        $"INSERT INTO Motions (document_type, pallet_number, articul, type_motion, id_start_position, date_motion) VALUES (@document_type, @pallet_number, @articul, @type_motion, @id_start_position, @date_motion)",
                        sqlConnection);
                    command.Parameters.AddWithValue("id_start_position", textBox_id_start_position.Text);
                }
                else if (textBox_id_finish_position.Text != "")
                {
                    command = new SqlCommand(
                        $"INSERT INTO Motions (document_type, pallet_number, articul, type_motion, id_finish_position, date_motion) VALUES (@document_type, @pallet_number, @articul, @type_motion, @id_finish_position, @date_motion)",
                        sqlConnection);
                    command.Parameters.AddWithValue("id_finish_position", textBox_id_finish_position.Text);
                }
                else
                {
                    MessageBox.Show("Введите id стартовой позиции или id конечной позиции");
                    command = new SqlCommand();
                }
                DateTime date_motion = DateTime.Parse(textBox_date_motion.Text);

                command.Parameters.AddWithValue("document_type", textBox_document_type.Text);
                command.Parameters.AddWithValue("pallet_number", textBox_pallet_number.Text);
                command.Parameters.AddWithValue("articul", textBox_articul.Text);
                command.Parameters.AddWithValue("type_motion", textBox_type_motion.Text);
                
                command.Parameters.AddWithValue("date_motion", $"{date_motion.Month}/{date_motion.Day}/{date_motion.Year}");
                

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Строка добавлена");
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_document_number.Text == "")
            {
                MessageBox.Show("Введите номер документа");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Motions WHERE document_number=N'{textBox_document_number.Text}'",
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
            if (textBox_document_number.Text == "")
            {
                MessageBox.Show("Введите номер документа");
            }
            else
            {
                if (textBox_document_type.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET document_type=@document_type WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    cmd.Parameters.AddWithValue("@document_type", $"{textBox_document_type.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                if (textBox_pallet_number.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET pallet_number=@pallet_number WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    cmd.Parameters.AddWithValue("@pallet_number", $"{textBox_pallet_number.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                if (textBox_articul.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET articul=@articul_type WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    cmd.Parameters.AddWithValue("@articul", $"{textBox_articul.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                if (textBox_type_motion.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET type_motion=@type_motion WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    cmd.Parameters.AddWithValue("@type_motion", $"{textBox_type_motion.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                if (textBox_id_start_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET id_start_position=@id_start_position WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    cmd.Parameters.AddWithValue("@id_start_position", $"{textBox_id_start_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                if (textBox_date_motion.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET date_motion=@date_motion WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    DateTime date_motion = DateTime.Parse(textBox_date_motion.Text);
                    cmd.Parameters.AddWithValue("@date_motion", $"{date_motion.Month}/{date_motion.Day}/{date_motion.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                if (textBox_id_finish_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Motions SET id_finish_position=@id_finish_position WHERE document_number=@document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    cmd.Parameters.AddWithValue("@id_finish_position", $"{textBox_id_finish_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
                button_renew_Click(sender, e);
            }
        }

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Motions", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_motions.DataSource = dataset.Tables[0];
        }

        private void textBox_document_number_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_document_number.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Motions where document_number = @document_number", sqlConnection);
                    cmd.Parameters.AddWithValue("@document_number", $"{textBox_document_number.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_document_type_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_motions.DataSource as DataTable).DefaultView.RowFilter = $"document_type LIKE '%{textBox_document_type.Text}%'";
        }

        private void textBox_pallet_number_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_motions.DataSource as DataTable).DefaultView.RowFilter = $"pallet_number LIKE '%{textBox_pallet_number.Text}%'";
        }

        private void textBox_articul_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_motions.DataSource as DataTable).DefaultView.RowFilter = $"articul LIKE '%{textBox_articul.Text}%'";
        }

        private void textBox_type_motion_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_motions.DataSource as DataTable).DefaultView.RowFilter = $"type_motion LIKE '%{textBox_type_motion.Text}%'";
        }


        private void textBox_id_finish_position_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_finish_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Motions where id_finish_position = @id_finish_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_finish_position", $"{textBox_id_finish_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void textBox_date_motion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_date_motion.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Motions where date_motion = @date_motion", sqlConnection);
                    DateTime date_motion = DateTime.Parse(textBox_date_motion.Text);
                    cmd.Parameters.AddWithValue("@date_motion", $"{date_motion.Month}/{date_motion.Day}/{date_motion.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n" + ex.Message);
            }
        }

        private void textBox_id_start_position_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && textBox_id_start_position.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Motions where id_start_position = @id_start_position", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_start_position", $"{textBox_id_start_position.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_motions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }
    }
}
