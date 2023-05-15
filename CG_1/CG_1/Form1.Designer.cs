namespace CG_1
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.IcoCheck = new System.Windows.Forms.CheckBox();
            this.DodCheck = new System.Windows.Forms.CheckBox();
            this.CubeCheck = new System.Windows.Forms.CheckBox();
            this.RotateBut = new System.Windows.Forms.Button();
            this.ScaleBut = new System.Windows.Forms.Button();
            this.TransBut = new System.Windows.Forms.Button();
            this.X_Cord = new System.Windows.Forms.TextBox();
            this.Y_Cord = new System.Windows.Forms.TextBox();
            this.Z_Cord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Check_ZBuff = new System.Windows.Forms.CheckBox();
            this.Check_Roberts = new System.Windows.Forms.CheckBox();
            this.BMP_Ckeck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.Location = new System.Drawing.Point(1, -1);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1078, 693);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            // 
            // IcoCheck
            // 
            this.IcoCheck.AutoSize = true;
            this.IcoCheck.Location = new System.Drawing.Point(1099, 16);
            this.IcoCheck.Name = "IcoCheck";
            this.IcoCheck.Size = new System.Drawing.Size(76, 17);
            this.IcoCheck.TabIndex = 2;
            this.IcoCheck.Text = "Икосаэдр";
            this.IcoCheck.UseVisualStyleBackColor = true;
            this.IcoCheck.CheckedChanged += new System.EventHandler(this.IcoCheck_CheckedChanged);
            // 
            // DodCheck
            // 
            this.DodCheck.AutoSize = true;
            this.DodCheck.Location = new System.Drawing.Point(1099, 39);
            this.DodCheck.Name = "DodCheck";
            this.DodCheck.Size = new System.Drawing.Size(83, 17);
            this.DodCheck.TabIndex = 3;
            this.DodCheck.Text = "Додекаэдр";
            this.DodCheck.UseVisualStyleBackColor = true;
            this.DodCheck.CheckedChanged += new System.EventHandler(this.DodCheck_CheckedChanged);
            // 
            // CubeCheck
            // 
            this.CubeCheck.AutoSize = true;
            this.CubeCheck.Location = new System.Drawing.Point(1099, 62);
            this.CubeCheck.Name = "CubeCheck";
            this.CubeCheck.Size = new System.Drawing.Size(44, 17);
            this.CubeCheck.TabIndex = 4;
            this.CubeCheck.Text = "Куб";
            this.CubeCheck.UseVisualStyleBackColor = true;
            this.CubeCheck.CheckedChanged += new System.EventHandler(this.CubeCheck_CheckedChanged);
            // 
            // RotateBut
            // 
            this.RotateBut.Location = new System.Drawing.Point(1085, 259);
            this.RotateBut.Name = "RotateBut";
            this.RotateBut.Size = new System.Drawing.Size(109, 23);
            this.RotateBut.TabIndex = 5;
            this.RotateBut.Text = "Повернуть";
            this.RotateBut.UseVisualStyleBackColor = true;
            this.RotateBut.Click += new System.EventHandler(this.RotateBut_Click);
            // 
            // ScaleBut
            // 
            this.ScaleBut.Location = new System.Drawing.Point(1085, 297);
            this.ScaleBut.Name = "ScaleBut";
            this.ScaleBut.Size = new System.Drawing.Size(109, 23);
            this.ScaleBut.TabIndex = 6;
            this.ScaleBut.Text = "Масштабировать";
            this.ScaleBut.UseVisualStyleBackColor = true;
            this.ScaleBut.Click += new System.EventHandler(this.ScaleBut_Click);
            // 
            // TransBut
            // 
            this.TransBut.Location = new System.Drawing.Point(1085, 335);
            this.TransBut.Name = "TransBut";
            this.TransBut.Size = new System.Drawing.Size(109, 23);
            this.TransBut.TabIndex = 7;
            this.TransBut.Text = "Сдвинуть";
            this.TransBut.UseVisualStyleBackColor = true;
            this.TransBut.Click += new System.EventHandler(this.TransBut_Click);
            // 
            // X_Cord
            // 
            this.X_Cord.Location = new System.Drawing.Point(1129, 120);
            this.X_Cord.Name = "X_Cord";
            this.X_Cord.Size = new System.Drawing.Size(100, 20);
            this.X_Cord.TabIndex = 8;
            // 
            // Y_Cord
            // 
            this.Y_Cord.Location = new System.Drawing.Point(1129, 161);
            this.Y_Cord.Name = "Y_Cord";
            this.Y_Cord.Size = new System.Drawing.Size(100, 20);
            this.Y_Cord.TabIndex = 9;
            // 
            // Z_Cord
            // 
            this.Z_Cord.Location = new System.Drawing.Point(1129, 204);
            this.Z_Cord.Name = "Z_Cord";
            this.Z_Cord.Size = new System.Drawing.Size(100, 20);
            this.Z_Cord.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1096, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1096, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1096, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Z:";
            // 
            // Check_ZBuff
            // 
            this.Check_ZBuff.AutoSize = true;
            this.Check_ZBuff.Location = new System.Drawing.Point(1188, 39);
            this.Check_ZBuff.Name = "Check_ZBuff";
            this.Check_ZBuff.Size = new System.Drawing.Size(75, 17);
            this.Check_ZBuff.TabIndex = 15;
            this.Check_ZBuff.Text = "Z-буффер";
            this.Check_ZBuff.UseVisualStyleBackColor = true;
            this.Check_ZBuff.CheckedChanged += new System.EventHandler(this.Check_ZBuff_CheckedChanged);
            // 
            // Check_Roberts
            // 
            this.Check_Roberts.AutoSize = true;
            this.Check_Roberts.Location = new System.Drawing.Point(1188, 16);
            this.Check_Roberts.Name = "Check_Roberts";
            this.Check_Roberts.Size = new System.Drawing.Size(68, 17);
            this.Check_Roberts.TabIndex = 14;
            this.Check_Roberts.Text = "Робертс";
            this.Check_Roberts.UseVisualStyleBackColor = true;
            this.Check_Roberts.CheckedChanged += new System.EventHandler(this.Check_Roberts_CheckedChanged);
            // 
            // BMP_Ckeck
            // 
            this.BMP_Ckeck.AutoSize = true;
            this.BMP_Ckeck.Location = new System.Drawing.Point(1099, 85);
            this.BMP_Ckeck.Name = "BMP_Ckeck";
            this.BMP_Ckeck.Size = new System.Drawing.Size(52, 17);
            this.BMP_Ckeck.TabIndex = 16;
            this.BMP_Ckeck.Text = ".BMP";
            this.BMP_Ckeck.UseVisualStyleBackColor = true;
            this.BMP_Ckeck.CheckedChanged += new System.EventHandler(this.BMP_Ckeck_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 774);
            this.Controls.Add(this.BMP_Ckeck);
            this.Controls.Add(this.Check_ZBuff);
            this.Controls.Add(this.Check_Roberts);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Z_Cord);
            this.Controls.Add(this.Y_Cord);
            this.Controls.Add(this.X_Cord);
            this.Controls.Add(this.TransBut);
            this.Controls.Add(this.ScaleBut);
            this.Controls.Add(this.RotateBut);
            this.Controls.Add(this.CubeCheck);
            this.Controls.Add(this.DodCheck);
            this.Controls.Add(this.IcoCheck);
            this.Controls.Add(this.Canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.CheckBox IcoCheck;
        private System.Windows.Forms.CheckBox DodCheck;
        private System.Windows.Forms.CheckBox CubeCheck;
        private System.Windows.Forms.Button RotateBut;
        private System.Windows.Forms.Button ScaleBut;
        private System.Windows.Forms.Button TransBut;
        private System.Windows.Forms.TextBox X_Cord;
        private System.Windows.Forms.TextBox Y_Cord;
        private System.Windows.Forms.TextBox Z_Cord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Check_ZBuff;
        private System.Windows.Forms.CheckBox Check_Roberts;
        private System.Windows.Forms.CheckBox BMP_Ckeck;
    }
}

