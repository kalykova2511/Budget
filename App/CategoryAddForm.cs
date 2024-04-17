using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class CategoryAddForm : Form
    {
        int tree;
        MainForm f;
        CategoriesWork c;
        FutureIncomeAddForm fi;
        FutureExpenseAddForm fe;
        RealIncomeAddForm re;
        RealExpenseAddForm ri;
        RealUpdate ru;
        PlanUpdate pu;

        public CategoryAddForm(int tree, MainForm f, CategoriesWork c)
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.c = c;
        }

        public CategoryAddForm(int tree, MainForm f, FutureIncomeAddForm fi)
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.fi = fi;
        }

        public CategoryAddForm(int tree, MainForm f, FutureExpenseAddForm fe)
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.fe = fe;
        }

        public CategoryAddForm(int tree, MainForm f, RealIncomeAddForm re)
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.re = re;
        }

        public CategoryAddForm(int tree, MainForm f, RealExpenseAddForm ri)
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.ri = ri;
        }

        public CategoryAddForm(int tree, MainForm f, RealUpdate ru) 
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.ru = ru;
        }
        public CategoryAddForm(int tree, MainForm f, PlanUpdate pu)
        {
            InitializeComponent();
            this.tree = tree;
            this.f = f;
            this.pu = pu;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tree == 1)
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    try
                    {
                        if(textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("INSERT INTO income_categories(name, user_id) " +
                       "VALUE (@name, (SELECT user_id FROM users WHERE login = @lg))", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                            insert.ExecuteNonQuery();
                            var parentNode = new TreeNode(textBox1.Text);
                            parentNode.ContextMenuStrip = c.contextMenuStrip1;
                            c.treeView1.Nodes.Add(parentNode);
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
            else if(tree == 2)
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    
                    try
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("INSERT INTO expenses_categories(name, user_id) " +
                         "VALUE (@name, (SELECT user_id FROM users WHERE login = @lg))", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                            insert.ExecuteNonQuery();
                            var parentNode = new TreeNode(textBox1.Text);
                            parentNode.ContextMenuStrip = c.contextMenuStrip2;
                            c.treeView2.Nodes.Add(parentNode);
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
            else if (tree == 3)
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    
                    try
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("INSERT INTO income_categories(name, user_id) " +
                         "VALUE (@name, (SELECT user_id FROM users WHERE login = @lg))", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                            insert.ExecuteNonQuery();
                            fi.FillCB();
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
            else if (tree == 4)
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    
                    try
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("INSERT INTO expenses_categories(name, user_id) " +
                         "VALUE (@name, (SELECT user_id FROM users WHERE login = @lg))", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                            insert.ExecuteNonQuery();
                            fe.FillCB();
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
            else if (tree == 5)
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    
                    try
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("INSERT INTO income_categories(name, user_id) " +
                        "VALUE (@name, (SELECT user_id FROM users WHERE login = @lg))", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                            insert.ExecuteNonQuery();
                            re.FillCB();
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
            else if (tree == 6)
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                   
                    try
                    {
                        if (textBox1.Text.Trim() != "")
                        {
                            MySqlCommand insert = new MySqlCommand("INSERT INTO expenses_categories(name, user_id) " +
                        "VALUE (@name, (SELECT user_id FROM users WHERE login = @lg))", co);
                            co.Open();
                            insert.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox1.Text;
                            insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                            insert.ExecuteNonQuery();
                            ri.FillCB();
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
            
            f.DisplayData();
        }
    }
}
