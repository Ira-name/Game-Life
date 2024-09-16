using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace житя
{
    public partial class AuthenticationForm : Form
    {
         private string username;
         public string Password { get; private set; }

        public string Username
        {
            get { return username; }
        }
        private string loggedInUser;

        public string LoggedInUser
        {
            get { return loggedInUser; }
        }

        public AuthenticationForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Будь ласка, введіть ім'я користувача та пароль!");
                return;
            }
            string[] users = File.ReadAllLines("users.txt");

            foreach (string user in users)
            {
                string[] userData = user.Split(',');

                string storedUsername = userData[0].Trim();
                string storedPassword = userData[1].Trim();

                if (storedUsername == username && storedPassword == password)
                {
                    loggedInUser = username;
                    DialogResult = DialogResult.OK;
                    return;
                }
            }

            MessageBox.Show("Невірне ім'я користувача або пароль!");
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Будь ласка, введіть ім'я користувача та пароль!");
                return;
            }
            string[] users = File.ReadAllLines("users.txt");

            foreach (string user in users)
            {
                string[] userData = user.Split(',');

                string storedUsername = userData[0].Trim();

                if (storedUsername == username)
                {
                    MessageBox.Show("Користувач з таким ім'ям вже існує!");
                    return;
                }
            }
            string newUser = $"{username},{password}";
            File.AppendAllText("users.txt", newUser + Environment.NewLine);
            MessageBox.Show("Реєстрація пройшла успішно!");
            loggedInUser = username;
            DialogResult = DialogResult.OK;
        }
    }
}
