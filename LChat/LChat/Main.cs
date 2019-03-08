using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Media;
using LChat.Properties;

namespace LChat
{
    public partial class Main : Form
    {
        Main m = null;
        string username;//用户名
        public List<Friend> myfriends = new List<Friend>();//好友列表
        public List<Group> mygroups = new List<Group>();//群组列表

        public int sticker_tag = 0;//表情标识 
        public Image[] images = new Image[3];//表情数据

        Friend Current_friend;//当前聊天好友
        Group Current_group;//当前聊天群组
        Panel pre_panel = null;//
        bool flag_FG;//判断是群组还是好友
        public bool flag_OC;//判读是打开表情还是关闭表情
        
        public bool appRun = true;//监听是否开始
        private Listener lis;//监听对象        
        

        public Main()
        {
            InitializeComponent();
        }
        //初始化
        public Main(string user)
        {
            InitializeComponent();
            username = user;
            Account_data.MyAccount = user;
            Account_data.CreatePath();
            lis = new Listener(AddMessage);
            lis.StartListener();
            m = this;
            read_friend_list();
            read_group_list();
            flag_FG = true;
            flag_OC = true;
            //create_friend_list();
            //Input_text.Visible = false;
            Btn_Send.Enabled = false;

            Btn_SendFile.Enabled = false;
            Btn_emoji.Enabled = false;

            images[0] = Resources._1;
            images[1] = Resources._2;
            images[2] = Resources._3;

            //richTextBox1.ReadOnly = true;
            CheckForIllegalCrossThreadCalls = false;
        }


        //查询状态
        public string search_friend_state(string account)
        {
            string search_friend_state = "q" + account;

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
                return "ooo";
            }

