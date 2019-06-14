namespace QLKS
{
    partial class HomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegistration = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblIDUser = new System.Windows.Forms.Label();
            this.btnRoom = new System.Windows.Forms.Button();
            this.btnGuest = new System.Windows.Forms.Button();
            this.btnCheckinList = new System.Windows.Forms.Button();
            this.btnGuestList = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.btnReservation = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRoomList = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegistration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnRegistration);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 100);
            this.panel1.TabIndex = 1;
            // 
            // btnRegistration
            // 
            this.btnRegistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRegistration.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistration.Image")));
            this.btnRegistration.Location = new System.Drawing.Point(840, 20);
            this.btnRegistration.Name = "btnRegistration";
            this.btnRegistration.Size = new System.Drawing.Size(60, 60);
            this.btnRegistration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnRegistration.TabIndex = 13;
            this.btnRegistration.TabStop = false;
            this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Harrington", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(99, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "Quản lý khách sạn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Harrington", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(101, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 29);
            this.label1.TabIndex = 12;
            this.label1.Text = "PHẦN MỀM";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::QLKS.Properties.Resources.hotel__1_;
            this.pictureBox2.Location = new System.Drawing.Point(15, 15);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(80, 70);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::QLKS.Properties.Resources.x_mark;
            this.btnClose.Location = new System.Drawing.Point(917, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 60);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 12;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblIDUser);
            this.panel2.Controls.Add(this.btnRoom);
            this.panel2.Controls.Add(this.btnGuest);
            this.panel2.Controls.Add(this.btnCheckinList);
            this.panel2.Controls.Add(this.btnGuestList);
            this.panel2.Controls.Add(this.lblUser);
            this.panel2.Controls.Add(this.btnReservation);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnRoomList);
            this.panel2.Controls.Add(this.btnLogout);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 600);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblIDUser
            // 
            this.lblIDUser.AutoSize = true;
            this.lblIDUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDUser.Location = new System.Drawing.Point(8, 467);
            this.lblIDUser.Name = "lblIDUser";
            this.lblIDUser.Size = new System.Drawing.Size(87, 21);
            this.lblIDUser.TabIndex = 12;
            this.lblIDUser.Text = "Username";
            this.lblIDUser.Visible = false;
            // 
            // btnRoom
            // 
            this.btnRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnRoom.FlatAppearance.BorderSize = 0;
            this.btnRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoom.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoom.Image = ((System.Drawing.Image)(resources.GetObject("btnRoom.Image")));
            this.btnRoom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoom.Location = new System.Drawing.Point(18, 320);
            this.btnRoom.Name = "btnRoom";
            this.btnRoom.Size = new System.Drawing.Size(160, 40);
            this.btnRoom.TabIndex = 7;
            this.btnRoom.Text = "TẠO PHÒNG";
            this.btnRoom.UseVisualStyleBackColor = false;
            this.btnRoom.Click += new System.EventHandler(this.btnRoom_Click);
            // 
            // btnGuest
            // 
            this.btnGuest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnGuest.FlatAppearance.BorderSize = 0;
            this.btnGuest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuest.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuest.Image = ((System.Drawing.Image)(resources.GetObject("btnGuest.Image")));
            this.btnGuest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuest.Location = new System.Drawing.Point(19, 200);
            this.btnGuest.Name = "btnGuest";
            this.btnGuest.Size = new System.Drawing.Size(160, 40);
            this.btnGuest.TabIndex = 5;
            this.btnGuest.Text = "KHÁCH HÀNG";
            this.btnGuest.UseVisualStyleBackColor = false;
            this.btnGuest.Click += new System.EventHandler(this.btnGuest_Click);
            // 
            // btnCheckinList
            // 
            this.btnCheckinList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnCheckinList.FlatAppearance.BorderSize = 0;
            this.btnCheckinList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckinList.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckinList.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckinList.Image")));
            this.btnCheckinList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCheckinList.Location = new System.Drawing.Point(20, 80);
            this.btnCheckinList.Name = "btnCheckinList";
            this.btnCheckinList.Size = new System.Drawing.Size(160, 40);
            this.btnCheckinList.TabIndex = 1;
            this.btnCheckinList.Text = "NHẬT KÍ";
            this.btnCheckinList.UseVisualStyleBackColor = false;
            this.btnCheckinList.Click += new System.EventHandler(this.btnCheckinList_Click);
            // 
            // btnGuestList
            // 
            this.btnGuestList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnGuestList.FlatAppearance.BorderSize = 0;
            this.btnGuestList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuestList.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuestList.Image = ((System.Drawing.Image)(resources.GetObject("btnGuestList.Image")));
            this.btnGuestList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuestList.Location = new System.Drawing.Point(18, 260);
            this.btnGuestList.Name = "btnGuestList";
            this.btnGuestList.Size = new System.Drawing.Size(160, 40);
            this.btnGuestList.TabIndex = 6;
            this.btnGuestList.Text = "DANH SÁCH";
            this.btnGuestList.UseVisualStyleBackColor = false;
            this.btnGuestList.Click += new System.EventHandler(this.btnGuestList_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(106, 548);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(87, 21);
            this.lblUser.TabIndex = 11;
            this.lblUser.Text = "Username";
            // 
            // btnReservation
            // 
            this.btnReservation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnReservation.FlatAppearance.BorderSize = 0;
            this.btnReservation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReservation.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReservation.Image = ((System.Drawing.Image)(resources.GetObject("btnReservation.Image")));
            this.btnReservation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReservation.Location = new System.Drawing.Point(19, 140);
            this.btnReservation.Name = "btnReservation";
            this.btnReservation.Size = new System.Drawing.Size(160, 40);
            this.btnReservation.TabIndex = 4;
            this.btnReservation.Text = "ĐẶT TRƯỚC";
            this.btnReservation.UseVisualStyleBackColor = false;
            this.btnReservation.Click += new System.EventHandler(this.btnReservation_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 569);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "CurrentTime";
            // 
            // btnRoomList
            // 
            this.btnRoomList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnRoomList.FlatAppearance.BorderSize = 0;
            this.btnRoomList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomList.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomList.Image = ((System.Drawing.Image)(resources.GetObject("btnRoomList.Image")));
            this.btnRoomList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoomList.Location = new System.Drawing.Point(20, 20);
            this.btnRoomList.Name = "btnRoomList";
            this.btnRoomList.Size = new System.Drawing.Size(160, 40);
            this.btnRoomList.TabIndex = 8;
            this.btnRoomList.Text = "PHÒNG";
            this.btnRoomList.UseVisualStyleBackColor = false;
            this.btnRoomList.Click += new System.EventHandler(this.btnRoomList_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(12, 509);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(173, 36);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Thoát";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(200, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 600);
            this.panel3.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 548);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 21);
            this.label4.TabIndex = 13;
            this.label4.Text = "Chào mừng";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomeForm";
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRegistration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRoom;
        private System.Windows.Forms.Button btnGuest;
        private System.Windows.Forms.Button btnReservation;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnCheckinList;
        private System.Windows.Forms.Button btnGuestList;
        private System.Windows.Forms.Button btnRoomList;
        private System.Windows.Forms.PictureBox btnRegistration;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        protected internal System.Windows.Forms.Panel panel3;
        protected internal System.Windows.Forms.Label lblIDUser;
        protected internal System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label4;
    }
}