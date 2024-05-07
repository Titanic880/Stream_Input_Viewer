using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KeyStreamOverlay {
    internal class SaveData {
        #region DataBlock
        public static string SaveFolder { get; set; } = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\{Application.ProductName}\\";
        public static string SaveLocation { get; set; } = SaveFolder + "Save_Debug.json";
        public KeyCombo PauseBind { get; set; } = new KeyCombo(Keys.Insert, true, true, true, true);
        public string[] PreAllowedWindows { get; set; } = Array.Empty<string>();
        public Dictionary<Keys, string> translations { get; set; } = new();

        //Change to literal value after first run
        public int BackColor { get; set; } = -16711936;
        public int TextColor { get; set; } = -986896;
        public int CharacterLineLimit { get; set; } = 13;          
        public StreamOutputType OutputControl { get; set; } = 0;
        public bool QuickLaunch { get; set; } = false;
        public bool ShiftToggle { get; set; } = false;
        public bool LoggingHookEnabled { get; set; } = false;
        public bool DeleteLogFileOnLaunch { get; set; } = false;
        public bool DeleteLogFileOnClose { get; set; } = false;
        public bool UseTranslations { get; set; } = true;
        public bool MouseClickToggle { get; set; } = false;
        public bool ModifierAsPrimary { get; set; } = false;
        #endregion

        [IgnoreDataMember]
        public Color GetBackColor {
            get => Color.FromArgb(BackColor);
        }
        [IgnoreDataMember]
        public Color GetTextColor {
            get => Color.FromArgb(TextColor);
        }
    }

    public enum StreamOutputType {
        Listbox = 0,
        Textbox = 1
    }
    public class KeyCombo {
        public readonly Keys key;
        public readonly bool Shift;
        public readonly bool Ctrl;
        public readonly bool Alt;
        public readonly bool Home;
        private readonly MouseButtons? MouseButton = null;
        [JsonConstructor]
        public KeyCombo(Keys key, bool Shift, bool Ctrl, bool Alt, bool Home) {
            this.key = key;
            this.Shift = Shift;
            this.Ctrl = Ctrl;
            this.Alt = Alt;
            this.Home = Home;
        }

        public KeyCombo(MouseButtons MouseButton) {
            this.MouseButton = MouseButton;
            key         = Keys.None;
            Shift       = false;
            Ctrl        = false;
            Alt         = false;
            Home        = false;
        }
        public bool Equals(KeyCombo other) {
            if (MouseButton != null) {
                return this.MouseButton! == other.MouseButton;
            }
            return (this.key   == other.key
                    && this.Shift == other.Shift
                    && this.Ctrl  == other.Ctrl
                    && this.Alt   == other.Alt
                    && this.Home  == other.Home);
        }
        public bool Equals(Keys otherkey, bool otherShift, bool otherCtrl, bool otherAlt, bool Home) {
            return this.key == otherkey
                && this.Shift == otherShift
                && this.Ctrl == otherCtrl
                && this.Alt == otherAlt
                && this.Home == Home;
        }
    }

    public static class TranslationDict {
        private readonly static Dictionary<Keys, string> DefaultTranslations = new()
        {
            { Keys.Oemtilde, "`" },
            { Keys.D1,"1"},
            { Keys.D2,"2" },
            { Keys.D3,"3" },
            { Keys.D4,"4" },
            { Keys.D5,"5" },
            { Keys.D6,"6" },
            { Keys.D7,"7" },
            { Keys.D8,"8" },
            { Keys.D9,"9" },
            { Keys.D0,"0" },
            { Keys.OemMinus, "-" },
            { Keys.Oemplus, "=" },
            { Keys.Space, "˽" },

            { Keys.Alt, "⎇" },
            { Keys.Menu, "⎇" },
            { Keys.LMenu, "⎇" },
            { Keys.RMenu, "⎇" },
            { Keys.Shift, "⇧" },
            { Keys.ShiftKey, "⇧" },
            { Keys.LShiftKey, "⇧" },
            { Keys.RShiftKey, "⇧" },
            { Keys.Control, "⎈" },
            { Keys.ControlKey, "⎈" },
            { Keys.LControlKey, "⎈" },
            { Keys.RControlKey, "⎈" },

            { Keys.Escape, "⎋" },
            { Keys.Tab, "⭾" },
            { Keys.CapsLock, "⇪" },
            { Keys.Delete, "⌦" },
            { Keys.Enter, "¶" },
            { Keys.Back, "⌫" },
            { Keys.LWin, "⌂" },
            { Keys.RWin, "⌂" },

            { Keys.OemOpenBrackets, "[" },
            { Keys.Oem6, "]" },
            { Keys.Oem5, "\\" },
            
            { Keys.Oem1, ";"},
            { Keys.Oem7, "'" },

            { Keys.Oemcomma, "," },
            { Keys.OemPeriod, "." },
            { Keys.OemQuestion , "/"}
        };
        public static readonly Dictionary<Keys,string> ShiftTranslation = new()
        {
            { Keys.Oemtilde, "~" },
            { Keys.D1, "!" },
            { Keys.D2, "@" },
            { Keys.D3, "#" },
            { Keys.D4, "$" },
            { Keys.D5, "%" },
            { Keys.D6, "^" },
            { Keys.D7, "&" },
            { Keys.D8, "*" },
            { Keys.D9, "(" },
            { Keys.D0, ")" },
            { Keys.OemMinus, "_" },
            { Keys.Oemplus, "+" },

            { Keys.OemOpenBrackets, "{" },
            { Keys.Oem6, "}" },
            { Keys.Oem5, "|" },

            { Keys.Oem1, ":"},
            { Keys.Oem7, '"'.ToString() },

            { Keys.Oemcomma, "<" },
            { Keys.OemPeriod, ">" },
            { Keys.OemQuestion , "?" },

        };
        public static Dictionary<Keys, string> Translations { get; private set; } = new();
        /// <summary>
        /// returns Translation, if not found returns input
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string GetTranslation(Keys Input) {
            if (Translations.TryGetValue(Input, out string? Translation)) {
                return Translation;
            } else {
                return Input.ToString();
            }
        }
        public static string GetShiftTranslation(Keys Input) {
            if(ShiftTranslation.TryGetValue(Input, out string? Translation)) {
                return Translation;
            } else {
                return Input.ToString();
            }
        }
        public static void RestoreDefaults() {
            Translations.Clear();
            Translations = DefaultTranslations;
        }
        public static bool LoadTranslations(Dictionary<Keys, string> importedTranslations) {
            if (importedTranslations == null) {
                Translations = DefaultTranslations;
                return false;
            }
            try {
                Translations = importedTranslations;
                foreach (Keys key in DefaultTranslations.Keys) {
                    if (!Translations.ContainsKey(key)) {
                        Translations.Add(key, DefaultTranslations[key]);
                    }
                }
                return true;
            } catch { 
                return false; 
            }
        }
        public static bool AddTranslation(Keys Input, string Output) {
            if (Translations.TryGetValue(Input, out _)) {
                return false;
            }
            Translations.Add(Input, Output);
            return true;
        }
        public static bool ReplaceTranslation(Keys OldKey, string NewValue) {
            if (!Translations.TryGetValue(OldKey, out _)) {
                return false;
            }
            Translations[OldKey] = NewValue;
            return true;
        }
        public static bool RemoveTranslation(Keys Input) {
            if (Translations.Remove(Input)) {
                return true;
            } else {
                return false;
            }
        }
    }
}
