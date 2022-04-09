namespace ExportMaps
{
    partial class SaveDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnResolution = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilePrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxFields = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(575, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "محل ذخیره";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(89, 18);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(480, 22);
            this.txtPath.TabIndex = 1;
            this.txtPath.Text = "C:\\Users\\mah\\Desktop\\temp";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(51, 17);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnResolution
            // 
            this.btnResolution.Location = new System.Drawing.Point(432, 62);
            this.btnResolution.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.btnResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.btnResolution.Name = "btnResolution";
            this.btnResolution.Size = new System.Drawing.Size(120, 22);
            this.btnResolution.TabIndex = 3;
            this.btnResolution.Value = new decimal(new int[] {
            320,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(573, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "رزولوشن";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(51, 110);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(93, 34);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "اعمال";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "پیشوند نام فایل های خروجی";
            // 
            // txtFilePrefix
            // 
            this.txtFilePrefix.Location = new System.Drawing.Point(51, 64);
            this.txtFilePrefix.Name = "txtFilePrefix";
            this.txtFilePrefix.Size = new System.Drawing.Size(173, 22);
            this.txtFilePrefix.TabIndex = 7;
            this.txtFilePrefix.Text = "Export";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(562, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "انتخاب فیلد نام";
            // 
            // cbxFields
            // 
            this.cbxFields.FormattingEnabled = true;
            this.cbxFields.Location = new System.Drawing.Point(215, 105);
            this.cbxFields.Name = "cbxFields";
            this.cbxFields.Size = new System.Drawing.Size(302, 24);
            this.cbxFields.TabIndex = 10;
            // 
            // SaveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 162);
            this.Controls.Add(this.cbxFields);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFilePrefix);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnResolution);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "خروجی نقشه";
            this.Load += new System.EventHandler(this.SaveDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnResolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.NumericUpDown btnResolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilePrefix;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxFields;
    }
}