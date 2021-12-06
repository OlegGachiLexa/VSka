using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OlegGoodMan;

namespace Oleg13
{
    public partial class Form1 : Form
    {
        public List<Material> materials = new List<Material>();

        public Form1()
        {
            InitializeComponent();
        }



        public void fetchBooks()
        {
            using (Model2 db = new Model2())
            {
                this.materials.Clear();
                foreach (Material mat in db.Material)
                {
                    this.materials.Add(mat);
                }
            }
        }

        public void renderBooks()
        {
            Class1 class1 = new Class1();
            container1.Controls.Clear();

            Label labelTitle1 = new Label();
            labelTitle1.Location = new Point(20, 10);
            labelTitle1.Text = "Название товара";
            labelTitle1.Width = 160;
            labelTitle1.Height = 20;
            container1.Controls.Add(labelTitle1);

            Label labelPages1 = new Label();
            labelPages1.Location = new Point(210, 10);
            labelPages1.Text = "Остаток";
            labelPages1.Width = 160;
            labelPages1.Height = 20;
            container1.Controls.Add(labelPages1);

            Label labelCost1 = new Label();
            labelCost1.Location = new Point(410, 10);
            labelCost1.Text = "Цена";
            labelCost1.Width = 160;
            labelCost1.Height = 20;
            container1.Controls.Add(labelCost1);

            int y = 0;
            double sum= 0;

            foreach (Material mat in this.materials)
            {
                GroupBox box = new GroupBox();
                box.Location = new Point(10, 40 + y++ * 40) ;
                box.Width = 660;
                box.Height = 40;
                box.FlatStyle = FlatStyle.Flat;

                Label title = new Label();
                title.Text = $"{ mat.Title }";
                title.Width = 150;
                title.Location = new Point(10, 14);


                Label ost = new Label();
                ost.Text = $"{ mat.CountInStock}";
                ost.Width = 200;
                ost.Location = new Point(200, 14);

                Label cost = new Label();
                cost.Text = $"{ (int)mat.Cost} руб.";
                cost.Width = 100;
                cost.Location = new Point(400, 14);

                Button btnEdit = new Button();
                btnEdit.Text = "Изменить";
                btnEdit.Location = new Point(500, 14);
                btnEdit.Width = 100;
                btnEdit.Click += (s, e) => BtnEdit_Click(mat);

                
               sum += class1.Sum(mat.CountInStock,mat.CountInPack,mat.Cost);

                box.Controls.Add(title);
                box.Controls.Add(ost);
                box.Controls.Add(cost);
                box.Controls.Add(btnEdit);

                container1.Controls.Add(box);
            }
            label1.Text = "Сумма всех товаров: " + sum.ToString();
        }


        private void BtnEdit_Click(Material mat)
        {
            Form2 editForm = new Form2();
            editForm.ID = mat.ID;
            editForm.Title = mat.Title;
            editForm.Cost = mat.Cost;
            editForm.CountlnStock = mat.CountInStock;
            editForm.mainForm = this;


            this.Hide();
            editForm.Show();
            editForm.Focus();


           
        }

 


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    {
                        MessageBox.Show("Сортировка по возрастанию (Цена)");
                        materials = this.materials.OrderByDescending(mat => mat.Cost).ToList();
                        this.renderBooks();
                        break;
                    }
                case 1:
                    {
                        MessageBox.Show("Сортировка по убыванию (Цена)");
                        materials = this.materials.OrderBy(mat => mat.Cost).ToList();
                        this.renderBooks();
                        break;
                    }

            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fetchBooks();
            switch (comboBox4.SelectedIndex)
            {
                case 0:
                    {
                        this.renderBooks();
                        break;
                    }
                case 1:
                    {
                        this.materials = materials.Where(mat => mat.Description == "Резина").ToList();
                        this.renderBooks();
                        break;
                    }
                case 2:
                    {
                        this.materials = materials.Where(mat => mat.Description == "Силикон").ToList();
                        this.renderBooks();
                        break;
                    }
                case 3:
                    {
                        this.materials = materials.Where(mat => mat.Description == "Краска").ToList();
                        this.renderBooks();
                        break;
                    }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fetchBooks();
            renderBooks();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.fetchBooks();
            this.materials = materials.Where(mat => mat.Title == textBox1.Text).ToList();
            this.renderBooks();
        }
    }
}
