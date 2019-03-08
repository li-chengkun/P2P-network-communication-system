using LChat.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LChat
{
    public partial class sticker : Form
    {
        public Main main ;
        static int N = 3;//表情数目
        Image[] images = new Image[N];
        
        
        public sticker()
        {
            InitializeComponent();
        }
        public sticker(Main m)
        {
            InitializeComponent();
            main = m;
            images[0] = Resources._1;
            images[1] = Resources._2;
            images[2] = Resources._3;
        }

        private void sticker_Load(object sender, EventArgs e)
        {
            //显示表情图像
            for (int i = 1; i <= N; i++)
            {
                PictureBox Ps = new PictureBox();
                Ps.Size = new Size(50, 50);
                Ps.SizeMode = PictureBoxSizeMode.Zoom;
                
                Ps.Image = images[i - 1];
                Ps.Location = new Point((i - 1) * 50 + 10, 50);
                Ps.Cursor = Cursors.Hand;
                Ps.BackColor = Color.Transparent;
                //Ps.Tag = images[i];
                Ps.Click += new EventHandler(SmailPic_Click);
                this.Controls.Add(Ps);
            }
        }

        private void SmailPic_Click(object sender, EventArgs e)
        {
            PictureBox psm = (PictureBox)sender;
            for(int i = 0; i < images.Length; i++)
            {
                if (psm.Image == images[i])
                {
                    
                    main.sticker_tag = i + 1;
                    main.flag_OC = true;
                    //main.Sticker_timer.Start();
                    Close();
                    break;
                }
            }

        }


    }
}
