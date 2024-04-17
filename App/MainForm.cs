using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using DGVPrinterHelper;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System.Threading;

namespace CourseWork
{
    public partial class MainForm : Form
    {
        public static DateTime dt1 = DateTime.Now;
        public static DateTime dt2 = DateTime.Now;

        public MainForm()
        {
            InitializeComponent();
            tabControl1.SelectTab(0);
            label18.Text = "";
            label19.Text = "";
            dateTimePicker5.Value =  dateTimePicker5.Value.AddMonths(1);
            DisplayData();
        }

        public void DisplayData()
        {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            treeView3.Nodes.Clear();
            treeView4.Nodes.Clear();
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
                        var parentNode2 = new TreeNode(dr["name"].ToString());
                        parentNode2.Tag = (int)dr["id"];
                        parentNode2.ContextMenuStrip = contextMenuStrip3;
                        treeView2.Nodes.Add(parentNode2);
                        DisplayData2(parentNode, parentNode2, (int)dr["id"]);
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
                        parentNode.ContextMenuStrip = contextMenuStrip5;
                        treeView3.Nodes.Add(parentNode);
                        var parentNode2 = new TreeNode(dr["name"].ToString());
                        parentNode2.Tag = (int)dr["id"];
                        parentNode2.ContextMenuStrip = contextMenuStrip6;
                        treeView4.Nodes.Add(parentNode2);
                        DisplayData3(parentNode, parentNode2, (int)dr["id"]);
                    }
                }
            }
        }

        private void DisplayData2(TreeNode parent1, TreeNode parent2, int typeId)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                co.Open();
                MySqlCommand selectIncome = new MySqlCommand("SELECT id, start_date, end_date, bill " +
                    "FROM future_incomes WHERE category_id = @category " +
                    "AND start_date >= @fromdate AND end_date <= @todate", co);

                MySqlCommand selectIncome2 = new MySqlCommand("SELECT id, date, bill " +
                   "FROM real_incomes WHERE category_id = @category " +
                   "AND date >= @fromdate AND date <= @todate", co);


                selectIncome.Parameters.AddWithValue("@category", typeId);
                selectIncome.Parameters.AddWithValue("@fromdate", dateTimePicker6.Value.AddDays(-1));
                selectIncome.Parameters.AddWithValue("@todate", dateTimePicker5.Value);
                selectIncome2.Parameters.AddWithValue("@category", typeId);
                selectIncome2.Parameters.AddWithValue("@fromdate", dateTimePicker6.Value.AddDays(-1));
                selectIncome2.Parameters.AddWithValue("@todate", dateTimePicker5.Value);

                using (var dr = selectIncome.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var childNode1 = new TreeNode(dr["bill"].ToString() + " руб.");
                        childNode1.ContextMenuStrip = contextMenuStrip2;
                        childNode1.Tag = (int)dr["id"];
                        parent1.Nodes.Add(childNode1);
                    }
                }

                using (var dr = selectIncome2.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var childNode2 = new TreeNode(dr["bill"].ToString() + " руб. Дата: " + dr["date"].ToString());
                        childNode2.ContextMenuStrip = contextMenuStrip4;
                        childNode2.Tag = (int)dr["id"];
                        parent2.Nodes.Add(childNode2);
                    }
                }
            }
        }

        private void DisplayData3(TreeNode parent1, TreeNode parent2, int typeId)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                co.Open();
                MySqlCommand selectIncome = new MySqlCommand("SELECT id, start_date, end_date, bill " +
                    "FROM future_expenses WHERE category_id = @category " +
                    "AND start_date >= @fromdate AND end_date <= @todate", co);

                MySqlCommand selectIncome2 = new MySqlCommand("SELECT id, date, bill " +
                   "FROM real_expenses WHERE category_id = @category " +
                   "AND date >= @fromdate AND date <= @todate", co);


                selectIncome.Parameters.AddWithValue("@category", typeId);
                selectIncome.Parameters.AddWithValue("@fromdate", dateTimePicker6.Value.AddDays(-1));
                selectIncome.Parameters.AddWithValue("@todate", dateTimePicker5.Value);
                selectIncome2.Parameters.AddWithValue("@category", typeId);
                selectIncome2.Parameters.AddWithValue("@fromdate", dateTimePicker6.Value.AddDays(-1));
                selectIncome2.Parameters.AddWithValue("@todate", dateTimePicker5.Value);

                using (var dr = selectIncome.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var childNode1 = new System.Windows.Forms.TreeNode(dr["bill"].ToString() + " руб.");
                        childNode1.ContextMenuStrip = contextMenuStrip7;
                        childNode1.Tag = (int)dr["id"];
                        parent1.Nodes.Add(childNode1);
                    }
                }

                using (var dr = selectIncome2.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var childNode2 = new System.Windows.Forms.TreeNode(dr["bill"].ToString() + " руб. Дата: " + dr["date"].ToString());
                        childNode2.ContextMenuStrip = contextMenuStrip8;
                        childNode2.Tag = (int)dr["id"];
                        parent2.Nodes.Add(childNode2);
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            textBox1.Text = "Список операций";

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand selectIncomes = new MySqlCommand("SELECT bill AS Сумма, date AS Дата, name AS Название, 'доход' as Тип " +
                "FROM real_incomes, income_categories " +
                "WHERE income_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND real_incomes.category_id = income_categories.id " +
                "AND date BETWEEN @fromdate AND @todate " +
                "UNION SELECT bill AS Сумма, date AS Дата, name AS Название, 'расход' as Тип FROM real_expenses, expenses_categories " +
                "WHERE expenses_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND real_expenses.category_id = expenses_categories.id " +
                "AND date BETWEEN @fromdate AND @todate", db.GetConnection());

            selectIncomes.Parameters.Add("@fromdate", MySqlDbType.DateTime).Value = dateTimePicker1.Value;
            selectIncomes.Parameters.Add("@todate", MySqlDbType.DateTime).Value = dateTimePicker2.Value;
            selectIncomes.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

            adapter.SelectCommand = selectIncomes;
            adapter.Fill(table);

            db.openConnection();

            dataGridView1.DataSource = table;

            db.closeConnection();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                var dgvPrinter = new Documents();
                dgvPrinter.CreateReport(textBox1.Text, dataGridView1);
            }
            else
            {
                MessageBox.Show("Отчет по текущим датам пуст. Выберите другой период для скачивания");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Доходы по категориям";
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand selectIncomes = new MySqlCommand("SELECT name AS Название, SUM(bill) AS Сумма " +
                "FROM real_incomes, income_categories " +
                "WHERE income_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND category_id = income_categories.id AND date BETWEEN @fromdate AND @todate GROUP BY category_id", db.GetConnection());

            selectIncomes.Parameters.Add("@fromdate", MySqlDbType.DateTime).Value = dateTimePicker1.Value;
            selectIncomes.Parameters.Add("@todate", MySqlDbType.DateTime).Value = dateTimePicker2.Value;
            selectIncomes.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

            adapter.SelectCommand = selectIncomes;
            adapter.Fill(table);

            db.openConnection();

            dataGridView1.DataSource = table;

            db.closeConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Расходы по категориям";

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand selectIncomes = new MySqlCommand("SELECT name AS Название, SUM(bill) AS Сумма " +
                "FROM real_expenses, expenses_categories " +
                "WHERE expenses_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND category_id = expenses_categories.id AND date BETWEEN @fromdate AND @todate GROUP BY category_id", db.GetConnection());

            selectIncomes.Parameters.Add("@fromdate", MySqlDbType.DateTime).Value = dateTimePicker1.Value;
            selectIncomes.Parameters.Add("@todate", MySqlDbType.DateTime).Value = dateTimePicker2.Value;
            selectIncomes.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

            adapter.SelectCommand = selectIncomes;
            adapter.Fill(table);

            db.openConnection();

            dataGridView1.DataSource = table;

            db.closeConnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand selectIncomes = new MySqlCommand("SELECT name AS Название, SUM(bill) as Сумма " +
                "FROM real_incomes, income_categories " +
                "WHERE income_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND category_id = income_categories.id AND date BETWEEN @fromdate AND @todate GROUP BY category_id", db.GetConnection());

            selectIncomes.Parameters.Add("@fromdate", MySqlDbType.DateTime).Value = dateTimePicker4.Value;
            selectIncomes.Parameters.Add("@todate", MySqlDbType.DateTime).Value = dateTimePicker3.Value;
            selectIncomes.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

            adapter.SelectCommand = selectIncomes;
            adapter.Fill(table);

            db.openConnection();

            if (table.Rows.Count > 0) 
            {
                label18.Text = "";
                dataGridView2.DataSource = table;

                db.closeConnection();
                chart1.Series.Clear();
                chart1.Series.Add("Доходы");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    var name = dataGridView2.Rows[i].Cells[0].Value?.ToString() ?? "";
                    var value = dataGridView2.Rows[i].Cells[1].Value?.ToString() ?? "";
                    chart1.Series["Доходы"].Points.AddXY(name, value);
                }
                chart1.Titles.Clear();
                chart1.Titles.Add("Доходы по категориям");
                chart1.ChartAreas[0].AxisX.Title = dataGridView2.Columns[0].HeaderText;
                chart1.ChartAreas[0].AxisY.Title = dataGridView2.Columns[1].HeaderText;
                chart1.Series[0].IsVisibleInLegend = false;
            }
            else
            {
                label18.Text = "Нет операций за выбранный период.";
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand selectIncomes = new MySqlCommand("SELECT name AS Название, SUM(bill) as Сумма " +
                "FROM real_expenses, expenses_categories " +
                "WHERE expenses_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND category_id = expenses_categories.id AND date BETWEEN @fromdate AND @todate GROUP BY category_id", db.GetConnection());

            selectIncomes.Parameters.Add("@fromdate", MySqlDbType.DateTime).Value = dateTimePicker4.Value;
            selectIncomes.Parameters.Add("@todate", MySqlDbType.DateTime).Value = dateTimePicker3.Value;
            selectIncomes.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

            adapter.SelectCommand = selectIncomes;
            adapter.Fill(table);

            db.openConnection();

            if (table.Rows.Count > 0)
            {
                label18.Text = "";
                dataGridView2.DataSource = table;

                db.closeConnection();
                chart1.Series.Clear();
                chart1.Series.Add("Расходы");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    var name = dataGridView2.Rows[i].Cells[0].Value?.ToString() ?? "";
                    var value = dataGridView2.Rows[i].Cells[1].Value?.ToString() ?? "";
                    chart1.Series["Расходы"].Points.AddXY(name, value);
                }
                chart1.Titles.Clear();
                chart1.Titles.Add("Расходы по категориям");
                chart1.ChartAreas[0].AxisX.Title = dataGridView2.Columns[0].HeaderText;
                chart1.ChartAreas[0].AxisY.Title = dataGridView2.Columns[1].HeaderText;
                chart1.Series[0].IsVisibleInLegend = false;
            }
            else
            {
                label18.Text = "Нет операций за выбранный период.";
            }   
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand selectIncomes = new MySqlCommand("SELECT CONVERT(SUM(real_incomes.bill) / 2, UNSIGNED) as Доходы, " +
                "CONVERT(SUM(real_expenses.bill) / 2, UNSIGNED) as Расходы " +
                "FROM real_expenses, expenses_categories, real_incomes, income_categories " +
                "WHERE expenses_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND real_expenses.category_id = expenses_categories.id AND income_categories.user_id = (SELECT user_id FROM users WHERE login = @lg) " +
                "AND real_incomes.category_id = income_categories.id " +
                "AND real_expenses.date BETWEEN @fromdate AND @todate " +
                "AND real_incomes.date BETWEEN @fromdate AND @todate", db.GetConnection());

            selectIncomes.Parameters.Add("@fromdate", MySqlDbType.DateTime).Value = dateTimePicker4.Value;
            selectIncomes.Parameters.Add("@todate", MySqlDbType.DateTime).Value = dateTimePicker3.Value;
            selectIncomes.Parameters.Add("@lg", MySqlDbType.String).Value = EnterForm.login;

            adapter.SelectCommand = selectIncomes;
            adapter.Fill(table);

            db.openConnection();

            dataGridView2.DataSource = table;

            db.closeConnection();
            if (dataGridView2.Rows[0].Cells[0].Value.ToString() != "" || dataGridView2.Rows[0].Cells[1].Value.ToString() != "")
            {
                int ti = 0;
                int te = 0;
                label18.Text = "";
                chart1.Series.Clear();
                chart1.Series.Add("Счет");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;

                var name = dataGridView2.Columns[0].HeaderText;
                var value = dataGridView2.Rows[0].Cells[0].Value?.ToString() ?? "";
                ti = Int32.Parse(value);
                chart1.Series["Счет"].Points.AddXY(name, value);
                name = dataGridView2.Columns[1].HeaderText;
                value = dataGridView2.Rows[0].Cells[1].Value?.ToString() ?? "";
                chart1.Series["Счет"].Points.AddXY(name, value);
                chart1.Titles.Clear();
                te = Int32.Parse(value);
                chart1.Titles.Add("Остаток на счету");
                chart1.Series[0].IsVisibleInLegend = false;
                string ost = (ti - te).ToString();
                label19.Text = "Остаток на счету: " + ost + " руб.";
            }
            else
            {
                label18.Text = "Нет операций за выбранный период.";
            }
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            DisplayData();
        }


        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand delete = new MySqlCommand("DELETE FROM future_incomes WHERE id = @id", co);

                co.Open();
                delete.Parameters.Add("@id", MySqlDbType.Int64).Value = treeView1.SelectedNode.Tag;
                delete.ExecuteNonQuery();
                treeView1.SelectedNode.Remove();
            }
        }

        private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand delete = new MySqlCommand("DELETE FROM real_incomes WHERE id = @id", co);

                co.Open();
                delete.Parameters.Add("@id", MySqlDbType.Int64).Value = treeView2.SelectedNode.Tag;
                delete.ExecuteNonQuery();
                treeView2.SelectedNode.Remove();
            }
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand insert = new MySqlCommand("DELETE FROM future_expenses WHERE id = @id", co);

                co.Open();
                insert.Parameters.Add("@id", MySqlDbType.Int64).Value = treeView3.SelectedNode.Tag;
                insert.ExecuteNonQuery();
                treeView3.SelectedNode.Remove();
            }    
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand delete = new MySqlCommand("DELETE FROM real_expenses WHERE id = @id", co);

                co.Open();
                delete.Parameters.Add("@id", MySqlDbType.Int64).Value = treeView4.SelectedNode.Tag;
                delete.ExecuteNonQuery();
                treeView4.SelectedNode.Remove();
            }
        }

        private void обновитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CategoryUpdateForm f = new CategoryUpdateForm((int)treeView2.SelectedNode.Tag, 1, treeView2.SelectedNode.Text);
            f.ShowDialog();
            DisplayData();
        }

        private void обновитьToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CategoryUpdateForm f = new CategoryUpdateForm((int)treeView3.SelectedNode.Tag, 2, treeView3.SelectedNode.Text);
            f.ShowDialog();
            DisplayData();
        }

        private void обновитьToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CategoryUpdateForm f = new CategoryUpdateForm((int)treeView4.SelectedNode.Tag, 2, treeView4.SelectedNode.Text);
            f.ShowDialog();
            DisplayData();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryUpdateForm f = new CategoryUpdateForm((int)treeView1.SelectedNode.Tag, 1, treeView1.SelectedNode.Text);
            f.ShowDialog();
            DisplayData();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            CategoriesWork f = new CategoriesWork(this);
            f.ShowDialog(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FutureIncomeAddForm form = new FutureIncomeAddForm(this);
            form.ShowDialog(this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FutureExpenseAddForm form = new FutureExpenseAddForm(this);
            form.ShowDialog(this);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RealIncomeAddForm form = new RealIncomeAddForm(this);
            form.ShowDialog(this);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RealExpenseAddForm form = new RealExpenseAddForm(this);
            form.ShowDialog(this);
        }

        private void обновитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PlanUpdate plan = new PlanUpdate((int)treeView1.SelectedNode.Tag, 1, this);
            plan.ShowDialog(this);
        }

        private void обновитьToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            PlanUpdate plan = new PlanUpdate((int)treeView3.SelectedNode.Tag, 2, this);
            plan.ShowDialog(this);
        }

        private void обновитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RealUpdate plan = new RealUpdate((int)treeView2.SelectedNode.Tag, 1, this);
            plan.ShowDialog(this);
        }

        private void обновитьToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RealUpdate plan = new RealUpdate((int)treeView2.SelectedNode.Tag, 2, this);
            plan.ShowDialog(this);
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
            }
        }

        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView2.SelectedNode = e.Node;
            }
        }

        private void treeView3_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void treeView4_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView4.SelectedNode = e.Node;
            }
        }

        private void treeView3_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeView3.SelectedNode = e.Node;
            }
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
