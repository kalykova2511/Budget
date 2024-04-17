using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class CategoriesWork : Form
    {
        MainForm f;

        public CategoriesWork(MainForm f)
        {
            InitializeComponent();
            label3.Text = "При удалении категории, стираются все внесенные в нее записи. \n" +
                "Если вы хотите поменять название, воспользуйтесь функцией \"Обновить\"";
            this.f = f;
            DisplayData();
        }

        public void DisplayData()
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                co.Open();
                MySqlCommand selectIncomesType = new MySqlCommand("SELECT id, name FROM income_categories " +
                    "WHERE income_categories.user_id = (SELECT user_id FROM users WHERE login = @lg)", co);

                selectIncomesType.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

                using (var dr = selectIncomesType.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var parentNode = new TreeNode(dr["name"].ToString());
                        treeView1.Nodes.Add(parentNode);
                        parentNode.Tag = (int)dr["id"];
                        parentNode.ContextMenuStrip = contextMenuStrip1;
                    }
                }

                MySqlCommand selectExpensesType = new MySqlCommand("SELECT id, name FROM expenses_categories " +
                   "WHERE expenses_categories.user_id = (SELECT user_id FROM users WHERE login = @lg)", co);

                selectExpensesType.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;
                using (var dr = selectExpensesType.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var parentNode = new TreeNode(dr["name"].ToString());
                        parentNode.Tag = (int)dr["id"];
                        parentNode.ContextMenuStrip = contextMenuStrip2;
                        treeView2.Nodes.Add(parentNode);
                    }
                }
            }
        }

        private void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CategoryUpdateForm fr = new CategoryUpdateForm((int)treeView1.SelectedNode.Tag, 1, treeView1.SelectedNode.Text);
            fr.ShowDialog();
            DisplayData();
            f.DisplayData();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryUpdateForm fr = new CategoryUpdateForm((int)treeView2.SelectedNode.Tag, 2, treeView2.SelectedNode.Text);
            fr.ShowDialog();
            DisplayData();
            f.DisplayData();
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand insert = new MySqlCommand("DELETE FROM income_categories WHERE id = @id", co);

                co.Open();
                insert.Parameters.Add("@id", MySqlDbType.Int64).Value = treeView1.SelectedNode.Tag;
                insert.ExecuteNonQuery();
                treeView1.SelectedNode.Remove();
                f.DisplayData();
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand insert = new MySqlCommand("DELETE FROM expenses_categories WHERE id = @id", co);

                co.Open();
                insert.Parameters.Add("@id", MySqlDbType.Int64).Value = treeView2.SelectedNode.Tag;
                insert.ExecuteNonQuery();
                treeView2.SelectedNode.Remove();
                f.DisplayData();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CategoryAddForm form = new CategoryAddForm(1, f, this);
            form.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategoryAddForm form = new CategoryAddForm(2, f, this);
            form.ShowDialog(this);
        }
    }
}
