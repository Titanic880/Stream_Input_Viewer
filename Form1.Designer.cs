namespace KeyStreamOverlay
{
    partial class Form1
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
            BtnAddProgram = new Button();
            listBox1 = new ListBox();
            LblList = new Label();
            BtnPauseKeybind = new Button();
            BtnStart = new Button();
            CBGlobal = new CheckBox();
            TbOutput = new TextBox();
            LblOut = new Label();
            TbTracked = new TextBox();
            BtnRemove = new Button();
            BtnForceSave = new Button();
            SuspendLayout();
            // 
            // BtnAddProgram
            // 
            BtnAddProgram.Location = new Point(12, 270);
            BtnAddProgram.Name = "BtnAddProgram";
            BtnAddProgram.Size = new Size(151, 23);
            BtnAddProgram.TabIndex = 0;
            BtnAddProgram.Text = "Add Tracked Program";
            BtnAddProgram.UseVisualStyleBackColor = true;
            BtnAddProgram.Click += BtnAddProgram_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(12, 26);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(151, 214);
            listBox1.TabIndex = 1;
            // 
            // LblList
            // 
            LblList.AutoSize = true;
            LblList.Location = new Point(12, 8);
            LblList.Name = "LblList";
            LblList.Size = new Size(151, 15);
            LblList.TabIndex = 2;
            LblList.Text = "ONLY these will show input";
            // 
            // BtnPauseKeybind
            // 
            BtnPauseKeybind.Location = new Point(169, 56);
            BtnPauseKeybind.Name = "BtnPauseKeybind";
            BtnPauseKeybind.Size = new Size(227, 23);
            BtnPauseKeybind.TabIndex = 3;
            BtnPauseKeybind.Text = "Set Pause Keybind";
            BtnPauseKeybind.UseVisualStyleBackColor = true;
            BtnPauseKeybind.Click += BtnPause_Keybind_Click;
            // 
            // BtnStart
            // 
            BtnStart.Location = new Point(169, 113);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(151, 47);
            BtnStart.TabIndex = 5;
            BtnStart.Text = "Start";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // CBGlobal
            // 
            CBGlobal.AutoSize = true;
            CBGlobal.Location = new Point(169, 88);
            CBGlobal.Name = "CBGlobal";
            CBGlobal.Size = new Size(160, 19);
            CBGlobal.TabIndex = 6;
            CBGlobal.Text = "Allow Global Hook Vision";
            CBGlobal.UseVisualStyleBackColor = true;
            // 
            // TbOutput
            // 
            TbOutput.Enabled = false;
            TbOutput.Location = new Point(169, 27);
            TbOutput.Name = "TbOutput";
            TbOutput.Size = new Size(227, 23);
            TbOutput.TabIndex = 7;
            // 
            // LblOut
            // 
            LblOut.AutoSize = true;
            LblOut.Location = new Point(169, 9);
            LblOut.Name = "LblOut";
            LblOut.Size = new Size(227, 15);
            LblOut.TabIndex = 8;
            LblOut.Text = "Hook Preview (Sets this to Pause keybind)";
            // 
            // TbTracked
            // 
            TbTracked.Location = new Point(12, 241);
            TbTracked.Name = "TbTracked";
            TbTracked.Size = new Size(151, 23);
            TbTracked.TabIndex = 9;
            TbTracked.Text = "Enter Program Name Here";
            // 
            // BtnRemove
            // 
            BtnRemove.Location = new Point(12, 299);
            BtnRemove.Name = "BtnRemove";
            BtnRemove.Size = new Size(151, 23);
            BtnRemove.TabIndex = 10;
            BtnRemove.Text = "Remove Selected";
            BtnRemove.UseVisualStyleBackColor = true;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // BtnForceSave
            // 
            BtnForceSave.Location = new Point(12, 328);
            BtnForceSave.Name = "BtnForceSave";
            BtnForceSave.Size = new Size(151, 23);
            BtnForceSave.TabIndex = 11;
            BtnForceSave.Text = "Save Settings";
            BtnForceSave.UseVisualStyleBackColor = true;
            BtnForceSave.Click += BtnForceSave_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 385);
            Controls.Add(BtnForceSave);
            Controls.Add(BtnRemove);
            Controls.Add(TbTracked);
            Controls.Add(LblOut);
            Controls.Add(TbOutput);
            Controls.Add(CBGlobal);
            Controls.Add(BtnStart);
            Controls.Add(BtnPauseKeybind);
            Controls.Add(LblList);
            Controls.Add(listBox1);
            Controls.Add(BtnAddProgram);
            Name = "Form1";
            Text = "Setup View";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnAddProgram;
        private ListBox listBox1;
        private Label LblList;
        private Button BtnPauseKeybind;
        private Button BtnStart;
        private CheckBox CBGlobal;
        private TextBox TbOutput;
        private Label LblOut;
        private TextBox TbTracked;
        private Button BtnRemove;
        private Button BtnForceSave;
    }
}