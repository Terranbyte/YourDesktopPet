namespace Your_Desktop_Pet.Forms.SDK
{
    partial class SDKWindow
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
            this.btn_projects = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.projectView = new System.Windows.Forms.DataGridView();
            this.metadata = new System.Windows.Forms.DataGridViewButtonColumn();
            this.test = new System.Windows.Forms.DataGridViewButtonColumn();
            this.compile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectDataSourceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.projectView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSourceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_projects
            // 
            this.btn_projects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_projects.Location = new System.Drawing.Point(339, 12);
            this.btn_projects.Name = "btn_projects";
            this.btn_projects.Size = new System.Drawing.Size(121, 23);
            this.btn_projects.TabIndex = 1;
            this.btn_projects.Text = "Move Project Folder";
            this.btn_projects.UseVisualStyleBackColor = true;
            this.btn_projects.Click += new System.EventHandler(this.btn_projects_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(121, 12);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(103, 23);
            this.btn_delete.TabIndex = 2;
            this.btn_delete.Text = "Delete Project";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(12, 12);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(103, 23);
            this.btn_create.TabIndex = 3;
            this.btn_create.Text = "Create Project";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // projectView
            // 
            this.projectView.AllowUserToAddRows = false;
            this.projectView.AllowUserToDeleteRows = false;
            this.projectView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectView.AutoGenerateColumns = false;
            this.projectView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projectView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.metadata,
            this.test,
            this.compile});
            this.projectView.DataSource = this.projectDataSourceBindingSource;
            this.projectView.Location = new System.Drawing.Point(12, 41);
            this.projectView.Name = "projectView";
            this.projectView.Size = new System.Drawing.Size(448, 337);
            this.projectView.TabIndex = 4;
            this.projectView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectView_CellContentClick);
            this.projectView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.projectView_CellValueChanged);
            // 
            // metadata
            // 
            this.metadata.HeaderText = "Metadata";
            this.metadata.Name = "metadata";
            this.metadata.Text = "Edit Metadata";
            this.metadata.UseColumnTextForButtonValue = true;
            // 
            // test
            // 
            this.test.HeaderText = "Test";
            this.test.Name = "test";
            this.test.Text = "Test Pet";
            this.test.UseColumnTextForButtonValue = true;
            // 
            // compile
            // 
            this.compile.HeaderText = "Build";
            this.compile.Name = "compile";
            this.compile.Text = "Build Pet";
            this.compile.UseColumnTextForButtonValue = true;
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_refresh.Location = new System.Drawing.Point(12, 384);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(103, 23);
            this.btn_refresh.TabIndex = 5;
            this.btn_refresh.Text = "Refresh List";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // projectDataSourceBindingSource
            // 
            this.projectDataSourceBindingSource.DataSource = typeof(Your_Desktop_Pet.Forms.SDK.ProjectDataSource);
            // 
            // SDKWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 419);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.projectView);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_projects);
            this.MinimumSize = new System.Drawing.Size(488, 274);
            this.Name = "SDKWindow";
            this.Text = "SDKWindow";
            this.Load += new System.EventHandler(this.SDKWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectDataSourceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_projects;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.BindingSource projectDataSourceBindingSource;
        private System.Windows.Forms.DataGridView projectView;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn metadata;
        private System.Windows.Forms.DataGridViewButtonColumn test;
        private System.Windows.Forms.DataGridViewButtonColumn compile;
    }
}