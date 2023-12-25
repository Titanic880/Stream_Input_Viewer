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

        public static void LogToFile(string DatatoLog) {
            if (LoggingToFile && !InfoLogPaused) {
                LogTask(LogToLocation, DatatoLog);
            }
        }
        public static void LogAsError(string ErrorMessage) {
            LogTask(ErrorLogToLocation, ErrorMessage);
        }
        private static void LogTask(string Location, string Message) {
            using StreamWriter sw = File.AppendText(Location);
            sw.WriteLine($"[{DateTime.Now}] {Message}");
        }
        public static void LoggingInit(bool LogToFile = false) {
            if (!Directory.Exists(MainCustomize.DefaultFolder)) {
                Directory.CreateDirectory(MainCustomize.DefaultFolder);
                File.Create(LogToLocation).Close();
            }else if (!File.Exists(LogToLocation)) {
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
