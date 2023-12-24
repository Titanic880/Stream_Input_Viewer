using Newtonsoft.Json;

namespace KeyStreamOverlay {
    public partial class MainCustomize : Form {
        private UI_Mimic.UIReader? KeyboardHook;
        private KeyCombo PauseBind;
        private const string DefaultSave = "Save.json";
        private string ImportedSave { get; set; } = "";
        public readonly static string DefaultFolder = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\{Application.ProductName}\\";
        private string SaveLocation {
            get {
                string ret = DefaultFolder;
                if (ImportedSave.Contains('\\')) {
                    ret = ImportedSave;
                } else {
                    ret += (ImportedSave == "" ? DefaultSave : ImportedSave);
                }
                return ret;
            }
        }
        public MainCustomize() {
            InitializeComponent();
            this.MaximizeBox = false;

            KeyboardHook = new(false, new string[] { this.Text });
            KeyboardHook.KeyDown += KeyboardHook_KeyDown;
            KeyboardHook.OnError += KeyboardHook_OnError;
            PauseBind = new(Keys.Insert, true, true, true);
            CBOutputTypes.Items.AddRange(Enum.GetNames(typeof(StreamOutputType)));
            CBOutputTypes.SelectedIndex = 0;
            JSONLoad();
            LoadTranslations();

            if (CBDeleteLogLaunch.Checked) {
                File.WriteAllText(InfoLogging.LogToLocation, "");
            }
            if (CBSkipSetupView.Checked) {
                LaunchStreamView();
            }
        }
        #region FormControl
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            JSONSave();
            if (CBDeleteLogClose.Checked) {
                File.WriteAllText(InfoLogging.LogToLocation, "");
            }
        }
        private void BtnStart_Click(object sender, EventArgs e) {
            LaunchStreamView();
        }
        private void KeyboardHook_OnError(Exception e) {
            InfoLogging.LogAsError($"KeyHook Error: {e.Message}");
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
        #endregion FormControl
        #region Functions
        private void LoadTranslations() {
            LstTranslations.Items.Clear();
            CBKeys.Items.Clear();
            foreach (KeyValuePair<Keys, string> pair in TranslationDict.Translations) {
                LstTranslations.Items.Add(pair.Key + " => " + pair.Value);
            }
            CBKeys.Items.AddRange(Enum.GetNames(typeof(Keys)));
        }
        private void LaunchStreamView() {
            if (LstTracked.Items.Count == 0 || PauseBind == null) {
                MessageBox.Show("List is empty or a pause bind has not been set...");
                return;
            }
            KeyboardHook!.Dispose();
            this.Hide();
            StreamView? view = new(Enum.Parse<StreamOutputType>(CBOutputTypes.SelectedItem.ToString()!), true, GetAllowedWindows(), PauseBind, BtnBackColorPicker.BackColor, BtnTextColorPicker.ForeColor);
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
        private void JSONSave() {
            if (!Directory.Exists(DefaultFolder)) {
                Directory.CreateDirectory(DefaultFolder);
            }
            File.WriteAllText(SaveLocation,
            JsonConvert.SerializeObject(
                new SaveData(SaveLocation, PauseBind,
                    GetAllowedWindows(), TranslationDict.Translations,
                    BtnBackColorPicker.BackColor,
                    BtnTextColorPicker.ForeColor,
                    CBSkipSetupView.Checked, CBShiftToggle.Checked,
                    InfoLogging.LoggingToFile, CBDeleteLogLaunch.Checked, CBDeleteLogClose.Checked, CBTranslationToggle.Checked)
                , Formatting.Indented)
            );
        }
        private void JSONLoad() {
            if (!File.Exists(SaveLocation)) {
                MessageBox.Show("Save file not found...\nLoading Defaults...");
                MessageBox.Show("PLEASE KNOW: This program READS EVERY KEY INPUT YOU DO, even if it is not shown\nIt does not log/record any inputs without user permission.");
                JSONSave();
            }
            try {
                SaveData? SaveInfo = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(SaveLocation));
                if (SaveInfo != null) {
                    LstTracked.Items.AddRange(SaveInfo.PreallowedWindows);
                    this.PauseBind = SaveInfo.PauseBind;
                    this.ImportedSave = SaveInfo.SaveLocation;
                    //CBGlobal.Checked = SaveInfo.Global;
                    TranslationDict.LoadTranslations(SaveInfo.translations);
                    BtnBackColorPicker.BackColor = SaveInfo.GetBackColor;
                    BtnTextColorPicker.ForeColor = SaveInfo.GetTextColor;
                    CBSkipSetupView.Checked = SaveInfo.QuickLaunch;
                    CBShiftToggle.Checked = SaveInfo.ShiftToggle;
                    InfoLogging.LoggingInit(SaveInfo.LoggingHookEnabled);
                    CBDeleteLogLaunch.Checked = SaveInfo.DeleteLogFileOnLaunch;
                    CBDeleteLogClose.Checked = SaveInfo.DeleteLogFileOnClose;
                    CBTranslationToggle.Checked = SaveInfo.UseTranslations;
                }
                LoadTranslations();
            } catch (Exception ex) {
                MessageBox.Show($"Failed to grab saved Data: {ex.Message}");
            }
        }
        #endregion Functions
        #region InputTracking Events
        private void BtnAddProgram_Click(object sender, EventArgs e) {
            if (TbTracked.Text == "Enter Program Name Here") {
                return;
            }

            LstTracked.Items.Add(TbTracked.Text);
            JSONSave();
            TbTracked.Text = "";
        }
        private void BtnRemove_Click(object sender, EventArgs e) {
            if (LstTracked.SelectedIndex == -1) {
                return;
            }
            LstTracked.Items.RemoveAt(LstTracked.SelectedIndex);
        }
        private void BtnForceSave_Click(object sender, EventArgs e) {
            JSONSave();
        }
        private void BtnPause_Keybind_Click(object sender, EventArgs e) {
            string[] options = TbOutput.Text.Split('+');
            if (Enum.TryParse(options[^1], out Keys key)) {
                PauseBind = new(key, options.Contains("Shift"), options.Contains("Ctrl"), options.Contains("Alt"));
                MessageBox.Show("Pause Bind Set!");
            } else {
                MessageBox.Show("Failed to Get Keybind");
            }
        }
        #endregion InputTracking Events
        #region Translation Events
        private void BtnAddTranslation_Click(object sender, EventArgs e) {
            if (CBKeys.SelectedIndex < 0 || TbTranslation.Text == "") {
                return;
            }
            if (TranslationDict.AddTranslation(Enum.Parse<Keys>((string)CBKeys.SelectedItem!), TbTranslation.Text)) {
                MessageBox.Show("Added Translation to the list.");
                LoadTranslations();
                JSONSave();
            } else {
                MessageBox.Show("Failed to add Translation (Possibly already exists for provided key?)");
            }
        }
        private void BtnEditTranslation_Click(object sender, EventArgs e) {
            if (LstTranslations.SelectedIndex < 0) {
                return;
            }
            if (BtnEditTranslation.Text == "Edit Translation") {
                BtnEditTranslation.Text = "Finish Editing";
                BtnAddTranslation.Enabled = false;
                BtnDeleteTranslation.Enabled = false;
                _ = Enum.TryParse(LstTranslations.SelectedItem!.ToString()!.Split("=>")[0], out Keys resultkey);
                CBKeys.SelectedIndex = CBKeys.Items.IndexOf(resultkey);
                TbTranslation.Text = LstTranslations.SelectedItem!.ToString()!.Split("=>")[1].Trim();
            } else {
                BtnEditTranslation.Text = "Edit Translation";
                BtnAddTranslation.Enabled = true;
                BtnDeleteTranslation.Enabled = true;

                string Key = LstTranslations.SelectedItem!.ToString()!.Split("=>")[0];
                if (Enum.TryParse(Key, out Keys resultkey)) {
                    if (TranslationDict.ReplaceTranslation(resultkey, TbTranslation.Text)) {
                        MessageBox.Show("Updated Translation!");
                        LoadTranslations();
                        JSONSave();
                        CBKeys.SelectedIndex = 0;
                        TbTranslation.Text = "";
                    } else {
                        MessageBox.Show("Failed to update Translation...");
                    }
                } else {
                    MessageBox.Show("Failed to Parse out Key...");
                }
            }
            JSONSave();
        }
        private void BtnDeleteTranslation_Click(object sender, EventArgs e) {
            if (MessageBox.Show($"Are you sure you want to delete: {LstTranslations.Items[LstTranslations.SelectedIndex]}", "Delete Translation", MessageBoxButtons.YesNo) == DialogResult.No) {
                return;
            }
            if (TranslationDict.RemoveTranslation(Enum.Parse<Keys>(LstTranslations.Items[LstTranslations.SelectedIndex].ToString()!.Split("=>")[0]))) {
                MessageBox.Show("Translation Deleted");
            } else {
                MessageBox.Show("Failed to remove from Dictionary");
            }
            LoadTranslations();
            JSONSave();
        }
        #endregion Translation Events
        #region GeneralSettings Events
        private void BtnColorChange_Click(object sender, EventArgs e) {
            ColorDialog dialog;
            if (((Button)sender).Name == "BtnBackColorPicker") {
                dialog = new() {
                    Color = BtnBackColorPicker.BackColor
                };
            } else {
                dialog = new() {
                    Color = BtnTextColorPicker.ForeColor
                };
            }

            if (dialog.ShowDialog() == DialogResult.OK) {
                if (((Button)sender).Name == "BtnBackColorPicker") {
                    BtnBackColorPicker.BackColor = dialog.Color;
                } else {
                    BtnTextColorPicker.ForeColor = dialog.Color;
                }
            }
            dialog.Dispose();
        }
        #endregion GeneralSettings Events








    }
}