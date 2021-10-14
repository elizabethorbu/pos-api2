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

            label1.Location = new Point(35, 26);
            label1.Width = this.Width - 70;
            label1.Height = 82;

            label5.Width = this.Width - 70;
            label5.Height = 42;
            label5.Location = new Point(35, label1.Location.Y + 101);

            label3.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();
            label3.Location = new Point(this.Width / 2 - label3.Width / 2, label1.Height + label2.Height);

            textBox1.Width = this.Width - 70;
            textBox1.Height = 62;
            textBox1.Location = new Point(35, label5.Location.Y + 60);

            pictureBox1.Width = textBox1.Width;
            pictureBox1.Height = 2;
            pictureBox1.Location = new Point(35, textBox1.Location.Y + 42);


            dataGridView1.Location = new Point(35, textBox1.Location.Y + 65);
            dataGridView1.Width = this.Width / 2 + 70;
            dataGridView1.Height = 400;

            dataGridView1.Columns[0].Width = dataGridView1.Width * 15 / 100;
            dataGridView1.Columns[1].Width = dataGridView1.Width * 45 / 100;
            dataGridView1.Columns[2].Width = dataGridView1.Width * 20 / 100;
            dataGridView1.Columns[3].Width = dataGridView1.Width * 20 / 100;
            dataGridView1.RowTemplate.Height = 50;


            label4.Location = new Point(this.Width / 2 + 70 - dataGridView1.Width * 45 / 100, dataGridView1.Location.Y + 420);

            label10.Location = new Point(label4.Location.X+229, label4.Location.Y);

            pictureBox3.Location = new Point(label4.Location.X - 25, label4.Location.Y);


            pictureBox2.Location = new Point(textBox1.Width-300-70, dataGridView1.Location.Y);
            pictureBox2.Controls.Add(pictureBox6);
            pictureBox6.Location = new Point(0, 0);
            pictureBox6.BackColor = Color.Transparent;
            //pictureBox6.Location = new Point(pictureBox2.Location.X, dataGridView1.Location.Y);

            label7.Location = new Point(pictureBox2.Location.X + pictureBox3.Width/3 - label7.Width, dataGridView1.Location.Y +320);
            label11.Location = new Point(label7.Location.X+140+3,label7.Location.Y);

            label6.Location = new Point(label7.Location.X,label7.Location.Y+label7.Height+20);
            label12.Location = new Point(label6.Location.X + label6.Width + 3, label6.Location.Y);

            pictureBox4.Location = new Point(label6.Location.X-28, label6.Location.Y);

            pictureBox5.Location = new Point(35, this.Height-pictureBox5.Height);
            label8.Location = new Point(35, pictureBox5.Location.Y + pictureBox5.Height / 2);

            label2.Location = new Point(textBox1.Width-label2.Width, 26);
            label9.Location = new Point(label2.Location.X + label2.Width/2 - label9.Width/2, label2.Location.Y+ label2.Height + 20);

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
                //MessageBox.Show(query);
                try
                {
                    
                    MySqlConnection mySqlConnection = new MySqlConnection("server=127.0.0.1; user=root; database=verificador_de_precios; SSL mode=none");
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                    MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                    if (mySqlDataReader.HasRows)
                    {
                        mySqlDataReader.Read();

                        pictureBox6.ImageLocation = mySqlDataReader.GetString(4);
                        pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
                        dataGridView1.Rows.Add("1", mySqlDataReader.GetString(1), mySqlDataReader.GetString(3), mySqlDataReader.GetString(3));

                        CalcularTotal();
                        
                        //==============================================================================================
                        //Último desarrollo del programa aquí (2021-09-23)
                        //Último en desarrollo: Mario López Ramonet
                        //==============================================================================================
                        textBox1.Clear();
                        textBox1.Focus();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (e.KeyChar == 'P' || e.KeyChar == 'p')
            {
                e.Handled = true;
                //MessageBox.Show($"¿Va a pagar? {textBox1.Text} {total} {Environment.NewLine} " +
                //    $"{Convert.ToDouble(textBox1.Text) - total}");

                label11.Text= $"$ {Math.Round(Convert.ToDouble(textBox1.Text), 2)}";
                label12.Text = $"$ {Math.Round(Convert.ToDouble(textBox1.Text) - total, 2)}";
                dataGridView1.Rows.Clear();
                textBox1.Clear();
                textBox1.Focus();
                
            }
            if (e.KeyChar == 'D' || e.KeyChar == 'd')
            {
                e.Handled = true;
                //MessageBox.Show($"¿Va a pagar? {textBox1.Text} {total} {Environment.NewLine} " +
                //    $"{Convert.ToDouble(textBox1.Text) - total}");

                label10.Text = "$ ";
                label11.Text = "$ ";
                label12.Text = "$ ";
                dataGridView1.Rows.Clear();
                textBox1.Clear();
                textBox1.Focus();

            }
        }

        private void CalcularTotal()
        {
            total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += Double.Parse(dataGridView1[3, i].Value.ToString());
            }
            label10.Text = "$ " + String.Format("{0:0.00}", total);
        }



    }
}
