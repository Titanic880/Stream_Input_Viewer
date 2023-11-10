namespace KeyStreamOverlay
{
    public class SaveData
    {
        public readonly string SaveLocation;
        public readonly PauseKeybind PauseBind;
        public readonly string[] PreallowedWindows;
        public readonly bool Global = false;
        public SaveData(string SaveLocation, PauseKeybind PauseBind, string[] PreallowedWindows, bool Global)
        {
            this.SaveLocation = SaveLocation;
            this.PauseBind = PauseBind;
            this.PreallowedWindows = PreallowedWindows;
            this.Global = Global;
        }
    }
    public class PauseKeybind
    {
        public readonly Keys key;
        public readonly bool Shift;
        public readonly bool Ctrl;
        public readonly bool Alt;

        public PauseKeybind(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            this.key = key;
            this.Shift = Shift;
            this.Ctrl = Ctrl;
            this.Alt = Alt;
        }
        public bool Equals(PauseKeybind other)
        {
            return this.key == other.key
                && this.Shift == other.Shift
                && this.Ctrl == other.Ctrl
                && this.Alt == other.Alt;
        }
        public bool Equals(Keys otherkey, bool otherShift, bool otherCtrl, bool otherAlt)
        {
            return this.key == otherkey
                && this.Shift == otherShift
                && this.Ctrl == otherCtrl
                && this.Alt == otherAlt;
        }
    }

    public static class TranslationDict
    {
        private const string DefaultTranslationSavePath = "DefaultTranslations.json";
        public static Dictionary<string, string> Translations { get; private set; } = new Dictionary<string, string>()
        {
            { "D1","1" },
            { "D2","2" },
            { "D3","3" },
            { "D4","4" },
            { "D5","5" },
            { "D6","6" },
            { "D7","7" },
            { "D8","8" },
            { "D9","9" },
            { "D0","0" },
            { "D1","1" },
            { "Oem5", "\\" },
            { "OemMinus", "-" },
            { "Oemplus", "=" },
            {"LMenu", "LAlt" },
            { "Oemtilde", "tilde" },
            { "OemOpenBrackets", "[" },
            { "Oem6", "}" },
            { "Oem7", "'" },
            { "Oem1", ";"},
            { "OemPeriod", "." },
            { "OemQuestion" , "?"},
            { "Oemcomma", "," }
        };
        public static string GetTranslation(string Input)
        {
            if (Translations.TryGetValue(Input, out string? Translation))
                return Translation;
            
            else 
                return Input;
        }
        public static bool LoadTranslations(string FilePath)
        {
            return false;
        }
        private static bool SaveTranslations(string FilePath)
        {
            return false;
        }
        public static bool AddTranslation(string Input, string Output)
        {
            if (Translations.TryGetValue(Input, out _))
                return false;
            Translations.Add(Input, Output);
            return true;
        }
        public static bool ReplaceTranslation(string Input, string Output)
        {
            if (Translations.TryGetValue(Input, out _))
                return false;
            Translations.Remove(Input);
            Translations.Add(Input,Output);
            return true;
        }
        public static bool RemoveTranslation(string Input)
        {
            if (Translations.Remove(Input))
                return true;
            else 
                return false;
        }
    }
}
