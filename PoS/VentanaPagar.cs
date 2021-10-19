using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoS
{
    public partial class VentanaPagar : Form
    {
        public int pagoCliente = 0;
        public double cambio  = 0.0;
        private double total;

        public VentanaPagar(double totalpv)
        {
            InitializeComponent();
            total = totalpv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PuntoDeVenta punto = new PuntoDeVenta();
            pagoCliente = Int32.Parse(textBox1.Text);
            
   
            if (pagoCliente >= total) 
            {
                cambio = pagoCliente - total;
                
                punto.Show(this);
                punto.label11.Text = "$ " + pagoCliente;
                punto.label12.Text = "$ " + cambio;
                //MessageBox.Show("el cambio es: " + cambio);
                punto.dataGridView1.Rows.Clear();
                punto.textBox1.Clear();
                punto.textBox1.Focus();
                //this.Close();    
            } else {
                MessageBox.Show("Cantidad insuficiente");
                textBox1.Text = "";
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
