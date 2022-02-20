namespace Your_Desktop_Pet.Forms.SDK
{
    partial class PetTestingWindow
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
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pos_x = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.curr_anim = new System.Windows.Forms.TextBox();
            this.pos_y = new System.Windows.Forms.TextBox();
            this.size_x = new System.Windows.Forms.TextBox();
            this.size_y = new System.Windows.Forms.TextBox();
            this.num_frames = new System.Windows.Forms.TextBox();
            this.anim_index = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_respawn = new System.Windows.Forms.Button();
            this.objectView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_luaObject = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_sprite = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.luaObjectDataSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spriteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.visible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.objectView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luaObjectDataSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 5;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Position X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Position Y";
            // 
            // pos_x
            // 
            this.pos_x.Location = new System.Drawing.Point(12, 25);
            this.pos_x.Name = "pos_x";
            this.pos_x.ReadOnly = true;
            this.pos_x.Size = new System.Drawing.Size(124, 20);
            this.pos_x.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Size X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(399, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Size Y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Current Animation";
            // 
            // curr_anim
            // 
            this.curr_anim.Location = new System.Drawing.Point(15, 64);
            this.curr_anim.Name = "curr_anim";
            this.curr_anim.ReadOnly = true;
            this.curr_anim.Size = new System.Drawing.Size(121, 20);
            this.curr_anim.TabIndex = 12;
            // 
            // pos_y
            // 
            this.pos_y.Location = new System.Drawing.Point(142, 25);
            this.pos_y.Name = "pos_y";
            this.pos_y.ReadOnly = true;
            this.pos_y.Size = new System.Drawing.Size(121, 20);
            this.pos_y.TabIndex = 13;
            // 
            // size_x
            // 
            this.size_x.Location = new System.Drawing.Point(269, 25);
            this.size_x.Name = "size_x";
            this.size_x.ReadOnly = true;
            this.size_x.Size = new System.Drawing.Size(121, 20);
            this.size_x.TabIndex = 14;
            // 
            // size_y
            // 
            this.size_y.Location = new System.Drawing.Point(396, 25);
            this.size_y.Name = "size_y";
            this.size_y.ReadOnly = true;
            this.size_y.Size = new System.Drawing.Size(121, 20);
            this.size_y.TabIndex = 15;
            // 
            // num_frames
            // 
            this.num_frames.Location = new System.Drawing.Point(142, 64);
            this.num_frames.Name = "num_frames";
            this.num_frames.ReadOnly = true;
            this.num_frames.Size = new System.Drawing.Size(121, 20);
            this.num_frames.TabIndex = 16;
            // 
            // anim_index
            // 
            this.anim_index.Location = new System.Drawing.Point(269, 64);
            this.anim_index.Name = "anim_index";
            this.anim_index.ReadOnly = true;
            this.anim_index.Size = new System.Drawing.Size(121, 20);
            this.anim_index.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Frames Of Animation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Current Frames";
            // 
            // btn_respawn
            // 
            this.btn_respawn.Location = new System.Drawing.Point(396, 61);
            this.btn_respawn.Name = "btn_respawn";
            this.btn_respawn.Size = new System.Drawing.Size(121, 23);
            this.btn_respawn.TabIndex = 20;
            this.btn_respawn.Text = "Respawn Pet";
            this.btn_respawn.UseVisualStyleBackColor = true;
            this.btn_respawn.Click += new System.EventHandler(this.btn_respawn_Click);
            // 
            // objectView
            // 
            this.objectView.AllowUserToAddRows = false;
            this.objectView.AllowUserToDeleteRows = false;
            this.objectView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectView.AutoGenerateColumns = false;
            this.objectView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.objectView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.pos,
            this.size,
            this.spriteName,
            this.visible});
            this.objectView.DataSource = this.luaObjectDataSourceBindingSource;
            this.objectView.Location = new System.Drawing.Point(15, 103);
            this.objectView.Name = "objectView";
            this.objectView.ReadOnly = true;
            this.objectView.Size = new System.Drawing.Size(505, 229);
            this.objectView.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Active objects";
            // 
            // cb_luaObject
            // 
            this.cb_luaObject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_luaObject.AutoCheck = false;
            this.cb_luaObject.AutoSize = true;
            this.cb_luaObject.Location = new System.Drawing.Point(12, 351);
            this.cb_luaObject.Name = "cb_luaObject";
            this.cb_luaObject.Size = new System.Drawing.Size(75, 17);
            this.cb_luaObject.TabIndex = 23;
            this.cb_luaObject.Text = "LuaObject";
            this.cb_luaObject.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 335);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Components";
            // 
            // cb_sprite
            // 
            this.cb_sprite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb_sprite.AutoCheck = false;
            this.cb_sprite.AutoSize = true;
            this.cb_sprite.Location = new System.Drawing.Point(93, 351);
            this.cb_sprite.Name = "cb_sprite";
            this.cb_sprite.Size = new System.Drawing.Size(53, 17);
            this.cb_sprite.TabIndex = 25;
            this.cb_sprite.Text = "Sprite";
            this.cb_sprite.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoCheck = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(152, 351);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 17);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "Animator";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox2.AutoCheck = false;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(225, 351);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(73, 17);
            this.checkBox2.TabIndex = 27;
            this.checkBox2.Text = "PetObject";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // luaObjectDataSourceBindingSource
            // 
            this.luaObjectDataSourceBindingSource.DataSource = typeof(Your_Desktop_Pet.Forms.SDK.LuaObjectDataSource);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // pos
            // 
            this.pos.HeaderText = "Position";
            this.pos.Name = "pos";
            this.pos.ReadOnly = true;
            // 
            // size
            // 
            this.size.HeaderText = "Size";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            // 
            // spriteName
            // 
            this.spriteName.HeaderText = "Sprite Name";
            this.spriteName.Name = "spriteName";
            this.spriteName.ReadOnly = true;
            // 
            // visible
            // 
            this.visible.HeaderText = "Is Visible";
            this.visible.Name = "visible";
            this.visible.ReadOnly = true;
            // 
            // PetTestingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 380);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cb_sprite);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cb_luaObject);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.objectView);
            this.Controls.Add(this.btn_respawn);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.anim_index);
            this.Controls.Add(this.num_frames);
            this.Controls.Add(this.size_y);
            this.Controls.Add(this.size_x);
            this.Controls.Add(this.pos_y);
            this.Controls.Add(this.curr_anim);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pos_x);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "PetTestingWindow";
            this.Text = "PetTestingWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PetTestingWindow_FormClosing);
            this.Load += new System.EventHandler(this.PetTestWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objectView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luaObjectDataSourceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pos_x;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox curr_anim;
        private System.Windows.Forms.TextBox pos_y;
        private System.Windows.Forms.TextBox size_x;
        private System.Windows.Forms.TextBox size_y;
        private System.Windows.Forms.TextBox num_frames;
        private System.Windows.Forms.TextBox anim_index;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_respawn;
        private System.Windows.Forms.DataGridView objectView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_luaObject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cb_sprite;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.BindingSource luaObjectDataSourceBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private System.Windows.Forms.DataGridViewTextBoxColumn spriteName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn visible;
    }
}