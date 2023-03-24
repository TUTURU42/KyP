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
    public partial class Statistics : Form
    {
        private SqlConnection sqlConnection = null;
        Autorization autorization;
        MainMenu mainMenu;
        public Statistics(Autorization x, MainMenu m)
        {
            InitializeComponent();
            this.autorization = x;
            this.mainMenu = m;
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBByte++"].ConnectionString);
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            

            SqlCommand command = new SqlCommand("SELECT count(*) FROM Positions", sqlConnection);
            int count_all_pos=(int)command.ExecuteScalar();
            command = new SqlCommand("SELECT count(*) FROM Positions where status_position = @status_position", sqlConnection);
            command.Parameters.AddWithValue("@status_position", $"ЗАНЯТ");
            int count_busy_pos = (int)command.ExecuteScalar();
            label4.Text = count_busy_pos.ToString()+"/"+count_all_pos.ToString();
            float f= (float)count_busy_pos / (float)count_all_pos;
            label5.Text = (f).ToString() + "%";

            command = new SqlCommand("SELECT count(*) FROM СomingConsumption where date_operation >@getdate and id_supplier is not null", sqlConnection);
            DateTime getdate = DateTime.Today.AddDays(-1);
           
            command.Parameters.AddWithValue("@getdate", $"{getdate.Month}/{getdate.Day}/{getdate.Year}");
            int count_supp_operate = (int)command.ExecuteScalar();
            label6.Text = count_supp_operate.ToString();

            command = new SqlCommand("SELECT count(*) FROM СomingConsumption where date_operation >@getdate and id_client is not null", sqlConnection);
            command.Parameters.AddWithValue("@getdate", $"{getdate.Month}/{getdate.Day}/{getdate.Year}");
            int count_client_operate = (int)command.ExecuteScalar();
            label7.Text = count_client_operate.ToString();

            button_renew_Prod_Art_Click(sender, e);
            button_renew_Art_Supp_Click(sender, e);
            button_renew_Prod_Pos_Click(sender, e);
        }

        private void Statistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            autorization.Close();
        }
        private void button_menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.mainMenu.Show();
        }
        private void button_select_Prod_Art_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(textBox_select_Prod_Art.Text, sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_Prod_Art.DataSource = dataset.Tables[0];
        }

        private void button_renew_Prod_Art_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select p.name_product, p.id_position, p.pallet_number, p.quality_condition, p.count, p.date_expiration, p.date_manufacture, a.articul, a.height, a.width, a.depth, a.weight, a.existence, a.id_supplier from Products p LEFT JOIN Articuls a ON a.articul = p.articul", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_Prod_Art.DataSource = dataset.Tables[0];
        }

        private void button_select_Prod_Pos_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(textBox_select_Prod_Pos.Text, sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_Prod_Pos.DataSource = dataset.Tables[0];
        }

        private void button_renew_Prod_Pos_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select p.name_product, p.pallet_number, p.quality_condition, p.count, p.date_expiration, p.date_manufacture, p.id_position, po.alley, po.place, po.level  from Products p LEFT JOIN Positions po ON po.Id_position = p.id_position", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_Prod_Pos.DataSource = dataset.Tables[0];
        }

        private void button_select_Art_Supp_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(textBox_select_Art_Supp.Text, sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_Art_Supp.DataSource = dataset.Tables[0];
        }

        private void button_renew_Art_Supp_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select a.articul, a.height, a.width, a.depth, a.weight, a.existence, a.id_supplier, s.name_organization, s.contract_number, s.phone, s.adress_organization from Articuls a LEFT JOIN Suppliers s ON a.id_supplier = s.Id_supplier", sqlConnection);

            DataSet dataset = new DataSet();
            adapter.Fill(dataset);
            dataGridView_Art_Supp.DataSource = dataset.Tables[0];
        }

      
    }
}
