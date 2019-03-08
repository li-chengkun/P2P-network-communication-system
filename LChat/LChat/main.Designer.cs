namespace LChat
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Log_out = new System.Windows.Forms.Button();
            this.Search_friend = new System.Windows.Forms.Button();
            this.search_account = new System.Windows.Forms.TextBox();
            this.Top_panel = new System.Windows.Forms.Panel();
            this.Btn_addGroup = new System.Windows.Forms.Button();
            this.Btn_Grouplist = new System.Windows.Forms.Button();
            this.Btn_Friendlist = new System.Windows.Forms.Button();
            this.Left_panel = new System.Windows.Forms.Panel();
            this.List_panel = new System.Windows.Forms.Panel();
            this.Send_panel = new System.Windows.Forms.Panel();
            this.Input_text = new System.Windows.Forms.RichTextBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.Btn_emoji = new System.Windows.Forms.Button();
            this.Btn_SendFile = new System.Windows.Forms.Button();
            this.UP_panel = new System.Windows.Forms.Panel();
            this.Chat_label = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Chat_panel = new System.Windows.Forms.Panel();
            this.Sticker_timer = new System.Windows.Forms.Timer(this.components);
            this.Top_panel.SuspendLayout();
            this.Left_panel.SuspendLayout();
            this.Send_panel.SuspendLayout();
            this.UP_panel.SuspendLayout();
            this.Chat_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Log_out
            // 
            this.Log_out.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Log_out.Location = new System.Drawing.Point(180, 32);
            this.Log_out.Name = "Log_out";
            this.Log_out.Size = new System.Drawing.Size(55, 23);
            this.Log_out.TabIndex = 0;
            this.Log_out.Text = "注销";
            this.Log_out.UseVisualStyleBackColor = true;
            this.Log_out.Click += new System.EventHandler(this.Log_out_Click);
            // 
            // Search_friend
            // 
            this.Search_friend.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Search_friend.Location = new System.Drawing.Point(180, 3);
            this.Search_friend.Name = "Search_friend";
            this.Search_friend.Size = new System.Drawing.Size(56, 23);
            this.Search_friend.TabIndex = 2;
            this.Search_friend.Text = "查询";
            this.Search_friend.UseVisualStyleBackColor = true;
            this.Search_friend.Click += new System.EventHandler(this.Search_friend_Click);
            // 
            // search_account
            // 
            this.search_account.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.search_account.Location = new System.Drawing.Point(72, 3);
            this.search_account.Name = "search_account";
            this.search_account.Size = new System.Drawing.Size(102, 23);
            this.search_account.TabIndex = 3;
            // 
            // Top_panel
            // 
            this.Top_panel.Controls.Add(this.Btn_addGroup);
            this.Top_panel.Controls.Add(this.Btn_Grouplist);
            this.Top_panel.Controls.Add(this.Btn_Friendlist);
            this.Top_panel.Controls.Add(this.Left_panel);
            this.Top_panel.Controls.Add(this.search_account);
            this.Top_panel.Controls.Add(this.Search_friend);
            this.Top_panel.Controls.Add(this.Log_out);
            this.Top_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.Top_panel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Top_panel.Location = new System.Drawing.Point(0, 0);
            this.Top_panel.Name = "Top_panel";
            this.Top_panel.Size = new System.Drawing.Size(238, 485);
            this.Top_panel.TabIndex = 4;
            // 
            // Btn_addGroup
            // 
            this.Btn_addGroup.Location = new System.Drawing.Point(168, 61);
            this.Btn_addGroup.Name = "Btn_addGroup";
            this.Btn_addGroup.Size = new System.Drawing.Size(66, 27);
            this.Btn_addGroup.TabIndex = 9;
            this.Btn_addGroup.Text = "添加群组";
            this.Btn_addGroup.UseVisualStyleBackColor = true;
            this.Btn_addGroup.Click += new System.EventHandler(this.Btn_addGroup_Click);
            // 
            // Btn_Grouplist
            // 
            this.Btn_Grouplist.Location = new System.Drawing.Point(87, 66);
            this.Btn_Grouplist.Name = "Btn_Grouplist";
            this.Btn_Grouplist.Size = new System.Drawing.Size(75, 23);
            this.Btn_Grouplist.TabIndex = 8;
            this.Btn_Grouplist.Text = "群组";
            this.Btn_Grouplist.UseVisualStyleBackColor = true;
            this.Btn_Grouplist.Click += new System.EventHandler(this.Btn_Grouplist_Click);
            // 
            // Btn_Friendlist
            // 
            this.Btn_Friendlist.Location = new System.Drawing.Point(6, 67);
            this.Btn_Friendlist.Name = "Btn_Friendlist";
            this.Btn_Friendlist.Size = new System.Drawing.Size(75, 23);
            this.Btn_Friendlist.TabIndex = 7;
            this.Btn_Friendlist.Text = "好友";
            this.Btn_Friendlist.UseVisualStyleBackColor = true;
            this.Btn_Friendlist.Click += new System.EventHandler(this.Btn_Friendlist_Click);
            // 
            // Left_panel
            // 
            this.Left_panel.Controls.Add(this.List_panel);
            this.Left_panel.Location = new System.Drawing.Point(6, 96);
            this.Left_panel.Name = "Left_panel";
            this.Left_panel.Size = new System.Drawing.Size(232, 386);
            this.Left_panel.TabIndex = 6;
            // 
            // List_panel
            // 
            this.List_panel.AutoScroll = true;
            this.List_panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.List_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.List_panel.Location = new System.Drawing.Point(0, 3);
            this.List_panel.Name = "List_panel";
            this.List_panel.Size = new System.Drawing.Size(232, 383);
            this.List_panel.TabIndex = 1;
            // 
            // Send_panel
            // 
            this.Send_panel.Controls.Add(this.Input_text);
            this.Send_panel.Controls.Add(this.Btn_Send);
            this.Send_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Send_panel.Location = new System.Drawing.Point(238, 385);
            this.Send_panel.Name = "Send_panel";
            this.Send_panel.Size = new System.Drawing.Size(562, 100);
            this.Send_panel.TabIndex = 6;
            // 
            // Input_text
            // 
            this.Input_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Input_text.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Input_text.Location = new System.Drawing.Point(3, 3);
            this.Input_text.Name = "Input_text";
            this.Input_text.Size = new System.Drawing.Size(399, 71);
            this.Input_text.TabIndex = 0;
            this.Input_text.Text = "";
            this.Input_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_text_KeyDown);
            this.Input_text.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Input_text_KeyUp);
            // 
            // Btn_Send
            // 
            this.Btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Send.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Send.Location = new System.Drawing.Point(408, 3);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(89, 40);
            this.Btn_Send.TabIndex = 1;
            this.Btn_Send.Text = "发送";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // Btn_emoji
            // 
            this.Btn_emoji.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_emoji.Location = new System.Drawing.Point(330, 357);
            this.Btn_emoji.Name = "Btn_emoji";
            this.Btn_emoji.Size = new System.Drawing.Size(66, 26);
            this.Btn_emoji.TabIndex = 3;
            this.Btn_emoji.Text = "表情";
            this.Btn_emoji.UseVisualStyleBackColor = true;
            this.Btn_emoji.Click += new System.EventHandler(this.Btn_emoji_Click);
            // 
            // Btn_SendFile
            // 
            this.Btn_SendFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_SendFile.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_SendFile.Location = new System.Drawing.Point(258, 357);
            this.Btn_SendFile.Name = "Btn_SendFile";
            this.Btn_SendFile.Size = new System.Drawing.Size(55, 26);
            this.Btn_SendFile.TabIndex = 2;
            this.Btn_SendFile.Text = "文件";
            this.Btn_SendFile.UseVisualStyleBackColor = true;
            this.Btn_SendFile.Click += new System.EventHandler(this.Btn_SendFile_Click);
            // 
            // UP_panel
            // 
            this.UP_panel.Controls.Add(this.Chat_label);
            this.UP_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.UP_panel.Location = new System.Drawing.Point(238, 0);
            this.UP_panel.Name = "UP_panel";
            this.UP_panel.Size = new System.Drawing.Size(562, 30);
            this.UP_panel.TabIndex = 7;
            // 
            // Chat_label
            // 
            this.Chat_label.AutoSize = true;
            this.Chat_label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Chat_label.Location = new System.Drawing.Point(17, 6);
            this.Chat_label.Name = "Chat_label";
            this.Chat_label.Size = new System.Drawing.Size(68, 17);
            this.Chat_label.TabIndex = 0;
            this.Chat_label.Text = "聊天对象：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(562, 318);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Chat_panel
            // 
            this.Chat_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Chat_panel.Controls.Add(this.richTextBox1);
            this.Chat_panel.Location = new System.Drawing.Point(241, 36);
            this.Chat_panel.Name = "Chat_panel";
            this.Chat_panel.Size = new System.Drawing.Size(562, 318);
            this.Chat_panel.TabIndex = 5;
            // 
            // Sticker_timer
            // 
            this.Sticker_timer.Interval = 1000;
            this.Sticker_timer.Tick += new System.EventHandler(this.Sticker_timer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 485);
            this.Controls.Add(this.Btn_SendFile);
            this.Controls.Add(this.Btn_emoji);
            this.Controls.Add(this.UP_panel);
            this.Controls.Add(this.Send_panel);
            this.Controls.Add(this.Chat_panel);
            this.Controls.Add(this.Top_panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing_1);
            this.Top_panel.ResumeLayout(false);
            this.Top_panel.PerformLayout();
            this.Left_panel.ResumeLayout(false);
            this.Send_panel.ResumeLayout(false);
            this.UP_panel.ResumeLayout(false);
            this.UP_panel.PerformLayout();
            this.Chat_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Log_out;
        private System.Windows.Forms.Button Search_friend;
        private System.Windows.Forms.TextBox search_account;
        private System.Windows.Forms.Panel Top_panel;
        private System.Windows.Forms.Panel Send_panel;
        private System.Windows.Forms.Panel Left_panel;
        private System.Windows.Forms.RichTextBox Input_text;
        private System.Windows.Forms.Panel List_panel;
        private System.Windows.Forms.Panel UP_panel;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Button Btn_Grouplist;
        private System.Windows.Forms.Button Btn_Friendlist;
        private System.Windows.Forms.Label Chat_label;
        private System.Windows.Forms.Button Btn_SendFile;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel Chat_panel;
        private System.Windows.Forms.Button Btn_addGroup;
        private System.Windows.Forms.Button Btn_emoji;
        private System.Windows.Forms.Timer Sticker_timer;
    }
}