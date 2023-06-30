using RedditClone.Administrator.Services.BoardService;
using RedditClone.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedditClone.Administrator.Forms
{
    public partial class MainForm : Form
    {
        HttpClient _http;
        BoardService _boardService;

        public MainForm()
        {
            

            InitializeComponent();

            
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var login = new LoginForm();
            var success = login.ShowWindow(this);

            _http = new HttpClient();
            _boardService = new BoardService(_http);

            if (!success)
            {
                label1.Text = "Login failure.";
            }
            else
            {
                label1.Text = "Login success.";
            }

            var boards = await _boardService.GetBoardsAsync();
            if (boards != null && boards.Success && boards.Data != null)
            {
                label2.Text = $"Fetched {boards.Data.Count} board(s)!";
            }
            else
            {
                label1.Text += boards.Message;
            }
        }
    }
}
