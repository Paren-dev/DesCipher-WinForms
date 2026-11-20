namespace WindowsFormsApp1
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
            this.buttonEN = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonde = new System.Windows.Forms.Button();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.buttonGenKey = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoadKey = new System.Windows.Forms.Button();
            this.buttonSaveKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonEN
            // 
            this.buttonEN.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonEN.Font = new System.Drawing.Font("Minecraft Rus", 9.25F);
            this.buttonEN.Location = new System.Drawing.Point(491, 124);
            this.buttonEN.Name = "buttonEN";
            this.buttonEN.Size = new System.Drawing.Size(144, 56);
            this.buttonEN.TabIndex = 2;
            this.buttonEN.Text = "Зашифорвать";
            this.buttonEN.UseVisualStyleBackColor = false;
            this.buttonEN.Click += new System.EventHandler(this.buttonEN_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 124);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(239, 56);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(189, 214);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(239, 58);
            this.textBox2.TabIndex = 4;
            // 
            // buttonde
            // 
            this.buttonde.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.buttonde.Font = new System.Drawing.Font("Minecraft Rus", 9.25F);
            this.buttonde.Location = new System.Drawing.Point(491, 214);
            this.buttonde.Name = "buttonde";
            this.buttonde.Size = new System.Drawing.Size(144, 56);
            this.buttonde.TabIndex = 5;
            this.buttonde.Text = "Дешифровать";
            this.buttonde.UseVisualStyleBackColor = false;
            this.buttonde.Click += new System.EventHandler(this.buttonde_Click);
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(491, 393);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(100, 20);
            this.textBoxKey.TabIndex = 6;
            // 
            // buttonGenKey
            // 
            this.buttonGenKey.Location = new System.Drawing.Point(0, 0);
            this.buttonGenKey.Name = "buttonGenKey";
            this.buttonGenKey.Size = new System.Drawing.Size(127, 23);
            this.buttonGenKey.TabIndex = 7;
            this.buttonGenKey.Text = "Сгенерировать ключ";
            this.buttonGenKey.UseVisualStyleBackColor = true;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(133, 0);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(102, 23);
            this.buttonLoad.TabIndex = 8;
            this.buttonLoad.Text = "Загрузить файл";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoadClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(449, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ключ:";
            // 
            // buttonLoadKey
            // 
            this.buttonLoadKey.Location = new System.Drawing.Point(0, 29);
            this.buttonLoadKey.Name = "buttonLoadKey";
            this.buttonLoadKey.Size = new System.Drawing.Size(127, 23);
            this.buttonLoadKey.TabIndex = 11;
            this.buttonLoadKey.Text = "Загрузить ключ";
            this.buttonLoadKey.UseVisualStyleBackColor = true;
            this.buttonLoadKey.Click += new System.EventHandler(this.buttonLoadKey_Click);
            // 
            // buttonSaveKey
            // 
            this.buttonSaveKey.Location = new System.Drawing.Point(133, 29);
            this.buttonSaveKey.Name = "buttonSaveKey";
            this.buttonSaveKey.Size = new System.Drawing.Size(102, 23);
            this.buttonSaveKey.TabIndex = 12;
            this.buttonSaveKey.Text = "Выгрузить  ключ";
            this.buttonSaveKey.UseVisualStyleBackColor = true;
            this.buttonSaveKey.Click += new System.EventHandler(this.buttonSaveKey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSaveKey);
            this.Controls.Add(this.buttonLoadKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonGenKey);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.buttonde);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonEN);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonEN;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonde;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Button buttonGenKey;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoadKey;
        private System.Windows.Forms.Button buttonSaveKey;
    }
}

