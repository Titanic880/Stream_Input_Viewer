using Newtonsoft.Json;
using UI_Mimic;

namespace KeyStreamOverlay {
    public partial class MainCustomize : Form {

        private InputReader? UIReaderHook;
        private KeyCombo PauseBind;
        private int CharacterLineLimit;

        public readonly static Keys[] KeyControl = {
                 Keys.Shift,
                 Keys.ShiftKey,
                 Keys.LShiftKey,
                 Keys.RShiftKey,
                 Keys.Control,
                 Keys.ControlKey,
                 Keys.LControlKey,
                 Keys.RControlKey,
                 Keys.Alt,
                 Keys.Menu,
                 Keys.LMenu,
                 Keys.RMenu,
                 Keys.Home,
                };

        public MainCustomize() {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.HelpButton = true;
            this.HelpButtonClicked += MainCustomize_HelpButtonClicked;

            //Fixes minor visual bug for labels
            LblOut.SendToBack();
            LblUserOutput.SendToBack();

            UIReaderHook = InputReader.ReaderFactory(false, new string[] { this.Text });
            UIReaderHook.GenerateHook(HookTypePub.Keyboard);
            UIReaderHook.KeyDown += KeyboardHook_KeyDown;
            UIReaderHook.OnError += KeyboardHook_OnError;
            UIReaderHook.GenerateHook(HookTypePub.Mouse);
            UIReaderHook.OnMouseDown += MouseHook_OnMouseDown;

            PauseBind = new KeyCombo(Keys.Insert, true, true, true, true);
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

        private void MainCustomize_HelpButtonClicked(object? sender, System.ComponentModel.CancelEventArgs e) {
            MessageBox.Show("To add a program to the allowed list;" +
                          "\ntake the name on the top left of the application" +
                          "\nand put it into the space with:" +
                          "\n'Enter program name here'" +
                          $"\nEX: this window is '{this.Text}'" +
                          "\n\nAll associated files can be found at:" +
                          $"\n{SaveData.SaveFolder}");
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
#if DEBUG
            InfoLogging.LogAsError($"KeyHook Error: {e}");
#else
            InfoLogging.LogAsError($"KeyHook Error: {e.Message}");
#endif
        }

        private void KeyboardHook_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt, bool Home) {
            if (PauseBind != null) {
                if (PauseBind.Equals(key, Shift, Ctrl, Alt, Home)) {
                    MessageBox.Show("Pause Bind Pressed!");
                }
            }

            if (CBModPrim.Checked is false) {
                if (KeyControl.Contains(key)) {
                    return;
                }
            }

            string sft =  Shift ? TranslationDict.GetTranslation(Keys.Shift)+"+" : "";
            string ctrl = Ctrl ? TranslationDict.GetTranslation(Keys.Control)+ "+" : "";
            string alt = Alt ? TranslationDict.GetTranslation(Keys.Alt)+"+" : "";
            string strkey;

            if (CBShiftToggle.Checked) {
                if (Shift && CBTranslationToggle.Checked) {
                    strkey = TranslationDict.GetShiftTranslation(key);
                } else if (CBTranslationToggle.Checked) {
                    strkey = TranslationDict.GetTranslation(key).ToLower();
                } else {
                    strkey = key.ToString().ToLower();
                }
                TbOutput.Text = ctrl + alt + strkey;
            } else {
                if (CBTranslationToggle.Checked) {
                    strkey = TranslationDict.GetTranslation(key);
                } else {
                    strkey = key.ToString().ToLower();
                }
                TbOutput.Text = sft + ctrl + alt + strkey;
            }
        }
        private void MouseHook_OnMouseDown(MouseButtons MouseAction) {
            if (CBMouseOut.Checked is false) {
                return;
            }
            string output = MouseAction switch {
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
            TbOutput.Text = output;
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
            UIReaderHook!.Dispose();
            UIReaderHook = null;
            this.Hide();
            StreamView? view = new(GenerateSave());
            view.ShowDialog();
            view.Close();
            view.Dispose();
            view = null;
            this.Show();
            UIReaderHook = InputReader.ReaderFactory(false, new string[] { this.Text });
            UIReaderHook.GenerateHook(HookTypePub.Keyboard);
            UIReaderHook.GenerateHook(HookTypePub.Mouse);
            UIReaderHook.OnError += KeyboardHook_OnError;
            UIReaderHook.KeyDown += KeyboardHook_KeyDown;
            UIReaderHook.OnMouseDown += MouseHook_OnMouseDown;
        }
        private string[] GetAllowedWindows() {
            List<string> AllowedPrograms = new();
            for (int i = 0; i < LstTracked.Items.Count; i++)
                AllowedPrograms.Add(LstTracked.Items[i].ToString()!);
            return AllowedPrograms.ToArray();
        }
        private SaveData GenerateSave() {
            return new SaveData() {
                PauseBind = this.PauseBind,
                PreAllowedWindows = GetAllowedWindows(),
                Translations = TranslationDict.Translations,
                BackColor = BtnBackColorPicker.BackColor.ToArgb(),
                TextColor = BtnTextColorPicker.ForeColor.ToArgb(),
                CharacterLineLimit = this.CharacterLineLimit,
                QuickLaunch = CBSkipSetupView.Checked,
                ShiftToggle = CBShiftToggle.Checked,
                LoggingHookEnabled = CBLogToggle.Checked,
                DeleteLogFileOnLaunch = CBDeleteLogLaunch.Checked,
                DeleteLogFileOnClose = CBDeleteLogClose.Checked,
                UseTranslations = CBTranslationToggle.Checked,
                MouseClickToggle = CBMouseOut.Checked,
                ModifierAsPrimary = CBModPrim.Checked,
                OutputControl = (StreamOutputType)CBOutputTypes.SelectedIndex,
                DuplicateSpamProtect = CBSpamProtect.Checked
            };
        }
        private void LoadSave(SaveData SaveInfo) {
            LstTracked.Items.AddRange(SaveInfo.PreAllowedWindows);
            this.PauseBind = SaveInfo.PauseBind;
            TranslationDict.LoadTranslations(SaveInfo.Translations);
            BtnBackColorPicker.BackColor = SaveInfo.GetBackColor;
            BtnTextColorPicker.ForeColor = SaveInfo.GetTextColor;
            CBSkipSetupView.Checked = SaveInfo.QuickLaunch;
            CBShiftToggle.Checked = SaveInfo.ShiftToggle;
            CBLogToggle.Checked = SaveInfo.LoggingHookEnabled;
            CBDeleteLogLaunch.Checked = SaveInfo.DeleteLogFileOnLaunch;
            CBDeleteLogClose.Checked = SaveInfo.DeleteLogFileOnClose;
            CBTranslationToggle.Checked = SaveInfo.UseTranslations;
            CharacterLineLimit = SaveInfo.CharacterLineLimit;
            CBMouseOut.Checked = SaveInfo.MouseClickToggle;
            CBModPrim.Checked = SaveInfo.ModifierAsPrimary;
            CBOutputTypes.SelectedIndex = (int)SaveInfo.OutputControl;
            CBSpamProtect.Checked = SaveInfo.DuplicateSpamProtect;
            InfoLogging.LoggingInit(CBLogToggle.Checked);
        }

        private void JSONSave() {
            if (!Directory.Exists(SaveData.SaveFolder)) {
                Directory.CreateDirectory(SaveData.SaveFolder);
            }
            File.WriteAllText(SaveData.SaveLocation, JsonConvert.SerializeObject(GenerateSave(), Formatting.Indented));
        }
        private void JSONLoad() {
            if (!File.Exists(SaveData.SaveLocation)) {
                MessageBox.Show("Save file not found...\nLoading Defaults...");
                MessageBox.Show("PLEASE KNOW: This program READS EVERY KEY INPUT YOU DO, even if it is not shown\nIt does not log/record any inputs without user permission.");
                JSONSave();
            }
            try {
                SaveData? SaveInfo = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(SaveData.SaveLocation));
                if (SaveInfo != null) {
                    LoadSave(SaveInfo);
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
            TbOutput.Text = "Saved Settings!";
        }
        private void BtnPause_Keybind_Click(object sender, EventArgs e) {
            string[] options = TbOutput.Text.Split('+');
            if (Enum.TryParse(options[^1], out Keys key)) {
                //Rebuild Pause bind addition.
                bool Shift = options.Contains(TranslationDict.GetTranslation(Keys.Shift)) || options.Contains("Shift");
                bool Ctrl = options.Contains(TranslationDict.GetTranslation(Keys.Control)) || options.Contains("Ctrl");
                bool Alt = options.Contains(TranslationDict.GetTranslation(Keys.Alt)) || options.Contains("Alt");
                bool Home = options.Contains(TranslationDict.GetTranslation(Keys.LWin)) || options.Contains("LWin");

                PauseBind = new KeyCombo(key, Shift, Ctrl, Alt, Home);
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
                _ = Enum.TryParse(LstTranslations.SelectedItem!.ToString()!.Split("=>")[0].Trim(), out Keys resultkey);
                CBKeys.SelectedIndex = CBKeys.Items.IndexOf(resultkey);
                if (CBKeys.Text == "") {
                    CBKeys.SelectedText = resultkey.ToString();
                }
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
        private void BtnResetTranslations_Click(object sender, EventArgs e) {
            DialogResult dr = MessageBox.Show("Would you like to reset ALL translations to default?\nPlease note, this also deletes custom translations","Are you sure?",MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes) {
                TranslationDict.RestoreDefaults();
                LoadTranslations();
            }
        }
        #endregion Translation Events
        #region GeneralSettings Events
        private void BtnColorChange_Click(object sender, EventArgs e) {
            ColorDialog dialog;
            if (((Button)sender).Name == "BtnBackColorPicker") {
                dialog = new ColorDialog {
                    Color = BtnBackColorPicker.BackColor
                };
            } else {
                dialog = new ColorDialog {
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
        private void BtnAutoCatchName_Click(object sender, EventArgs e) {
            MessageBox.Show("After Closing this popup open the window you want to catch the name of\n another popup will happen when it is done...");
            for(int i = 5; i != 0; i--) {
                Invoke(() => BtnAutoCatchName.Text = i.ToString());
                this.Update();
                //Sleep for 1 second so we can update the UI every Second
                System.Threading.Thread.Sleep(1000);
            }
            string selectedwindow = UI_Mimic.Windows.W_WindowInfo.GetActiveWindowTitle();
            DialogResult res = MessageBox.Show($"Window with the name of: '{selectedwindow}' was caught!\n(Please note that names can sometimes be inaccurate to the title provided but still relevant)\n is this correct?","",MessageBoxButtons.YesNo);
            
            if(res == DialogResult.Yes) {
                LstTracked.Items.Add(selectedwindow);
            }
            BtnAutoCatchName.Text = "Auto Grab Window Name";
        }
        #endregion GeneralSettings Events

    }
}