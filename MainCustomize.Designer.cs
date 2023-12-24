namespace KeyStreamOverlay {
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
            LblList = new Label();
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
            BtnBackColorPicker = new Button();
            BtnTextColorPicker = new Button();
            CBOutputTypes = new ComboBox();
            LblUserOutput = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // BtnAddProgram
            // 
            BtnAddProgram.Location = new Point(180, 55);
            BtnAddProgram.Name = "BtnAddProgram";
            BtnAddProgram.Size = new Size(151, 23);
            BtnAddProgram.TabIndex = 0;
            BtnAddProgram.Text = "Add Tracked Program";
            BtnAddProgram.UseVisualStyleBackColor = true;
            BtnAddProgram.Click += BtnAddProgram_Click;
            // 
            // LstTracked
            // 
            LstTracked.FormattingEnabled = true;
            LstTracked.ItemHeight = 15;
            LstTracked.Location = new Point(12, 26);
            LstTracked.Name = "LstTracked";
            LstTracked.Size = new Size(158, 289);
            LstTracked.TabIndex = 1;
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
            BtnPauseKeybind.Location = new Point(180, 186);
            BtnPauseKeybind.Name = "BtnPauseKeybind";
            BtnPauseKeybind.Size = new Size(151, 23);
            BtnPauseKeybind.TabIndex = 3;
            BtnPauseKeybind.Text = "Set Pause Keybind";
            BtnPauseKeybind.UseVisualStyleBackColor = true;
            BtnPauseKeybind.Click += BtnPause_Keybind_Click;
            // 
            // BtnStart
            // 
            BtnStart.Location = new Point(168, 162);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(151, 53);
            BtnStart.TabIndex = 5;
            BtnStart.Text = "Start StreamView";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // TbOutput
            // 
            TbOutput.Enabled = false;
            TbOutput.Location = new Point(180, 157);
            TbOutput.Name = "TbOutput";
            TbOutput.Size = new Size(151, 23);
            TbOutput.TabIndex = 7;
            // 
            // LblOut
            // 
            LblOut.AutoSize = true;
            LblOut.Location = new Point(176, 139);
            LblOut.Name = "LblOut";
            LblOut.Size = new Size(160, 15);
            LblOut.TabIndex = 8;
            LblOut.Text = "Keyboard Input (Pause Bind):";
            // 
            // TbTracked
            // 
            TbTracked.Location = new Point(180, 26);
            TbTracked.Name = "TbTracked";
            TbTracked.Size = new Size(151, 23);
            TbTracked.TabIndex = 9;
            TbTracked.Text = "Enter Program Name Here";
            // 
            // BtnRemove
            // 
            BtnRemove.Location = new Point(180, 84);
            BtnRemove.Name = "BtnRemove";
            BtnRemove.Size = new Size(151, 23);
            BtnRemove.TabIndex = 10;
            BtnRemove.Text = "Remove Selected";
            BtnRemove.UseVisualStyleBackColor = true;
            BtnRemove.Click += BtnRemove_Click;
            // 
            // BtnForceSave
            // 
            BtnForceSave.Location = new Point(180, 113);
            BtnForceSave.Name = "BtnForceSave";
            BtnForceSave.Size = new Size(151, 23);
            BtnForceSave.TabIndex = 11;
            BtnForceSave.Text = "Save Settings";
            BtnForceSave.UseVisualStyleBackColor = true;
            BtnForceSave.Click += BtnForceSave_Click;
            // 
            // BtnAddTranslation
            // 
            BtnAddTranslation.Location = new Point(168, 74);
            BtnAddTranslation.Name = "BtnAddTranslation";
            BtnAddTranslation.Size = new Size(151, 23);
            BtnAddTranslation.TabIndex = 13;
            BtnAddTranslation.Text = "Add Translation";
            BtnAddTranslation.UseVisualStyleBackColor = true;
            BtnAddTranslation.Click += BtnAddTranslation_Click;
            // 
            // BtnEditTranslation
            // 
            BtnEditTranslation.Location = new Point(168, 103);
            BtnEditTranslation.Name = "BtnEditTranslation";
            BtnEditTranslation.Size = new Size(151, 23);
            BtnEditTranslation.TabIndex = 14;
            BtnEditTranslation.Text = "Edit Translation";
            BtnEditTranslation.UseVisualStyleBackColor = true;
            BtnEditTranslation.Click += BtnEditTranslation_Click;
            // 
            // BtnDeleteTranslation
            // 
            BtnDeleteTranslation.Location = new Point(168, 132);
            BtnDeleteTranslation.Name = "BtnDeleteTranslation";
            BtnDeleteTranslation.Size = new Size(151, 23);
            BtnDeleteTranslation.TabIndex = 15;
            BtnDeleteTranslation.Text = "Delete Translation";
            BtnDeleteTranslation.UseVisualStyleBackColor = true;
            BtnDeleteTranslation.Click += BtnDeleteTranslation_Click;
            // 
            // LstTranslations
            // 
            LstTranslations.FormattingEnabled = true;
            LstTranslations.ItemHeight = 15;
            LstTranslations.Location = new Point(4, 16);
            LstTranslations.Name = "LstTranslations";
            LstTranslations.Size = new Size(158, 199);
            LstTranslations.TabIndex = 16;
            // 
            // TbTranslation
            // 
            TbTranslation.Location = new Point(168, 45);
            TbTranslation.Name = "TbTranslation";
            TbTranslation.Size = new Size(151, 23);
            TbTranslation.TabIndex = 18;
            // 
            // CBKeys
            // 
            CBKeys.FormattingEnabled = true;
            CBKeys.Location = new Point(168, 16);
            CBKeys.Name = "CBKeys";
            CBKeys.Size = new Size(151, 23);
            CBKeys.TabIndex = 19;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(LstTranslations);
            groupBox1.Controls.Add(CBKeys);
            groupBox1.Controls.Add(BtnStart);
            groupBox1.Controls.Add(TbTranslation);
            groupBox1.Controls.Add(BtnAddTranslation);
            groupBox1.Controls.Add(BtnEditTranslation);
            groupBox1.Controls.Add(BtnDeleteTranslation);
            groupBox1.Location = new Point(12, 323);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(324, 221);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            groupBox1.Text = "Translation Options";
            // 
            // BtnBackColorPicker
            // 
            BtnBackColorPicker.Location = new Point(180, 265);
            BtnBackColorPicker.Name = "BtnBackColorPicker";
            BtnBackColorPicker.Size = new Size(151, 23);
            BtnBackColorPicker.TabIndex = 20;
            BtnBackColorPicker.Text = "Select Back Color";
            BtnBackColorPicker.UseVisualStyleBackColor = true;
            BtnBackColorPicker.Click += BtnColorChange_Click;
            // 
            // BtnTextColorPicker
            // 
            BtnTextColorPicker.Location = new Point(180, 294);
            BtnTextColorPicker.Name = "BtnTextColorPicker";
            BtnTextColorPicker.Size = new Size(151, 23);
            BtnTextColorPicker.TabIndex = 23;
            BtnTextColorPicker.Text = "Select Text Color";
            BtnTextColorPicker.UseVisualStyleBackColor = true;
            BtnTextColorPicker.Click += BtnColorChange_Click;
            // 
            // CBOutputTypes
            // 
            CBOutputTypes.FormattingEnabled = true;
            CBOutputTypes.Location = new Point(180, 236);
            CBOutputTypes.Name = "CBOutputTypes";
            CBOutputTypes.Size = new Size(151, 23);
            CBOutputTypes.TabIndex = 20;
            // 
            // LblUserOutput
            // 
            LblUserOutput.AutoSize = true;
            LblUserOutput.Location = new Point(180, 218);
            LblUserOutput.Name = "LblUserOutput";
            LblUserOutput.Size = new Size(111, 15);
            LblUserOutput.TabIndex = 24;
            LblUserOutput.Text = "Key output Control:";
            // 
            // MainCustomize
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(349, 558);
            Controls.Add(LblUserOutput);
            Controls.Add(CBOutputTypes);
            Controls.Add(BtnTextColorPicker);
            Controls.Add(BtnBackColorPicker);
            Controls.Add(groupBox1);
            Controls.Add(BtnForceSave);
            Controls.Add(BtnRemove);
            Controls.Add(TbTracked);
            Controls.Add(LblOut);
            Controls.Add(TbOutput);
            Controls.Add(BtnPauseKeybind);
            Controls.Add(LblList);
            Controls.Add(LstTracked);
            Controls.Add(BtnAddProgram);
            Name = "MainCustomize";
            Text = "Setup View";
            FormClosing += Form1_FormClosing;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnAddProgram;
        private ListBox LstTracked;
        private Label LblList;
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
    }
}