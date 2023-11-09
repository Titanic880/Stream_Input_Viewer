////TODO
////Debug Stream Overlay
////Test With OBS

using Newtonsoft.Json;

namespace KeyStreamOverlay
{
    public partial class Form1 : Form
    {
        private UI_Mimic.UIReader? KeyboardHook;
        private PauseKeybind PauseBind;
        private const string DefaultSave = "Save.json";
        private string ImportedSave = "";
        public Form1()
        {
            InitializeComponent();

            this.MaximizeBox = false;

            KeyboardHook = new(false, new string[] { this.Text });
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;
            KeyboardHook.OnError += KeyboardHook_OnError;
            PauseBind = new(Keys.Insert, true, true, true);
            JSONLoad(DefaultSave);
        }
        ~Form1()
        {
            if (ImportedSave != "")
                JSONSave(ImportedSave);
            else
                JSONSave(DefaultSave);
        }
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0 || PauseBind == null)
            {
                MessageBox.Show("List is empty or a pause bind has not been set...");
                return;
            }
            KeyboardHook = null;
            this.Hide();
            SteamView? view = new(CBGlobal.Checked, GetAllowedWindows(), PauseBind);
            view.ShowDialog();
            view.Close();
            view.Dispose();
            view = null;
            this.Show();
            MessageBox.Show("Stream View Cleaned up, Restarting Testing Hook...");
            KeyboardHook = new(false, new string[] { this.Text });
            KeyboardHook.OnError += KeyboardHook_OnError;
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;
        }
        private string[] GetAllowedWindows()
        {
            List<string> AllowedPrograms = new();
            for (int i = 0; i < listBox1.Items.Count; i++)
                AllowedPrograms.Add(listBox1.Items[i].ToString()!);
            return AllowedPrograms.ToArray();
        }
        private void KeyboardHook_OnError(Exception e)
        {
            //TODO: Output in a non disruptive way
        }

        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            if (PauseBind != null)
                if (PauseBind.Equals(key, Shift, Ctrl, Alt))
                    MessageBox.Show("Pause Bind Pressed!");

            string sft = Shift ? "Shift+" : "";
            string ctrl = Ctrl ? "Ctrl+" : "";
            string alt = Alt ? "Alt+" : "";
            TbOutput.Text = sft + ctrl + alt + key;
        }

        private void BtnPause_Keybind_Click(object sender, EventArgs e)
        {
            string[] options = TbOutput.Text.Split('+');
            if (Enum.TryParse(options[^1], out Keys key))
            {
                PauseBind = new(key, options.Contains("Shift"), options.Contains("Ctrl"), options.Contains("Alt"));
                MessageBox.Show("Pause Bind Set!");
            }
            else MessageBox.Show("Failed to Get Keybind");
        }

        private void BtnAddProgram_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(TbTracked.Text);
            //CurrentSave.AddWindow(TbTracked.Text);
            JSONSave(this.ImportedSave is "" ? DefaultSave : ImportedSave);
            TbTracked.Text = "";
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void JSONSave(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                File.Create(FilePath).Close();

                File.WriteAllText(FilePath,
                JsonConvert.SerializeObject(new SaveData(FilePath, PauseBind, GetAllowedWindows(), CBGlobal.Checked), Formatting.Indented)
                );
            }
            else
            {
                MessageBox.Show("File Created");
            }
        }
        private void JSONLoad(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("File Not Found");
                if (!File.Exists(DefaultSave))
                    JSONSave(DefaultSave);
            }
            try
            {
                SaveData? SaveInfo = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(FilePath));
                if (SaveInfo != null)
                {
                    listBox1.Items.AddRange(SaveInfo.PreallowedWindows);
                    this.PauseBind = SaveInfo.PauseBind;
                    this.ImportedSave = SaveInfo.SaveLocation;
                    CBGlobal.Checked = SaveInfo.Global;
                }
                if (ImportedSave != DefaultSave)
                    JSONLoad(ImportedSave);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to grab saved Data: {ex.Message}");
            }
        }

        private void BtnForceSave_Click(object sender, EventArgs e)
        {
            JSONSave(ImportedSave is "" ? DefaultSave : ImportedSave);
        }
    }
}