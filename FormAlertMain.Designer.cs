namespace BitWhiskey
{
    partial class FormAlertMain
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonAddAlert = new System.Windows.Forms.Button();
            this.panelTable = new System.Windows.Forms.Panel();
            this.dGridAlerts = new System.Windows.Forms.DataGridView();
            this.buttonDeleteAlerts = new System.Windows.Forms.Button();
            this.buttonStopAlerts = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridAlerts)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonStopAlerts);
            this.panelMain.Controls.Add(this.buttonDeleteAlerts);
            this.panelMain.Controls.Add(this.buttonAddAlert);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(628, 36);
            this.panelMain.TabIndex = 0;
            // 
            // buttonAddAlert
            // 
            this.buttonAddAlert.Location = new System.Drawing.Point(10, 8);
            this.buttonAddAlert.Name = "buttonAddAlert";
            this.buttonAddAlert.Size = new System.Drawing.Size(80, 23);
            this.buttonAddAlert.TabIndex = 0;
            this.buttonAddAlert.Text = "Create Alert";
            this.buttonAddAlert.UseVisualStyleBackColor = true;
            this.buttonAddAlert.Click += new System.EventHandler(this.buttonAddAlert_Click);
            // 
            // panelTable
            // 
            this.panelTable.Controls.Add(this.dGridAlerts);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(0, 36);
            this.panelTable.Name = "panelTable";
            this.panelTable.Size = new System.Drawing.Size(628, 153);
            this.panelTable.TabIndex = 1;
            // 
            // dGridAlerts
            // 
            this.dGridAlerts.AllowUserToAddRows = false;
            this.dGridAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGridAlerts.Location = new System.Drawing.Point(0, 0);
            this.dGridAlerts.MultiSelect = false;
            this.dGridAlerts.Name = "dGridAlerts";
            this.dGridAlerts.ReadOnly = true;
            this.dGridAlerts.RowHeadersVisible = false;
            this.dGridAlerts.Size = new System.Drawing.Size(628, 153);
            this.dGridAlerts.TabIndex = 0;
            // 
            // buttonDeleteAlerts
            // 
            this.buttonDeleteAlerts.Location = new System.Drawing.Point(115, 8);
            this.buttonDeleteAlerts.Name = "buttonDeleteAlerts";
            this.buttonDeleteAlerts.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteAlerts.TabIndex = 1;
            this.buttonDeleteAlerts.Text = "Delete Alerts";
            this.buttonDeleteAlerts.UseVisualStyleBackColor = true;
            this.buttonDeleteAlerts.Click += new System.EventHandler(this.buttonDeleteAlerts_Click);
            // 
            // buttonStopAlerts
            // 
            this.buttonStopAlerts.Location = new System.Drawing.Point(252, 7);
            this.buttonStopAlerts.Name = "buttonStopAlerts";
            this.buttonStopAlerts.Size = new System.Drawing.Size(80, 23);
            this.buttonStopAlerts.TabIndex = 2;
            this.buttonStopAlerts.Text = "Stop Sound";
            this.buttonStopAlerts.UseVisualStyleBackColor = true;
            this.buttonStopAlerts.Click += new System.EventHandler(this.buttonStopAlerts_Click);
            // 
            // FormAlertMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 189);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.panelMain);
            this.Name = "FormAlertMain";
            this.Text = "Alerts";
            this.panelMain.ResumeLayout(false);
            this.panelTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGridAlerts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Button buttonAddAlert;
        private System.Windows.Forms.DataGridView dGridAlerts;
        private System.Windows.Forms.Button buttonDeleteAlerts;
        private System.Windows.Forms.Button buttonStopAlerts;
    }
}