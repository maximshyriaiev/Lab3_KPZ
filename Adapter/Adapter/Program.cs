using System;

namespace Adapter
{
    // Клас для логування повідомлень у консоль
    public class Logger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Log] " + message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error] " + message);
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[Warn] " + message);
            Console.ResetColor();
        }
    }

    // Клас для запису в файл
    public class FileWriter
    {
        public void Write(string message)
        {
            System.IO.File.AppendAllText("log.txt", message);
        }

        public void WriteLine(string message)
        {
            System.IO.File.AppendAllText("log.txt", message + Environment.NewLine);
        }
    }

    // Адаптер для забезпечення сумісності з Logger
    public class FileLoggerAdapter : Logger
    {
        private FileWriter fileWriter;

        public FileLoggerAdapter()
        {
            fileWriter = new FileWriter();
        }

        public new void Log(string message)
        {
            fileWriter.WriteLine("[Log] " + message);
            // Виводимо у консоль
            base.Log(message);
        }

        public new void Error(string message)
        {
            fileWriter.WriteLine("[Error] " + message);
            // Виводимо також у консоль
            base.Error(message);
        }

        public new void Warn(string message)
        {
            fileWriter.WriteLine("[Warn] " + message);
            // Виводимо також у консоль
            base.Warn(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єкту адаптера
            FileLoggerAdapter fileLogger = new FileLoggerAdapter();

            // Тестування методів логування
            fileLogger.Log("Це тестове повідомлення.");
            fileLogger.Error("Помилка! Щось пiшло не так.");
            fileLogger.Warn("Попередження! Не забудьте зробити резервну копiю.");
        }
    }
}
