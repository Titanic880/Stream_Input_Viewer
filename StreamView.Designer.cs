namespace KeyStreamOverlay {
    partial class StreamView {
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
            LblCropVals = new Label();
            SuspendLayout();
            // 
            // BtnPause
            // 
            BtnPause.Location = new Point(12, 307);
            BtnPause.Name = "BtnPause";
            BtnPause.Size = new Size(176, 47);
            BtnPause.TabIndex = 1;
            BtnPause.Text = "Pause";
            BtnPause.UseVisualStyleBackColor = true;
            BtnPause.Click += BtnPause_Click;
            // 
            // LblCropVals
            // 
            LblCropVals.AutoSize = true;
            LblCropVals.Location = new Point(12, 289);
            LblCropVals.Name = "LblCropVals";
            LblCropVals.Size = new Size(119, 15);
            LblCropVals.TabIndex = 100;
            LblCropVals.Text = "Crop Sizing (L:T:R:B): ";
            // 
            // StreamView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(200, 364);
            Controls.Add(LblCropVals);
            Controls.Add(BtnPause);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "StreamView";
            Text = "StreamView";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BtnPause;
        private Label LblCropVals;
    }
}