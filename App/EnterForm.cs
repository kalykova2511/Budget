using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class EnterForm : Form
    {
        static public string login = "";
        bool isClickedLogin = false;
        bool isClickedPassword = false;

        public EnterForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            MyBudget registerForm = new MyBudget();
            registerForm.Show();
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
            textBox2.PasswordChar = '*';
            if (!isClickedPassword)
            {
                isClickedPassword = true;
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void EnterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string loginUser = textBox1.Text.Trim(); //Убираем пробельные символы справа и слева
            string passwordUser = textBox2.Text.Trim(); //Убираем пробельные символы справа и слева



            if (loginUser == "" || passwordUser == "") //Проверка ввода
            { 
                // Подсвечиваем поля, если пользователь их не заполнил
                if (loginUser == "")
                {
                    textBox1.BackColor = System.Drawing.Color.Red; 
                }
                if (passwordUser == "")
                {
                    textBox2.BackColor = System.Drawing.Color.Red;
                }
                return;
            }

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand selectUsers = new MySqlCommand("SELECT * FROM users, passwords WHERE users.user_id=passwords.user_id " +
                "AND users.login=@uL AND passwords.password=@uP", db.GetConnection());

            selectUsers.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            selectUsers.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passwordUser;

            adapter.SelectCommand = selectUsers;
            adapter.Fill(table);

            //Смотрим найден ли пользователь
            if (table.Rows.Count > 0)
            {
                login = textBox1.Text;
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Такой пользователь не найден. Проверте корректность введеных данных или зарегистрируйтесь.");
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

        private void EnterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
