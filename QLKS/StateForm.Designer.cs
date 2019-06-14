namespace QLKS
{
    partial class StateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StateForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.listViewGuest = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewSV = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblRoom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentPeople = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSVprice = new System.Windows.Forms.Label();
            this.lblRoomPrice = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAdvance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReg = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(740, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 60);
            this.btnReset.TabIndex = 71;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnBack
            // 
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(60, 60);
            this.btnBack.TabIndex = 64;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // listViewGuest
            // 
            this.listViewGuest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewGuest.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewGuest.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewGuest.FullRowSelect = true;
            this.listViewGuest.Location = new System.Drawing.Point(0, 60);
            this.listViewGuest.Name = "listViewGuest";
            this.listViewGuest.Size = new System.Drawing.Size(800, 150);
            this.listViewGuest.TabIndex = 2;
            this.listViewGuest.UseCompatibleStateImageBehavior = false;
            this.listViewGuest.View = System.Windows.Forms.View.Details;
            this.listViewGuest.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewGuest_ColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "STT";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Loại";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "CMND";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Họ đệm";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Tên";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ngày đăng kí";
            this.columnHeader6.Width = 200;
            // 
            // listViewSV
            // 
            this.listViewSV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewSV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17});
            this.listViewSV.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewSV.FullRowSelect = true;
            this.listViewSV.Location = new System.Drawing.Point(0, 210);
            this.listViewSV.Name = "listViewSV";
            this.listViewSV.Size = new System.Drawing.Size(800, 132);
            this.listViewSV.TabIndex = 3;
            this.listViewSV.UseCompatibleStateImageBehavior = false;
            this.listViewSV.View = System.Windows.Forms.View.Details;
            this.listViewSV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSV_ColumnClick);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "STT";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Loại dịch vụ";
            this.columnHeader14.Width = 150;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Tên dịch vụ";
            this.columnHeader15.Width = 150;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Số lượng";
            this.columnHeader16.Width = 80;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Đơn giá";
            this.columnHeader17.Width = 150;
            // 
            // lblRoom
            // 
            this.lblRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblRoom.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoom.Location = new System.Drawing.Point(257, 372);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(130, 32);
            this.lblRoom.TabIndex = 65;
            this.lblRoom.Text = "Phòng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 32);
            this.label1.TabIndex = 66;
            this.label1.Text = "Phòng :-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(89, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 32);
            this.label2.TabIndex = 67;
            this.label2.Text = "Loại phòng :-";
            // 
            // lblRoomType
            // 
            this.lblRoomType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblRoomType.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomType.Location = new System.Drawing.Point(257, 428);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(130, 32);
            this.lblRoomType.TabIndex = 68;
            this.lblRoomType.Text = "Phòng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 484);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 32);
            this.label4.TabIndex = 69;
            this.label4.Text = "Số người hiện có :-";
            // 
            // lblCurrentPeople
            // 
            this.lblCurrentPeople.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblCurrentPeople.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPeople.Location = new System.Drawing.Point(257, 484);
            this.lblCurrentPeople.Name = "lblCurrentPeople";
            this.lblCurrentPeople.Size = new System.Drawing.Size(130, 32);
            this.lblCurrentPeople.TabIndex = 70;
            this.lblCurrentPeople.Text = "Phòng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(423, 452);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 32);
            this.label6.TabIndex = 71;
            this.label6.Text = "Tiền dịch vụ :-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(431, 532);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 32);
            this.label7.TabIndex = 72;
            this.label7.Text = "Tiền phòng :-";
            // 
            // lblSVprice
            // 
            this.lblSVprice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblSVprice.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSVprice.Location = new System.Drawing.Point(585, 452);
            this.lblSVprice.Name = "lblSVprice";
            this.lblSVprice.Size = new System.Drawing.Size(192, 32);
            this.lblSVprice.TabIndex = 71;
            this.lblSVprice.Text = "Phòng";
            // 
            // lblRoomPrice
            // 
            this.lblRoomPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblRoomPrice.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomPrice.Location = new System.Drawing.Point(585, 532);
            this.lblRoomPrice.Name = "lblRoomPrice";
            this.lblRoomPrice.Size = new System.Drawing.Size(192, 32);
            this.lblRoomPrice.TabIndex = 73;
            this.lblRoomPrice.Text = "Phòng";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(411, 372);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 32);
            this.label8.TabIndex = 74;
            this.label8.Text = "Tiền trả trước :-";
            // 
            // lblAdvance
            // 
            this.lblAdvance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblAdvance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvance.Location = new System.Drawing.Point(585, 372);
            this.lblAdvance.Name = "lblAdvance";
            this.lblAdvance.Size = new System.Drawing.Size(192, 32);
            this.lblAdvance.TabIndex = 75;
            this.lblAdvance.Text = "Phòng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 532);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 32);
            this.label3.TabIndex = 76;
            this.label3.Text = "Số người đăng kí  :-";
            // 
            // lblReg
            // 
            this.lblReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.lblReg.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReg.Location = new System.Drawing.Point(257, 532);
            this.lblReg.Name = "lblReg";
            this.lblReg.Size = new System.Drawing.Size(130, 32);
            this.lblReg.TabIndex = 77;
            this.lblReg.Text = "Phòng";
            // 
            // StateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblReg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCurrentPeople);
            this.Controls.Add(this.lblAdvance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRoomPrice);
            this.Controls.Add(this.lblSVprice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.listViewSV);
            this.Controls.Add(this.listViewGuest);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StateForm";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.StateForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ListView listViewGuest;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ListView listViewSV;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.Label lblCurrentPeople;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSVprice;
        private System.Windows.Forms.Label lblRoomPrice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAdvance;
        protected internal System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblReg;
        protected internal System.Windows.Forms.Label lblRoom;
        protected internal System.Windows.Forms.Label lblRoomType;
    }
}
