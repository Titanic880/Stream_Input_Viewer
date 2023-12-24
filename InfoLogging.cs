namespace KeyStreamOverlay {
    internal class InfoLogging {
        public static bool LoggingToFile { get; private set; } = false;
        public static bool InfoLogPaused { get; private set; } = false;
        public static string LogToLocation { get {
                return MainCustomize.DefaultFolder + DefaultLogFile;
            } }
        public static string ErrorLogToLocation { get {
                return MainCustomize.DefaultFolder + DefaultErrorFile;
            } }
        private const string DefaultLogFile = "KeyLog.txt";
        private const string DefaultErrorFile = "Errors.txt";

        public static async void LogToFile(string DatatoLog) {
            if (LoggingToFile && !InfoLogPaused) {
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
        public static void LoggingInit(bool LogToFile = false) {
            if (!Directory.Exists(MainCustomize.DefaultFolder)) {
                Directory.CreateDirectory(MainCustomize.DefaultFolder);
                File.Create(LogToLocation).Close();
            }else if (!File.Exists(DefaultLogFile)) {
                File.Create(LogToLocation).Close();
            }
            if (!File.Exists(ErrorLogToLocation)) {
                File.Create(ErrorLogToLocation).Close();
            }
            LoggingToFile = LogToFile;
        }

        public static void PauseLoggingHook() {
            InfoLogPaused = true;
        }
        public static void ResumeLoggingHook() {
            InfoLogPaused = false;
        }
    }
}
