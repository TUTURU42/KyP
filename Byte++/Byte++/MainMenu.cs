using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Byte__
{
    public partial class MainMenu : Form
    {
        Autorization autorization;
        Clients clients=null;
        Suppliers suppliers=null;  
        Requests requests=null;
        Articuls articuls=null;
        Products products=null;
        Positions positions=null;
        Motions motions=null;
        ComingConsumption coming_consumption=null;
        Boolean root;
        Statistics statistics=null;
        public MainMenu(Autorization x, Boolean r)
        {
            InitializeComponent();
            autorization= x;
            root = r;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (clients == null)
            {
               clients = new Clients(autorization, this, root);
            }
            clients.Show();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            autorization.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (suppliers == null)
            {
                suppliers = new Suppliers(autorization, this, root);
            }
            suppliers.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (requests == null)
            {
                requests = new Requests(autorization, this, root);
            }
            requests.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (articuls == null)
            {
                articuls = new Articuls(autorization, this, root);
            }
            articuls.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (products == null)
            {
                products = new Products(autorization, this, root);
            }
            products.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (positions == null)
            {
                positions = new Positions(autorization, this, root);
            }
            positions.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (motions == null)
            {
                motions = new Motions(autorization, this, root);
            }
            motions.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (coming_consumption == null)
            {
                coming_consumption = new ComingConsumption(autorization, this, root);
            }
            coming_consumption.Show();
            this.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if(!root)
            {
                label1.Visible = root;
                button9.Visible = root;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (statistics == null)
            {
                statistics = new Statistics(autorization, this);
            }
            statistics.Show();
            this.Hide();
        }
    }
}
