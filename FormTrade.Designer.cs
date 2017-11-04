namespace BitWhiskey
{
    partial class FormTrade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrade));
            this.panelBalance = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.labelBalanceMarketValue = new System.Windows.Forms.Label();
            this.labelBalanceMarket = new System.Windows.Forms.Label();
            this.labelBalanceBaseValue = new System.Windows.Forms.Label();
            this.labelBalanceBase = new System.Windows.Forms.Label();
            this.buttonUpdateOrderBook = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxLimitOrder = new System.Windows.Forms.CheckBox();
            this.buttonSetAskPrice = new System.Windows.Forms.Button();
            this.buttonSetBidPrice = new System.Windows.Forms.Button();
            this.labelAverage = new System.Windows.Forms.Label();
            this.buttonSell = new System.Windows.Forms.Button();
            this.labelSpread = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textPrice = new System.Windows.Forms.TextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textAmount = new System.Windows.Forms.TextBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.buttonBuy = new System.Windows.Forms.Button();
            this.buttonUpdateTradeHistory = new System.Windows.Forms.Button();
            this.dgridTradeHistory = new System.Windows.Forms.DataGridView();
            this.dgridSellOrders = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelActiveOrders = new System.Windows.Forms.Panel();
            this.buttonUpdateMyOpenOrders = new System.Windows.Forms.Button();
            this.dgridOpenOrders = new System.Windows.Forms.DataGridView();
            this.buttonCancellAllOrders = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dgridMyOrdersHistory = new System.Windows.Forms.DataGridView();
            this.dgridBuyOrders = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timerTradeLast = new System.Windows.Forms.Timer(this.components);
            this.timerTradeHistory = new System.Windows.Forms.Timer(this.components);
            this.buttonMyOrdersHistory = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPageTradeHistory = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabPageMyOrders = new System.Windows.Forms.TabPage();
            this.panelMyOrdersHistory = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.buttonCollapsePanelOrderBook = new System.Windows.Forms.Button();
            this.panelOrderBook = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCollapsePanelTabMain = new System.Windows.Forms.Button();
            this.panelTabMain = new System.Windows.Forms.Panel();
            this.panelBalance.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridTradeHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridSellOrders)).BeginInit();
            this.panelActiveOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridOpenOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridMyOrdersHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBuyOrders)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPageTradeHistory.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPageMyOrders.SuspendLayout();
            this.panelMyOrdersHistory.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panelOrderBook.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelTabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBalance
            // 
            this.panelBalance.Controls.Add(this.label7);
            this.panelBalance.Controls.Add(this.labelBalanceMarketValue);
            this.panelBalance.Controls.Add(this.labelBalanceMarket);
            this.panelBalance.Controls.Add(this.labelBalanceBaseValue);
            this.panelBalance.Controls.Add(this.labelBalanceBase);
            this.panelBalance.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBalance.Location = new System.Drawing.Point(0, 0);
            this.panelBalance.Margin = new System.Windows.Forms.Padding(2);
            this.panelBalance.Name = "panelBalance";
            this.panelBalance.Size = new System.Drawing.Size(473, 24);
            this.panelBalance.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Balances:";
            // 
            // labelBalanceMarketValue
            // 
            this.labelBalanceMarketValue.AutoSize = true;
            this.labelBalanceMarketValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBalanceMarketValue.Location = new System.Drawing.Point(303, 2);
            this.labelBalanceMarketValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBalanceMarketValue.Name = "labelBalanceMarketValue";
            this.labelBalanceMarketValue.Size = new System.Drawing.Size(16, 18);
            this.labelBalanceMarketValue.TabIndex = 5;
            this.labelBalanceMarketValue.Text = "0";
            // 
            // labelBalanceMarket
            // 
            this.labelBalanceMarket.AutoSize = true;
            this.labelBalanceMarket.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBalanceMarket.ForeColor = System.Drawing.Color.MediumOrchid;
            this.labelBalanceMarket.Location = new System.Drawing.Point(246, 2);
            this.labelBalanceMarket.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBalanceMarket.Name = "labelBalanceMarket";
            this.labelBalanceMarket.Size = new System.Drawing.Size(30, 18);
            this.labelBalanceMarket.TabIndex = 4;
            this.labelBalanceMarket.Text = "Btc";
            // 
            // labelBalanceBaseValue
            // 
            this.labelBalanceBaseValue.AutoSize = true;
            this.labelBalanceBaseValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBalanceBaseValue.Location = new System.Drawing.Point(113, 2);
            this.labelBalanceBaseValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBalanceBaseValue.Name = "labelBalanceBaseValue";
            this.labelBalanceBaseValue.Size = new System.Drawing.Size(16, 18);
            this.labelBalanceBaseValue.TabIndex = 1;
            this.labelBalanceBaseValue.Text = "0";
            // 
            // labelBalanceBase
            // 
            this.labelBalanceBase.AutoSize = true;
            this.labelBalanceBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBalanceBase.ForeColor = System.Drawing.Color.BlueViolet;
            this.labelBalanceBase.Location = new System.Drawing.Point(64, 2);
            this.labelBalanceBase.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelBalanceBase.Name = "labelBalanceBase";
            this.labelBalanceBase.Size = new System.Drawing.Size(30, 18);
            this.labelBalanceBase.TabIndex = 0;
            this.labelBalanceBase.Text = "Btc";
            // 
            // buttonUpdateOrderBook
            // 
            this.buttonUpdateOrderBook.Location = new System.Drawing.Point(77, 3);
            this.buttonUpdateOrderBook.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpdateOrderBook.Name = "buttonUpdateOrderBook";
            this.buttonUpdateOrderBook.Size = new System.Drawing.Size(50, 19);
            this.buttonUpdateOrderBook.TabIndex = 13;
            this.buttonUpdateOrderBook.Text = "Update";
            this.buttonUpdateOrderBook.UseVisualStyleBackColor = true;
            this.buttonUpdateOrderBook.Click += new System.EventHandler(this.buttonUpdateOrderBook_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxLimitOrder);
            this.panel1.Controls.Add(this.buttonSetAskPrice);
            this.panel1.Controls.Add(this.buttonSetBidPrice);
            this.panel1.Controls.Add(this.labelAverage);
            this.panel1.Controls.Add(this.buttonSell);
            this.panel1.Controls.Add(this.labelSpread);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textPrice);
            this.panel1.Controls.Add(this.labelPrice);
            this.panel1.Controls.Add(this.textAmount);
            this.panel1.Controls.Add(this.labelAmount);
            this.panel1.Controls.Add(this.buttonBuy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 96);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxLimitOrder
            // 
            this.checkBoxLimitOrder.AutoSize = true;
            this.checkBoxLimitOrder.Checked = true;
            this.checkBoxLimitOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLimitOrder.Location = new System.Drawing.Point(305, 6);
            this.checkBoxLimitOrder.Name = "checkBoxLimitOrder";
            this.checkBoxLimitOrder.Size = new System.Drawing.Size(76, 17);
            this.checkBoxLimitOrder.TabIndex = 14;
            this.checkBoxLimitOrder.Text = "Limit Order";
            this.checkBoxLimitOrder.UseVisualStyleBackColor = true;
            this.checkBoxLimitOrder.CheckedChanged += new System.EventHandler(this.checkBoxLimitOrder_CheckedChanged);
            // 
            // buttonSetAskPrice
            // 
            this.buttonSetAskPrice.Location = new System.Drawing.Point(221, 3);
            this.buttonSetAskPrice.Name = "buttonSetAskPrice";
            this.buttonSetAskPrice.Size = new System.Drawing.Size(39, 23);
            this.buttonSetAskPrice.TabIndex = 13;
            this.buttonSetAskPrice.Text = "ASK";
            this.buttonSetAskPrice.UseVisualStyleBackColor = true;
            this.buttonSetAskPrice.Click += new System.EventHandler(this.buttonSetAskPrice_Click);
            // 
            // buttonSetBidPrice
            // 
            this.buttonSetBidPrice.Location = new System.Drawing.Point(264, 3);
            this.buttonSetBidPrice.Name = "buttonSetBidPrice";
            this.buttonSetBidPrice.Size = new System.Drawing.Size(36, 23);
            this.buttonSetBidPrice.TabIndex = 12;
            this.buttonSetBidPrice.Text = "BID";
            this.buttonSetBidPrice.UseVisualStyleBackColor = true;
            this.buttonSetBidPrice.Click += new System.EventHandler(this.buttonSetBidPrice_Click);
            // 
            // labelAverage
            // 
            this.labelAverage.AutoSize = true;
            this.labelAverage.Location = new System.Drawing.Point(1, 80);
            this.labelAverage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAverage.Name = "labelAverage";
            this.labelAverage.Size = new System.Drawing.Size(47, 13);
            this.labelAverage.TabIndex = 11;
            this.labelAverage.Text = "Average";
            // 
            // buttonSell
            // 
            this.buttonSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSell.ForeColor = System.Drawing.Color.Blue;
            this.buttonSell.Location = new System.Drawing.Point(251, 53);
            this.buttonSell.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.Size = new System.Drawing.Size(204, 25);
            this.buttonSell.TabIndex = 10;
            this.buttonSell.Text = "SELL";
            this.buttonSell.UseVisualStyleBackColor = true;
            this.buttonSell.Click += new System.EventHandler(this.buttonSell_Click);
            // 
            // labelSpread
            // 
            this.labelSpread.AutoSize = true;
            this.labelSpread.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSpread.Location = new System.Drawing.Point(204, 64);
            this.labelSpread.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSpread.Name = "labelSpread";
            this.labelSpread.Size = new System.Drawing.Size(14, 15);
            this.labelSpread.TabIndex = 9;
            this.labelSpread.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 50);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "spread";
            // 
            // textPrice
            // 
            this.textPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textPrice.Location = new System.Drawing.Point(110, 2);
            this.textPrice.Margin = new System.Windows.Forms.Padding(2);
            this.textPrice.Name = "textPrice";
            this.textPrice.Size = new System.Drawing.Size(108, 23);
            this.textPrice.TabIndex = 4;
            this.textPrice.Text = "0";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrice.Location = new System.Drawing.Point(4, 4);
            this.labelPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(40, 17);
            this.labelPrice.TabIndex = 3;
            this.labelPrice.Text = "Price";
            // 
            // textAmount
            // 
            this.textAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textAmount.Location = new System.Drawing.Point(110, 27);
            this.textAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textAmount.Name = "textAmount";
            this.textAmount.Size = new System.Drawing.Size(107, 23);
            this.textAmount.TabIndex = 2;
            this.textAmount.Text = "0";
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAmount.Location = new System.Drawing.Point(5, 29);
            this.labelAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(56, 17);
            this.labelAmount.TabIndex = 1;
            this.labelAmount.Text = "Amount";
            // 
            // buttonBuy
            // 
            this.buttonBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBuy.ForeColor = System.Drawing.Color.Blue;
            this.buttonBuy.Location = new System.Drawing.Point(2, 53);
            this.buttonBuy.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(199, 25);
            this.buttonBuy.TabIndex = 0;
            this.buttonBuy.Text = "BUY";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuy_Click);
            // 
            // buttonUpdateTradeHistory
            // 
            this.buttonUpdateTradeHistory.Location = new System.Drawing.Point(4, 4);
            this.buttonUpdateTradeHistory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUpdateTradeHistory.Name = "buttonUpdateTradeHistory";
            this.buttonUpdateTradeHistory.Size = new System.Drawing.Size(51, 20);
            this.buttonUpdateTradeHistory.TabIndex = 15;
            this.buttonUpdateTradeHistory.Text = "Update";
            this.buttonUpdateTradeHistory.UseVisualStyleBackColor = true;
            this.buttonUpdateTradeHistory.Click += new System.EventHandler(this.buttonUpdateTradeHistory_Click);
            // 
            // dgridTradeHistory
            // 
            this.dgridTradeHistory.AllowUserToAddRows = false;
            this.dgridTradeHistory.AllowUserToDeleteRows = false;
            this.dgridTradeHistory.AllowUserToOrderColumns = true;
            this.dgridTradeHistory.AllowUserToResizeRows = false;
            this.dgridTradeHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgridTradeHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgridTradeHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridTradeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgridTradeHistory.Location = new System.Drawing.Point(0, 0);
            this.dgridTradeHistory.Margin = new System.Windows.Forms.Padding(2);
            this.dgridTradeHistory.MultiSelect = false;
            this.dgridTradeHistory.Name = "dgridTradeHistory";
            this.dgridTradeHistory.ReadOnly = true;
            this.dgridTradeHistory.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgridTradeHistory.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgridTradeHistory.RowTemplate.Height = 24;
            this.dgridTradeHistory.RowTemplate.ReadOnly = true;
            this.dgridTradeHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridTradeHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridTradeHistory.ShowCellToolTips = false;
            this.dgridTradeHistory.ShowEditingIcon = false;
            this.dgridTradeHistory.Size = new System.Drawing.Size(459, 156);
            this.dgridTradeHistory.TabIndex = 8;
            // 
            // dgridSellOrders
            // 
            this.dgridSellOrders.AllowUserToAddRows = false;
            this.dgridSellOrders.AllowUserToDeleteRows = false;
            this.dgridSellOrders.AllowUserToResizeRows = false;
            this.dgridSellOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridSellOrders.Location = new System.Drawing.Point(4, 24);
            this.dgridSellOrders.Margin = new System.Windows.Forms.Padding(2);
            this.dgridSellOrders.MultiSelect = false;
            this.dgridSellOrders.Name = "dgridSellOrders";
            this.dgridSellOrders.ReadOnly = true;
            this.dgridSellOrders.RowHeadersVisible = false;
            this.dgridSellOrders.RowTemplate.Height = 24;
            this.dgridSellOrders.RowTemplate.ReadOnly = true;
            this.dgridSellOrders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridSellOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridSellOrders.ShowCellToolTips = false;
            this.dgridSellOrders.ShowEditingIcon = false;
            this.dgridSellOrders.Size = new System.Drawing.Size(231, 133);
            this.dgridSellOrders.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.IndianRed;
            this.label4.Location = new System.Drawing.Point(198, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "ASK";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.ForeColor = System.Drawing.Color.Brown;
            this.label2.Location = new System.Drawing.Point(11, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sell Orders";
            // 
            // panelActiveOrders
            // 
            this.panelActiveOrders.Controls.Add(this.buttonUpdateMyOpenOrders);
            this.panelActiveOrders.Controls.Add(this.dgridOpenOrders);
            this.panelActiveOrders.Controls.Add(this.buttonCancellAllOrders);
            this.panelActiveOrders.Controls.Add(this.label9);
            this.panelActiveOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActiveOrders.Location = new System.Drawing.Point(3, 3);
            this.panelActiveOrders.Margin = new System.Windows.Forms.Padding(2);
            this.panelActiveOrders.Name = "panelActiveOrders";
            this.panelActiveOrders.Size = new System.Drawing.Size(459, 88);
            this.panelActiveOrders.TabIndex = 10;
            // 
            // buttonUpdateMyOpenOrders
            // 
            this.buttonUpdateMyOpenOrders.Location = new System.Drawing.Point(81, 3);
            this.buttonUpdateMyOpenOrders.Name = "buttonUpdateMyOpenOrders";
            this.buttonUpdateMyOpenOrders.Size = new System.Drawing.Size(61, 19);
            this.buttonUpdateMyOpenOrders.TabIndex = 9;
            this.buttonUpdateMyOpenOrders.Text = "Update";
            this.buttonUpdateMyOpenOrders.UseVisualStyleBackColor = true;
            this.buttonUpdateMyOpenOrders.Click += new System.EventHandler(this.buttonUpdateMyOpenOrders_Click);
            // 
            // dgridOpenOrders
            // 
            this.dgridOpenOrders.AllowUserToAddRows = false;
            this.dgridOpenOrders.AllowUserToDeleteRows = false;
            this.dgridOpenOrders.AllowUserToResizeColumns = false;
            this.dgridOpenOrders.AllowUserToResizeRows = false;
            this.dgridOpenOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridOpenOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridOpenOrders.Location = new System.Drawing.Point(2, 25);
            this.dgridOpenOrders.Margin = new System.Windows.Forms.Padding(2);
            this.dgridOpenOrders.MultiSelect = false;
            this.dgridOpenOrders.Name = "dgridOpenOrders";
            this.dgridOpenOrders.ReadOnly = true;
            this.dgridOpenOrders.RowHeadersVisible = false;
            this.dgridOpenOrders.RowTemplate.Height = 24;
            this.dgridOpenOrders.RowTemplate.ReadOnly = true;
            this.dgridOpenOrders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridOpenOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridOpenOrders.ShowCellToolTips = false;
            this.dgridOpenOrders.ShowEditingIcon = false;
            this.dgridOpenOrders.Size = new System.Drawing.Size(450, 59);
            this.dgridOpenOrders.TabIndex = 7;
            this.dgridOpenOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridOpenOrders_CellContentClick);
            // 
            // buttonCancellAllOrders
            // 
            this.buttonCancellAllOrders.Location = new System.Drawing.Point(157, 3);
            this.buttonCancellAllOrders.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancellAllOrders.Name = "buttonCancellAllOrders";
            this.buttonCancellAllOrders.Size = new System.Drawing.Size(80, 19);
            this.buttonCancellAllOrders.TabIndex = 8;
            this.buttonCancellAllOrders.Text = "Cancel All";
            this.buttonCancellAllOrders.UseVisualStyleBackColor = true;
            this.buttonCancellAllOrders.Click += new System.EventHandler(this.buttonCancellAllOrders_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 7);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Active Orders";
            // 
            // dgridMyOrdersHistory
            // 
            this.dgridMyOrdersHistory.AllowUserToAddRows = false;
            this.dgridMyOrdersHistory.AllowUserToDeleteRows = false;
            this.dgridMyOrdersHistory.AllowUserToResizeColumns = false;
            this.dgridMyOrdersHistory.AllowUserToResizeRows = false;
            this.dgridMyOrdersHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridMyOrdersHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridMyOrdersHistory.Location = new System.Drawing.Point(3, 27);
            this.dgridMyOrdersHistory.Margin = new System.Windows.Forms.Padding(2);
            this.dgridMyOrdersHistory.MultiSelect = false;
            this.dgridMyOrdersHistory.Name = "dgridMyOrdersHistory";
            this.dgridMyOrdersHistory.ReadOnly = true;
            this.dgridMyOrdersHistory.RowHeadersVisible = false;
            this.dgridMyOrdersHistory.RowTemplate.Height = 24;
            this.dgridMyOrdersHistory.RowTemplate.ReadOnly = true;
            this.dgridMyOrdersHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridMyOrdersHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridMyOrdersHistory.ShowCellToolTips = false;
            this.dgridMyOrdersHistory.ShowEditingIcon = false;
            this.dgridMyOrdersHistory.Size = new System.Drawing.Size(449, 63);
            this.dgridMyOrdersHistory.TabIndex = 9;
            // 
            // dgridBuyOrders
            // 
            this.dgridBuyOrders.AllowUserToAddRows = false;
            this.dgridBuyOrders.AllowUserToDeleteRows = false;
            this.dgridBuyOrders.AllowUserToResizeRows = false;
            this.dgridBuyOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridBuyOrders.Location = new System.Drawing.Point(239, 25);
            this.dgridBuyOrders.Margin = new System.Windows.Forms.Padding(2);
            this.dgridBuyOrders.MultiSelect = false;
            this.dgridBuyOrders.Name = "dgridBuyOrders";
            this.dgridBuyOrders.ReadOnly = true;
            this.dgridBuyOrders.RowHeadersVisible = false;
            this.dgridBuyOrders.RowTemplate.Height = 24;
            this.dgridBuyOrders.RowTemplate.ReadOnly = true;
            this.dgridBuyOrders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgridBuyOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgridBuyOrders.ShowCellToolTips = false;
            this.dgridBuyOrders.ShowEditingIcon = false;
            this.dgridBuyOrders.Size = new System.Drawing.Size(230, 132);
            this.dgridBuyOrders.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(385, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Buy Orders";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(243, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "BID";
            // 
            // timerTradeLast
            // 
            this.timerTradeLast.Interval = 10000;
            this.timerTradeLast.Tick += new System.EventHandler(this.timerTradeLast_Tick);
            // 
            // timerTradeHistory
            // 
            this.timerTradeHistory.Interval = 57000;
            this.timerTradeHistory.Tick += new System.EventHandler(this.timerTradeHistory_Tick);
            // 
            // buttonMyOrdersHistory
            // 
            this.buttonMyOrdersHistory.Location = new System.Drawing.Point(100, 4);
            this.buttonMyOrdersHistory.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMyOrdersHistory.Name = "buttonMyOrdersHistory";
            this.buttonMyOrdersHistory.Size = new System.Drawing.Size(54, 21);
            this.buttonMyOrdersHistory.TabIndex = 14;
            this.buttonMyOrdersHistory.Text = "Update";
            this.buttonMyOrdersHistory.UseVisualStyleBackColor = true;
            this.buttonMyOrdersHistory.Click += new System.EventHandler(this.buttonMyOrdersHistory_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 7);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "My Orders History";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPageTradeHistory);
            this.tabMain.Controls.Add(this.tabPageMyOrders);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(473, 214);
            this.tabMain.TabIndex = 13;
            // 
            // tabPageTradeHistory
            // 
            this.tabPageTradeHistory.Controls.Add(this.panel4);
            this.tabPageTradeHistory.Controls.Add(this.panel3);
            this.tabPageTradeHistory.Location = new System.Drawing.Point(4, 22);
            this.tabPageTradeHistory.Name = "tabPageTradeHistory";
            this.tabPageTradeHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTradeHistory.Size = new System.Drawing.Size(465, 188);
            this.tabPageTradeHistory.TabIndex = 0;
            this.tabPageTradeHistory.Text = "Market";
            this.tabPageTradeHistory.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgridTradeHistory);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(459, 156);
            this.panel4.TabIndex = 17;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonUpdateTradeHistory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(459, 26);
            this.panel3.TabIndex = 16;
            // 
            // tabPageMyOrders
            // 
            this.tabPageMyOrders.Controls.Add(this.panelMyOrdersHistory);
            this.tabPageMyOrders.Controls.Add(this.panelActiveOrders);
            this.tabPageMyOrders.Location = new System.Drawing.Point(4, 22);
            this.tabPageMyOrders.Name = "tabPageMyOrders";
            this.tabPageMyOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMyOrders.Size = new System.Drawing.Size(465, 188);
            this.tabPageMyOrders.TabIndex = 1;
            this.tabPageMyOrders.Text = "My Orders";
            this.tabPageMyOrders.UseVisualStyleBackColor = true;
            // 
            // panelMyOrdersHistory
            // 
            this.panelMyOrdersHistory.Controls.Add(this.label8);
            this.panelMyOrdersHistory.Controls.Add(this.buttonMyOrdersHistory);
            this.panelMyOrdersHistory.Controls.Add(this.dgridMyOrdersHistory);
            this.panelMyOrdersHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMyOrdersHistory.Location = new System.Drawing.Point(3, 91);
            this.panelMyOrdersHistory.Name = "panelMyOrdersHistory";
            this.panelMyOrdersHistory.Size = new System.Drawing.Size(459, 94);
            this.panelMyOrdersHistory.TabIndex = 12;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.buttonCollapsePanelOrderBook);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 120);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(473, 22);
            this.panel6.TabIndex = 15;
            // 
            // buttonCollapsePanelOrderBook
            // 
            this.buttonCollapsePanelOrderBook.BackColor = System.Drawing.Color.LightBlue;
            this.buttonCollapsePanelOrderBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCollapsePanelOrderBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCollapsePanelOrderBook.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCollapsePanelOrderBook.Image = ((System.Drawing.Image)(resources.GetObject("buttonCollapsePanelOrderBook.Image")));
            this.buttonCollapsePanelOrderBook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCollapsePanelOrderBook.Location = new System.Drawing.Point(0, 0);
            this.buttonCollapsePanelOrderBook.Name = "buttonCollapsePanelOrderBook";
            this.buttonCollapsePanelOrderBook.Size = new System.Drawing.Size(473, 22);
            this.buttonCollapsePanelOrderBook.TabIndex = 0;
            this.buttonCollapsePanelOrderBook.Text = "Order Book";
            this.buttonCollapsePanelOrderBook.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCollapsePanelOrderBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCollapsePanelOrderBook.UseVisualStyleBackColor = false;
            this.buttonCollapsePanelOrderBook.Click += new System.EventHandler(this.buttonCollapsePanelOrderBook_Click);
            // 
            // panelOrderBook
            // 
            this.panelOrderBook.Controls.Add(this.dgridBuyOrders);
            this.panelOrderBook.Controls.Add(this.label4);
            this.panelOrderBook.Controls.Add(this.dgridSellOrders);
            this.panelOrderBook.Controls.Add(this.buttonUpdateOrderBook);
            this.panelOrderBook.Controls.Add(this.label2);
            this.panelOrderBook.Controls.Add(this.label6);
            this.panelOrderBook.Controls.Add(this.label1);
            this.panelOrderBook.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrderBook.Location = new System.Drawing.Point(0, 142);
            this.panelOrderBook.Name = "panelOrderBook";
            this.panelOrderBook.Size = new System.Drawing.Size(473, 160);
            this.panelOrderBook.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonCollapsePanelTabMain);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 302);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 22);
            this.panel2.TabIndex = 19;
            // 
            // buttonCollapsePanelTabMain
            // 
            this.buttonCollapsePanelTabMain.BackColor = System.Drawing.Color.LightBlue;
            this.buttonCollapsePanelTabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCollapsePanelTabMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCollapsePanelTabMain.Image = ((System.Drawing.Image)(resources.GetObject("buttonCollapsePanelTabMain.Image")));
            this.buttonCollapsePanelTabMain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCollapsePanelTabMain.Location = new System.Drawing.Point(0, 0);
            this.buttonCollapsePanelTabMain.Name = "buttonCollapsePanelTabMain";
            this.buttonCollapsePanelTabMain.Size = new System.Drawing.Size(473, 22);
            this.buttonCollapsePanelTabMain.TabIndex = 1;
            this.buttonCollapsePanelTabMain.Text = "Trade Windows";
            this.buttonCollapsePanelTabMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCollapsePanelTabMain.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCollapsePanelTabMain.UseVisualStyleBackColor = false;
            this.buttonCollapsePanelTabMain.Click += new System.EventHandler(this.buttonCollapsePanelTabMain_Click);
            // 
            // panelTabMain
            // 
            this.panelTabMain.Controls.Add(this.tabMain);
            this.panelTabMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabMain.Location = new System.Drawing.Point(0, 324);
            this.panelTabMain.Name = "panelTabMain";
            this.panelTabMain.Size = new System.Drawing.Size(473, 218);
            this.panelTabMain.TabIndex = 20;
            // 
            // FormTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(473, 548);
            this.Controls.Add(this.panelTabMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelOrderBook);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBalance);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormTrade";
            this.Text = "Trade";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTrade_FormClosed);
            this.Load += new System.EventHandler(this.FormTrade_Load);
            this.panelBalance.ResumeLayout(false);
            this.panelBalance.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridTradeHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridSellOrders)).EndInit();
            this.panelActiveOrders.ResumeLayout(false);
            this.panelActiveOrders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridOpenOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridMyOrdersHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgridBuyOrders)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabPageTradeHistory.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPageMyOrders.ResumeLayout(false);
            this.panelMyOrdersHistory.ResumeLayout(false);
            this.panelMyOrdersHistory.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panelOrderBook.ResumeLayout(false);
            this.panelOrderBook.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panelTabMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBalance;
        private System.Windows.Forms.Label labelBalanceBase;
        private System.Windows.Forms.Label labelBalanceBaseValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.TextBox textAmount;
        private System.Windows.Forms.TextBox textPrice;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgridSellOrders;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSpread;
        private System.Windows.Forms.Panel panelActiveOrders;
        private System.Windows.Forms.DataGridView dgridOpenOrders;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonCancellAllOrders;
        private System.Windows.Forms.Label labelBalanceMarket;
        private System.Windows.Forms.Label labelBalanceMarketValue;
        private System.Windows.Forms.DataGridView dgridTradeHistory;
        private System.Windows.Forms.Button buttonSell;
        private System.Windows.Forms.DataGridView dgridBuyOrders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonUpdateOrderBook;
        private System.Windows.Forms.Timer timerTradeLast;
        private System.Windows.Forms.Timer timerTradeHistory;
        private System.Windows.Forms.DataGridView dgridMyOrdersHistory;
        private System.Windows.Forms.Button buttonMyOrdersHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelAverage;
        private System.Windows.Forms.Button buttonUpdateTradeHistory;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPageTradeHistory;
        private System.Windows.Forms.TabPage tabPageMyOrders;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button buttonCollapsePanelOrderBook;
        private System.Windows.Forms.Panel panelOrderBook;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelTabMain;
        private System.Windows.Forms.Panel panelMyOrdersHistory;
        private System.Windows.Forms.Button buttonCollapsePanelTabMain;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonSetBidPrice;
        private System.Windows.Forms.Button buttonSetAskPrice;
        private System.Windows.Forms.CheckBox checkBoxLimitOrder;
        private System.Windows.Forms.Button buttonUpdateMyOpenOrders;
    }
}