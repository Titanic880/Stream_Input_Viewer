namespace KeyStreamOverlay {
    public partial class SteamView_List : Form {
        private bool Paused = false;
        private readonly bool UseTranslations = false;
        private readonly UI_Mimic.UIReader? KeyboardHook;
        private readonly string[] AllowedPrograms;
        private readonly System.Timers.Timer TextClearTimer;
        private const int ListMax = 13;
        private readonly PauseKeybind PauseButtons;
        public SteamView_List(bool Global, bool UseInputTranslations, string[] AllowedWindows, PauseKeybind PauseBind, Color BackColor) {
            InitializeComponent();

            this.TopMost = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.AcceptButton = BtnPause;

            AllowedPrograms = AllowedWindows;
            PauseButtons = PauseBind;
            this.UseTranslations = UseInputTranslations;
            listBox1.BackColor = BackColor;

            KeyboardHook = new UI_Mimic.UIReader(Global, AllowedPrograms);
            KeyboardHook.OnError += KeyboardHook_OnError;
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;

            TextClearTimer = new(4000);
            TextClearTimer.Elapsed += ActiveTimer_Elapsed;

            //Fill list with blank strings for easy String manipulation
            for (int i = 0; i < ListMax; i++) {
                listBox1.Items.Add("");
            }
            TextClearTimer.Start();
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
            } else if (BtnPause.Text == "Resume") {
                BtnPause.Text = "Pause";
                Paused = false;
            } else
                MessageBox.Show("Error: Pause Button");
        }

        PauseKeybind Previous_Key = new(Keys.F24,true,true,true);
        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt) {
            listBox1.SelectedIndex = -1;
            if (PauseButtons.Equals(key, Shift, Ctrl, Alt)) {
                Paused = !Paused;
                BtnPause.Text = Paused ? "Resume" : "Pause";
                AddToUI(Paused ? "Output Paused" : "Output Un-Paused");
                return;
            }
            //Handles holding a key down/spamming it
            if (Previous_Key.Equals(key, Shift, Ctrl, Alt)) {
                return;
            }
            Previous_Key = new(key, Shift, Ctrl, Alt);

            string sft = Shift ? "Shift+" : "";
            string ctrl = Ctrl ? "Ctrl+" : "";
            string alt = Alt ? "Alt+" : "";
            if (UseTranslations)
                AddToUI(sft + ctrl + alt + TranslationDict.GetTranslation(key));
            else
                AddToUI(sft + ctrl + alt + key);
        }

        private void KeyboardHook_OnError(Exception e) {
            AddToUI($"Error Occoured in DLL: {e.Message}");
        }

        private void AddToUI(string input) {
            if (input == "")
                return;

            listBox1.SuspendLayout();
            for (int i = ListMax - 1; i > 0; i--)
                listBox1.Items[i] = listBox1.Items[i - 1];
            listBox1.Items[0] = input;
            listBox1.ResumeLayout();
        }

        private void ActiveTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e) 
            => Invoke(TimerTick_Action);
        private void TimerTick_Action() {
            listBox1.SuspendLayout();
            //
            for (int i = 0; i < ListMax; i++) {
                if (listBox1.Items[i].ToString() == "") {
                    if (i == 0)
                        listBox1.Items[0] = "";
                    else
                        listBox1.Items[i - 1] = "";
                    break;
                }
            }
            listBox1.Invoke(() => listBox1.Items[^1] = "");
            listBox1.ResumeLayout();
        }
    }
}
