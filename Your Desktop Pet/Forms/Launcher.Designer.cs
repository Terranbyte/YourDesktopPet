
namespace Your_Desktop_Pet.Forms
{
    partial class Launcher
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
            this.list_petList = new System.Windows.Forms.ListView();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_spawn = new System.Windows.Forms.Button();
            this.btn_sdk = new System.Windows.Forms.Button();
            this.btn_options = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_details = new System.Windows.Forms.Button();
            this.btn_activePets = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // list_petList
            // 
            this.list_petList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_petList.HideSelection = false;
            this.list_petList.Location = new System.Drawing.Point(12, 89);
            this.list_petList.Name = "list_petList";
            this.list_petList.Size = new System.Drawing.Size(306, 386);
            this.list_petList.TabIndex = 1;
            this.list_petList.UseCompatibleStateImageBehavior = false;
            this.list_petList.View = System.Windows.Forms.View.SmallIcon;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 51);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(98, 32);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_spawn
            // 
            this.btn_spawn.Location = new System.Drawing.Point(12, 13);
            this.btn_spawn.Name = "btn_spawn";
            this.btn_spawn.Size = new System.Drawing.Size(306, 32);
            this.btn_spawn.TabIndex = 4;
            this.btn_spawn.Text = "Spawn Pet";
            this.btn_spawn.UseVisualStyleBackColor = true;
            this.btn_spawn.Click += new System.EventHandler(this.btn_spawn_Click);
            // 
            // btn_sdk
            // 
            this.btn_sdk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_sdk.Location = new System.Drawing.Point(12, 557);
            this.btn_sdk.Name = "btn_sdk";
            this.btn_sdk.Size = new System.Drawing.Size(306, 32);
            this.btn_sdk.TabIndex = 5;
            this.btn_sdk.Text = "SDK";
            this.btn_sdk.UseVisualStyleBackColor = true;
            this.btn_sdk.Click += new System.EventHandler(this.btn_sdk_Click);
            // 
            // btn_options
            // 
            this.btn_options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_options.Location = new System.Drawing.Point(12, 519);
            this.btn_options.Name = "btn_options";
            this.btn_options.Size = new System.Drawing.Size(306, 32);
            this.btn_options.TabIndex = 6;
            this.btn_options.Text = "Options";
            this.btn_options.UseVisualStyleBackColor = true;
            this.btn_options.Click += new System.EventHandler(this.btn_options_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 32);
            this.button2.TabIndex = 7;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_details
            // 
            this.btn_details.Location = new System.Drawing.Point(220, 51);
            this.btn_details.Name = "btn_details";
            this.btn_details.Size = new System.Drawing.Size(98, 32);
            this.btn_details.TabIndex = 8;
            this.btn_details.Text = "Pet Details";
            this.btn_details.UseVisualStyleBackColor = true;
            this.btn_details.Click += new System.EventHandler(this.btn_details_Click);
            // 
            // btn_activePets
            // 
            this.btn_activePets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_activePets.Location = new System.Drawing.Point(12, 481);
            this.btn_activePets.Name = "btn_activePets";
            this.btn_activePets.Size = new System.Drawing.Size(306, 32);
            this.btn_activePets.TabIndex = 9;
            this.btn_activePets.Text = "View Active Pets";
            this.btn_activePets.UseVisualStyleBackColor = true;
            this.btn_activePets.Click += new System.EventHandler(this.btn_activePets_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 5;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 601);
            this.Controls.Add(this.btn_activePets);
            this.Controls.Add(this.btn_details);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_options);
            this.Controls.Add(this.btn_sdk);
            this.Controls.Add(this.btn_spawn);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.list_petList);
            this.Name = "Launcher";
            this.Text = "Your Desktop Pet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView list_petList;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_spawn;
        private System.Windows.Forms.Button btn_sdk;
        private System.Windows.Forms.Button btn_options;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_details;
        private System.Windows.Forms.Button btn_activePets;
        private System.Windows.Forms.Timer updateTimer;
    }
}