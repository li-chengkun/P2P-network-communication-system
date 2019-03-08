using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;

namespace LChat
{
    class Sender
    {
        private string obj; //目标主机
        private string account;//自己账户

        

        public Sender(string str, string a)
        {
            this.obj = str;
            this.account = a;
        }

        private bool SendMessage(string head, string sendMessage)
        {
            try
            {
                TcpClient client = new TcpClient(obj, Account_data.ServerPort);
                NetworkStream stream = client.GetStream();

                byte[] data;

                string pkt = head + sendMessage;
                data = Encoding.UTF8.GetBytes(pkt);
                stream.Write(data, 0, data.Length);

                stream.Close();
                client.Close();

                return true;
            }
            catch
            {
                MessageBox.Show("无法连接到对方");
                return false;
            }
        }

        public bool SendFriendMessage(string msg)
        {
            string head = MsgType.Text.ToString() + account;
            return SendMessage(head, msg);
        }

        public bool SendGroupMessage(string group, string msg)
        {
            
            string head = MsgType.GroupText.ToString() + account + group.Length.ToString()+ group;//包含群名的长度信息
            return SendMessage(head, msg);
        }

        public bool SendFile(string head, string name, FileStream file)
        {
            try
            {
                TcpClient client = new TcpClient(obj, Account_data.ServerPort);
                NetworkStream stream = client.GetStream();

                byte[] headByte = Encoding.UTF8.GetBytes(head);
                stream.Write(headByte, 0, headByte.Length);

                byte[] fileNameByte = Encoding.UTF8.GetBytes(name);
                byte[] fileNameLengthByte = Encoding.UTF8.GetBytes(fileNameByte.Length.ToString("D11"));

                byte[] fileAttributeByte = new byte[fileNameByte.Length + fileNameLengthByte.Length];
                fileNameLengthByte.CopyTo(fileAttributeByte, 0);  //文件名字符流的长度的字符流排在前面。
                fileNameByte.CopyTo(fileAttributeByte, fileNameLengthByte.Length);  //紧接着文件名的字符流

                stream.Write(fileAttributeByte, 0, fileAttributeByte.Length);

                int fileReadSize = 0;
                long fileLength = 0;
                while (fileLength < file.Length)
                {
                    byte[] buffer = new byte[2048];
                    fileReadSize = file.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, fileReadSize);
                    fileLength += fileReadSize;

                }
                file.Flush();
                stream.Flush();
                file.Close();
                stream.Close();

                return true;
            }
            catch
            {
                MessageBox.Show("无法连接到对方");
                return false;
            }
        }

        public bool SendFriendFile(string name, FileStream file)
        {
            string head = MsgType.File.ToString() + account;
            return SendFile(head, name, file);
        }

        public bool SendGroupFile(string group, string name, FileStream file)
        {
            //string head = MsgType.GroupFile.ToString() + account + group;
            string head = MsgType.GroupFile.ToString() + account + group.Length.ToString() + group;//包含群名的长度信息
            return SendFile(head, name, file);
        }
    }
}

