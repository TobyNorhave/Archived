using CafeBooking.Model;
using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;
            int zip = Int32.Parse(txtZip.Text);
            int priceRange = Int32.Parse(txtPriceRange.Text);

            Cafe cafe = new Cafe(name, zip, address, priceRange, 1);
            CafeCtr cafeCtr = new CafeCtr();
            cafeCtr.Create(cafe);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
