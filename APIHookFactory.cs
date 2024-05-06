using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_Mimic.Windows;

namespace KeyStreamOverlay {
    internal class APIHookFactory {
        public UIReader? KeyboardHook { get; private set; }
        public UIReader? MouseHook { get; private set; }


        private APIHookFactory(bool keyboard, bool mouse, string[] ActiveWindows) {
            if (keyboard) {
                KeyboardHook = new UIReader(true, ActiveWindows, UIReader.HookTypePub.Keyboard);
                
            }
            if (mouse) {
                MouseHook = new UIReader(true, ActiveWindows, UIReader.HookTypePub.Mouse);
            }

        }
        public static APIHookFactory GetHook(bool keyboard, bool mouse, string[] ActiveWindows) {
            return new APIHookFactory(keyboard, mouse, ActiveWindows);
        }
        public void ConnectMouse() {

        }
        public void ConnectKeyboard() {

        }

        public void DisconnectKeyboard() {

        }
        public void DisconnectMouse() { 
        
        }
        private void KeyboardTranslation() {

        }
    }
}
