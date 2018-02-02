namespace BitWhiskey
{
    partial class FormTicker
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelUp = new System.Windows.Forms.Panel();
            this.textBoxAutoFind = new System.Windows.Forms.TextBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.dGridTicker = new System.Windows.Forms.DataGridView();
            this.panelUp.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridTicker)).BeginInit();
            this.SuspendLayout();
            // 
            // panelUp
            // 
            this.panelUp.Controls.Add(this.textBoxAutoFind);
            this.panelUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUp.Location = new System.Drawing.Point(0, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(683, 33);
            this.panelUp.TabIndex = 0;
            // 
            // textBoxAutoFind
            // 
            this.textBoxAutoFind.Location = new System.Drawing.Point(6, 5);
            this.textBoxAutoFind.Name = "textBoxAutoFind";
            this.textBoxAutoFind.Size = new System.Drawing.Size(136, 20);
            this.textBoxAutoFind.TabIndex = 0;
            this.textBoxAutoFind.TextChanged += new System.EventHandler(this.textBoxAutoFind_TextChanged);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.dGridTicker);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 33);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(683, 464);
            this.panelMain.TabIndex = 1;
            // 
            // dGridTicker
            // 
            this.dGridTicker.AllowUserToAddRows = false;
            this.dGridTicker.AllowUserToDeleteRows = false;
            this.dGridTicker.AllowUserToResizeColumns = false;
            this.dGridTicker.AllowUserToResizeRows = false;
            this.dGridTicker.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridTicker.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGridTicker.DefaultCellStyle = dataGridViewCellStyle1;
            this.dGridTicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGridTicker.Location = new System.Drawing.Point(0, 0);
            this.dGridTicker.MultiSelect = false;
            this.dGridTicker.Name = "dGridTicker";
            this.dGridTicker.ReadOnly = true;
            this.dGridTicker.RowHeadersVisible = false;
            this.dGridTicker.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dGridTicker.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dGridTicker.Size = new System.Drawing.Size(683, 464);
            this.dGridTicker.TabIndex = 0;
            this.dGridTicker.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridTicker_CellContentClick);
            // 
            // FormTicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 497);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelUp);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormTicker";
            this.ShowInTaskbar = false;
            this.Text = "Choose Trade Pair";
            this.panelUp.ResumeLayout(false);
            this.panelUp.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGridTicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.TextBox textBoxAutoFind;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.DataGridView dGridTicker;
    }
}