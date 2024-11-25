//-
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;


namespace NarrowCalc.App;


/// <summary>
/// Интерфейс для реализации логгера
/// </summary>
public interface ILogger
{
    public void Event(string message);
    public void Error(string message);
}


/// <summary>
/// Реализация логгера
/// </summary>
public class Logger : ILogger
{
    public void Event(string message)
    {
        ConsoleColor prevBackground = Console.BackgroundColor;
        ConsoleColor prevForeground = Console.ForegroundColor;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(message);
        Console.BackgroundColor = prevBackground;
        Console.ForegroundColor = prevForeground;
        Console.Write(Environment.NewLine);
    }

    public void Error(string message)
    {
        ConsoleColor prevBackground = Console.BackgroundColor;
        ConsoleColor prevForeground = Console.ForegroundColor;
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(message);
        Console.BackgroundColor = prevBackground;
        Console.ForegroundColor = prevForeground;
        Console.Write(Environment.NewLine);
    }
}

