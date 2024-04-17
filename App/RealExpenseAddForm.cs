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
    public partial class RealExpenseAddForm : Form
    {
        MainForm f;
        public RealExpenseAddForm(MainForm f)
        {
            InitializeComponent();
            this.f = f;
            FillCB();
            label5.Text = "";
        }

        public void FillCB()
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand insert = new MySqlCommand("SELECT id, name FROM expenses_categories " +
                    "WHERE user_id = (SELECT user_id FROM users WHERE login = @lg)", co);

                co.Open();
                insert.Parameters.Add("@lg", MySqlDbType.VarChar).Value = EnterForm.login;
                insert.ExecuteNonQuery();

                DataTable table = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = insert;
                adapter.Fill(table);
                comboBox1.DataSource = table;
                comboBox1.DisplayMember = "name"; //отображаемое поле (данные из поля будут отображаться в комбобоксе)
                comboBox1.ValueMember = "id"; //поле-значение (данные из этого поля будут записываться в свойство SelectedValue)
                comboBox1.Tag = "id";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryAddForm form = new CategoryAddForm(6, f, this);
            form.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (!Int32.TryParse(textBox1.Text, out i))
            {
                label5.Text = "Ошибка: сумма должна быть числом";
                return;
            }
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                MySqlCommand insert = new MySqlCommand("INSERT INTO real_expenses(bill, date, category_id) " +
                    "VALUE (@bill, @sd, @cid)", co);
                co.Open();
                insert.Parameters.AddWithValue("@bill", textBox1.Text);
                insert.Parameters.AddWithValue("@sd", dateTimePicker1.Value);
                insert.Parameters.AddWithValue("@cid", comboBox1.SelectedValue);
                insert.ExecuteNonQuery();
            }
            f.DisplayData();
            label5.Text = "Успешно добавлено";
        }
    }
}
