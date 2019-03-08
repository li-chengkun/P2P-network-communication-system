using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace LChat
{
    public partial class Login : Form
    {
        public static Login l = null; //用来引用主窗口

        public Login()
        {
            InitializeComponent();
            Account.Text = "2016011467";
            Password.Text = "net2018";
            l = this;
        }

        private IPAddress Server_ip = IPAddress.Parse("166.111.140.14");
        int port = 8000;
        string account;
        string password;

        private void Button_login_Click(object sender, EventArgs e)
        {
            account = Account.Text;
            password = Password.Text;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(Server_ip, port)); //配置服务器IP与端口
            }
            catch
            {
                MessageBox.Show("网络故障，请检查网络后重新连接", "网络问题", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);                
                return;
            }

            string request = account + "_" + password;

            try
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(request));
            }
            catch
            {
                MessageBox.Show("连接超时，请重新连接", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            byte[] login_result = new byte[1024];
            int resultlength = clientSocket.Receive(login_result);
            string result = Encoding.ASCII.GetString(login_result, 0, resultlength);

            if (result == "lol")
            {
                clientSocket.Close();
                Hide();
                Main mymain = new Main(account);
                mymain.Show();
            }
            else
            {
                MessageBox.Show("账号或密码错误！", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
        }
    }

}
