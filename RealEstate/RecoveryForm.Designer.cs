namespace RealEstate
{
    partial class RecoveryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecoveryForm));
            this.OrderButtons = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.RecoverList = new System.Windows.Forms.DataGridView();
            this.cl_CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cl_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl_info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_buttons = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnl_selectrecBut = new System.Windows.Forms.Panel();
            this.btn_estateBill = new System.Windows.Forms.Button();
            this.btn_estate = new System.Windows.Forms.Button();
            this.btn_saleType = new System.Windows.Forms.Button();
            this.btn_estaeType = new System.Windows.Forms.Button();
            this.btn_area = new System.Windows.Forms.Button();
            this.OrderButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecoverList)).BeginInit();
            this.pnl_buttons.SuspendLayout();
            this.pnl_selectrecBut.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrderButtons
            // 
            this.OrderButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(40)))), ((int)(((byte)(59)))));
            this.OrderButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.OrderButtons.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.OrderButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.OrderButtons.Location = new System.Drawing.Point(0, 0);
            this.OrderButtons.Name = "OrderButtons";
            this.OrderButtons.Size = new System.Drawing.Size(1220, 31);
            this.OrderButtons.TabIndex = 1;
            this.OrderButtons.Text = "بار";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // RecoverList
            // 
            this.RecoverList.AllowUserToAddRows = false;
            this.RecoverList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RecoverList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cl_CheckBox,
            this.cl_id,
            this.cl_info});
            this.RecoverList.Location = new System.Drawing.Point(106, 27);
            this.RecoverList.Name = "RecoverList";
            this.RecoverList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RecoverList.RowHeadersWidth = 51;
            this.RecoverList.RowTemplate.Height = 24;
            this.RecoverList.Size = new System.Drawing.Size(1114, 373);
            this.RecoverList.TabIndex = 2;
            // 
            // cl_CheckBox
            // 
            this.cl_CheckBox.HeaderText = "انتخاب";
            this.cl_CheckBox.MinimumWidth = 6;
            this.cl_CheckBox.Name = "cl_CheckBox";
            this.cl_CheckBox.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cl_CheckBox.Width = 50;
            // 
            // cl_id
            // 
            this.cl_id.HeaderText = "شماره";
            this.cl_id.MinimumWidth = 6;
            this.cl_id.Name = "cl_id";
            this.cl_id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cl_id.Width = 70;
            // 
            // cl_info
            // 
            this.cl_info.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cl_info.HeaderText = "اطلاعات";
            this.cl_info.MinimumWidth = 6;
            this.cl_info.Name = "cl_info";
            // 
            // pnl_buttons
            // 
            this.pnl_buttons.Controls.Add(this.label1);
            this.pnl_buttons.Controls.Add(this.button1);
            this.pnl_buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_buttons.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_buttons.Location = new System.Drawing.Point(106, 347);
            this.pnl_buttons.Name = "pnl_buttons";
            this.pnl_buttons.Size = new System.Drawing.Size(1114, 53);
            this.pnl_buttons.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(733, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "برای بازگردانی تیک رکورد مورد نظر را بزنید";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(976, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "بازگردانی اطلاعات";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnl_selectrecBut
            // 
            this.pnl_selectrecBut.Controls.Add(this.btn_estateBill);
            this.pnl_selectrecBut.Controls.Add(this.btn_estate);
            this.pnl_selectrecBut.Controls.Add(this.btn_saleType);
            this.pnl_selectrecBut.Controls.Add(this.btn_estaeType);
            this.pnl_selectrecBut.Controls.Add(this.btn_area);
            this.pnl_selectrecBut.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_selectrecBut.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_selectrecBut.Location = new System.Drawing.Point(0, 31);
            this.pnl_selectrecBut.Name = "pnl_selectrecBut";
            this.pnl_selectrecBut.Size = new System.Drawing.Size(106, 369);
            this.pnl_selectrecBut.TabIndex = 4;
            // 
            // btn_estateBill
            // 
            this.btn_estateBill.Location = new System.Drawing.Point(0, 176);
            this.btn_estateBill.Name = "btn_estateBill";
            this.btn_estateBill.Size = new System.Drawing.Size(104, 44);
            this.btn_estateBill.TabIndex = 8;
            this.btn_estateBill.Text = "قرارداد";
            this.btn_estateBill.UseVisualStyleBackColor = true;
            this.btn_estateBill.Click += new System.EventHandler(this.btn_estateBill_Click);
            // 
            // btn_estate
            // 
            this.btn_estate.Location = new System.Drawing.Point(0, 132);
            this.btn_estate.Name = "btn_estate";
            this.btn_estate.Size = new System.Drawing.Size(104, 44);
            this.btn_estate.TabIndex = 7;
            this.btn_estate.Text = "ملک";
            this.btn_estate.UseVisualStyleBackColor = true;
            this.btn_estate.Click += new System.EventHandler(this.btn_estate_Click);
            // 
            // btn_saleType
            // 
            this.btn_saleType.Location = new System.Drawing.Point(0, 88);
            this.btn_saleType.Name = "btn_saleType";
            this.btn_saleType.Size = new System.Drawing.Size(104, 44);
            this.btn_saleType.TabIndex = 6;
            this.btn_saleType.Text = "نوع پرداخت";
            this.btn_saleType.UseVisualStyleBackColor = true;
            this.btn_saleType.Click += new System.EventHandler(this.btn_saleType_Click);
            // 
            // btn_estaeType
            // 
            this.btn_estaeType.Location = new System.Drawing.Point(0, 44);
            this.btn_estaeType.Name = "btn_estaeType";
            this.btn_estaeType.Size = new System.Drawing.Size(104, 44);
            this.btn_estaeType.TabIndex = 5;
            this.btn_estaeType.Text = "نوع ملک";
            this.btn_estaeType.UseVisualStyleBackColor = true;
            this.btn_estaeType.Click += new System.EventHandler(this.btn_estaeType_Click);
            // 
            // btn_area
            // 
            this.btn_area.Location = new System.Drawing.Point(0, 0);
            this.btn_area.Name = "btn_area";
            this.btn_area.Size = new System.Drawing.Size(104, 44);
            this.btn_area.TabIndex = 0;
            this.btn_area.Text = "منطقه";
            this.btn_area.UseVisualStyleBackColor = true;
            this.btn_area.Click += new System.EventHandler(this.btn_area_Click);
            // 
            // RecoveryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 400);
            this.Controls.Add(this.pnl_buttons);
            this.Controls.Add(this.RecoverList);
            this.Controls.Add(this.pnl_selectrecBut);
            this.Controls.Add(this.OrderButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RecoveryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "بازگردانی اطلاعات";
            this.Load += new System.EventHandler(this.RecoveryForm_Load);
            this.OrderButtons.ResumeLayout(false);
            this.OrderButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RecoverList)).EndInit();
            this.pnl_buttons.ResumeLayout(false);
            this.pnl_buttons.PerformLayout();
            this.pnl_selectrecBut.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip OrderButtons;
        private System.Windows.Forms.DataGridView RecoverList;
        private System.Windows.Forms.Panel pnl_buttons;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnl_selectrecBut;
        private System.Windows.Forms.Button btn_estateBill;
        private System.Windows.Forms.Button btn_estate;
        private System.Windows.Forms.Button btn_saleType;
        private System.Windows.Forms.Button btn_estaeType;
        private System.Windows.Forms.Button btn_area;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cl_CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_info;
        private System.Windows.Forms.Label label1;
    }
}