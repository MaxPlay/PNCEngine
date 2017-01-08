using PNCEngine.Exceptions;
using PNCEngine.Utils.Events;
using System;
using System.Collections.Generic;
using System.IO;

namespace PNCEngine.Utils
{
    public sealed class Debug
    {
        #region Public Fields

        public const string FILENAME = "Debugger.pref";
        public const string LOG_FILENAME = "Debug.log";

        #endregion Public Fields

        #region Private Fields

        private const ConsoleColor DEFAULT_ATTENTION = ConsoleColor.Blue;
        private const bool DEFAULT_CLEAR_LOG_FILE_ON_LOAD = true;
        private const ConsoleColor DEFAULT_CRITICAL = ConsoleColor.DarkRed;
        private const ConsoleColor DEFAULT_DEFAULT = ConsoleColor.White;
        private const ConsoleColor DEFAULT_ERROR = ConsoleColor.Red;
        private const ConsoleColor DEFAULT_WARNING = ConsoleColor.Yellow;
        private static Debug instance;

        private bool clearOnLoad;
        private DebugColors colors;

        private Dictionary<string, Lines> elements;

        #endregion Private Fields

        #region Public Constructors

        public Debug()
        {
            if (instance == null)
                instance = this;
            else
                throw new SingletonAlreadyExistsException("Debug");

            Logged += Debug_Logged;

            elements = new Dictionary<string, Lines>();
            elements.Add("default", Lines.Default);
            elements.Add("def", Lines.Default);
            elements.Add("regular", Lines.Default);
            elements.Add("attention", Lines.Attention);
            elements.Add("at", Lines.Attention);
            elements.Add("critical", Lines.Critical);
            elements.Add("error", Lines.Error);
            elements.Add("err", Lines.Error);
            elements.Add("warning", Lines.Warning);
            elements.Add("logged", Lines.ClearLogOnLoad);
            elements.Add("clear", Lines.ClearLogOnLoad);

            Load();
            if (clearOnLoad)
                File.WriteAllText(LOG_FILENAME, string.Empty);
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<LogArgs> Logged;

        #endregion Public Events

        #region Public Enums

        public enum Lines
        {
            Default,
            Warning,
            Critical,
            Error,
            Attention,
            ClearLogOnLoad
        }

        public enum LogDepth
        {
            Default,
            Warning,
            Critical,
            Error,
            Attention
        }

        #endregion Public Enums

        #region Public Properties

        public static Debug Instance
        {
            get { return instance; }
        }

        public bool ClearOnLoad
        {
            get { return clearOnLoad; }
            set { clearOnLoad = value; }
        }

        public DebugColors Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public static void Log(string message, object arg0)
        {
            instance.Log(string.Format(message, arg0), LogDepth.Default);
        }

        public static void Log(string message, object arg0, object arg1)
        {
            instance.Log(string.Format(message, arg0, arg1), LogDepth.Default);
        }

        public static void Log(string message, object arg0, object arg1, object arg2)
        {
            instance.Log(string.Format(message, arg0, arg1, arg2), LogDepth.Default);
        }

        public static void Log(string message, params object[] args)
        {
            instance.Log(string.Format(message, args), LogDepth.Default);
        }

        public static void Log(object message)
        {
            instance.Log(message.ToString(), LogDepth.Default);
        }

        public static void Log(string message)
        {
            instance.Log(message, LogDepth.Default);
        }

        public static void LogAttention(string message, object arg0)
        {
            instance.Log(string.Format(message, arg0), LogDepth.Attention);
        }

        public static void LogAttention(string message, object arg0, object arg1)
        {
            instance.Log(string.Format(message, arg0, arg1), LogDepth.Attention);
        }

        public static void LogAttention(string message, object arg0, object arg1, object arg2)
        {
            instance.Log(string.Format(message, arg0, arg1, arg2), LogDepth.Attention);
        }

        public static void LogAttention(string message, object[] arg)
        {
            instance.Log(string.Format(message, arg), LogDepth.Attention);
        }

        public static void LogAttention(object message)
        {
            instance.Log(message.ToString(), LogDepth.Attention);
        }

        public static void LogAttention(string message)
        {
            instance.Log(message, LogDepth.Attention);
        }

        public static void LogCritical(string message, object arg0)
        {
            instance.Log(string.Format(message, arg0), LogDepth.Critical);
        }

        public static void LogCritical(string message, object arg0, object arg1)
        {
            instance.Log(string.Format(message, arg0, arg1), LogDepth.Critical);
        }

        public static void LogCritical(string message, object arg0, object arg1, object arg2)
        {
            instance.Log(string.Format(message, arg0, arg1, arg2), LogDepth.Critical);
        }

        public static void LogCritical(string message, object[] arg)
        {
            instance.Log(string.Format(message, arg), LogDepth.Critical);
        }

        public static void LogCritical(object message)
        {
            instance.Log(message.ToString(), LogDepth.Critical);
        }

        public static void LogCritical(string message)
        {
            instance.Log(message, LogDepth.Critical);
        }

        public static void LogError(string message, object arg0)
        {
            instance.Log(string.Format(message, arg0), LogDepth.Error);
        }

        public static void LogError(string message, object arg0, object arg1)
        {
            instance.Log(string.Format(message, arg0, arg1), LogDepth.Error);
        }

        public static void LogError(string message, object arg0, object arg1, object arg2)
        {
            instance.Log(string.Format(message, arg0, arg1, arg2), LogDepth.Error);
        }

        public static void LogError(string message, object[] arg)
        {
            instance.Log(string.Format(message, arg), LogDepth.Error);
        }

        public static void LogError(object message)
        {
            instance.Log(message.ToString(), LogDepth.Error);
        }

        public static void LogError(string message)
        {
            instance.Log(message, LogDepth.Error);
        }

        public static void LogWarning(string message, object arg0)
        {
            instance.Log(string.Format(message, arg0), LogDepth.Warning);
        }

        public static void LogWarning(string message, object arg0, object arg1)
        {
            instance.Log(string.Format(message, arg0, arg1), LogDepth.Warning);
        }

        public static void LogWarning(string message, object arg0, object arg1, object arg2)
        {
            instance.Log(string.Format(message, arg0, arg1, arg2), LogDepth.Warning);
        }

        public static void LogWarning(string message, object[] arg)
        {
            instance.Log(string.Format(message, arg), LogDepth.Warning);
        }

        public static void LogWarning(object message)
        {
            instance.Log(message.ToString(), LogDepth.Warning);
        }

        public static void LogWarning(string message)
        {
            instance.Log(message, LogDepth.Warning);
        }

        public void Load()
        {
            if (File.Exists(FILENAME))
                Parse(File.ReadAllLines(FILENAME));
            else
                LoadDefaults();
        }

        public void Save()
        {
            using (StreamWriter writer = File.CreateText(FILENAME))
            {
                writer.WriteLine("{0}={1}", "default", colors.Default);
                writer.WriteLine("{0}={1}", "warning", colors.Warning);
                writer.WriteLine("{0}={1}", "error", colors.Error);
                writer.WriteLine("{0}={1}", "critical", colors.Critical);
                writer.WriteLine("{0}={1}", "attention", colors.Attention);
                writer.WriteLine("{0}={1}", "clear", clearOnLoad ? 1 : 0);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void Debug_Logged(object sender, LogArgs e)
        {
            string depth = string.Empty;
            switch (e.Depth)
            {
                case LogDepth.Default:
                    Console.ForegroundColor = colors.Default;
                    break;

                case LogDepth.Warning:
                    Console.ForegroundColor = colors.Warning;
                    depth = "[WARNING]";
                    break;

                case LogDepth.Critical:
                    Console.ForegroundColor = colors.Critical;
                    depth = "[CRITICAL]";
                    break;

                case LogDepth.Error:
                    Console.ForegroundColor = colors.Error;
                    depth = "[ERROR]";
                    break;

                case LogDepth.Attention:
                    Console.ForegroundColor = colors.Attention;
                    depth = "[ATTENTION]";
                    break;
            }

            Console.WriteLine("[{0}]{1} {2}", e.Time.ToShortTimeString(), depth, e.Message);
            File.AppendAllText(LOG_FILENAME, "[" + e.Time.ToShortTimeString() + "]" + depth + " " + e.Message + Environment.NewLine);
        }

        private void LoadDefaults()
        {
            this.colors = new DebugColors();
            colors.Default = DEFAULT_DEFAULT;
            colors.Warning = DEFAULT_WARNING;
            colors.Error = DEFAULT_ERROR;
            colors.Critical = DEFAULT_CRITICAL;
            colors.Attention = DEFAULT_ATTENTION;
            clearOnLoad = DEFAULT_CLEAR_LOG_FILE_ON_LOAD;
        }

        private void Log(string message, LogDepth depth)
        {
            OnLogged(message, depth, DateTime.Now);
        }

        private void OnLogged(string message, LogDepth depth, DateTime time)
        {
            Logged?.Invoke(null, new LogArgs(message, depth, time));
        }

        private void Parse(string[] content)
        {
            for (int i = 0; i < content.Length; i++)
            {
                string[] linedata = content[i].Split('=');

                string identifier = linedata[0].ToLower();

                if (elements.ContainsKey(identifier))
                    switch (elements[identifier])
                    {
                        case Lines.Default:
                            setConsoleColor(ref colors.Default, linedata, DEFAULT_DEFAULT);
                            break;

                        case Lines.Warning:
                            setConsoleColor(ref colors.Warning, linedata, DEFAULT_WARNING);
                            break;

                        case Lines.Critical:
                            setConsoleColor(ref colors.Critical, linedata, DEFAULT_CRITICAL);
                            break;

                        case Lines.Error:
                            setConsoleColor(ref colors.Error, linedata, DEFAULT_ERROR);
                            break;

                        case Lines.Attention:
                            setConsoleColor(ref colors.Attention, linedata, DEFAULT_ATTENTION);
                            break;

                        case Lines.ClearLogOnLoad:
                            if (linedata.Length > 1)
                            {
                                int value = 0;
                                if (!int.TryParse(linedata[1], out value))
                                    clearOnLoad = DEFAULT_CLEAR_LOG_FILE_ON_LOAD;
                                else
                                    clearOnLoad = value > 0;
                            }
                            else
                                clearOnLoad = DEFAULT_CLEAR_LOG_FILE_ON_LOAD;
                            break;
                    }
            }
        }

        private void setConsoleColor(ref ConsoleColor field, string[] linedata, ConsoleColor defaultvalue)
        {
            if (linedata.Length > 1)
            {
                ConsoleColor value = 0;
                if (!Enum.TryParse(linedata[1], out value))
                    field = defaultvalue;
                else
                    field = value;
            }
            else
                field = defaultvalue;
        }

        #endregion Private Methods

        #region Public Structs

        public struct DebugColors
        {
            #region Public Fields

            public ConsoleColor Attention;
            public ConsoleColor Critical;
            public ConsoleColor Default;
            public ConsoleColor Error;
            public ConsoleColor Warning;

            #endregion Public Fields
        }

        #endregion Public Structs
    }
}