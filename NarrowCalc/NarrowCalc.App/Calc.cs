//-
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;


namespace NarrowCalc.App;


/// <summary>
/// Интерфейс для реализации калькулятора
/// </summary>
/// <typeparam name="T">double, decimal</typeparam>
internal interface ICalc<T> where T : notnull, INumber<T>
{
    internal T? Sum(params T[] numbers);
    internal bool TryParseNumberFromString(string sInput, out T? result);
}


/// <summary>
/// Реализация калькулятора
/// </summary>
/// <typeparam name="T">double, decimal</typeparam>
internal class Calc<T> : ICalc<T> where T : notnull, INumber<T>
{
    ILogger Logger { get; }

    /// <summary>
    /// Конструктор, принимает логгер в качестве примера реализации DI
    /// </summary>
    /// <param name="logger">DI параметр для вывода сообщений</param>
    internal Calc(ILogger logger)
    {
        Logger = logger;
    }


    /// <summary>
    /// Функция суммирования произвольного количества значений текущего типа
    /// </summary>
    /// <param name="numbers">одно или несколько значений текущего типа</param>
    /// <returns>сумма значений в виде текущего типа</returns>
    public T? Sum(params T[] numbers)
    {
        Logger.Event("sum started");
        T? res = default;
        
        if (res is T res1)
        {
            Console.Write($"{res1}");
            foreach (T item in numbers)
            {
                res1 += item;
                Console.Write($" + {item}");
            }
            Console.Write(Environment.NewLine);
            res = res1;
        }

        Logger.Event("sum finished");
        return res;
    }


    /// <summary>
    /// Функция обработки одиночного значения для текущего типа
    /// </summary>
    /// <param name="sInput">значение в виде строки</param>
    /// <param name="result">возвращаемое значение в виде текущего типа</param>
    /// <returns>true - при успешном чтении, false - если чтение не удалось</returns>
    public bool TryParseNumberFromString(string sInput, out T? result)
    {
        bool res;

        // InvariantCulture
        sInput = sInput.Replace(',', '.');

        try
        {
            int count = 0;
            foreach (char c in sInput) 
                if (c == '.') count++;
            
            if (count > 1)
                throw new FormatException("wrong number format - too many decimal separators");
        }
        catch (FormatException ex)
        {
            Logger.Error(ex.Message);
        }
        finally
        {
            res = T.TryParse(sInput, CultureInfo.InvariantCulture, out result);
        }

        return res;
    }
}

