using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LChat
{
    class Account_data
    {
        public const int ServerPort = 10000;
        public static string MyAccount = "";
        
       
        public static string DirContacts
        {
            get
            {
                return Application.StartupPath + "\\" + MyAccount + "\\Contacts\\";
            }
        }

        public static string DirFile
        {
            get
            {
                return Application.StartupPath + "\\" + MyAccount + "\\File\\";
            }
        }

        public static void CreatePath()
        {
            string dirPath = Application.StartupPath + "\\" + MyAccount + "\\";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                string path = dirPath + "\\File\\";
                Directory.CreateDirectory(path);
                path = dirPath + "\\Contacts\\";
                Directory.CreateDirectory(path);
                FileStream file = File.Create(path + "Friends.txt");
                file.Close();
                file = File.Create(path + "Groups.txt");
                file.Close();
                StreamWriter f = new StreamWriter(DirContacts + "Friends.txt");
                f.WriteLine(MyAccount);
                f.Flush();
                f.Close();
            }
        }
    }
}
