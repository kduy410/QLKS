namespace QLKS
{
    partial class LsOrderForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewSV = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewSV
            // 
            this.listViewSV.BackColor = System.Drawing.SystemColors.Window;
            this.listViewSV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewSV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewSV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSV.FullRowSelect = true;
            this.listViewSV.Location = new System.Drawing.Point(0, 60);
            this.listViewSV.Name = "listViewSV";
            this.listViewSV.Size = new System.Drawing.Size(600, 360);
            this.listViewSV.TabIndex = 0;
            this.listViewSV.UseCompatibleStateImageBehavior = false;
            this.listViewSV.View = System.Windows.Forms.View.Details;
            this.listViewSV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSV_ColumnClick);
            this.listViewSV.DoubleClick += new System.EventHandler(this.listViewSV_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Dịch vụ";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Tên";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Giá";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Số lượng";
            this.columnHeader5.Width = 150;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(160, 40);
            this.lblName.TabIndex = 75;
            this.lblName.Text = "ORDER";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LsOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.listViewSV);
            this.Controls.Add(this.lblName);
            this.Name = "LsOrderForm";
            this.Size = new System.Drawing.Size(600, 420);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        protected internal System.Windows.Forms.ListView listViewSV;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
