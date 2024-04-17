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
    public partial class RealUpdate : Form
    {
        int id;
        int type;
        MainForm f;

        public RealUpdate(int id, int type, MainForm f)
        {
            InitializeComponent();
            this.id = id;
            this.f = f;
            this.type = type;
            label5.Text = "";
            FillCB();
        }

        public void FillCB()
        {
            using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
            {
                if (type == 1) //
                {
                    MySqlCommand insert = new MySqlCommand("SELECT id, name FROM income_categories " +
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
                else
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (!Int32.TryParse(textBox1.Text, out i))
            {
                label5.Text = "Ошибка: сумма должна быть числом";
                return;
            }
            if (type == 1) //Доходы
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    MySqlCommand insert = new MySqlCommand("UPDATE real_incomes SET " +
                    "bill = @bill, " +
                    "date = @sd, " +
                    "category_id = @cid " +
                    "WHERE id = @id", co);
                    co.Open();
                    insert.Parameters.AddWithValue("@bill", textBox1.Text);
                    insert.Parameters.AddWithValue("@sd", dateTimePicker1.Value);
                    insert.Parameters.AddWithValue("@cid", comboBox1.SelectedValue);
                    insert.Parameters.AddWithValue("@id", id);
                    insert.ExecuteNonQuery();
                    Close();
                }
            }
            else //Расходы
            {
                using (var co = new MySqlConnection("server=christlh.beget.tech;port=3306;database=christlh_fin_sys;user=christlh_fin_sys;password=OisbkfA5oMaX;"))
                {
                    MySqlCommand insert = new MySqlCommand("UPDATE real_expenses SET " +
                    "bill = @bill, " +
                    "date = @sd, " +
                    "category_id = @cid " +
                    "WHERE id = @id", co);
                    co.Open();
                    insert.Parameters.AddWithValue("@bill", textBox1.Text);
                    insert.Parameters.AddWithValue("@sd", dateTimePicker1.Value);
                    insert.Parameters.AddWithValue("@cid", comboBox1.SelectedValue);
                    insert.Parameters.AddWithValue("@id", id);
                    insert.ExecuteNonQuery();
                    Close();
                }
            }
            f.DisplayData();
        }
    }
}
