using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class MyBudget : Form
    {
        bool isClickedLogin = false;
        bool isClickedPassword = false;

        public MyBudget()
        {
            InitializeComponent();
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            EnterForm form = new EnterForm();
            form.Show();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (!isClickedLogin)
            {
                isClickedLogin = true;
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (!isClickedPassword)
            {
                isClickedPassword = true;
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox2.Text.Length < 8 || textBox2.Text.Contains(" "))
            {
                if (textBox1.Text == "") textBox1.BackColor = System.Drawing.Color.Red;
                if (textBox2.Text == "") textBox2.BackColor = System.Drawing.Color.Red;
                if (textBox2.Text.Length < 8 || textBox2.Text.Contains(" ")) MessageBox.Show("Пароль должен быть больше 8 символов и не должен содержать пробельные символы");
                return;
            }

            if (!checkUser())
                return;

            DB db = new DB();
            MySqlCommand registerUser = new MySqlCommand("INSERT INTO users (login) VALUES (@login)", db.GetConnection());
            MySqlCommand insertPassword = new MySqlCommand("INSERT INTO passwords(password, user_id) VALUE(@password, (SELECT user_id FROM users WHERE login = @login))", db.GetConnection());

            registerUser.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBox1.Text;
            insertPassword.Parameters.Add("@login", MySqlDbType.VarChar).Value = textBox1.Text;
            insertPassword.Parameters.Add("@password", MySqlDbType.VarChar).Value = textBox2.Text;

            db.openConnection();

            if (registerUser.ExecuteNonQuery() == 1 && insertPassword.ExecuteNonQuery() == 1)
            {
                EnterForm.login = textBox1.Text.Trim();
                this.Hide();
                MainForm mainForm = new MainForm();
                MessageBox.Show("Вы успешно зарегестрированы!");
                mainForm.Show();

            }
            else
            {
                MessageBox.Show("Аккаунт не был создан");
            }

            db.closeConnection();
        }

        public Boolean checkUser()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand selectUsers = new MySqlCommand("SELECT * FROM users WHERE users.login=@uL", db.GetConnection());
            selectUsers.Parameters.Add("@uL", MySqlDbType.VarChar).Value = textBox1.Text;

            adapter.SelectCommand = selectUsers;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                textBox1.Text = "";
                MessageBox.Show("Введенный логин занят");
                return false;
            }
            else
            {
                EnterForm.login = textBox1.Text;
                return true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.SystemColors.Window;
            textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = System.Drawing.SystemColors.Window;
            textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
        }

        private void MyBudget_Load(object sender, EventArgs e)
        {

        }
    }

}
