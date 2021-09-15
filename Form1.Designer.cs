
namespace dcqe_test
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonStart1 = new System.Windows.Forms.Button();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.buttonStart2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonEraser2 = new System.Windows.Forms.RadioButton();
            this.radioButtonEraser1 = new System.Windows.Forms.RadioButton();
            this.radioButtonEraser0 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonClearWhitePoints = new System.Windows.Forms.Button();
            this.buttonCls = new System.Windows.Forms.Button();
            this.pictureBoxDetector = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonColoring3 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonColoring2 = new System.Windows.Forms.RadioButton();
            this.radioButtonColoring1 = new System.Windows.Forms.RadioButton();
            this.radioButtonColoring0 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDetector)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart1
            // 
            this.buttonStart1.Location = new System.Drawing.Point(633, 12);
            this.buttonStart1.Name = "buttonStart1";
            this.buttonStart1.Size = new System.Drawing.Size(124, 23);
            this.buttonStart1.TabIndex = 0;
            this.buttonStart1.Text = "1 fotonpár indítása";
            this.buttonStart1.UseVisualStyleBackColor = true;
            this.buttonStart1.Click += new System.EventHandler(this.buttonStart1_Click);
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.BackColor = System.Drawing.Color.White;
            this.pictureBoxMain.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(609, 561);
            this.pictureBoxMain.TabIndex = 2;
            this.pictureBoxMain.TabStop = false;
            this.pictureBoxMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMain_Paint);
            // 
            // buttonStart2
            // 
            this.buttonStart2.Location = new System.Drawing.Point(773, 12);
            this.buttonStart2.Name = "buttonStart2";
            this.buttonStart2.Size = new System.Drawing.Size(120, 23);
            this.buttonStart2.TabIndex = 3;
            this.buttonStart2.Text = "100 fotonpár indítása";
            this.buttonStart2.UseVisualStyleBackColor = true;
            this.buttonStart2.Click += new System.EventHandler(this.buttonStart2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonEraser2);
            this.groupBox1.Controls.Add(this.radioButtonEraser1);
            this.groupBox1.Controls.Add(this.radioButtonEraser0);
            this.groupBox1.Location = new System.Drawing.Point(633, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 99);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kvantum radír beállítása";
            // 
            // radioButtonEraser2
            // 
            this.radioButtonEraser2.AutoSize = true;
            this.radioButtonEraser2.Location = new System.Drawing.Point(6, 65);
            this.radioButtonEraser2.Name = "radioButtonEraser2";
            this.radioButtonEraser2.Size = new System.Drawing.Size(153, 17);
            this.radioButtonEraser2.TabIndex = 2;
            this.radioButtonEraser2.Text = "Kvantum radír be (fordítva)";
            this.radioButtonEraser2.UseVisualStyleBackColor = true;
            this.radioButtonEraser2.CheckedChanged += new System.EventHandler(this.Eraserbuttons_CheckedChanged);
            // 
            // radioButtonEraser1
            // 
            this.radioButtonEraser1.AutoSize = true;
            this.radioButtonEraser1.Location = new System.Drawing.Point(6, 42);
            this.radioButtonEraser1.Name = "radioButtonEraser1";
            this.radioButtonEraser1.Size = new System.Drawing.Size(147, 17);
            this.radioButtonEraser1.TabIndex = 1;
            this.radioButtonEraser1.Text = "Kvantum radír be (normál)";
            this.radioButtonEraser1.UseVisualStyleBackColor = true;
            this.radioButtonEraser1.CheckedChanged += new System.EventHandler(this.Eraserbuttons_CheckedChanged);
            // 
            // radioButtonEraser0
            // 
            this.radioButtonEraser0.AutoSize = true;
            this.radioButtonEraser0.Checked = true;
            this.radioButtonEraser0.Location = new System.Drawing.Point(6, 19);
            this.radioButtonEraser0.Name = "radioButtonEraser0";
            this.radioButtonEraser0.Size = new System.Drawing.Size(134, 17);
            this.radioButtonEraser0.TabIndex = 0;
            this.radioButtonEraser0.TabStop = true;
            this.radioButtonEraser0.Text = "Kanvum radír ki (nincs)";
            this.radioButtonEraser0.UseVisualStyleBackColor = true;
            this.radioButtonEraser0.CheckedChanged += new System.EventHandler(this.Eraserbuttons_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonClearWhitePoints);
            this.groupBox2.Controls.Add(this.buttonCls);
            this.groupBox2.Controls.Add(this.pictureBoxDetector);
            this.groupBox2.Location = new System.Drawing.Point(562, 356);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 221);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "A signal fotonok detektor képernyője";
            // 
            // buttonClearWhitePoints
            // 
            this.buttonClearWhitePoints.Location = new System.Drawing.Point(204, 185);
            this.buttonClearWhitePoints.Name = "buttonClearWhitePoints";
            this.buttonClearWhitePoints.Size = new System.Drawing.Size(124, 23);
            this.buttonClearWhitePoints.TabIndex = 10;
            this.buttonClearWhitePoints.Text = "Fehér pontok törlése";
            this.buttonClearWhitePoints.UseVisualStyleBackColor = true;
            this.buttonClearWhitePoints.Click += new System.EventHandler(this.buttonClearWhitePoints_Click);
            // 
            // buttonCls
            // 
            this.buttonCls.Location = new System.Drawing.Point(6, 185);
            this.buttonCls.Name = "buttonCls";
            this.buttonCls.Size = new System.Drawing.Size(124, 23);
            this.buttonCls.TabIndex = 9;
            this.buttonCls.Text = "Képernyő törlése";
            this.buttonCls.UseVisualStyleBackColor = true;
            this.buttonCls.Click += new System.EventHandler(this.buttonCls_Click);
            // 
            // pictureBoxDetector
            // 
            this.pictureBoxDetector.BackColor = System.Drawing.Color.Black;
            this.pictureBoxDetector.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxDetector.Name = "pictureBoxDetector";
            this.pictureBoxDetector.Size = new System.Drawing.Size(320, 160);
            this.pictureBoxDetector.TabIndex = 5;
            this.pictureBoxDetector.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonColoring3);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.radioButtonColoring2);
            this.groupBox3.Controls.Add(this.radioButtonColoring1);
            this.groupBox3.Controls.Add(this.radioButtonColoring0);
            this.groupBox3.Location = new System.Drawing.Point(633, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 190);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "A detektált signal fotonok színezése";
            // 
            // radioButtonColoring3
            // 
            this.radioButtonColoring3.AutoSize = true;
            this.radioButtonColoring3.Location = new System.Drawing.Point(6, 88);
            this.radioButtonColoring3.Name = "radioButtonColoring3";
            this.radioButtonColoring3.Size = new System.Drawing.Size(104, 17);
            this.radioButtonColoring3.TabIndex = 4;
            this.radioButtonColoring3.Text = "Színezés kékkel";
            this.radioButtonColoring3.UseVisualStyleBackColor = true;
            this.radioButtonColoring3.CheckedChanged += new System.EventHandler(this.Coloringbuttons_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 71);
            this.label1.TabIndex = 3;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // radioButtonColoring2
            // 
            this.radioButtonColoring2.AutoSize = true;
            this.radioButtonColoring2.Location = new System.Drawing.Point(6, 65);
            this.radioButtonColoring2.Name = "radioButtonColoring2";
            this.radioButtonColoring2.Size = new System.Drawing.Size(107, 17);
            this.radioButtonColoring2.TabIndex = 2;
            this.radioButtonColoring2.TabStop = true;
            this.radioButtonColoring2.Text = "Színezés pirossal";
            this.radioButtonColoring2.UseVisualStyleBackColor = true;
            this.radioButtonColoring2.CheckedChanged += new System.EventHandler(this.Coloringbuttons_CheckedChanged);
            // 
            // radioButtonColoring1
            // 
            this.radioButtonColoring1.AutoSize = true;
            this.radioButtonColoring1.Location = new System.Drawing.Point(6, 42);
            this.radioButtonColoring1.Name = "radioButtonColoring1";
            this.radioButtonColoring1.Size = new System.Drawing.Size(97, 17);
            this.radioButtonColoring1.TabIndex = 1;
            this.radioButtonColoring1.TabStop = true;
            this.radioButtonColoring1.Text = "Nincs színezés";
            this.radioButtonColoring1.UseVisualStyleBackColor = true;
            this.radioButtonColoring1.CheckedChanged += new System.EventHandler(this.Coloringbuttons_CheckedChanged);
            // 
            // radioButtonColoring0
            // 
            this.radioButtonColoring0.AutoSize = true;
            this.radioButtonColoring0.Checked = true;
            this.radioButtonColoring0.Location = new System.Drawing.Point(6, 19);
            this.radioButtonColoring0.Name = "radioButtonColoring0";
            this.radioButtonColoring0.Size = new System.Drawing.Size(235, 17);
            this.radioButtonColoring0.TabIndex = 0;
            this.radioButtonColoring0.TabStop = true;
            this.radioButtonColoring0.Text = "Automata színezés a radír beállítása alapján";
            this.radioButtonColoring0.UseVisualStyleBackColor = true;
            this.radioButtonColoring0.CheckedChanged += new System.EventHandler(this.Coloringbuttons_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 589);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStart2);
            this.Controls.Add(this.pictureBoxMain);
            this.Controls.Add(this.buttonStart1);
            this.Name = "Form1";
            this.Text = "Késleltetett választásos kvantum radír szimuláció";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDetector)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart1;
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.Button buttonStart2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonEraser2;
        private System.Windows.Forms.RadioButton radioButtonEraser1;
        private System.Windows.Forms.RadioButton radioButtonEraser0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBoxDetector;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonColoring0;
        private System.Windows.Forms.RadioButton radioButtonColoring2;
        private System.Windows.Forms.RadioButton radioButtonColoring1;
        private System.Windows.Forms.RadioButton radioButtonColoring3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCls;
        private System.Windows.Forms.Button buttonClearWhitePoints;
    }
}

