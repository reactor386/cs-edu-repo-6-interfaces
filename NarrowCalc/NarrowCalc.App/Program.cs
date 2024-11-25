// -
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;


namespace NarrowCalc.App;

/*

Задание 1

Создайте консольный мини-калькулятор, который будет подсчитывать сумму двух чисел.
 Определите интерфейс для функции сложения числа и реализуйте его.

Дополнительно: добавьте конструкцию Try/Catch/Finally
 для проверки корректности введённого значения.

Задание 2

Реализуйте механизм внедрения зависимостей:
 добавьте в мини-калькулятор логгер, используя материал из скринкаста юнита 10.1.

Дополнительно: текст ошибки, выводимый в логгере, окрасьте в красный цвет,
 а текст события — в синий цвет.

*/

internal class Program
{
    static ILogger? Logger { get; set; }

    /// <summary>
    /// Запрашиваем у пользователя вещественные числа
    ///  и выводим их сумму
    /// Выполняем данное действие дважды, используя
    ///  типы double и decimal поочередно
    /// Можем увидеть, что для одинакового набора чисел
    ///  результат с типом decimal будет более точен
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Narrow Calculator");
        Console.WriteLine("---");
        int errCode = 0;

        Logger = new Logger();

        Console.WriteLine("Sum numbers - double");

        string result1 = ProcessCalculation<double>(Logger);
        Console.WriteLine($"The result is: {result1}");

        Console.WriteLine("");
        
        Console.WriteLine("Sum numbers - decimal");

        string result2 = ProcessCalculation<decimal>(Logger);
        Console.WriteLine($"The result is: {result2}");

        Console.WriteLine("---");
        Console.WriteLine("return: [" + errCode.ToString() + "]");
        Console.ReadKey();
    }


    /// <summary>
    /// Команда, выполняющая обработку вводимых значений для указанного типа
    /// </summary>
    /// <typeparam name="T">double, decimal</typeparam>
    /// <param name="logger">DI параметр для вывода сообщений</param>
    /// <returns>общая сумма введенных значений, переведенная в строку</returns>
    static string ProcessCalculation<T>(ILogger logger) where T : notnull, INumber<T>
    {
        Calc<T> calc = new(logger);
        List<T> numbers = [];

        string s = string.Empty;
        do
        {
            Console.Write("Enter a number (empty string to return): ");
            s = Console.ReadLine() ?? string.Empty;
            if ((s != string.Empty) && calc.TryParseNumberFromString(s, out T? num))
            {
                if (num is T num1)
                    numbers.Add(num1);
            }
            else if (s != string.Empty)
            {
                logger.Error("Invalid input. Please enter a valid number");
            }
        } while (s != string.Empty);

        T? result = calc.Sum(numbers.ToArray());

        return result?.ToString() ?? string.Empty;
    }
}

