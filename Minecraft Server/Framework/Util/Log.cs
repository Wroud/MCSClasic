using System;

namespace Minecraft_Server.Framework.Util
{
    class Log
    {
        public class LogMessage
        {
            public string LogType;
            public string Message;
            public ConsoleColor BGColor;
            public ConsoleColor FGColor;

            public LogMessage(string logType, object message, ConsoleColor bgColor, ConsoleColor fgColor)
            {
                LogType = logType;
                Message = message.ToString();
                BGColor = bgColor;
                FGColor = fgColor;
            }
        }

        static Log()
        {
            Console.Clear();
        }

        static void writeTimePrefix()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write(DateTime.Now.ToString(" HH:mm:ss "));
        }

        static void writeTypePrefix(string type, ConsoleColor bg, ConsoleColor fg)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            Console.Write(" {0,8} ", type);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(" ");
        }

        public static int id = 0;

        public static void Info(string message, params object[] args)
        {
            id = 0;
            string mes = args.Length > 0 ? string.Format(message, args) : message.ToString();
            Display(new LogMessage("INFO", mes, ConsoleColor.DarkGreen, ConsoleColor.Green)); // Can't use the method below because if the message contains { or } it will throw invalid string format exception.
        }

        public static void Warning(string message, params object[] args)
        {
            id = 1;
            string mes = args.Length > 0 ? string.Format(message, args) : message.ToString();
            Display(new LogMessage("WARNING", mes, ConsoleColor.DarkYellow, ConsoleColor.Yellow));
        }

        public static void Error(string message, params object[] args)
        {
            id = 2;
            string mes = args.Length > 0 ? string.Format(message, args) : message.ToString();
            Display(new LogMessage("ERROR", mes, ConsoleColor.DarkRed, ConsoleColor.Red));
        }

        public static void Update(string message, string p = "", int c = 3, params object[] args)
        {
            message = args.Length > 0 ? string.Format(message, args) : message.ToString();
            if (p != "")
                message += Points(p, c);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            switch (id)
            {
                case 0:
                    Info(message);
                    break;
                case 1:
                    Warning(message);
                    break;
                case 2:
                    Error(message);
                    break;
            }
        }

        public static ushort points = 1;
        public static string Points(string p, int c = 3)
        {
            string poi = "";
            for (int i = 0; i < points; i++)
                poi += p;
            points++;
            if (points > c)
                points = 0;
            return poi;
        }

        public static void Display(LogMessage message)
        {
            writeTimePrefix();
            writeTypePrefix(message.LogType, message.BGColor, message.FGColor);
            for (int i = 0; i < 40 - message.Message.Length; i++)
                message.Message += " ";
            Console.WriteLine(message.Message);
        }
    }
}
