namespace testingGrid
{
    partial class cardInterface
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cardInterface));
            this.name = new System.Windows.Forms.Label();
            this.YesNo = new System.Windows.Forms.Label();
            this.Pool = new System.Windows.Forms.Label();
            this.costText = new System.Windows.Forms.Label();
            this.powerText = new System.Windows.Forms.Label();
            this.acceptBox = new System.Windows.Forms.PictureBox();
            this.declineBox = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Image = new System.Windows.Forms.PictureBox();
            this.editButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.acceptBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.declineBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Bahnschrift Condensed", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name.Location = new System.Drawing.Point(3, 0);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(224, 30);
            this.name.TabIndex = 1;
            this.name.Text = "Name";
            this.name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.name.Click += new System.EventHandler(this.name_Click);
            // 
            // YesNo
            // 
            this.YesNo.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.YesNo.Location = new System.Drawing.Point(3, 41);
            this.YesNo.Name = "YesNo";
            this.YesNo.Size = new System.Drawing.Size(224, 21);
            this.YesNo.TabIndex = 3;
            this.YesNo.Text = "Owner";
            this.YesNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Pool
            // 
            this.Pool.Font = new System.Drawing.Font("Bahnschrift SemiLight", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Pool.Location = new System.Drawing.Point(3, 24);
            this.Pool.Name = "Pool";
            this.Pool.Size = new System.Drawing.Size(224, 23);
            this.Pool.TabIndex = 8;
            this.Pool.Text = "1";
            this.Pool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // costText
            // 
            this.costText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(182)))), ((int)(((byte)(243)))));
            this.costText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.costText.Font = new System.Drawing.Font("Srbija Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costText.ForeColor = System.Drawing.Color.White;
            this.costText.Image = ((System.Drawing.Image)(resources.GetObject("costText.Image")));
            this.costText.Location = new System.Drawing.Point(4, 5);
            this.costText.Name = "costText";
            this.costText.Size = new System.Drawing.Size(38, 25);
            this.costText.TabIndex = 11;
            this.costText.Text = "1";
            this.costText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.costText.Click += new System.EventHandler(this.costText_Click);
            // 
            // powerText
            // 
            this.powerText.BackColor = System.Drawing.Color.Transparent;
            this.powerText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.powerText.Font = new System.Drawing.Font("Srbija Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerText.ForeColor = System.Drawing.Color.White;
            this.powerText.Image = ((System.Drawing.Image)(resources.GetObject("powerText.Image")));
            this.powerText.Location = new System.Drawing.Point(189, 5);
            this.powerText.Name = "powerText";
            this.powerText.Size = new System.Drawing.Size(38, 25);
            this.powerText.TabIndex = 12;
            this.powerText.Text = "12";
            this.powerText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.powerText.Click += new System.EventHandler(this.powerText_Click);
            // 
            // acceptBox
            // 
            this.acceptBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.acceptBox.Image = global::testingGrid.Properties.Resources._1904674_accept_approved_check_checked_confirm_done_tick_122524;
            this.acceptBox.Location = new System.Drawing.Point(73, 302);
            this.acceptBox.Name = "acceptBox";
            this.acceptBox.Size = new System.Drawing.Size(50, 39);
            this.acceptBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.acceptBox.TabIndex = 14;
            this.acceptBox.TabStop = false;
            this.acceptBox.Visible = false;
            this.acceptBox.Click += new System.EventHandler(this.acceptBox_Click);
            // 
            // declineBox
            // 
            this.declineBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.declineBox.Image = global::testingGrid.Properties.Resources.interface_decline_reject_close_delete_failed_circle_icon_132982;
            this.declineBox.Location = new System.Drawing.Point(73, 302);
            this.declineBox.Name = "declineBox";
            this.declineBox.Size = new System.Drawing.Size(50, 39);
            this.declineBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.declineBox.TabIndex = 13;
            this.declineBox.TabStop = false;
            this.declineBox.Visible = false;
            this.declineBox.Click += new System.EventHandler(this.declineBox_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(193, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // Image
            // 
            this.Image.Location = new System.Drawing.Point(3, 3);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(232, 220);
            this.Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Image.TabIndex = 0;
            this.Image.TabStop = false;
            // 
            // editButton
            // 
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.editButton.Font = new System.Drawing.Font("Bahnschrift SemiBold Condensed", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editButton.Location = new System.Drawing.Point(73, 347);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 28);
            this.editButton.TabIndex = 15;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.costText);
            this.panel1.Controls.Add(this.powerText);
            this.panel1.Controls.Add(this.name);
            this.panel1.Controls.Add(this.Pool);
            this.panel1.Controls.Add(this.YesNo);
            this.panel1.Location = new System.Drawing.Point(3, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(232, 63);
            this.panel1.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // cardInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.acceptBox);
            this.Controls.Add(this.declineBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Image);
            this.Controls.Add(this.panel1);
            this.Name = "cardInterface";
            this.Size = new System.Drawing.Size(238, 297);
            this.Click += new System.EventHandler(this.cardInterface_Click);
            ((System.ComponentModel.ISupportInitialize)(this.acceptBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.declineBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox Image;
        public System.Windows.Forms.Label name;
        public System.Windows.Forms.Label YesNo;
        public System.Windows.Forms.Label Pool;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label costText;
        public System.Windows.Forms.Label powerText;
        public System.Windows.Forms.PictureBox declineBox;
        public System.Windows.Forms.PictureBox acceptBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
