using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oleg13
{
    public partial class Form2 : Form
    {
        public Form1 mainForm;
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public double? CountlnStock { get; set; }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Title;
            textBox2.Text = CountlnStock.ToString();
            textBox3.Text = Cost.ToString();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
        
        }


        private void button2_Click(object sender, EventArgs e)
        {
            using (Model2 db = new Model2()) {

                Material mat = db.Material.SingleOrDefault(el => el.ID == this.ID);

                mat.Title = textBox1.Text;
                mat.CountInStock = Convert.ToDouble( textBox2.Text);
                mat.Cost = Convert.ToDecimal(textBox3.Text);

               // db.Material.Add(mat);
                db.SaveChanges();
                mainForm.fetchBooks();
                mainForm.renderBooks();
                mainForm.Show();
                this.Close();
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
