﻿namespace KeyStreamOverlay {
    partial class MainCustomize {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            BtnAddProgram = new Button();
            LstTracked = new ListBox();
            BtnPauseKeybind = new Button();
            BtnStart = new Button();
            TbOutput = new TextBox();
            LblOut = new Label();
            TbTracked = new TextBox();
            BtnRemove = new Button();
            BtnForceSave = new Button();
            BtnAddTranslation = new Button();
            BtnEditTranslation = new Button();
            BtnDeleteTranslation = new Button();
            LstTranslations = new ListBox();
            TbTranslation = new TextBox();
            CBKeys = new ComboBox();
            groupBox1 = new GroupBox();
            BtnResetTranslations = new Button();
            CBTranslationToggle = new CheckBox();
            BtnBackColorPicker = new Button();
            BtnTextColorPicker = new Button();
            CBOutputTypes = new ComboBox();
            LblUserOutput = new Label();
            groupBox2 = new GroupBox();
            CBSpamProtect = new CheckBox();
            CBModPrim = new CheckBox();
            CBMouseOut = new CheckBox();
            CBSkipSetupView = new CheckBox();
            CBShiftToggle = new CheckBox();
            CBDeleteLogClose = new CheckBox();
            CBDeleteLogLaunch = new CheckBox();
            CBLogToggle = new CheckBox();
            groupBox3 = new GroupBox();
            BtnAutoCatchName = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // BtnAddProgram
            // 
            BtnAddProgram.Location = new Point(172, 45);
            BtnAddProgram.Name = "BtnAddProgram";
            BtnAddProgram.Size = new Size(151, 23);
            BtnAddProgram.TabIndex = 5;
            BtnAddProgram.Text = "Add Tracked Program";
            BtnAddProgram.UseVisualStyleBackColor = true;
            BtnAddProgram.Click += BtnAddProgram_Click;
            // 
            // LstTracked
            // 
            LstTracked.FormattingEnabled = true;
            LstTracked.ItemHeight = 15;
            LstTracked.Location = new Point(4, 16);
            LstTracked.Name = "LstTracked";
            LstTracked.Size = new Size(158, 214);
            LstTracked.TabIndex = 3;
            // 
            // BtnPauseKeybind
            // 
            BtnPauseKeybind.Location = new Point(169, 207);
            BtnPauseKeybind.Name = "BtnPauseKeybind";
            BtnPauseKeybind.Size = new Size(151, 23);
            BtnPauseKeybind.TabIndex = 10;
            BtnPauseKeybind.Text = "Set Pause Keybind";
            BtnPauseKeybind.UseVisualStyleBackColor = true;
            BtnPauseKeybind.Click += BtnPause_Keybind_Click;
            // 
            // BtnStart
            // 
            BtnStart.Location = new Point(672, 191);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(328, 55);
            BtnStart.TabIndex = 30;
            BtnStart.Text = "Start StreamView";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // TbOutput
            // 
            TbOutput.Enabled = false;
            TbOutput.Location = new Point(169, 178);
            TbOutput.Name = "TbOutput";
            TbOutput.Size = new Size(151, 23);
            TbOutput.TabIndex = 9;
            // 
            // LblOut
            // 
            LblOut.AutoSize = true;
            LblOut.Location = new Point(165, 158);
            LblOut.Name = "LblOut";
            LblOut.Size = new Size(160, 15);
            LblOut.TabIndex = 99;
            LblOut.Text = "Keyboard Input (Pause Bind):";
            // 
            // TbTracked
            // 
            TbTracked.Location = new Point(172, 16);
            TbTracked.Name = "TbTracked";
            TbTracked.Size = new Size(151, 23);
            TbTracked.TabIndex = 4;
            TbTracked.Text = "Enter Program Name Here";
            // 
            // BtnRemove
            // 
            BtnRemove.Location = new Point(172, 74);
            BtnRemove.Name = "BtnRemove";
            BtnRemove.Size = new Size(151, 23);
            BtnRemove.TabIndex = 6;
            BtnRemove.Text = "Remove Selected";
            BtnRemove.UseVisualStyleBackColor = true;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // BtnForceSave
            // 
            BtnForceSave.Location = new Point(172, 132);
            BtnForceSave.Name = "BtnForceSave";
            BtnForceSave.Size = new Size(151, 23);
            BtnForceSave.TabIndex = 8;
            BtnForceSave.Text = "Save Settings";
            BtnForceSave.UseVisualStyleBackColor = true;
            BtnForceSave.Click += BtnForceSave_Click;
            // 
            // BtnAddTranslation
            // 
            BtnAddTranslation.Location = new Point(167, 74);
            BtnAddTranslation.Name = "BtnAddTranslation";
            BtnAddTranslation.Size = new Size(151, 23);
            BtnAddTranslation.TabIndex = 14;
            BtnAddTranslation.Text = "Add Translation";
            BtnAddTranslation.UseVisualStyleBackColor = true;
            BtnAddTranslation.Click += BtnAddTranslation_Click;
            // 
            // BtnEditTranslation
            // 
            BtnEditTranslation.Location = new Point(167, 103);
            BtnEditTranslation.Name = "BtnEditTranslation";
            BtnEditTranslation.Size = new Size(151, 23);
            BtnEditTranslation.TabIndex = 15;
            BtnEditTranslation.Text = "Edit Translation";
            BtnEditTranslation.UseVisualStyleBackColor = true;
            BtnEditTranslation.Click += BtnEditTranslation_Click;
            // 
            // BtnDeleteTranslation
            // 
            BtnDeleteTranslation.Location = new Point(167, 132);
            BtnDeleteTranslation.Name = "BtnDeleteTranslation";
            BtnDeleteTranslation.Size = new Size(151, 23);
            BtnDeleteTranslation.TabIndex = 16;
            BtnDeleteTranslation.Text = "Delete Translation";
            BtnDeleteTranslation.UseVisualStyleBackColor = true;
            BtnDeleteTranslation.Click += BtnDeleteTranslation_Click;
            // 
            // LstTranslations
            // 
            LstTranslations.FormattingEnabled = true;
            LstTranslations.ItemHeight = 15;
            LstTranslations.Location = new Point(3, 16);
            LstTranslations.Name = "LstTranslations";
            LstTranslations.Size = new Size(158, 199);
            LstTranslations.TabIndex = 11;
            // 
            // TbTranslation
            // 
            TbTranslation.Location = new Point(168, 45);
            TbTranslation.Name = "TbTranslation";
            TbTranslation.Size = new Size(150, 23);
            TbTranslation.TabIndex = 13;
            // 
            // CBKeys
            // 
            CBKeys.FormattingEnabled = true;
            CBKeys.Location = new Point(168, 16);
            CBKeys.Name = "CBKeys";
            CBKeys.Size = new Size(150, 23);
            CBKeys.TabIndex = 12;
            CBKeys.Text = "None";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(BtnResetTranslations);
            groupBox1.Controls.Add(CBTranslationToggle);
            groupBox1.Controls.Add(LstTranslations);
            groupBox1.Controls.Add(CBKeys);
            groupBox1.Controls.Add(TbTranslation);
            groupBox1.Controls.Add(BtnAddTranslation);
            groupBox1.Controls.Add(BtnEditTranslation);
            groupBox1.Controls.Add(BtnDeleteTranslation);
            groupBox1.Location = new Point(342, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(324, 221);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Translation Options";
            // 
            // BtnResetTranslations
            // 
            BtnResetTranslations.Location = new Point(167, 161);
            BtnResetTranslations.Name = "BtnResetTranslations";
            BtnResetTranslations.Size = new Size(151, 23);
            BtnResetTranslations.TabIndex = 17;
            BtnResetTranslations.Text = "Reset to Default";
            BtnResetTranslations.UseVisualStyleBackColor = true;
            BtnResetTranslations.Click += BtnResetTranslations_Click;
            // 
            // CBTranslationToggle
            // 
            CBTranslationToggle.AutoSize = true;
            CBTranslationToggle.Checked = true;
            CBTranslationToggle.CheckState = CheckState.Checked;
            CBTranslationToggle.Location = new Point(167, 190);
            CBTranslationToggle.Name = "CBTranslationToggle";
            CBTranslationToggle.Size = new Size(151, 19);
            CBTranslationToggle.TabIndex = 18;
            CBTranslationToggle.Text = "Use System Translations";
            CBTranslationToggle.UseVisualStyleBackColor = true;
            // 
            // BtnBackColorPicker
            // 
            BtnBackColorPicker.Location = new Point(4, 65);
            BtnBackColorPicker.Name = "BtnBackColorPicker";
            BtnBackColorPicker.Size = new Size(140, 23);
            BtnBackColorPicker.TabIndex = 20;
            BtnBackColorPicker.Text = "Select Back Color";
            BtnBackColorPicker.UseVisualStyleBackColor = true;
            BtnBackColorPicker.Click += BtnColorChange_Click;
            // 
            // BtnTextColorPicker
            // 
            BtnTextColorPicker.Location = new Point(4, 92);
            BtnTextColorPicker.Name = "BtnTextColorPicker";
            BtnTextColorPicker.Size = new Size(140, 23);
            BtnTextColorPicker.TabIndex = 21;
            BtnTextColorPicker.Text = "Select Text Color";
            BtnTextColorPicker.UseVisualStyleBackColor = true;
            BtnTextColorPicker.Click += BtnColorChange_Click;
            // 
            // CBOutputTypes
            // 
            CBOutputTypes.FormattingEnabled = true;
            CBOutputTypes.Location = new Point(4, 36);
            CBOutputTypes.Name = "CBOutputTypes";
            CBOutputTypes.Size = new Size(140, 23);
            CBOutputTypes.TabIndex = 19;
            // 
            // LblUserOutput
            // 
            LblUserOutput.AutoSize = true;
            LblUserOutput.Location = new Point(6, 18);
            LblUserOutput.Name = "LblUserOutput";
            LblUserOutput.Size = new Size(111, 15);
            LblUserOutput.TabIndex = 99;
            LblUserOutput.Text = "Key output Control:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(CBSpamProtect);
            groupBox2.Controls.Add(CBModPrim);
            groupBox2.Controls.Add(CBMouseOut);
            groupBox2.Controls.Add(CBSkipSetupView);
            groupBox2.Controls.Add(CBShiftToggle);
            groupBox2.Controls.Add(CBDeleteLogClose);
            groupBox2.Controls.Add(CBDeleteLogLaunch);
            groupBox2.Controls.Add(CBLogToggle);
            groupBox2.Controls.Add(BtnBackColorPicker);
            groupBox2.Controls.Add(LblUserOutput);
            groupBox2.Controls.Add(BtnTextColorPicker);
            groupBox2.Controls.Add(CBOutputTypes);
            groupBox2.Location = new Point(672, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(328, 173);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "General Settings";
            // 
            // CBSpamProtect
            // 
            CBSpamProtect.AutoSize = true;
            CBSpamProtect.Location = new Point(150, 150);
            CBSpamProtect.Name = "CBSpamProtect";
            CBSpamProtect.Size = new Size(152, 19);
            CBSpamProtect.TabIndex = 29;
            CBSpamProtect.Text = "Toggle Spam Protection";
            CBSpamProtect.UseVisualStyleBackColor = true;
            // 
            // CBModPrim
            // 
            CBModPrim.AutoSize = true;
            CBModPrim.Location = new Point(150, 132);
            CBModPrim.Name = "CBModPrim";
            CBModPrim.Size = new Size(170, 19);
            CBModPrim.TabIndex = 28;
            CBModPrim.Text = "Disable Modifier as Primary";
            CBModPrim.UseVisualStyleBackColor = true;
            // 
            // CBMouseOut
            // 
            CBMouseOut.AutoSize = true;
            CBMouseOut.Location = new Point(150, 114);
            CBMouseOut.Name = "CBMouseOut";
            CBMouseOut.Size = new Size(166, 19);
            CBMouseOut.TabIndex = 27;
            CBMouseOut.Text = "Toggle mouse click output";
            CBMouseOut.UseVisualStyleBackColor = true;
            // 
            // CBSkipSetupView
            // 
            CBSkipSetupView.AutoSize = true;
            CBSkipSetupView.Location = new Point(150, 24);
            CBSkipSetupView.Name = "CBSkipSetupView";
            CBSkipSetupView.Size = new Size(168, 19);
            CBSkipSetupView.TabIndex = 22;
            CBSkipSetupView.Text = "Skip Setup View on Launch";
            CBSkipSetupView.UseVisualStyleBackColor = true;
            // 
            // CBShiftToggle
            // 
            CBShiftToggle.AutoSize = true;
            CBShiftToggle.Location = new Point(150, 42);
            CBShiftToggle.Name = "CBShiftToggle";
            CBShiftToggle.Size = new Size(88, 19);
            CBShiftToggle.TabIndex = 23;
            CBShiftToggle.Text = "Shift Toggle";
            CBShiftToggle.UseVisualStyleBackColor = true;
            // 
            // CBDeleteLogClose
            // 
            CBDeleteLogClose.AutoSize = true;
            CBDeleteLogClose.Location = new Point(150, 96);
            CBDeleteLogClose.Name = "CBDeleteLogClose";
            CBDeleteLogClose.Size = new Size(145, 19);
            CBDeleteLogClose.TabIndex = 26;
            CBDeleteLogClose.Text = "Delete log file on close";
            CBDeleteLogClose.UseVisualStyleBackColor = true;
            // 
            // CBDeleteLogLaunch
            // 
            CBDeleteLogLaunch.AutoSize = true;
            CBDeleteLogLaunch.Location = new Point(150, 78);
            CBDeleteLogLaunch.Name = "CBDeleteLogLaunch";
            CBDeleteLogLaunch.Size = new Size(154, 19);
            CBDeleteLogLaunch.TabIndex = 25;
            CBDeleteLogLaunch.Text = "Delete log file on launch";
            CBDeleteLogLaunch.UseVisualStyleBackColor = true;
            // 
            // CBLogToggle
            // 
            CBLogToggle.AutoSize = true;
            CBLogToggle.Location = new Point(150, 60);
            CBLogToggle.Name = "CBLogToggle";
            CBLogToggle.Size = new Size(165, 19);
            CBLogToggle.TabIndex = 24;
            CBLogToggle.Text = "Log unpaused input to file";
            CBLogToggle.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(BtnAutoCatchName);
            groupBox3.Controls.Add(LstTracked);
            groupBox3.Controls.Add(BtnPauseKeybind);
            groupBox3.Controls.Add(BtnAddProgram);
            groupBox3.Controls.Add(BtnForceSave);
            groupBox3.Controls.Add(BtnRemove);
            groupBox3.Controls.Add(TbOutput);
            groupBox3.Controls.Add(TbTracked);
            groupBox3.Controls.Add(LblOut);
            groupBox3.Location = new Point(8, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(328, 234);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Programs to Allow input tracking";
            // 
            // BtnAutoCatchName
            // 
            BtnAutoCatchName.Location = new Point(172, 103);
            BtnAutoCatchName.Name = "BtnAutoCatchName";
            BtnAutoCatchName.Size = new Size(151, 23);
            BtnAutoCatchName.TabIndex = 7;
            BtnAutoCatchName.Text = "Auto Grab Window Name";
            BtnAutoCatchName.UseVisualStyleBackColor = true;
            BtnAutoCatchName.Click += BtnAutoCatchName_Click;
            // 
            // MainCustomize
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 253);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(BtnStart);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainCustomize";
            Text = "Setup View";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button BtnAddProgram;
        private ListBox LstTracked;
        private Button BtnPauseKeybind;
        private Button BtnStart;
        private TextBox TbOutput;
        private Label LblOut;
        private TextBox TbTracked;
        private Button BtnRemove;
        private Button BtnForceSave;
        private Button BtnAddTranslation;
        private Button BtnEditTranslation;
        private Button BtnDeleteTranslation;
        private ListBox LstTranslations;
        private ComboBox CBKeys;
        private TextBox TbTranslation;
        private GroupBox groupBox1;
        private Button BtnBackColorPicker;
        private Button BtnTextColorPicker;
        private ComboBox CBOutputTypes;
        private Label LblUserOutput;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private CheckBox CBDeleteLogClose;
        private CheckBox CBDeleteLogLaunch;
        private CheckBox CBLogToggle;
        private CheckBox CBShiftToggle;
        private CheckBox CBSkipSetupView;
        private CheckBox CBTranslationToggle;
        private Button BtnResetTranslations;
        private CheckBox CBModPrim;
        private CheckBox CBMouseOut;
        private Button BtnAutoCatchName;
        private CheckBox CBSpamProtect;
    }
}