namespace docker_backup
{
    partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.dockerEndpointEntry = new System.Windows.Forms.TextBox();
            this.dockerEndpointLabel = new System.Windows.Forms.Label();
            this.basicAuthCheckbox = new System.Windows.Forms.CheckBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameEntry = new System.Windows.Forms.TextBox();
            this.passwordEntry = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.testConnectivityButton = new System.Windows.Forms.Button();
            this.listViewContainers = new System.Windows.Forms.ListView();
            this.ContainerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // dockerEndpointEntry
            // 
            this.dockerEndpointEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockerEndpointEntry.Location = new System.Drawing.Point(12, 30);
            this.dockerEndpointEntry.Name = "dockerEndpointEntry";
            this.dockerEndpointEntry.Size = new System.Drawing.Size(401, 27);
            this.dockerEndpointEntry.TabIndex = 0;
            // 
            // dockerEndpointLabel
            // 
            this.dockerEndpointLabel.AutoSize = true;
            this.dockerEndpointLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockerEndpointLabel.Location = new System.Drawing.Point(12, 9);
            this.dockerEndpointLabel.Name = "dockerEndpointLabel";
            this.dockerEndpointLabel.Size = new System.Drawing.Size(121, 18);
            this.dockerEndpointLabel.TabIndex = 1;
            this.dockerEndpointLabel.Text = "Docker endpoint:";
            // 
            // basicAuthCheckbox
            // 
            this.basicAuthCheckbox.AutoSize = true;
            this.basicAuthCheckbox.Location = new System.Drawing.Point(12, 74);
            this.basicAuthCheckbox.Name = "basicAuthCheckbox";
            this.basicAuthCheckbox.Size = new System.Drawing.Size(204, 21);
            this.basicAuthCheckbox.TabIndex = 2;
            this.basicAuthCheckbox.Text = "Enable basic authentication";
            this.basicAuthCheckbox.UseVisualStyleBackColor = true;
            this.basicAuthCheckbox.CheckedChanged += new System.EventHandler(this.basicAuthCheckbox_CheckedChanged);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.Location = new System.Drawing.Point(12, 108);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(81, 18);
            this.usernameLabel.TabIndex = 3;
            this.usernameLabel.Text = "Username:";
            this.usernameLabel.Visible = false;
            // 
            // usernameEntry
            // 
            this.usernameEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameEntry.Location = new System.Drawing.Point(12, 129);
            this.usernameEntry.Name = "usernameEntry";
            this.usernameEntry.Size = new System.Drawing.Size(401, 27);
            this.usernameEntry.TabIndex = 4;
            this.usernameEntry.Visible = false;
            // 
            // passwordEntry
            // 
            this.passwordEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordEntry.Location = new System.Drawing.Point(12, 185);
            this.passwordEntry.Name = "passwordEntry";
            this.passwordEntry.Size = new System.Drawing.Size(401, 27);
            this.passwordEntry.TabIndex = 6;
            this.passwordEntry.Visible = false;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLabel.Location = new System.Drawing.Point(12, 164);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(79, 18);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Password:";
            this.passwordLabel.Visible = false;
            // 
            // testConnectivityButton
            // 
            this.testConnectivityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testConnectivityButton.Location = new System.Drawing.Point(12, 227);
            this.testConnectivityButton.Name = "testConnectivityButton";
            this.testConnectivityButton.Size = new System.Drawing.Size(401, 33);
            this.testConnectivityButton.TabIndex = 7;
            this.testConnectivityButton.Text = "Get containers";
            this.testConnectivityButton.UseVisualStyleBackColor = true;
            this.testConnectivityButton.Click += new System.EventHandler(this.testConnectivityButton_Click);
            // 
            // listViewContainers
            // 
            this.listViewContainers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ContainerName,
            this.ID});
            this.listViewContainers.Location = new System.Drawing.Point(12, 278);
            this.listViewContainers.Name = "listViewContainers";
            this.listViewContainers.Size = new System.Drawing.Size(401, 219);
            this.listViewContainers.TabIndex = 8;
            this.listViewContainers.UseCompatibleStateImageBehavior = false;
            this.listViewContainers.View = System.Windows.Forms.View.Details;
            this.listViewContainers.DoubleClick += new System.EventHandler(this.listViewContainers_DoubleClick);
            // 
            // ContainerName
            // 
            this.ContainerName.Text = "Container name";
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 509);
            this.Controls.Add(this.listViewContainers);
            this.Controls.Add(this.testConnectivityButton);
            this.Controls.Add(this.passwordEntry);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameEntry);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.basicAuthCheckbox);
            this.Controls.Add(this.dockerEndpointLabel);
            this.Controls.Add(this.dockerEndpointEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartForm";
            this.Text = "Simple Docker Backup";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dockerEndpointEntry;
        private System.Windows.Forms.Label dockerEndpointLabel;
        private System.Windows.Forms.CheckBox basicAuthCheckbox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameEntry;
        private System.Windows.Forms.TextBox passwordEntry;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button testConnectivityButton;
        private System.Windows.Forms.ListView listViewContainers;
        private System.Windows.Forms.ColumnHeader ContainerName;
        private System.Windows.Forms.ColumnHeader ID;
    }
}

