
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
            this.list_petList = new System.Windows.Forms.ListView();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_spawn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // list_petList
            // 
            this.list_petList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_petList.HideSelection = false;
            this.list_petList.Location = new System.Drawing.Point(12, 84);
            this.list_petList.Name = "list_petList";
            this.list_petList.Size = new System.Drawing.Size(304, 386);
            this.list_petList.TabIndex = 1;
            this.list_petList.UseCompatibleStateImageBehavior = false;
            this.list_petList.View = System.Windows.Forms.View.SmallIcon;
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_add.Location = new System.Drawing.Point(12, 46);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(149, 32);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_remove.Location = new System.Drawing.Point(167, 46);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(149, 32);
            this.btn_remove.TabIndex = 3;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_spawn
            // 
            this.btn_spawn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_spawn.Location = new System.Drawing.Point(12, 8);
            this.btn_spawn.Name = "btn_spawn";
            this.btn_spawn.Size = new System.Drawing.Size(304, 32);
            this.btn_spawn.TabIndex = 4;
            this.btn_spawn.Text = "Spawn Pet";
            this.btn_spawn.UseVisualStyleBackColor = true;
            this.btn_spawn.Click += new System.EventHandler(this.btn_spawn_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 482);
            this.Controls.Add(this.btn_spawn);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.list_petList);
            this.Name = "Launcher";
            this.Text = "Your Desktop Pet";
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView list_petList;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_spawn;
    }
}