using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LChat
{
    public partial class Add_Group : Form
    {
        string username;
        string Group_name;
        int num = 1;//群聊成员数
        List<Friend> G = new List<Friend>();

        public Add_Group()
        {
            InitializeComponent();
        }
        public Add_Group(string s)
        {
            InitializeComponent();
            username = s;
            G.Add(new Friend(s));
            create_group_list();
        }

        private void Add_Group_member_Click(object sender, EventArgs e)
        {
            if (Groupmember.Text == "")
            {
                MessageBox.Show("请输入账号！");
                return;
            }

            for(int i = 0; i > G.Count; i++)
            {
                if (G[i].ID == Groupname.Text)
                {
                    MessageBox.Show("重复！");
                    return;
                }
            }
            string search_friend_state = "q" + Groupmember.Text;
            string result;

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress Server_ip = IPAddress.Parse("166.111.140.14");

            int port = 8000;
            try
            {
                clientSocket.Connect(new IPEndPoint(Server_ip, port)); //配置服务器IP与端口
            }
            catch
            {
                MessageBox.Show("网络故障，请检查网络后重新连接", "网络问题", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                result = "ooo";
            }

            try
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(search_friend_state));
            }
            catch
            {
                MessageBox.Show("连接超时，请重新连接", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                result = "hhh";//凡是调用这个函数的都要处理这个返回值
            }
            byte[] search_result = new byte[1024];
            int resultlength = clientSocket.Receive(search_result);
            result = Encoding.ASCII.GetString(search_result, 0, resultlength);
            clientSocket.Close();

            if (result == "Please send the correct message.")
            {
                MessageBox.Show("请输入正确格式信息！");
            }
            else if (result == "Incorrect No.")
            {
                MessageBox.Show("账号不存在！");
            }
            else if (result == "hhh")
            {

            }
            else if (result == "ooo")
            {

            }
            else
            {
                string add_account = Groupmember.Text;
                G.Add(new Friend(add_account));

                create_group_list();
                Groupmember.Clear();
                num++;
            }
        }

        //产生群组成员列表
        private void create_group_list()
        {
            Add_group_panel.Controls.Clear();



            for (int i = 0; i < G.Count; i++)
            {
                Panel friend_item = new Panel();
                Label id = new Label();
                Label state = new Label();
                id.Text = G[i].ID;
                id.Location = new Point(30, 5);
                friend_item.Controls.Add(id);

                if (!G[i].online_state)
                {
                    state.Text = "离线";
                }
                else
                {
                    state.Text = "在线";
                }
                state.Location = new Point(150, 5);
                friend_item.Controls.Add(state);

                friend_item.Height = 40;
                friend_item.Dock = DockStyle.Top;
                friend_item.Cursor = Cursors.Hand;
                
                Add_group_panel.Controls.Add(friend_item);

                //id.MouseClick += new MouseEventHandler(label_MouseClick);
                //friend_item.MouseClick += new MouseEventHandler(panel_MouseClick);

            }

            //List_panel.ScrollToCaret()
        }

        private void CreatGroup_Click(object sender, EventArgs e)
        {
            if (num < 2)
            {
                MessageBox.Show("群组人数不能少于2人！");
                return;
            }
            if (Groupname.Text.Length >= 10)
            {
                MessageBox.Show("名字不需要这么长");
                return;
            }
            StreamReader file = new StreamReader(Account_data.DirContacts + "Groups.txt");
            String line;
            while ((line = file.ReadLine()) != null)
            {
                if (line == Groupname.Text)
                {
                    MessageBox.Show("你这个群名重复了，换一个！");
                    Groupname.Focus();
                    file.Close();
                    Groupname.Clear();
                    return;
                }
            }
            file.Close();

            Group_name = Groupname.Text;
            StreamWriter s = new StreamWriter(Account_data.DirContacts + "Groups.txt", true);
            s.Write(Group_name + "\r\n");
            s.Close();

            if (!File.Exists(Account_data.DirContacts + Group_name + "_G" + ".txt"))
            {
                File.Create(Account_data.DirContacts + Group_name + "_G" + ".txt").Close();
            }
            

            StreamWriter sw = new StreamWriter(Account_data.DirContacts + Group_name + "_G" + ".txt", true);
            for(int i = 0; i < G.Count; i++)
            {
                sw.Write(G[i].ID + "\r\n");

            }
            sw.Flush();
            sw.Close();
            G.Clear();
            Add_group_panel.Controls.Clear();
            Groupname.Clear();
            this.Close();
        }
    }
}
