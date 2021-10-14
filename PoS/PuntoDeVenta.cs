using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PoS
{
    public partial class PuntoDeVenta : Form
    {
        private double total = 0.0;
        public PuntoDeVenta()
        {
            InitializeComponent();
        }

        private void PuntoDeVenta_Load(object sender, EventArgs e)
        {
            label1.Location = new Point(this.Width / 2 - label1.Width / 2, 0);
            label2.Location = new Point(this.Width / 2 - label2.Width / 2, label1.Height);
            label3.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height);
            dataGridView1.Location = new Point(10, label3.Location.Y + label3.Height);
            dataGridView1.Width = this.Width - 20;
            dataGridView1.Height = (this.Height / 4) * 3;
            //MessageBox.Show(this.Width + "  " + this.Height);
            this.BackColor = Color.HotPink;
            dataGridView1.Columns[0].Width = dataGridView1.Width * 15 / 100;
            dataGridView1.Columns[1].Width = dataGridView1.Width * 45 / 100;
            dataGridView1.Columns[2].Width = dataGridView1.Width * 20 / 100;
            dataGridView1.Columns[3].Width = dataGridView1.Width * 20 / 100;
            dataGridView1.RowTemplate.Height = 60;
            textBox1.Location = new Point(10, this.Height - textBox1.Height);
            textBox1.Width = this.Width - 20;
            label4.Location = new Point(dataGridView1.Width - label3.Width - 20, dataGridView1.Location.Y + dataGridView1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13)
            {
                String query = "SELECT * FROM productos WHERE producto_codigo =" + textBox1.Text;
                MessageBox.Show(query);
                try
                {
                    MySqlConnection mySqlConnection = new MySqlConnection("server=127.0.0.1; user=root; database=verificador_de_precios; SSL mode=none");
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    if (mySqlDataReader.HasRows)
                    {
                        mySqlDataReader.Read();
                        dataGridView1.Rows.Add("1", mySqlDataReader.GetString(1), mySqlDataReader.GetString(3), mySqlDataReader.GetString(3));
                        CalcularTotal();
                        //==============================================================================================
                        //Último desarrollo del programa aquí (2021-09-13)
                        //Último en desarrollo: Elías López García
                        //==============================================================================================

                    }
                    else
                    {

                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void CalcularTotal()
        {
            total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += Double.Parse(dataGridView1[3,i].Value.ToString());
            }
            label4.Text = "Total: " + total;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
