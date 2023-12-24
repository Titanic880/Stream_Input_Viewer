namespace KeyStreamOverlay {
    public partial class SteamView_List : Form {

        private bool Paused = false;
        private readonly bool UseTranslations = false;
        private readonly UI_Mimic.UIReader? KeyboardHook;
        private readonly string[] AllowedPrograms;
        private readonly System.Timers.Timer TextClearTimer;
        private const int ListMax = 13;
        private const int TextboxMaxChar = 15;
        private readonly KeyCombo PauseButtons;
        private readonly Color DisplayBackColor;
        private readonly Color TextColor;

        public SteamView_List(StreamOutputType OutputType, bool UseInputTranslations, string[] AllowedWindows, KeyCombo PauseBind, Color BackColor, Color TextColor) {
            InitializeComponent();

            this.TopMost = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.AcceptButton = BtnPause;

            AllowedPrograms = AllowedWindows;
            PauseButtons = PauseBind;
            this.UseTranslations = UseInputTranslations;
            DisplayBackColor = BackColor;
            this.TextColor = TextColor;


            KeyboardHook = new UI_Mimic.UIReader(true, AllowedPrograms);
            KeyboardHook.OnError += KeyboardHook_OnError;
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;

            TextClearTimer = new(4000);
            TextClearTimer.Elapsed += ActiveTimer_Elapsed;

            GenerateUIControl(OutputType);
            TextClearTimer.Start();
        }

        private void GenerateUIControl(StreamOutputType ObjType) {
            Control toadd;
            switch (ObjType) {
            case StreamOutputType.Textbox:
                toadd = new TextBox() {
                    BackColor = DisplayBackColor,
                    ForeColor = TextColor,
                    Font = new Font("Nirmala UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                    Location = new Point(12, 12),
                    Name = "TbOutput",
                    Size = new Size(176, 23),
                    TabIndex = 0,
                    Enabled = false
                };

                BtnPause.Top = toadd.Bottom + 10;
                this.Height = BtnPause.Top + BtnPause.Height + 45;
                break;
            case StreamOutputType.Listbox:
                toadd = new ListBox() {
                    BackColor = DisplayBackColor,
                    ForeColor = TextColor,
                    Font = new Font("Nirmala UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                    FormattingEnabled = true,
                    ItemHeight = 21,
                    Location = new Point(12, 12),
                    Name = "listBox1",
                    Size = new Size(176, 277),
                    TabIndex = 0,
                    SelectedIndex = -1
                };
                //Fill list with blank strings for easy String manipulation
                for (int i = 0; i < ListMax; i++) {
                    ((ListBox)toadd).Items.Add("");
                }
                break;
            default:
                return;
            }
            Controls.Add(toadd);
        }
        ~SteamView_List() {
            KeyboardHook?.Dispose();
            TextClearTimer?.Stop();
            TextClearTimer?.Dispose();
        }
        public new void Dispose() {
            KeyboardHook?.Dispose();
            TextClearTimer?.Stop();
            TextClearTimer?.Dispose();
            base.Dispose();
        }
        private void BtnPause_Click(object sender, EventArgs e) {
            if (BtnPause.Text == "Pause") {
                BtnPause.Text = "Resume";
                Paused = true;
                LoggingHook.PauseLoggingHook();
            } else if (BtnPause.Text == "Resume") {
                BtnPause.Text = "Pause";
                Paused = false;
                LoggingHook.ResumeLoggingHook();
            } else
                MessageBox.Show("Error: Pause Button");
        }

        KeyCombo Previous_Key = new(Keys.F24,true,true,true);
        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt) {
            //listBox1.SelectedIndex = -1;
            if (PauseButtons.Equals(key, Shift, Ctrl, Alt)) {
                Paused = !Paused;
                BtnPause.Text = Paused ? "Resume" : "Pause";
                AddToUI(Paused ? "Output Paused" : "Output Un-Paused");
                return;
            }

            string sft = Shift ? "Shift+" : "";
            string ctrl = Ctrl ? "Ctrl+" : "";
            string alt = Alt ? "Alt+" : "";
            //Handles holding a key down/spamming it
            if (Previous_Key.Equals(key, Shift, Ctrl, Alt)) {
                AddToUI_Duplicate(sft + ctrl + alt + (UseTranslations ? TranslationDict.GetTranslation(key) : key));
                return;
            }
            Previous_Key = new(key, Shift, Ctrl, Alt);
            AddToUI(sft + ctrl + alt + (UseTranslations ? TranslationDict.GetTranslation(key) : key));
        }

        private void KeyboardHook_OnError(Exception e) {
            AddToUI($"Error Occoured in DLL");
            LoggingHook.LogAsError($"Error in KeyboardHook: {e.Message}");
        }

        private void AddToUI(string input) {
            if (input == "")
                return;
            foreach (Control a in Controls) {
                if (a is ListBox listBox1) {
                    listBox1.SuspendLayout();
                    for (int i = ListMax - 1; i > 0; i--) {
                        listBox1.Items[i] = listBox1.Items[i - 1];
                    }
                    listBox1.Items[0] = input;
                    listBox1.ResumeLayout();
                } else if (a is TextBox TbUI) {
                    TbUI.Text += input;
                    if (TbUI.Text.Length > TextboxMaxChar) {
                        TbUI.Text = TbUI.Text.Remove(0, TbUI.Text.Length - TextboxMaxChar);
                    }
                }
            }
        }

        private void AddToUI_Duplicate(string input) {
            if (input == "")
                return;
            foreach (Control a in Controls) {
                if (a is ListBox listBox1) {
                    listBox1.Items[0] += input;
                    return;
                } else if (a is TextBox TbUI) {
                    TbUI.Text += input;
                    if (TbUI.Text.Length > TextboxMaxChar) {
                        TbUI.Text = TbUI.Text.Remove(0, TbUI.Text.Length - TextboxMaxChar);
                    }
                    return;
                }
            }
        }

        private void ActiveTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
            => Invoke(TimerTick_Action);
        private void TimerTick_Action() {
            foreach(Control a in Controls) {
                if(a is ListBox listBox1) {
                    listBox1.SuspendLayout();
                    listBox1.SelectedIndex = -1;    //Astethic.
                    //
                    for (int i = 0; i < ListMax; i++) {
                        if (listBox1.Items[i].ToString() == "") {
                            if (i == 0) {
                                listBox1.Items[0] = "";
                            } else {
                                listBox1.Items[i - 1] = "";
                            }
                            break;
                        }
                    }
                    listBox1.Invoke(() => listBox1.Items[^1] = "");
                    listBox1.ResumeLayout();
                    return;
                }
                else if (a is TextBox TbUI) {
                    if (TbUI.Text != "") {
                        TbUI.Text = TbUI.Text.Remove(0, 1);
                    }
                    return;
                }
            }

        }
    }
}
