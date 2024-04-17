using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class CategoryUpdateForm : Form
    {
        int id;
        int tree;

        public CategoryUpdateForm(int id, int tree, string oldName)
        {
            InitializeComponent();
            this.id = id;
            this.tree = tree;
            textBox1.Text = oldName;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tree == 1) // категории доходов
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    try
                    {
                        if(textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("UPDATE income_categories SET " +
                    "name = @name " +
                    "WHERE id = @id", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@id", MySqlDbType.Int64).Value = id;
                            insert.ExecuteNonQuery();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Введите непустое название");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Введите уникальное значение");
                    }
                }
            }
            else // категории расходов
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    try
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("UPDATE expenses_categories SET " + 
                                "name = @name " +
                                "WHERE id = @id", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@id", MySqlDbType.Int64).Value = id;
                            insert.ExecuteNonQuery();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Введите непустое название");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Введите уникальное значение");
                    }
                }
            }
        }
    }
}
