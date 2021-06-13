
namespace speech_recognition_test_2
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Animation = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.MenuText;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 46;
            this.listBox1.Items.AddRange(new object[] {
            " "});
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1408, 632);
            this.listBox1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.ControlText;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.checkBox1.Location = new System.Drawing.Point(0, 719);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(1408, 24);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Auto scroll";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // Animation
            // 
            this.Animation.Tick += new System.EventHandler(this.Animation_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1408, 87);
            this.panel1.TabIndex = 3;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Right;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button4.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.button4.Location = new System.Drawing.Point(1198, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(74, 71);
            this.button4.TabIndex = 3;
            this.button4.Text = "_";
            this.button4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button4.UseCompatibleTextRendering = true;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            this.button4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button4_MouseMove);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Right;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button3.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.button3.Location = new System.Drawing.Point(1272, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(68, 71);
            this.button3.TabIndex = 2;
            this.button3.Text = "□";
            this.button3.UseCompatibleTextRendering = true;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            this.button3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button3_MouseMove);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Right;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.button2.Location = new System.Drawing.Point(1340, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 71);
            this.button2.TabIndex = 1;
            this.button2.Text = "x";
            this.button2.UseCompatibleTextRendering = true;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            this.button2.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            this.button2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button2_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1408, 16);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1408, 632);
            this.panel3.TabIndex = 4;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 1000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::speech_recognition_test_2.Properties.Resources.jarvis1logo;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(54, 71);
            this.panel4.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::speech_recognition_test_2.Properties.Resources.frame1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(0, 743);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1408, 101);
            this.button1.TabIndex = 1;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label1.Location = new System.Drawing.Point(54, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 71);
            this.label1.TabIndex = 5;
            this.label1.Text = "Jarvis 0.1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1408, 844);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Jarvis 0.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Timer Animation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
    }
}

