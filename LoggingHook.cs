namespace KeyStreamOverlay {
    internal class LoggingHook {
        public static bool HookActive { get; private set; } = false;
        public static string LogToLocation { get {
                return MainCustomize.DefaultFolder + DefaultLogFile;
            } }
        private const string DefaultLogFile = "KeyLog.txt";
        
        public static async void LogToFile(string DatatoLog) {
            await Task.Run(async () => {
                using StreamWriter sw = new(LogToLocation);
                await sw.WriteLineAsync($"[{DateTime.Now}] {DatatoLog}");
                return Task.CompletedTask;
            });
        }
        public static void Hookinit() {
            if (!Directory.Exists(MainCustomize.DefaultFolder)) {
                Directory.CreateDirectory(MainCustomize.DefaultFolder);
                File.Create(LogToLocation).Close();
            }else if (!File.Exists(DefaultLogFile)) {
                File.Create(LogToLocation).Close();
            }
        }
    }
}
