namespace KeyStreamOverlay {
    partial class SteamView_List {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            BtnPause = new Button();
            SuspendLayout();
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
            // SteamView_List
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(200, 365);
            Controls.Add(BtnPause);
            Name = "SteamView_List";
            Text = "StreamView";
            ResumeLayout(false);
        }

        #endregion
        private Button BtnPause;
    }
}