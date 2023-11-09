namespace KeyStreamOverlay
{
    public partial class SteamView : Form
    {
        private bool Paused = false;
        private readonly UI_Mimic.UIReader? KeyboardHook;
        private readonly string[] AllowedPrograms;
        private readonly System.Timers.Timer ActiveTimer;
        private const int ListMax = 13;
        private readonly PauseKeybind PauseButtons;
        public SteamView(bool Global, string[] AllowedWindows, PauseKeybind PauseBind)
        {
            InitializeComponent();

            this.TopMost = true;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.AcceptButton = BtnPause;

            AllowedPrograms = AllowedWindows;
            PauseButtons = PauseBind;

            KeyboardHook = new UI_Mimic.UIReader(Global, AllowedPrograms);
            KeyboardHook.OnError += KeyboardHook_OnError;
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;

            ActiveTimer = new(4000);
            ActiveTimer.Elapsed += ActiveTimer_Elapsed;

            //Fill list with blank strings for easy String manipulation
            for (int i = 0; i < ListMax; i++)
                listBox1.Items.Add("");
            ActiveTimer.Start();
        }
        ~SteamView()
        {
            KeyboardHook?.Dispose();
            ActiveTimer?.Stop();
            ActiveTimer?.Dispose();
        }
        public new void Dispose()
        {
            KeyboardHook?.Dispose();
            ActiveTimer?.Stop();
            ActiveTimer?.Dispose();
            base.Dispose();
        }
        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (BtnPause.Text == "Pause")
            {
                BtnPause.Text = "Resume";
                Paused = true;
            }
            else if (BtnPause.Text == "Resume")
            {
                BtnPause.Text = "Pause";
                Paused = false;
            }
            else MessageBox.Show("Error: Pause Button");
        }

        PauseKeybind Previous_Key = new(Keys.F24,true,true,true);
        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            if (PauseButtons.Equals(key, Shift, Ctrl, Alt))
            {
                Paused = !Paused;
                BtnPause.Text = Paused ? "Resume" : "Pause";
                Action(Paused ? "Output Paused" : "Output Un-Paused");
                return;
            }

            //Handles holding a key down/spamming it
            if (Previous_Key.Equals(key,Shift,Ctrl,Alt))
                return;
            Previous_Key = new(key, Shift, Ctrl, Alt);

            string sft = Shift ? "Shift+" : "";
            string ctrl = Ctrl ? "Ctrl+" : "";
            string alt = Alt ? "Alt+" : "";
            Action(sft + ctrl + alt + key);
        }

        private void KeyboardHook_OnError(Exception e)
        {
            Action("Error Occoured in DLL");
        }

        private void Action(string input)
        {
            listBox1.SuspendLayout();
            for (int i = ListMax-1; i > 0; i--)
                listBox1.Items[i] = listBox1.Items[i - 1];
            listBox1.Items[0] = input;
            listBox1.ResumeLayout();
        }
        private void TimerTick_Action()
        {
            listBox1.SuspendLayout();
            //
            for (int i = 0; i < ListMax; i++)
            {
                if (listBox1.Items[i].ToString() == "")
                {
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

        private void ActiveTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(TimerTick_Action);
            /*
            for (int i = 0; i < ListMax-1; i++)
            {
                listBox1.Invoke(() => listBox1.Items[i] = listBox1.Items[i + 1]);
            }*/
        }
    }
}
