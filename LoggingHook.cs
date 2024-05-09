namespace KeyStreamOverlay {
    internal class LoggingHook {
        public static bool HookActive { get; private set; } = false;
        public static bool HookPaused { get; private set; } = false;
        public static string LogToLocation { get {
                return SaveData.SaveFolder + DefaultLogFile;
            } }
        public static string ErrorLogToLocation { get {
                return SaveData.SaveFolder + DefaultErrorFile;
            } }
        private const string DefaultLogFile = "KeyLog.txt";
        private const string DefaultErrorFile = "Errors.txt";

        public static async void LogToFile(string DatatoLog) {
            if (HookActive && !HookPaused) {
                await LogTask(LogToLocation, DatatoLog);
            }
        }
        public static async void LogAsError(string ErrorMessage) {
            await LogTask(ErrorLogToLocation, ErrorMessage);
        }
        private static async Task<Task> LogTask(string Location, string Message) {
            using StreamWriter sw = new(Location);
            await sw.WriteLineAsync($"[{DateTime.Now}] {Message}");
            return Task.CompletedTask;
        }
        public static void Hookinit(bool HookActiveStatus = false) {
            if (!Directory.Exists(SaveData.SaveFolder)) {
                Directory.CreateDirectory(SaveData.SaveFolder);
                File.Create(LogToLocation).Close();
            }else if (!File.Exists(DefaultLogFile)) {
                File.Create(LogToLocation).Close();
            }
            if (!File.Exists(ErrorLogToLocation)) {
                File.Create(ErrorLogToLocation).Close();
            }
            HookActive = HookActiveStatus;
        }

        public static void PauseLoggingHook() {
            HookPaused = true;
        }
        public static void ResumeLoggingHook() {
            HookPaused = false;
        }
    }
}
