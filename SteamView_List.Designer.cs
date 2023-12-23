namespace KeyStreamOverlay
{
    partial class SteamView_List
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
            listBox1 = new ListBox();
            BtnPause = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.BackColor = Color.Lime;
            listBox1.Font = new Font("Nirmala UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(176, 277);
            listBox1.TabIndex = 0;
            // 
            // BtnPause
            // 
            BtnPause.Location = new Point(12, 307);
            BtnPause.Name = "BtnPause";
            BtnPause.Size = new Size(176, 47);
            BtnPause.TabIndex = 5;
            BtnPause.Text = "Pause";
            BtnPause.UseVisualStyleBackColor = true;
            BtnPause.Click += BtnPause_Click;
            // 
            // SteamView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(200, 365);
            Controls.Add(BtnPause);
            Controls.Add(listBox1);
            Name = "SteamView";
            Text = "StreamView";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button BtnPause;
    }
}