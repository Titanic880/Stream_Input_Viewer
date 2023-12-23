using Newtonsoft.Json;

namespace KeyStreamOverlay {
    public partial class MainCustomize : Form {
        private UI_Mimic.UIReader? KeyboardHook;
        private PauseKeybind PauseBind;
        private const string DefaultSave = "Save.json";
        private string ImportedSave = "";
        public readonly static string DefaultFolder = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\{Application.ProductName}\\";
        private string SaveLocation { get => DefaultFolder + (ImportedSave == "" ? DefaultSave : ImportedSave); }
        public MainCustomize() {
            InitializeComponent();

            this.MaximizeBox = false;
            LoadTranslations();

            KeyboardHook = new(false, new string[] { this.Text });
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;
            KeyboardHook.OnError += KeyboardHook_OnError;
            PauseBind = new(Keys.Insert, true, true, true);
            JSONLoad(SaveLocation);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            JSONSave(SaveLocation);
        }
        private void LoadTranslations() {
            LstTranslations.Items.Clear();
            foreach (KeyValuePair<Keys, string> pair in TranslationDict.Translations)
                LstTranslations.Items.Add(pair.Key + " => " + pair.Value);
            CBKeys.Items.AddRange(Enum.GetNames(typeof(Keys)));
        }

        private void BtnStart_Click(object sender, EventArgs e) {
            if (LstTracked.Items.Count == 0 || PauseBind == null) {
                MessageBox.Show("List is empty or a pause bind has not been set...");
                return;
            }
            KeyboardHook = null;
            this.Hide();
            SteamView_List? view = new(CBGlobal.Checked, true, GetAllowedWindows(), PauseBind, BtnColorChange.BackColor);
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
        private string[] GetAllowedWindows() {
            List<string> AllowedPrograms = new();
            for (int i = 0; i < LstTracked.Items.Count; i++)
                AllowedPrograms.Add(LstTracked.Items[i].ToString()!);
            return AllowedPrograms.ToArray();
        }
        private void KeyboardHook_OnError(Exception e) {
            LoggingHook.LogAsError($"KeyHook Error: {e.Message}");
        }

        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt) {
            if (PauseBind != null)
                if (PauseBind.Equals(key, Shift, Ctrl, Alt))
                    MessageBox.Show("Pause Bind Pressed!");

            string sft = Shift ? "Shift+" : "";
            string ctrl = Ctrl ? "Ctrl+" : "";
            string alt = Alt ? "Alt+" : "";
            TbOutput.Text = sft + ctrl + alt + key;
        }

        private void BtnPause_Keybind_Click(object sender, EventArgs e) {
            string[] options = TbOutput.Text.Split('+');
            if (Enum.TryParse(options[^1], out Keys key)) {
                PauseBind = new(key, options.Contains("Shift"), options.Contains("Ctrl"), options.Contains("Alt"));
                MessageBox.Show("Pause Bind Set!");
            } else
                MessageBox.Show("Failed to Get Keybind");
        }

        private void BtnAddProgram_Click(object sender, EventArgs e) {
            if(TbTracked.Text == "Enter Program Name Here") {
                return;
            }

            LstTracked.Items.Add(TbTracked.Text);
            JSONSave(SaveLocation);
            TbTracked.Text = "";
        }

        private void BtnRemove_Click(object sender, EventArgs e) {
            if (LstTracked.SelectedIndex == -1) {
                return;
            }
            LstTracked.Items.RemoveAt(LstTracked.SelectedIndex);
        }

        private void JSONSave(string FilePath) {
            if (!Directory.Exists(DefaultFolder)) {
                Directory.CreateDirectory(DefaultFolder);
            }
            File.WriteAllText(FilePath,
            JsonConvert.SerializeObject(
                new SaveData(FilePath, PauseBind,
                    GetAllowedWindows(), CBGlobal.Checked,
                    TranslationDict.Translations, BtnColorChange.BackColor)
                , Formatting.Indented)
            );
        }
        private void JSONLoad(string FilePath) {
            if (!File.Exists(FilePath)) {
                MessageBox.Show("File Not Found");
                if (!File.Exists(SaveLocation))
                    JSONSave(SaveLocation);
            }
            try {
                SaveData? SaveInfo = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(FilePath));
                if (SaveInfo != null) {
                    LstTracked.Items.AddRange(SaveInfo.PreallowedWindows);
                    this.PauseBind = SaveInfo.PauseBind;
                    this.ImportedSave = SaveInfo.SaveLocation;
                    CBGlobal.Checked = SaveInfo.Global;
                    TranslationDict.LoadTranslations(SaveInfo.translations);
                    BtnColorChange.BackColor = SaveInfo.GetBackColor;
                    LoadTranslations();
                }
                if (ImportedSave != DefaultSave)
                    JSONLoad(ImportedSave);
            } catch (Exception ex) {
                MessageBox.Show($"Failed to grab saved Data: {ex.Message}");
            }
        }

        private void BtnForceSave_Click(object sender, EventArgs e) {
            JSONSave(SaveLocation);
        }

        private void BtnDeleteTranslation_Click(object sender, EventArgs e) {
            if (MessageBox.Show($"Are you sure you want to delete: {LstTranslations.Items[LstTranslations.SelectedIndex]}", "Delete Translation", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            if (TranslationDict.RemoveTranslation(((KeyValuePair<Keys, string>)LstTranslations.Items[LstTranslations.SelectedIndex]).Key))
                MessageBox.Show("Translation Deleted");
            else
                MessageBox.Show("Failed to remove from Dictionary");
            LstTranslations.Items.RemoveAt(LstTranslations.SelectedIndex);
            JSONSave(SaveLocation);
        }

        private void BtnEditTranslation_Click(object sender, EventArgs e) {
            //TODO: This

            JSONSave(SaveLocation);
        }

        private void BtnAddTranslation_Click(object sender, EventArgs e) {
            //TODO: This

            JSONSave(SaveLocation);
        }

        private void BtnColorChange_Click(object sender, EventArgs e) {
            ColorDialog dialog = new()
            {
                Color = BtnColorChange.BackColor
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                BtnColorChange.BackColor = dialog.Color;
            }
            dialog.Dispose();
        }
    }
}