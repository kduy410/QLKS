namespace QLKS
{
    partial class LsItemForm
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
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TenDV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DonGia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.soluong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewSV
            // 
            this.listViewSV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.TenDV,
            this.DonGia,
            this.soluong});
            this.listViewSV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewSV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSV.FullRowSelect = true;
            this.listViewSV.LabelEdit = true;
            this.listViewSV.Location = new System.Drawing.Point(0, 60);
            this.listViewSV.Name = "listViewSV";
            this.listViewSV.Size = new System.Drawing.Size(600, 360);
            this.listViewSV.TabIndex = 72;
            this.listViewSV.UseCompatibleStateImageBehavior = false;
            this.listViewSV.View = System.Windows.Forms.View.Details;
            this.listViewSV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSV_ColumnClick);
            this.listViewSV.DoubleClick += new System.EventHandler(this.listViewSV_DoubleClick);
            // 
            // id
            // 
            this.id.Text = "Mã";
            this.id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.id.Width = 50;
            // 
            // TenDV
            // 
            this.TenDV.Text = "Tên";
            this.TenDV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TenDV.Width = 200;
            // 
            // DonGia
            // 
            this.DonGia.Text = "Giá";
            this.DonGia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DonGia.Width = 200;
            // 
            // soluong
            // 
            this.soluong.Text = "Số lượng";
            this.soluong.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.soluong.Width = 150;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(0, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(160, 40);
            this.lblName.TabIndex = 75;
            this.lblName.Text = "ĐỒ DÙNG";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LsItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewSV);
            this.Controls.Add(this.lblName);
            this.Name = "LsItemForm";
            this.Size = new System.Drawing.Size(600, 420);
            this.Load += new System.EventHandler(this.LsItemForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.ListView listViewSV;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader TenDV;
        private System.Windows.Forms.ColumnHeader DonGia;
        private System.Windows.Forms.ColumnHeader soluong;
        private System.Windows.Forms.Label lblName;
    }
}
