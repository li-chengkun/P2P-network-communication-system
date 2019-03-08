namespace LChat
{
    partial class Add_Group
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Add_Group));
            this.Groupname = new System.Windows.Forms.TextBox();
            this.label_group = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Groupmember = new System.Windows.Forms.TextBox();
            this.Add_Group_member = new System.Windows.Forms.Button();
            this.Add_group_panel = new System.Windows.Forms.Panel();
            this.CreatGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Groupname
            // 
            this.Groupname.Location = new System.Drawing.Point(86, 11);
            this.Groupname.Name = "Groupname";
            this.Groupname.Size = new System.Drawing.Size(105, 23);
            this.Groupname.TabIndex = 0;
            // 
            // label_group
            // 
            this.label_group.AutoSize = true;
            this.label_group.Location = new System.Drawing.Point(12, 14);
            this.label_group.Name = "label_group";
            this.label_group.Size = new System.Drawing.Size(68, 17);
            this.label_group.TabIndex = 1;
            this.label_group.Text = "群聊名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "群聊成员：";
            // 
            // Groupmember
            // 
            this.Groupmember.Location = new System.Drawing.Point(86, 47);
            this.Groupmember.Name = "Groupmember";
            this.Groupmember.Size = new System.Drawing.Size(105, 23);
            this.Groupmember.TabIndex = 3;
            // 
            // Add_Group_member
            // 
            this.Add_Group_member.Location = new System.Drawing.Point(202, 11);
            this.Add_Group_member.Name = "Add_Group_member";
            this.Add_Group_member.Size = new System.Drawing.Size(33, 58);
            this.Add_Group_member.TabIndex = 4;
            this.Add_Group_member.Text = "添加";
            this.Add_Group_member.UseVisualStyleBackColor = true;
            this.Add_Group_member.Click += new System.EventHandler(this.Add_Group_member_Click);
            // 
            // Add_group_panel
            // 
            this.Add_group_panel.AutoScroll = true;
            this.Add_group_panel.Location = new System.Drawing.Point(3, 77);
            this.Add_group_panel.Name = "Add_group_panel";
            this.Add_group_panel.Size = new System.Drawing.Size(240, 234);
            this.Add_group_panel.TabIndex = 5;
            // 
            // CreatGroup
            // 
            this.CreatGroup.Location = new System.Drawing.Point(132, 325);
            this.CreatGroup.Name = "CreatGroup";
            this.CreatGroup.Size = new System.Drawing.Size(102, 26);
            this.CreatGroup.TabIndex = 6;
            this.CreatGroup.Text = "创建";
            this.CreatGroup.UseVisualStyleBackColor = true;
            this.CreatGroup.Click += new System.EventHandler(this.CreatGroup_Click);
            // 
            // Add_Group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 356);
            this.Controls.Add(this.CreatGroup);
            this.Controls.Add(this.Add_group_panel);
            this.Controls.Add(this.Add_Group_member);
            this.Controls.Add(this.Groupmember);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_group);
            this.Controls.Add(this.Groupname);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Add_Group";
            this.Text = "Creat Group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Groupname;
        private System.Windows.Forms.Label label_group;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Groupmember;
        private System.Windows.Forms.Button Add_Group_member;
        private System.Windows.Forms.Panel Add_group_panel;
        private System.Windows.Forms.Button CreatGroup;
    }
}