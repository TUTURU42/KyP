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
    public partial class Clients : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        Boolean root;
        private void Clients_Load(object sender, EventArgs e)
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

        public Clients(Autorization x, MainMenu m, Boolean r)
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
            this.root = r;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            autorization.Close();
        }


        private void button_create_Click(object sender, EventArgs e)
        {
            if (textBox_name_org.Text==""|| textBox_contract_num.Text==""|| textBox_phone.Text==""|| textBox_adress_org.Text==""|| textBox_date_concl.Text==""|| textBox_date_exp.Text==""|| textBox_status.Text=="")
            {
                MessageBox.Show("Вы не ввели необходимые данные");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"INSERT INTO Clients (name_organization, contract_number, phone, adress_organization, date_conclusion, date_expiration, status) VALUES (@name_organization, @contract_number, @phone, @adress_organization, @date_conclusion, @date_expiration, @status)",
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

        private void button_renew_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Clients", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_clients.DataSource = dataset.Tables[0];
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id_client.Text == "")
            {
                MessageBox.Show("Введите id клиента");
            }
            else
            {
                SqlCommand command = new SqlCommand(
                    $"DELETE FROM Clients WHERE id_client=N'{textBox_id_client.Text}'",
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
            if (textBox_id_client.Text == "")
            {
                MessageBox.Show("Введите id клиента");
            }
            else
            {
                if(textBox_name_org.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET name_organization=@name_org WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    cmd.Parameters.AddWithValue("@name_org", $"{textBox_name_org.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                if (textBox_contract_num.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET contract_number=@contract_num WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    cmd.Parameters.AddWithValue("@contract_num", $"{textBox_contract_num.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                if (textBox_phone.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET phone=@phone WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    cmd.Parameters.AddWithValue("@phone", $"{textBox_phone.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                if (textBox_adress_org.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET adress_organization=@adress_org WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    cmd.Parameters.AddWithValue("@adress_org", $"{textBox_adress_org.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                if (textBox_date_concl.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET date_conclusion=@date_concl WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    DateTime date_concl = DateTime.Parse(textBox_date_concl.Text);
                    cmd.Parameters.AddWithValue("@date_concl", $"{date_concl.Month}/{date_concl.Day}/{date_concl.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                if (textBox_date_exp.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET date_expiration=@date_exp WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    DateTime date_exp = DateTime.Parse(textBox_date_exp.Text);
                    cmd.Parameters.AddWithValue("@date_exp", $"{date_exp.Month}/{date_exp.Day}/{date_exp.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                if (textBox_status.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("UPDATE Clients SET status=@status WHERE Id_client=@id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    cmd.Parameters.AddWithValue("@status", $"{textBox_status.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
                button_renew_Click(sender, e);
                /*SqlDataAdapter sqladapter = new SqlDataAdapter("SELECT * FROM Clients", sqlConnection);

                DataSet dataset = new DataSet();
                sqladapter.Fill(dataset);
                dataGridView_clients.DataSource = dataset.Tables[0];*/
            }
        }



        private void textBox_name_org_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_clients.DataSource as DataTable).DefaultView.RowFilter = $"name_organization LIKE '%{textBox_name_org.Text}%'";
        }

        private void textBox_contract_num_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_clients.DataSource as DataTable).DefaultView.RowFilter = $"contract_number LIKE '%{textBox_contract_num.Text}%'";
        }

        private void button_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.mainMenu.Show();
        }

        private void textBox_phone_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_clients.DataSource as DataTable).DefaultView.RowFilter = $"phone LIKE '%{textBox_phone.Text}%'";
        }

        private void textBox_adress_org_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_clients.DataSource as DataTable).DefaultView.RowFilter = $"adress_organization LIKE '%{textBox_adress_org.Text}%'";
        }

        

        private void textBox_status_TextChanged(object sender, EventArgs e)
        {
            (dataGridView_clients.DataSource as DataTable).DefaultView.RowFilter = $"status LIKE '%{textBox_status.Text}%'";
        }

        private void textBox_date_concl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_concl.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Clients where date_conclusion = @date_concl", sqlConnection);
                    DateTime date_concl = DateTime.Parse(textBox_date_concl.Text);
                    cmd.Parameters.AddWithValue("@date_concl", $"{date_concl.Month}/{date_concl.Day}/{date_concl.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n"+ex.Message);
            }
        }

        private void textBox_date_exp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_date_exp.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Clients where date_expiration = @date_exp", sqlConnection);
                    DateTime date_exp = DateTime.Parse(textBox_date_exp.Text);
                    cmd.Parameters.AddWithValue("@date_exp", $"{date_exp.Month}/{date_exp.Day}/{date_exp.Year}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма даты\n"+ ex.Message);
            }
        }

        private void textBox_id_client_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter && textBox_id_client.Text != "")
                {
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("Select * from Clients where Id_client = @id_client", sqlConnection);
                    cmd.Parameters.AddWithValue("@id_client", $"{textBox_id_client.Text}");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    dataGridView_clients.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильная форма данных\n" + ex.Message);
            }
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(textBox_select.Text, sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_clients.DataSource = dataset.Tables[0];
        }
    }
}
