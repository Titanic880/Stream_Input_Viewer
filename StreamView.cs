using Timer = System.Timers.Timer;
using UI_Mimic;

namespace KeyStreamOverlay {
    public partial class StreamView : Form {

        private readonly InputReader? UIHook;
        private readonly Control UserInteractionControl;
        private readonly Timer TextClearTimer;
        private readonly KeyCombo PauseButtons;
        
        private const int ListMax = 13;
        private readonly int TextboxMaxChar = 15;
        private readonly bool UseTranslations = false;
        private readonly bool ShiftToggle = false;
        private readonly bool Keylogging = false;
        private readonly bool MouseClickToggle = false;
        private readonly bool ModifierAsPrimary = false;
        private readonly bool DuplicateSpamProtect = false;

        private bool Paused = false;

        public StreamView(SaveData Save) {
            InitializeComponent();
            
            //Set form toggles
            TopMost = true;
            MinimizeBox = false;
            MaximizeBox = false;
            AcceptButton = BtnPause;
            LblCropVals.SendToBack();

            //Set Locals
            PauseButtons = Save.PauseBind;
            UseTranslations = Save.UseTranslations;
            ShiftToggle = Save.ShiftToggle;
            Keylogging = Save.LoggingHookEnabled;
            TextboxMaxChar = Save.CharacterLineLimit;
            MouseClickToggle = Save.MouseClickToggle;
            ModifierAsPrimary = !Save.ModifierAsPrimary;
            DuplicateSpamProtect = Save.DuplicateSpamProtect;

            //Generate and Hook into Keystream & Mouse stream
            UIHook = InputReader.ReaderFactory(true, Save.PreAllowedWindows);
            UIHook.GenerateHook(HookTypePub.Keyboard);
            UIHook.OnError += KeyboardHook_OnError;
            UIHook.KeyDown += KeyboardHook_KeyDown;
            if (MouseClickToggle) {
                UIHook.GenerateHook(HookTypePub.Mouse);
                UIHook.OnMouseDown += MouseHookOnOnMouseClick;
            }

            //Setup Timer (Run at end of construction)
            TextClearTimer = new Timer(4000);
            TextClearTimer.Elapsed += ActiveTimer_Elapsed;

            //Build the User specified output control
            UserInteractionControl = GenerateUIControl(Save.OutputControl, Save.GetBackColor, Save.GetTextColor);
            if (UserInteractionControl == new Control()) {
                MessageBox.Show("Failed to generate User output Control, Exiting...");
                Close();
                return;
            }
            Controls.Add(UserInteractionControl);
            
            //Change the size of the form according to output type (Sizing is magic numbers)
            if (Save.OutputControl == StreamOutputType.Textbox) {
                Height = BtnPause.Height + 105;
                LblCropVals.Top = UserInteractionControl.Bottom;
                BtnPause.Top = LblCropVals.Bottom-3;
            }

            //Crop Sizing HERE
            int[] values = GetCropValues();
            LblCropVals.Text += $" {values[0]}:{values[1]}:{values[2]}:{values[3]}";

            InfoLogging.LoggingInit(Save.LoggingHookEnabled);
            TextClearTimer.Start();
        }
        private static Control GenerateUIControl(StreamOutputType ObjType, Color DisplayBackColor, Color TextColor) {
            Control toadd;
            switch (ObjType) {
                case StreamOutputType.Textbox:
                    toadd = new TextBox() {
                        BackColor = DisplayBackColor,
                        ForeColor = TextColor,
                        Font      = new Font("Nirmala UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                        Location  = new Point(12, 12),
                        Name      = "TbOutput",
                        Size      = new Size(176, 23),
                        TabIndex  = 0,
                        Enabled   = false
                    };
                    break;
                case StreamOutputType.Listbox:
                    toadd = new ListBox() {
                        BackColor         = DisplayBackColor,
                        ForeColor         = TextColor,
                        Font              = new Font("Nirmala UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                        FormattingEnabled = true,
                        ItemHeight        = 21,
                        Location          = new Point(12, 12),
                        Name              = "listBox1",
                        Size              = new Size(176, 277),
                        TabIndex          = 0,
                        SelectedIndex     = -1
                    };
                    //Fill list with blank strings for easy String manipulation
                    for (int i = 0; i < ListMax; i++) {
                        ((ListBox)toadd).Items.Add("");
                    }
                    break;
            default:
                toadd = new Control();
                break;
            }

            return toadd;
        }

        public new void Dispose() {
            InfoLogging.LoggingInit(false);
            UIHook?.Dispose();
            TextClearTimer!.Stop();
            TextClearTimer!.Elapsed -= ActiveTimer_Elapsed;
            TextClearTimer!.Dispose();
            base.Dispose();
        }

        private void BtnPause_Click(object sender, EventArgs e) {
            if (BtnPause.Text == "Pause") {
                BtnPause.Text = "Resume";
                Paused        = true;
                InfoLogging.PauseLoggingHook();
            }
            else if (BtnPause.Text == "Resume") {
                BtnPause.Text = "Pause";
                Paused        = false;
                InfoLogging.ResumeLoggingHook();
            }
            else
                MessageBox.Show("Error: Pause Button");
        }

        KeyCombo Previous_Key = new(Keys.F24, true, true, true, true);
        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt, bool Home) {
            if (PauseButtons.Equals(key, Shift, Ctrl, Alt, Home)) {
                Paused        = !Paused;
                BtnPause.Text = Paused ? "Resume" : "Pause";
                AddToUI(Paused ? "Output Paused" : "Output Un-Paused");
                InfoLogging.PauseLoggingHookToggle(Paused);
                return;
            }
            //TODO: Test Directional Spam Toggle - Not Working...?

            if (Paused) {
                return;
            }

            if (ModifierAsPrimary is false) {
                if (MainCustomize.KeyControl.Contains(key)) {   //Find a new home for this value set (no dupes)
                    return;
                }
            }
            string sft  = Shift ? TranslationDict.GetTranslation(Keys.Shift)  + "+" : "";
            string ctrl = Ctrl ? TranslationDict.GetTranslation(Keys.Control) + "+" : "";
            string alt  = Alt ? TranslationDict.GetTranslation(Keys.Alt)      + "+" : "";

            string strkey = "";
            if((Shift || Ctrl || Alt) && ShiftToggle is false) {
                strkey = " ";
            }
            //Handles holding a key down/spamming it
            if (Previous_Key.Equals(key, Shift, Ctrl, Alt, Home)) {
                if (DuplicateSpamProtect is false) {
                    ShiftTranslationLogic(AddToUI_Duplicate);
                }
                return;
            }

            Previous_Key = new KeyCombo(key, Shift, Ctrl, Alt, Home);

            ShiftTranslationLogic(AddToUI);

            void ShiftTranslationLogic(Action<string> FuncCall) {

                if (ShiftToggle) {
                    if (Shift && UseTranslations) {
                         strkey += TranslationDict.GetShiftTranslation(key);
                    }
                    else if (UseTranslations) {
                        strkey += TranslationDict.GetTranslation(key).ToLower();
                    }
                    else {
                        strkey += key.ToString().ToLower();
                    }

                    FuncCall(ctrl + alt + strkey);
                }
                else {
                    if (UseTranslations) {
                        strkey += TranslationDict.GetTranslation(key);
                    }
                    else {
                        strkey += key.ToString().ToLower();
                    }

                    FuncCall(sft + ctrl + alt + strkey);
                }

                if (Keylogging) {
                    InfoLogging.LogToFile(strkey);
                }
            }
        }
        private void MouseHookOnOnMouseClick(MouseButtons mouseaction) {
            if (Paused || MouseClickToggle is false) {
                return;
            }

            string output = mouseaction switch {
                MouseButtons.Left     => "M1",
                MouseButtons.Right    => "M2",
                MouseButtons.Middle   => "M3",
                MouseButtons.XButton1 => "MB1",
                MouseButtons.XButton2 => "MB2",
                _                     => ""
            };

            if (output == "") {
                return;
            }

            if (Previous_Key.Equals(new KeyCombo(mouseaction))) {
                if (DuplicateSpamProtect is false) {
                    AddToUI_Duplicate(output);
                }
                return;
            }

            Previous_Key = new KeyCombo(mouseaction);
            AddToUI(output);
            if (Keylogging) {
                InfoLogging.LogToFile(output);
            }
        }
        private void KeyboardHook_OnError(Exception e) {
            AddToUI($"Error Occurred in DLL");
            InfoLogging.LogAsError($"Error in KeyboardHook: {e.Message}");
        }

        private void AddToUI(string input) {
            if (input == "") {
                return;
            }

            switch (UserInteractionControl) {
                case ListBox listBox1: {
                    for (int i = ListMax - 1; i > 0; i--) {
                        listBox1.Items[i] = listBox1.Items[i - 1];
                    }

                    listBox1.Items[0] = input;
                    break;
                }
                case TextBox TbUI: {
                    TbUI.Text += input;
                    if (TbUI.Text.Length >= TextboxMaxChar) {
                        int toRemove = TbUI.Text.Length - TextboxMaxChar;
                        TbUI.Text = TbUI.Text.Remove(0, toRemove == 0 ? 1 : toRemove);
                    }

                    break;
                }
            }
        }
        private void AddToUI_Duplicate(string input) {
            if (input == "") {
                return;
            }

            switch (UserInteractionControl) {
                case ListBox listBox1:
                    listBox1.Items[0] += input;
                    return;
                case TextBox TbUI: {
                    TbUI.Text += input;
                    if (TbUI.Text.Length > TextboxMaxChar) {
                        TbUI.Text = TbUI.Text.Remove(0, TbUI.Text.Length - TextboxMaxChar);
                    }

                    return;
                }
            }
        }

        private void ActiveTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) {
            if (!this.IsDisposed) {
                Invoke(TimerTick_Action);
            }
        }
        private void TimerTick_Action() {
            foreach (Control a in Controls) {
                switch (a) {
                    case ListBox listBox1: {
                        listBox1.SuspendLayout();
                        listBox1.SelectedIndex = -1; //Clears user selection to have better visual clarity.
                        for (int i = 0; i < ListMax; i++) {
                            if (listBox1.Items[i].ToString() != "") {
                                continue;
                            }

                            if (i == 0) {
                                listBox1.Items[0] = "";
                            }
                            else {
                                listBox1.Items[i - 1] = "";
                            }

                            break;
                        }

                        listBox1.Items[^1] = "";
                        listBox1.ResumeLayout();
                        return;
                    }
                    case TextBox TbUI when TbUI.Text != "":
                        TbUI.Text = TbUI.Text.Remove(0, 1);
                        return;
                }
            }

        }
    
        private int[] GetCropValues() {
            //Left:Top:Right:Bottom Respectively
            int[] vals = new int[4];
            //Add 2px for control inbuilt white border
            vals[0] = UserInteractionControl.Left + 2;
            vals[1] = UserInteractionControl.Top + 2;
            vals[2] = vals[0]; //Should always be the same due to control being centered
            vals[3] = LblCropVals.Height + BtnPause.Height + 6; //Magic Number is Spacing from Construction
            return vals;
        }
    }
}
