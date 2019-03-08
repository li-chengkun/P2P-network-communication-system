using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LChat
{
    public class AddMessageEventArgs : EventArgs
    {
        public string mess;   //存放要显示的内容
        public string Sender; //发送者
        public string ChatName;

        public AddMessageEventArgs(string msg, string sender, string chatName)
        {
            mess = msg;
            Sender = sender;
            ChatName = chatName;
        }

    }

    public static class MsgType
    {
        public const char
        Text = '1',
        File = '2',
        GroupText = '3',
        GroupFile = '4';
    }

    class Listener
    {

        //private Thread th;
        private TcpListener tcpl;
        public bool listenerRun = true;    //判断是否启动

        public event EventHandler<AddMessageEventArgs> OnAddMessage;

        public Listener(EventHandler<AddMessageEventArgs> e)
        {
            OnAddMessage = e;
        }
        
        public Listener()
        {

        }

        //另一个线程开始监听
        public void StartListener()
        {
            Thread thread = new Thread(Listen)
            {
                IsBackground = true
            };
            thread.Start();
        }

        //停止监听
        public void Stop()
        {
            if (tcpl != null)
            {
                tcpl.Stop();
                //th.Abort();
            }
            
        }

        private void Listen()
        {
            try
            {

                //IPAddress ipAddress = new IPAddress(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].Address);
                IPAddress ipAddress = new IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);

                tcpl = new TcpListener(ipAddress, Account_data.ServerPort);
                tcpl.Start();

                while (listenerRun)
                {
                    Socket rcvSocket = tcpl.AcceptSocket();
                    Thread th = new Thread(ExtractData);
                    th.Start(rcvSocket);
                }
            }
            catch
            {

            }
        }

        private void ExtractData(Object obj)
        {
            Socket client = obj as Socket;
            AddMessageEventArgs eventArgs;

            #region Head
            byte[] cmdB = new byte[2];
            client.Receive(cmdB, 1, SocketFlags.None);
            string cmd = Encoding.UTF8.GetString(cmdB);

            byte[] accountB = new byte[10];
            client.Receive(accountB, 10, SocketFlags.None);
            string account = Encoding.UTF8.GetString(accountB);

            string group = "";
            if (cmd[0] == MsgType.GroupText || cmd[0] == MsgType.GroupFile)
            {
                byte[] groupnamel = new byte[2];//群名长度
                client.Receive(groupnamel, 1, SocketFlags.None);
                string l = Encoding.UTF8.GetString(groupnamel);
                int L;
                int.TryParse(l, out L);
                byte[] groupB = new byte[L];
                client.Receive(groupB, L, SocketFlags.None);
                group = Encoding.UTF8.GetString(groupB);
            }
            #endregion

            string msg;

            #region Message Type
            switch (cmd[0])
            {
                #region Message
                case MsgType.Text:
                    msg = GetText(client);

                    eventArgs = new AddMessageEventArgs(msg, account, account);
                    OnAddMessage(this, eventArgs);
                    break;
                #endregion

                #region GroupMessage
                case MsgType.GroupText:
                    msg = GetText(client);

                    eventArgs = new AddMessageEventArgs(msg, account, group);
                    OnAddMessage(this, eventArgs);
                    break;
                #endregion

                #region File
                case MsgType.File:
                    msg = GetFile(client);

                    eventArgs = new AddMessageEventArgs(msg, account, account);
                    OnAddMessage(this, eventArgs);
                    break;
                #endregion

                #region GroupFile
                case MsgType.GroupFile:
                    msg = GetFile(client);

                    eventArgs = new AddMessageEventArgs(msg, account, group);
                    OnAddMessage(this, eventArgs);
                    break;
                #endregion
                default:

                    break;
            }
            #endregion
        }



        private string GetText(Socket client)
        {
            byte[] stream = new byte[1024];
            int rcvLen = client.Receive(stream);
            string msg = Encoding.UTF8.GetString(stream, 0, rcvLen);
            client.Close();
            return msg;
        }


        private string GetFile(Socket client)
        {
            byte[] fileNameLengthForValueByte = Encoding.UTF8.GetBytes((256).ToString("D11"));
            byte[] fileNameLengByte = new byte[1024];
            int fileNameLengthSize = client.Receive(fileNameLengByte, fileNameLengthForValueByte.Length, SocketFlags.None);
            string fileNameLength = Encoding.UTF8.GetString(fileNameLengByte, 0, fileNameLengthSize);

            int fileNameLengthNum = Convert.ToInt32(fileNameLength);
            byte[] fileNameByte = new byte[fileNameLengthNum];

            int fileNameSize = client.Receive(fileNameByte, fileNameLengthNum, SocketFlags.None);
            string fileName = Encoding.UTF8.GetString(fileNameByte, 0, fileNameSize);

            string filePath = Account_data.DirFile + fileName;
            string tempPath = filePath;
            int i = 1;
            while (File.Exists(tempPath))
            {
                tempPath = filePath.Insert(filePath.LastIndexOf('.'), " (" + i.ToString() + ")");
                i++;
            }
            filePath = tempPath;

            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            int fileReadSize = 0;
            byte[] buffer = new byte[2048];
            while ((fileReadSize = client.Receive(buffer, buffer.Length, SocketFlags.None)) > 0)
            {
                file.Write(buffer, 0, fileReadSize);
            }

            file.Flush();
            file.Close();

            client.Close();

            return fileName;
        }


    }
}
