using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KeyStreamOverlay {
    public class SaveData {
        public readonly string SaveLocation;
        public readonly PauseKeybind PauseBind;
        public readonly string[] PreallowedWindows;
        public readonly Dictionary<Keys, string> translations = new();
        public readonly int[] BackColor = new int[4] { 255,0,255,0 };
        [IgnoreDataMember]
        public Color GetBackColor {
            get =>
                Color.FromArgb(BackColor[0], BackColor[1], BackColor[2], BackColor[3]);
        }
        public readonly bool Global = false;
        public SaveData(string SaveLocation, PauseKeybind PauseBind, string[] PreallowedWindows, bool Global, Dictionary<Keys, string> translations, Color StreamViewBackColor) {
            this.SaveLocation = SaveLocation;
            this.PauseBind = PauseBind;
            this.PreallowedWindows = PreallowedWindows;
            this.Global = Global;
            this.translations = translations;
            this.BackColor = new int[] { StreamViewBackColor.A, StreamViewBackColor.R, StreamViewBackColor.G, StreamViewBackColor.B };
        }
        [JsonConstructor]
        public SaveData(string SaveLocation, PauseKeybind PauseBind, string[] PreallowedWindows, bool Global, Dictionary<Keys, string> translations, int[] BackColor) {
            this.SaveLocation = SaveLocation;
            this.PauseBind = PauseBind;
            this.PreallowedWindows = PreallowedWindows;
            this.Global = Global;
            this.translations = translations;

            if (BackColor.Length > 4)
                BackColor = new int[4] { BackColor[0], BackColor[1], BackColor[2], BackColor[3] };
            for (int i = 0; i < 4; i++) {
                if (BackColor[i] > 255) {
                    BackColor[i] = 255;
                } else if (BackColor[i] < 0) {
                    BackColor[i] = 0;
                }
            }
            this.BackColor = BackColor;
        }
    }
    public class PauseKeybind {
        public readonly Keys key;
        public readonly bool Shift;
        public readonly bool Ctrl;
        public readonly bool Alt;

        public PauseKeybind(Keys key, bool Shift, bool Ctrl, bool Alt) {
            this.key = key;
            this.Shift = Shift;
            this.Ctrl = Ctrl;
            this.Alt = Alt;
        }
        public bool Equals(PauseKeybind other) {
            return this.key == other.key
                && this.Shift == other.Shift
                && this.Ctrl == other.Ctrl
                && this.Alt == other.Alt;
        }
        public bool Equals(Keys otherkey, bool otherShift, bool otherCtrl, bool otherAlt) {
            return this.key == otherkey
                && this.Shift == otherShift
                && this.Ctrl == otherCtrl
                && this.Alt == otherAlt;
        }
    }

    public static class TranslationDict { 
        private readonly static Dictionary<Keys, string> DefaultTranslations = new()
        {
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

            { Keys.Oem5, "\\" },
            { Keys.OemMinus, "-" },
            { Keys.Oemplus, "=" },
            { Keys.LMenu, "LAlt" },
            { Keys.Oemtilde, "tilde" },
            { Keys.OemOpenBrackets, "[" },
            { Keys.Oem6, "}" },
            { Keys.Oem7, "'" },
            { Keys.Oem1, ";"},
            { Keys.OemPeriod, "." },
            { Keys.OemQuestion , "?"},
            { Keys.Oemcomma, "," }
        };
        public static Dictionary<Keys, string> Translations { get; private set; } = new();
        /// <summary>
        /// returns Translation, if not found returns input
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string GetTranslation(Keys Input) {
            if (Translations.TryGetValue(Input, out string? Translation))
                return Translation;

            else
                return Input.ToString();
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
                foreach(Keys key in DefaultTranslations.Keys) {
                    if (!Translations.ContainsKey(key)) {
                        Translations.Add(key, DefaultTranslations[key]);
                    }
                }
                return true;
            } catch { return false; }
        }
        public static bool AddTranslation(Keys Input, string Output) {
            if (Translations.TryGetValue(Input, out _))
                return false;
            Translations.Add(Input, Output);
            return true;
        }
        public static bool ReplaceTranslation(Keys OldKey, string NewValue) {
            if (Translations.TryGetValue(OldKey, out _))
                return false;
            Translations[OldKey] = NewValue;
            return true;
        }
        public static bool RemoveTranslation(Keys Input) {
            if (Translations.Remove(Input))
                return true;
            else
                return false;
        }
    }
}
