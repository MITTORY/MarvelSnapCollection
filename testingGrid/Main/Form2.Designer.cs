namespace testingGrid
{
    partial class Form2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resetcolorBTN = new System.Windows.Forms.Button();
            this.codeColorConfirm = new System.Windows.Forms.Button();
            this.codeColor = new System.Windows.Forms.TextBox();
            this.colorBTN = new System.Windows.Forms.Button();
            this.resetimageBTN = new System.Windows.Forms.Button();
            this.imageBTN = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "BACKGROUND";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resetcolorBTN);
            this.groupBox1.Controls.Add(this.codeColorConfirm);
            this.groupBox1.Controls.Add(this.codeColor);
            this.groupBox1.Controls.Add(this.colorBTN);
            this.groupBox1.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 199);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color";
            // 
            // resetcolorBTN
            // 
            this.resetcolorBTN.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetcolorBTN.Location = new System.Drawing.Point(21, 158);
            this.resetcolorBTN.Name = "resetcolorBTN";
            this.resetcolorBTN.Size = new System.Drawing.Size(113, 34);
            this.resetcolorBTN.TabIndex = 2;
            this.resetcolorBTN.Text = "RESET";
            this.resetcolorBTN.UseVisualStyleBackColor = true;
            this.resetcolorBTN.Click += new System.EventHandler(this.resetBTN_Click);
            // 
            // codeColorConfirm
            // 
            this.codeColorConfirm.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F);
            this.codeColorConfirm.Location = new System.Drawing.Point(6, 59);
            this.codeColorConfirm.Name = "codeColorConfirm";
            this.codeColorConfirm.Size = new System.Drawing.Size(145, 35);
            this.codeColorConfirm.TabIndex = 6;
            this.codeColorConfirm.Text = "Confirm";
            this.codeColorConfirm.UseVisualStyleBackColor = true;
            this.codeColorConfirm.Click += new System.EventHandler(this.codeColorConfirm_Click);
            // 
            // codeColor
            // 
            this.codeColor.Location = new System.Drawing.Point(6, 26);
            this.codeColor.Name = "codeColor";
            this.codeColor.Size = new System.Drawing.Size(145, 27);
            this.codeColor.TabIndex = 5;
            this.codeColor.Text = "#RRGGBB";
            this.codeColor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colorBTN
            // 
            this.colorBTN.Location = new System.Drawing.Point(6, 118);
            this.colorBTN.Name = "colorBTN";
            this.colorBTN.Size = new System.Drawing.Size(145, 34);
            this.colorBTN.TabIndex = 3;
            this.colorBTN.Text = "ColorDialog";
            this.colorBTN.UseVisualStyleBackColor = true;
            this.colorBTN.Click += new System.EventHandler(this.colorBTN_Click);
            // 
            // resetimageBTN
            // 
            this.resetimageBTN.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetimageBTN.Location = new System.Drawing.Point(6, 66);
            this.resetimageBTN.Name = "resetimageBTN";
            this.resetimageBTN.Size = new System.Drawing.Size(142, 34);
            this.resetimageBTN.TabIndex = 4;
            this.resetimageBTN.Text = "RESET";
            this.resetimageBTN.UseVisualStyleBackColor = true;
            this.resetimageBTN.Click += new System.EventHandler(this.resetimageBTN_Click);
            // 
            // imageBTN
            // 
            this.imageBTN.Location = new System.Drawing.Point(6, 26);
            this.imageBTN.Name = "imageBTN";
            this.imageBTN.Size = new System.Drawing.Size(142, 34);
            this.imageBTN.TabIndex = 2;
            this.imageBTN.Text = "IMAGE";
            this.imageBTN.UseVisualStyleBackColor = true;
            this.imageBTN.Click += new System.EventHandler(this.imageBTN_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.imageBTN);
            this.groupBox2.Controls.Add(this.resetimageBTN);
            this.groupBox2.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.groupBox2.Location = new System.Drawing.Point(175, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(153, 106);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(335, 254);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button imageBTN;
        private System.Windows.Forms.Button colorBTN;
        private System.Windows.Forms.Button resetcolorBTN;
        private System.Windows.Forms.Button resetimageBTN;
        private System.Windows.Forms.TextBox codeColor;
        private System.Windows.Forms.Button codeColorConfirm;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}