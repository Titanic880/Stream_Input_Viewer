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
}
