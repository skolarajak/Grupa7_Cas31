using System;
using System.IO;


namespace Cas31.Libraries
{
    class Logger
    {
        private static string logFilename = @"C:\Kurs\test.log";

        public static void empty()
        {
            File.WriteAllText(logFilename, string.Empty);
        }

        private static void rawLog(string logMessage)
        {
            using (StreamWriter fileHandle = new StreamWriter(logFilename, true))
            {
                fileHandle.WriteLine(logMessage);

            }
        }

        public static void log(string messageType, string logMessage)
        {
            rawLog($"[{DateTime.Now}] {messageType}: {logMessage}");
        }

        public static void info(string testName, string logMessage)
        {
            log(
                "info",
                $"<{testName}> {logMessage}"
            );
        }

        public static void test(string testName, string logMessage)
        {
            log(
                "test",
                $"<{testName}> {logMessage}"
            );
        }

        public static void separator(char character = '=')
        {
            rawLog(new String(character, 100));
        }
    }
}
