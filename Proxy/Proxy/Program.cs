using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Proxy
{
    // Абстрактний клас читача тексту
    public abstract class TextReader
    {
        public abstract char[][] ReadText(string filePath);
    }

    // Конкретний клас читача тексту
    public class SmartTextReader : TextReader
    {
        public override char[][] ReadText(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            char[][] textArray = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                textArray[i] = lines[i].ToCharArray();
            }

            return textArray;
        }
    }

    // Проксі для SmartTextReader з логуванням
    public class SmartTextChecker : TextReader
    {
        private TextReader reader;

        public SmartTextChecker(TextReader reader)
        {
            this.reader = reader;
        }

        public override char[][] ReadText(string filePath)
        {
            Console.WriteLine("Opening file: " + filePath);
            char[][] textArray = reader.ReadText(filePath);
            Console.WriteLine("File read successfully");
            Console.WriteLine($"Total lines: {textArray.Length}");
            int totalChars = 0;
            foreach (var line in textArray)
            {
                totalChars += line.Length;
            }
            Console.WriteLine($"Total characters: {totalChars}");
            Console.WriteLine("Closing file");

            return textArray;
        }
    }

    // Проксі для SmartTextReader з обмеженням доступу до певних файлів
    public class SmartTextReaderLocker : TextReader
    {
        private TextReader reader;
        private Regex allowedFilesRegex;

        public SmartTextReaderLocker(TextReader reader, string allowedFilesPattern)
        {
            this.reader = reader;
            this.allowedFilesRegex = new Regex(allowedFilesPattern);
        }

        public override char[][] ReadText(string filePath)
        {
            if (!allowedFilesRegex.IsMatch(filePath))
            {
                Console.WriteLine("Access denied!");
                return null;
            }
            else
            {
                return reader.ReadText(filePath);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення об'єкта SmartTextReader
            TextReader smartTextReader = new SmartTextReader();

            // Створення проксі з логуванням
            TextReader smartTextChecker = new SmartTextChecker(smartTextReader);

            // Читання та виведення інформації про текстовий файл з логуванням
            Console.WriteLine("Reading file with logging:");
            smartTextChecker.ReadText("example.txt");

            // Створення проксі з обмеженням доступу до певних файлів
            TextReader smartTextReaderLocker = new SmartTextReaderLocker(smartTextReader, @".*\.txt");

            // Читання лімітованого файлу
            Console.WriteLine("\nReading restricted file:");
            smartTextReaderLocker.ReadText("restricted.txt");
        }
    }
}
