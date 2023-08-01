namespace excel
{
    partial class Простои
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Простои));
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            button3 = new Button();
            label1 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            textBox3 = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Yellow;
            button1.Location = new Point(31, 59);
            button1.Name = "button1";
            button1.Size = new Size(164, 58);
            button1.TabIndex = 0;
            button1.Text = "Открыть файл";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Yellow;
            button2.CausesValidation = false;
            button2.Location = new Point(255, 59);
            button2.Name = "button2";
            button2.Size = new Size(156, 58);
            button2.TabIndex = 1;
            button2.Text = "Записать файл";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(31, 254);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(379, 23);
            textBox1.TabIndex = 2;
            // 
            // button3
            // 
            button3.BackColor = Color.Gainsboro;
            button3.Location = new Point(338, 295);
            button3.Name = "button3";
            button3.Size = new Size(73, 32);
            button3.TabIndex = 4;
            button3.Text = "Закрыть";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 236);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 5;
            label1.Text = "Путь файла";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(31, 173);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(158, 23);
            textBox2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 155);
            label2.Name = "label2";
            label2.Size = new Size(119, 15);
            label2.TabIndex = 7;
            label2.Text = "Количество записей";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(226, 173);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(158, 23);
            textBox3.TabIndex = 8;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // Простои
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(452, 339);
            Controls.Add(textBox3);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Простои";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Выгрузка простоев";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private Button button3;
        private Label label1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
    }
}