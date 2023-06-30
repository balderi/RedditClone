using RedditClone.Administrator.Services.AuthService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditClone.Administrator.Forms
{
    public partial class LoginForm : Form
    {
        HttpClient _http;
        AuthService _authService;
        bool _success = false;

        public LoginForm()
        {
            _http = new HttpClient();
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            if(CheckFields())
            {
                string message = string.Empty;
                _authService = new AuthService(_http, TbUrl.Text);
                var result = await _authService.LoginAsync(new Shared.Models.UserLogin { Email = TbEmail.Text, Password = TbPassword.Text });
                if (result != null && result.Success && result.Data != null)
                {
                    _success = true;
                    Settings.BaseUrl = TbUrl.Text;
                    Close();
                }
                else if(result != null && !string.IsNullOrEmpty(result.Message))
                {
                    MessageBox.Show(
                    "There was a problem logging in to the service." + Environment.NewLine + result.Message,
                    "Connection error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckFields()
        {
            if(string.IsNullOrEmpty(TbUrl.Text))
            {
                MessageBox.Show("URL is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TbUrl.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TbEmail.Text))
            {
                MessageBox.Show("Email is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TbEmail.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(TbPassword.Text))
            {
                MessageBox.Show("Password is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TbPassword.Focus();
                return false;
            }

            return true;
        }
        
        public bool ShowWindow(IWin32Window owner)
        {
            ShowDialog(owner);
            return _success;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
