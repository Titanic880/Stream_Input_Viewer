using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KeyStreamOverlay {
    //TODO: Rebuild for ease of development
    public class SaveData {
        public readonly string SaveLocation;
        public readonly KeyCombo PauseBind;
        public readonly string[] PreallowedWindows;
        public readonly Dictionary<Keys, string> translations = new();
        public readonly int[] BackColor = new int[4] { 255,0,255,0 };
        public readonly int[] TextColor = new int[4] { 255,0,0,0 };
        public readonly int CharacterLineLimit = 13;
        public readonly int OutputControl = 0;

        [IgnoreDataMember]
        public Color GetBackColor {
            get =>
                Color.FromArgb(BackColor[0], BackColor[1], BackColor[2], BackColor[3]);
        }
        [IgnoreDataMember]
        public Color GetTextColor {
            get =>
                Color.FromArgb(TextColor[0], TextColor[1], TextColor[2], TextColor[3]);
        }
        public readonly bool QuickLaunch = false;
        public readonly bool ShiftToggle = false;
        public readonly bool LoggingHookEnabled = false;
        public readonly bool DeleteLogFileOnLaunch = false;
        public readonly bool DeleteLogFileOnClose = false;
        public readonly bool UseTranslations = true;
        public readonly bool MouseClickToggle = false;
        public readonly bool ModifierAsPrimary = false;

        public SaveData(
            string SaveLocation, 
            KeyCombo PauseBind, 
            string[] PreallowedWindows,  
            Dictionary<Keys, string> translations, 
            Color StreamViewBackColor,
            Color StreamViewTextColor,
            bool QuickLaunch, 
            bool ShiftToggle, 
            bool LoggingHookEnabled, 
            bool DeleteLogFileOnLaunch, 
            bool DeleteLogFileOnClose,
            bool UseTranslations, 
            int CharacterLineLimit,
            bool MouseClickToggle,
            bool ModifierAsPrimary,
            int OutputControl
            ) {
            this.SaveLocation = SaveLocation;
            this.PauseBind = PauseBind;
            this.PreallowedWindows = PreallowedWindows;
            this.translations = translations;

            this.BackColor = new int[] { StreamViewBackColor.A, StreamViewBackColor.R, StreamViewBackColor.G, StreamViewBackColor.B };
            this.TextColor = new int[] {StreamViewTextColor.A, StreamViewTextColor.R,StreamViewTextColor.G, StreamViewTextColor.B };
            
            this.QuickLaunch = QuickLaunch;
            this.ShiftToggle = ShiftToggle;
            this.LoggingHookEnabled = LoggingHookEnabled;
            this.DeleteLogFileOnLaunch = DeleteLogFileOnLaunch;
            this.DeleteLogFileOnClose = DeleteLogFileOnClose;
            this.UseTranslations = UseTranslations;
            this.CharacterLineLimit = CharacterLineLimit;
            this.MouseClickToggle = MouseClickToggle;
            this.ModifierAsPrimary = ModifierAsPrimary;
            this.OutputControl = OutputControl;
        }
        [JsonConstructor]
        public SaveData(
            string SaveLocation, 
            KeyCombo PauseBind, 
            string[] PreallowedWindows, 
            Dictionary<Keys, string> translations, 
            int[] BackColor, 
            int[] TextColor, 
            bool QuickLaunch, 
            bool ShiftToggle, 
            bool LoggingHookEnabled,
            bool DeleteLogFileOnLaunch, 
            bool DeleteLogFileOnClose,
            bool UseTranslations, 
            int CharacterLineLimit,
            bool MouseClickToggle,
            bool ModifierAsPrimary,
            int OutputControl
            ) {
            this.SaveLocation = SaveLocation;
            this.PauseBind = PauseBind;
            this.PreallowedWindows = PreallowedWindows;
            this.translations = translations;
            
            this.QuickLaunch = QuickLaunch;
            this.ShiftToggle = ShiftToggle;
            this.LoggingHookEnabled = LoggingHookEnabled;
            this.DeleteLogFileOnLaunch = DeleteLogFileOnLaunch;
            this.DeleteLogFileOnClose = DeleteLogFileOnClose;
            this.UseTranslations = UseTranslations;
            this.CharacterLineLimit = CharacterLineLimit;

            this.MouseClickToggle = MouseClickToggle;
            this.ModifierAsPrimary = ModifierAsPrimary;
            this.OutputControl = OutputControl;

            if (TextColor.Length > 4)
                TextColor = new int[4] { TextColor[0], TextColor[1], TextColor[2], TextColor[3] };
            for (int i = 0; i < 4; i++) {
                if (TextColor[i] > 255) {
                    TextColor[i] = 255;
                } else if (TextColor[i] < 0) {
                    TextColor[i] = 0;
                }
            }
            if (BackColor.Length > 4)
                BackColor = new int[4] { BackColor[0], BackColor[1], BackColor[2], BackColor[3] };
            for (int i = 0; i < 4; i++) {
                if (BackColor[i] > 255) {
                    BackColor[i] = 255;
                } else if (BackColor[i] < 0) {
                    BackColor[i] = 0;
                }
            }
            this.TextColor = TextColor;
            this.BackColor = BackColor;
            this.LoggingHookEnabled = LoggingHookEnabled;
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
