namespace QLKS
{
    partial class ServiceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnItem = new System.Windows.Forms.Button();
            this.btnDrink = new System.Windows.Forms.Button();
            this.btnFood = new System.Windows.Forms.Button();
            this.pnList = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(740, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 60);
            this.btnAdd.TabIndex = 65;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSubmit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 540);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 60);
            this.panel2.TabIndex = 61;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubmit.Location = new System.Drawing.Point(550, 0);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(250, 60);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "ĐỒNG Ý";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(419, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 32);
            this.label1.TabIndex = 62;
            this.label1.Text = "Phòng :-";
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoom.Location = new System.Drawing.Point(544, 74);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(106, 32);
            this.lblRoom.TabIndex = 67;
            this.lblRoom.Text = "Phòng :-";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(360, 60);
            this.button1.TabIndex = 66;
            this.button1.Text = "DỊCH VỤ";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnOrder);
            this.panel3.Controls.Add(this.btnItem);
            this.panel3.Controls.Add(this.btnDrink);
            this.panel3.Controls.Add(this.btnFood);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 480);
            this.panel3.TabIndex = 68;
            // 
            // btnOrder
            // 
            this.btnOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnOrder.FlatAppearance.BorderSize = 0;
            this.btnOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnOrder.Image")));
            this.btnOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrder.Location = new System.Drawing.Point(20, 80);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(160, 40);
            this.btnOrder.TabIndex = 12;
            this.btnOrder.Text = "ORDER";
            this.btnOrder.UseVisualStyleBackColor = false;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnItem
            // 
            this.btnItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnItem.FlatAppearance.BorderSize = 0;
            this.btnItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItem.Image = ((System.Drawing.Image)(resources.GetObject("btnItem.Image")));
            this.btnItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItem.Location = new System.Drawing.Point(20, 260);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(160, 40);
            this.btnItem.TabIndex = 11;
            this.btnItem.Text = "VẬT DỤNG";
            this.btnItem.UseVisualStyleBackColor = false;
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // btnDrink
            // 
            this.btnDrink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnDrink.FlatAppearance.BorderSize = 0;
            this.btnDrink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrink.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDrink.Image = ((System.Drawing.Image)(resources.GetObject("btnDrink.Image")));
            this.btnDrink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDrink.Location = new System.Drawing.Point(20, 200);
            this.btnDrink.Name = "btnDrink";
            this.btnDrink.Size = new System.Drawing.Size(160, 40);
            this.btnDrink.TabIndex = 10;
            this.btnDrink.Text = "THỨC UỐNG";
            this.btnDrink.UseVisualStyleBackColor = false;
            this.btnDrink.Click += new System.EventHandler(this.btnDrink_Click);
            // 
            // btnFood
            // 
            this.btnFood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(190)))));
            this.btnFood.FlatAppearance.BorderSize = 0;
            this.btnFood.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFood.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFood.Image = ((System.Drawing.Image)(resources.GetObject("btnFood.Image")));
            this.btnFood.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFood.Location = new System.Drawing.Point(20, 140);
            this.btnFood.Name = "btnFood";
            this.btnFood.Size = new System.Drawing.Size(160, 40);
            this.btnFood.TabIndex = 9;
            this.btnFood.Text = "ĐỒ ĂN";
            this.btnFood.UseVisualStyleBackColor = false;
            this.btnFood.Click += new System.EventHandler(this.btnFood_Click);
            // 
            // pnList
            // 
            this.pnList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnList.Location = new System.Drawing.Point(200, 122);
            this.pnList.Name = "pnList";
            this.pnList.Size = new System.Drawing.Size(600, 418);
            this.pnList.TabIndex = 69;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Control;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(740, 60);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 62);
            this.btnReset.TabIndex = 70;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pnList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ServiceForm";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.ServiceForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        protected internal System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Button button1;
        protected internal System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnItem;
        private System.Windows.Forms.Button btnDrink;
        private System.Windows.Forms.Button btnFood;
        private System.Windows.Forms.Panel pnList;
        protected internal System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnOrder;
    }
}