            try
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(search_friend_state));
            }
            catch
            {
                MessageBox.Show("连接超时，请重新连接", "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "hhh";//凡是调用这个函数的都要处理这个返回值
            }
            byte[] search_result = new byte[1024];
            int resultlength = clientSocket.Receive(search_result);
            string result = Encoding.ASCII.GetString(search_result, 0, resultlength);
            clientSocket.Close();
            return result;
        }
        //查询并添加好友
        private void Search_friend_Click(object sender, EventArgs e)
        {

            string result = search_friend_state(search_account.Text);

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
                StreamReader file = new StreamReader(Account_data.DirContacts + "Friends.txt");
                String line;//看是否已经是好友
                bool flag = true;
                while ((line = file.ReadLine()) != null)
                {

                    if (search_account.Text == line)
                    {
                        flag = false;
                        MessageBox.Show("您已添加对方。");
                        break;
                    }
                }
                file.Close();
                if (flag)
                {
                    StreamWriter sw = new StreamWriter(Account_data.DirContacts + "Friends.txt", true);
                    sw.Write(search_account.Text + "\r\n");
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    read_friend_list();
                    create_friend_list();
                }
            }


        }

        //读取好友列表文件
        private void read_friend_list()
        {
            myfriends.Clear();
            StreamReader file = new StreamReader(Account_data.DirContacts + "Friends.txt");
            String line;
            while ((line = file.ReadLine()) != null)
            {
                if (line == "\r\n")
                {
                    continue;
                }
                myfriends.Add(new Friend(line));
            }
            file.Close();
        }
        //产生好友列表
        private void create_friend_list()
        {
            List_panel.Controls.Clear();

            for (int i = 0; i < myfriends.Count; i++)
            {
                Panel friend_item = new Panel();
                Label id = new Label();
                Label state = new Label();
                id.Text = myfriends[i].ID;
                id.Location = new Point(30, 5);
                friend_item.Controls.Add(id);

                if (!myfriends[i].online_state)
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
                if (Current_friend != null)
                {
                    if (id.Text == Current_friend.ID)
                    {
                        friend_item.BackColor = Color.FromArgb(220, 220, 220);
                        pre_panel = friend_item;
                    }
                }
                List_panel.Controls.Add(friend_item);

                id.MouseClick += new MouseEventHandler(label_MouseClick);
                friend_item.MouseClick += new MouseEventHandler(panel_MouseClick);

            }

            //List_panel.ScrollToCaret()
        }
        //读取群组列表文件
        private void read_group_list()
        {
            mygroups.Clear();
            StreamReader file = new StreamReader(Account_data.DirContacts + "Groups.txt");
            String line;
            while ((line = file.ReadLine()) != null)
            {
                mygroups.Add(new Group(line));
            }
            file.Close();
        }
        //产生群组列表
        private void create_group_list()
        {
            List_panel.Controls.Clear();

            for (int i = 0; i < mygroups.Count; i++)
            {
                Panel friend_item = new Panel();
                Label id = new Label();
                Label num = new Label();
                id.Text = mygroups[i].ID;
                id.Location = new Point(30, 5);
                friend_item.Controls.Add(id);
                num.Text = mygroups[i].group_member.Count.ToString();
                num.Location = new Point(150, 5);
                friend_item.Controls.Add(num);

                friend_item.Height = 40;
                friend_item.Dock = DockStyle.Top;
                friend_item.Cursor = Cursors.Hand;
                if (Current_group != null)
                {
                    if (id.Text == Current_group.ID)
                    {
                        friend_item.BackColor = Color.FromArgb(220, 220, 220);
                        pre_panel = friend_item;
                    }
                }
                List_panel.Controls.Add(friend_item);

                id.MouseClick += new MouseEventHandler(label_MouseClick);
                friend_item.MouseClick += new MouseEventHandler(panel_MouseClick);

            }

            //List_panel.ScrollToCaret()
        }
        
        //单击显示好友列表
        private void Btn_Friendlist_Click(object sender, EventArgs e)
        {
            create_friend_list();

            flag_FG = true;
        }
        //单击显示群组列表
        private void Btn_Grouplist_Click(object sender, EventArgs e)
        {

            read_group_list();
            create_group_list();
            flag_FG = false;
        }

        //下线
        private string Logout()
        {
            string logout_req = "logout" + username;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress Server_ip = IPAddress.Parse("166.111.140.14");

            int port = 8000;
            try
            {
                clientSocket.Connect(new IPEndPoint(Server_ip, port)); //配置服务器IP与端口
            }
            catch
            {
                //MessageBox.Show("网络故障，请检查网络后重新连接", "网络问题", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "ooo";
            }

            try
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(logout_req));
            }
            catch
            {
                //MessageBox.Show("连接失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "hhh";
            }
            byte[] logout_result = new byte[1024];
            int resultlength = clientSocket.Receive(logout_result);
            string result = Encoding.ASCII.GetString(logout_result, 0, resultlength);

            if (result == "loo")
            {
                lis.Stop();
                return result;
            }
            else
            {
                //MessageBox.Show("下线失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return result;
            }
        }
        //点击注销按钮时下线
        private void Log_out_Click(object sender, EventArgs e)
        {
            string r = Logout();
            if (r == "loo")
            {
                Login.l.Close();
                Hide();
            }
            else
            {
                MessageBox.Show("下线失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //Disconnnect();
        }
        //关闭窗口时，下线
        private void Main_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            string r = Logout();
            if (r == "loo")
            {
                Login.l.Close();
            }
            else
            {
                MessageBox.Show("下线失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //Disconnnect();
        }

        //点击聊天
        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            Panel chanel = (Panel)sender;
            string target_id = chanel.Controls[0].Text.ToString();
            if (flag_FG)
            {
                Current_group = null;
                Current_friend = new Friend(target_id);
            }
            else
            {
                Current_friend = null;
                Current_group = new Group(target_id);
            }

            if (pre_panel != null)
            {
                pre_panel.BackColor = Color.FromArgb(245, 245, 245);
            }

            chanel.BackColor = Color.FromArgb(220, 220, 220);

            pre_panel = chanel;

            Chat_label.Text = "聊天对象：" + target_id;
            Chat_label.Visible = true;
            Input_text.Visible = true;
            Btn_Send.Enabled = true;
            Btn_SendFile.Enabled = true;
            Btn_emoji.Enabled = true;

            richTextBox1.Visible = true;
            richTextBox1.Clear();
            Input_text.Clear();
            //读取聊天记录
            string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
            if (!File.Exists(Path + target_id + "_LTJL" + ".txt"))
            {
                File.Create(Path + target_id + "_LTJL" + ".txt").Close();
            }
            StreamReader file = new StreamReader(Path + target_id + "_LTJL" + ".txt");
            String line;
            while ((line = file.ReadLine()) != null)
            {
                if (line == "sticker_1" || line == "sticker_2" || line == "sticker_3")//如果是表情
                {
                    string s = line.Substring(line.Length - 1, 1);
                    int t = int.Parse(s);
                    //richTextBox1.ReadOnly = false;                  

                    //SetClipboard2(images[t - 1]);
                    Clipboard.SetImage(images[t - 1]);
                    richTextBox1.Paste();
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.ScrollToCaret();
                    //richTextBox1.ReadOnly = true;
                    continue;
                }
                richTextBox1.AppendText(line);
                richTextBox1.AppendText("\r\n");
                richTextBox1.ScrollToCaret();
            }
            file.Close();
        }
        //点击聊天
        private void label_MouseClick(object sender, MouseEventArgs e)
        {
            Label text = (Label)sender;

            string target_id = text.Text.ToString();

            if (flag_FG)
            {
                Current_group = null;
                Current_friend = new Friend(target_id);
            }
            else
            {
                Current_friend = null;
                Current_group = new Group(target_id);
            }

            if (pre_panel != null)
            {
                pre_panel.BackColor = Color.FromArgb(245, 245, 245);
            }

            text.Parent.BackColor = Color.FromArgb(220, 220, 220);

            pre_panel = (Panel)text.Parent;

            Chat_label.Text = "聊天对象：" + target_id;
            Chat_label.Visible = true;
            Input_text.Visible = true;
            Btn_Send.Enabled = true;
            Btn_emoji.Enabled = true;
            Btn_SendFile.Enabled = true;
            richTextBox1.Visible = true;

            richTextBox1.Clear();
            Input_text.Clear();

            //读取聊天记录
            string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
            if (!File.Exists(Path + target_id + "_LTJL" + ".txt"))
            {
                File.Create(Path + target_id + "_LTJL" + ".txt").Close();
            }
            StreamReader file = new StreamReader(Path + target_id + "_LTJL" + ".txt");
            String line;
            while ((line = file.ReadLine()) != null)
            {
                if (line == "sticker_1" || line == "sticker_2" || line == "sticker_3")//如果是表情
                {
                    string s = line.Substring(line.Length - 1, 1);
                    int t = int.Parse(s);
                    //richTextBox1.ReadOnly = false;                  

                    //SetClipboard2(images[t - 1]);
                    Clipboard.SetImage(images[t - 1]);
                    richTextBox1.Paste();
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.ScrollToCaret();
                    //richTextBox1.ReadOnly = true;
                    continue;
                }
                richTextBox1.AppendText(line);
                richTextBox1.AppendText("\r\n");
                richTextBox1.ScrollToCaret();
            }
            file.Close();
        }
        //发送消息
        private void Btn_Send_Click(object sender, EventArgs e)
        {
            if (Input_text.Text == "")
            {
                Input_text.Focus();
                //SystemSounds.Beep.Play();
                return;
            }
            byte[] smsg = System.Text.Encoding.Default.GetBytes(Input_text.Text);
            if (smsg.Length > 1024)
            {
                MessageBox.Show("不要这么长好吗");
                return;
            }
            if (flag_FG)//给好友发
            {
                string account = Current_friend.ID;

                string r = search_friend_state(account);
                if (r == "ooo" || r == "hhh" || r == "n")
                {
                    MessageBox.Show("连接不到对方");
                }
                else
                {
                    Sender s = new Sender(r, Account_data.MyAccount);
                    if (s.SendFriendMessage(Input_text.Text))
                    {
                        string str = "<Me>: " + DateTime.Now.ToString() + Environment.NewLine + Input_text.Text + Environment.NewLine;
                        richTextBox1.AppendText(str);
                        //richTextBox1.AppendText(str);
                        richTextBox1.ScrollToCaret();
                        Input_text.Clear();
                        Input_text.Focus();
                        //加入聊天记录
                        string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
                        if (!File.Exists(Path + Current_friend.ID + "_LTJL" + ".txt"))
                        {
                            File.Create(Path + Current_friend.ID + "LTJU" + ".txt").Close();

                        }
                        StreamWriter sw = new StreamWriter(Path + Current_friend.ID + "_LTJL" + ".txt", true);
                        sw.Write(str);
                        //清空缓冲区
                        sw.Flush();
                        //关闭流
                        sw.Close();
                    }
                }
            }
            else//给群组发
            {
                string id = Current_group.ID;

                foreach (Friend f in Current_group.group_member)
                {
                    if (f.ID == username)//这个地方一定要改啊！！！！
                    {
                        continue;
                    }

                    string r = search_friend_state(f.ID);
                    if (r == "ooo" || r == "hhh" || r == "n")
                    {

                    }
                    else
                    {
                        Sender s = new Sender(r, Account_data.MyAccount);
                        if (s.SendGroupMessage(id, Input_text.Text))
                        {
                            string str = "<Me>: " + DateTime.Now.ToString() + Environment.NewLine + Input_text.Text + Environment.NewLine;
                            richTextBox1.AppendText(str);
                            richTextBox1.ScrollToCaret();
                            Input_text.Clear();
                            Input_text.Focus();
                            //加入聊天记录
                            string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
                            if (!File.Exists(Path + id + "_LTJL" + ".txt"))
                            {
                                File.Create(Path + id + "LTJU" + ".txt").Close();

                            }
                            StreamWriter sw = new StreamWriter(Path + id + "_LTJL" + ".txt", true);
                            sw.Write(str);
                            //清空缓冲区
                            sw.Flush();
                            //关闭流
                            sw.Close();
                        }

                    }

                }
            }

        }
        //接受消息
        public void AddMessage(object sender, AddMessageEventArgs e)
        {
            string message = e.mess;
            string appendText;
            string appendText1;
            string id;
            
            appendText = "<" + e.Sender + ">" + System.DateTime.Now.ToString() + Environment.NewLine + message + Environment.NewLine;
            //int txtGetMsgLength = this.Input_text.Text.Length;
            appendText1 = "<" + e.Sender + ">" + System.DateTime.Now.ToString() + Environment.NewLine;
            if (flag_FG)
            {
                id = e.Sender;//发送者
                if (e.Sender == Current_friend.ID)
                {
                    if (message=="sticker_1"||message=="sticker_2"||message=="sticker_3")//如果是表情
                    {
                        string s = message.Substring(message.Length - 1, 1);
                        int t = int.Parse(s);
                        //richTextBox1.ReadOnly = false;
                        richTextBox1.AppendText(appendText1);
                                            
                        SetClipboard2(images[t-1]);

                        
                            richTextBox1.Paste();                                                
                            
                        
                        
                        richTextBox1.AppendText(Environment.NewLine);
                        richTextBox1.ScrollToCaret();
                        //richTextBox1.ReadOnly = true;
                    }
                    else
                    {
                        richTextBox1.AppendText(appendText);
                        richTextBox1.ScrollToCaret();
                    }
                }
            }
            else
            {
                id = e.ChatName;//群聊的名字
                if (e.ChatName == Current_group.ID)
                {
                    if (message == "sticker_1" || message == "sticker_2" || message == "sticker_3")//如果是表情
                    {
                        string s = message.Substring(message.Length - 1, 1);
                        int t = int.Parse(s);

                        richTextBox1.AppendText(appendText1);

                        SetClipboard2(images[t-1]);

                        if (richTextBox1.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
                        {
                            richTextBox1.Paste();
                            //richTextBox1.AppendText(Environment.NewLine);
                        }

                        richTextBox1.AppendText(Environment.NewLine);
                        richTextBox1.ScrollToCaret();

                    }
                    else
                    {
                        this.richTextBox1.AppendText(appendText);
                        this.richTextBox1.ScrollToCaret();
                    }
                }
            }


            //加入聊天记录
            string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
            if (!File.Exists(Path + id + "_LTJL" + ".txt"))
            {
                File.Create(Path + id + "_LTJL" + ".txt");
            }
            StreamWriter sw = new StreamWriter(Path + id + "_LTJL" + ".txt", true);
            sw.Write(appendText);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();

        }
        //发送文件
        private void Btn_SendFile_Click(object sender, EventArgs e)
        {
            if (flag_FG)
            {
                string account = Current_friend.ID;

                string r = search_friend_state(account);
                if (r == "ooo" || r == "hhh" || r == "n")
                {
                    MessageBox.Show("连接不到对方");
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "所有文件(*.*)|*.*";
                    ofd.ShowDialog();
                    string path = ofd.FileName;
                    if (path == "") return;

                    string[] temp = path.Split('\\');
                    string name = temp[temp.Length - 1];

                    FileStream file = new FileStream(path, FileMode.Open);

                    Sender s = new Sender(r, Account_data.MyAccount);
                    if (s.SendFriendFile(name, file))
                    {
                        string str = "<Me>: " + DateTime.Now.ToString() + "\n"
                        + name + "\n";

                        richTextBox1.AppendText(str);
                        richTextBox1.ScrollToCaret();
                        Input_text.Focus();

                        string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
                        if (!File.Exists(Path + Current_friend.ID + "_LTJL" + ".txt"))
                        {
                            File.Create(Path + Current_friend.ID + "_LTJL" + ".txt");
                        }
                        StreamWriter sw = new StreamWriter(Path + Current_friend.ID + "_LTJL" + ".txt", true);
                        sw.Write(str);
                        //清空缓冲区
                        sw.Flush();
                        //关闭流
                        sw.Close();
                    }
                }
            }
            else
            {
                string account = Current_group.ID;

                foreach (Friend f in Current_group.group_member)
                {
                    if (f.ID == username)//这个地方一定要改啊！！！！
                    {
                        continue;
                    }

                    string r = search_friend_state(f.ID);
                    if (r == "ooo" || r == "hhh" || r == "n")
                    {
                        //MessageBox.Show("连接不到对方");
                    }
                    else
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Filter = "所有文件(*.*)|*.*";
                        ofd.ShowDialog();
                        string path = ofd.FileName;
                        if (path == "") return;

                        string[] temp = path.Split('\\');
                        string name = temp[temp.Length - 1];

                        FileStream file = new FileStream(path, FileMode.Open);

                        Sender s = new Sender(r, Account_data.MyAccount);
                        if (s.SendGroupFile(Current_group.ID, name, file))
                        {
                            string str = "<Me>: " + DateTime.Now.ToString() + "\n"
                            + name + "\n";

                            richTextBox1.AppendText(str);
                            richTextBox1.ScrollToCaret();
                            Input_text.Focus();

                            string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
                            if (!File.Exists(Path + account + "_LTJL" + ".txt"))
                            {
                                File.Create(Path + account + "_LTJL" + ".txt");
                            }
                            StreamWriter sw = new StreamWriter(Path + account + "_LTJL" + ".txt", true);
                            sw.Write(str);
                            //清空缓冲区
                            sw.Flush();
                            //关闭流
                            sw.Close();
                        }
                    }
                }
            }
        }
        
        //添加群组
        private void Btn_addGroup_Click(object sender, EventArgs e)
        {
            Add_Group add = new Add_Group(username);
            add.Show();
        }
        //快捷键ctrl+Enter发送
        private void Input_text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
            {
                //Btn_Send.PerformClick();
                Input_text.Clear();//抬起之后清除输入框
                Input_text.Focus();
            }
        }
        //去除快捷键产生的多余的回车
        private void Input_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
            {
                Btn_Send.PerformClick();
                Input_text.Clear();
                Input_text.Focus();
            }
        }
        //发表情窗体
        private void Btn_emoji_Click(object sender, EventArgs e)
        {
            Form Sticker_Form = new sticker(m);
            Point mouse_p = MousePosition;
            if (flag_OC)
            {                
                flag_OC = false;

                Sticker_Form.Show();
                Sticker_Form.Location = new Point(mouse_p.X - Sticker_Form.Width + 10, mouse_p.Y - Sticker_Form.Height + 10);
                Sticker_timer.Start();
            }
            else
            {
                Sticker_Form.Close();
                flag_OC = true;
                Sticker_timer.Stop();
            }
        }

        //发表情
        private void afterTagChanged()
        {
            if (flag_FG)//给好友发
            {
                string account = Current_friend.ID;

                string r = search_friend_state(account);
                if (r == "ooo" || r == "hhh" || r == "n")
                {
                    MessageBox.Show("连接不到对方");
                }
                else
                {
                    Sender s = new Sender(r, Account_data.MyAccount);
                    if (s.SendFriendMessage("sticker" + "_" + sticker_tag.ToString()))
                    {
                        string str = "<Me>: " + DateTime.Now.ToString() + Environment.NewLine;
                        //richTextBox1.ReadOnly = false;
                        richTextBox1.AppendText(str);
                        Clipboard.SetImage(images[sticker_tag - 1]);

                        if (richTextBox1.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
                        {
                            richTextBox1.Paste();
                        }
                        richTextBox1.AppendText(Environment.NewLine);
                        richTextBox1.ScrollToCaret();
                        //richTextBox1.ReadOnly = true;
                        //加入聊天记录
                        string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
                        if (!File.Exists(Path + Current_friend.ID + "_LTJL" + ".txt"))
                        {
                            File.Create(Path + Current_friend.ID + "LTJU" + ".txt").Close();

                        }
                        StreamWriter sw = new StreamWriter(Path + Current_friend.ID + "_LTJL" + ".txt", true);
                        sw.Write(str + "sticker" + "_" + sticker_tag.ToString() + Environment.NewLine);
                        //清空缓冲区
                        sw.Flush();
                        //关闭流
                        sw.Close();
                    }
                }
            }
            else//群组发
            {
                string id = Current_group.ID;

                foreach (Friend f in Current_group.group_member)
                {
                    if (f.ID == username)//这个地方一定要改啊！！！！
                    {
                        continue;
                    }

                    string r = search_friend_state(f.ID);
                    if (r == "ooo" || r == "hhh" || r == "n")
                    {

                    }
                    else
                    {
                        Sender s = new Sender(r, Account_data.MyAccount);
                        if (s.SendGroupMessage(id, "sticker" + "_" + sticker_tag.ToString()))
                        {
                            string str = "<Me>: " + DateTime.Now.ToString() + Environment.NewLine;
                            //richTextBox1.ReadOnly = false;
                            richTextBox1.AppendText(str);
                            Clipboard.SetImage(images[sticker_tag - 1]);

                            if (richTextBox1.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
                            {
                                richTextBox1.Paste();
                            }
                            richTextBox1.AppendText(Environment.NewLine);
                            richTextBox1.ScrollToCaret();
                           
                            //加入聊天记录
                            string Path = Application.StartupPath + "\\" + username + "\\" + "Contacts" + "\\";
                            if (!File.Exists(Path + id + "_LTJL" + ".txt"))
                            {
                                File.Create(Path + id + "LTJU" + ".txt").Close();

                            }
                            StreamWriter sw = new StreamWriter(Path + id + "_LTJL" + ".txt", true);
                            sw.Write(str);
                            //清空缓冲区
                            sw.Flush();
                            //关闭流
                            sw.Close();
                        }

                    }
                }
            }
        }

        //用于检测是否发送表情
        private void Sticker_timer_Tick(object sender, EventArgs e)
        {
            if (sticker_tag != 0)
            {
                Sticker_timer.Stop();
                afterTagChanged();
                sticker_tag = 0;                
            }
        }

        /// <summary>
        /// 复制图像到剪切板
        /// </summary>
        [STAThread]
        public static void SetClipboard(Image image)
        {
            Clipboard.SetImage(image);
        }

        /// <summary>
        /// 复制图像到剪切板
        /// </summary>
        public static void SetClipboard2(Image image)
        {
            Thread th = new Thread(new ThreadStart(delegate ()
            {
                SetClipboard(image);
            }));
            th.TrySetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();
        }

        
    }
}
