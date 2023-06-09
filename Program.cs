﻿using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        var folderPath = string.Empty;

        if (args.Length > 0)
        {
            folderPath = args[0];
        }
        else
        {
            Console.Write("Введите путь до папки:");
            folderPath = Console.ReadLine();
        }

        try
        {
            if (Directory.Exists(folderPath))
            {
                CleanFolder(folderPath);
                Console.WriteLine("Операция выполнена успешно.");
            }
            else
            {
                Console.WriteLine("Папка не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка:");
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }

    static void CleanFolder(string folderPath)
    {
        var directory = new DirectoryInfo(folderPath);

        /// Обрабатываем файлы в текущей папке
        foreach (var file in directory.GetFiles())
        {
            if (file.LastWriteTime < DateTime.Now.Subtract(TimeSpan.FromMinutes(30)))
            {
                file.Delete();
            }
        }

        /// Обрабатываем подпапки рекурсивно
        foreach (var subDirectory in directory.GetDirectories())
        {
            CleanFolder(subDirectory.FullName);
        }

        /// Удаляем текущую папку, если она пустая
        if (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0)
        {
            directory.Delete();
        }
    }
}