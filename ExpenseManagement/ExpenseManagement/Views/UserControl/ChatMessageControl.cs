using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public partial class ChatMessageControl : UserControl
    {
        private Timer _timer;
        private int _currentIndex = 0;
        private string _textToDisplay;
        private int _delay;
        private const int CharactersPerTick = 4; // Số ký tự sẽ hiển thị mỗi lần

        public ChatMessageControl(string message, string senderName, bool isUser)
        {
            InitializeComponent();

            lblSender.Text = senderName;
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");

            _textToDisplay = FormatBoldText(message);

            // Đặt delay ban đầu cho tất cả các tin nhắn
            _delay = 50;  // Delay mặc định cho văn bản

            if (_textToDisplay.Length > 100) // Nếu văn bản dài hơn 100 ký tự
            {
                _delay = 30;  // Giảm thời gian hiển thị ký tự đối với nội dung dài
            }

            if (isUser)
            {
                lblMessage.Text = _textToDisplay;
            }
            else
            {
                lblMessage.Text = "";
                StartTypingEffect();
            }
        }

        private void StartTypingEffect()
        {
            _timer = new Timer();
            _timer.Interval = _delay;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int endIndex = Math.Min(_currentIndex + CharactersPerTick, _textToDisplay.Length);
            lblMessage.Text = _textToDisplay.Substring(0, endIndex);
            _currentIndex = endIndex;

            if (_currentIndex >= _textToDisplay.Length)
            {
                _timer.Stop();
            }
        }

        private string FormatBoldText(string input)
        {
            return Regex.Replace(input, @"\*\*(.*?)\*\*", match => match.Groups[1].Value);
        }
    }
}
