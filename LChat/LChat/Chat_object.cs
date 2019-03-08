using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace LChat
{
    public class Chat_object
    {
        public string ID;//用于确认身份的号码，好友是学号，群组是其他命名方式
    }

    public class Friend : Chat_object
    {
        public bool online_state;
        public IPAddress address;

        public Friend(string ID1)
        {
            ID = ID1;
            Search_state();
        }

        private void Search_state()//查询状态
        {
            string search_friend_state = "q" + ID;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress Server_ip = IPAddress.Parse("166.111.140.14");
            try
            {
                clientSocket.Connect(new IPEndPoint(Server_ip,8000)); //配置服务器IP与端口
            }
            catch
            {
                MessageBox.Show("网络故障，请检查网络后重新连接", "网络问题", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            try
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(search_friend_state));
            }
            catch
            {

            }
            byte[] search_result = new byte[1024];
            int resultlength = clientSocket.Receive(search_result);
            string result = Encoding.ASCII.GetString(search_result, 0, resultlength);

            if (result == "n")
            {
                online_state = false;
            }
            else
            {
                online_state = true;
                address = IPAddress.Parse(result);
            }
            clientSocket.Close();
        }

    }

    public class Group : Chat_object
    {
        public List<Friend> group_member = new List<Friend>();

        public Group(string ID1)
        {
            ID = ID1;
            Getfriend();
        }

        void Getfriend (){
            StreamReader file = new StreamReader(Account_data.DirContacts + this.ID + "_G" + ".txt");
            string account;

            while ((account = file.ReadLine()) != null)
            {
                group_member.Add(new Friend(account));
            }

            file.Close();
        }

    }

   
}
