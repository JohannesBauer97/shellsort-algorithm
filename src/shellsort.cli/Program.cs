using System;
using System.Collections.Generic;
using System.Globalization;

namespace shellsort.cli
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Shellsort CLI");

      // User input
      int length = ReadNumberFromConsole("Numbers to generate: ");
      int lowerBound = ReadNumberFromConsole("Type a lower bound for generated numbers: ");
      int upperBound = ReadNumberFromConsole("Type a upper bound for generated numbers: ");
      int[] sequence = GenerateSequence(length, lowerBound, upperBound);
      Console.WriteLine("\n");
      Console.Write("Print to generated numbers to console (Y/N): ");
      ConsoleKeyInfo key = Console.ReadKey();
      Console.WriteLine();
      if (key.Key == ConsoleKey.Y)
      {
        Console.WriteLine("Generated sorted:");
        PrintSequenceToConsole(sequence);
      }
      Console.WriteLine("\n");

      // Insertion Sort
      DateTime start = DateTime.Now;
      int[] sortedInsertSort = InsertionSort(sequence);
      DateTime stop = DateTime.Now;
      TimeSpan timespan = stop.Subtract(start);
      Console.WriteLine($"Sorting with regular insertion sort took {timespan.TotalMilliseconds} ms.");

      // Shell Sort
      int[] gaps = CalculateGapsForSequenceLikeDonaldShell(sequence.Length);
      start = DateTime.Now;
      int[] sortedShellSort = ShellSort(sequence, gaps);
      stop = DateTime.Now;
      timespan = stop.Subtract(start);
      Console.WriteLine($"Sorting with shell sort took {timespan.TotalMilliseconds} ms.");

      // User output
      Console.WriteLine("\n");
      Console.Write("Print sorted numbers to console (Y/N): ");
      key = Console.ReadKey();
      Console.WriteLine();
      if (key.Key == ConsoleKey.Y)
      {
        Console.WriteLine("Sorted sorted:");
        PrintSequenceToConsole(sortedShellSort);
      }

      Console.WriteLine("\n");
      Console.WriteLine("Press any key to quit...");
      Console.ReadKey();
    }

    /// <summary>
    /// Orders the given sorted ascending using the shell sort algorithm
    /// </summary>
    /// <param name="sequence">Sequence to order</param>
    /// <param name="gaps">Descendant ordered array of numbers (minimum value is 1)</param>
    /// <returns></returns>
    static int[] ShellSort(int[] sequence, int[] gaps)
    {
      int[] sorted = new int[sequence.Length];
      sequence.CopyTo(sorted, 0);
      foreach (int h in gaps)
      {
        for (int i = 0; i < sorted.Length; i++)
        {
          int j = i;
          int tmp = sorted[i];
          while ((j >= h) && (sorted[j - h] > tmp))
          {
            sorted[j] = sorted[j - h];
            j = j - h;
          }

          sorted[j] = tmp;
        }
      }

      return sorted;
    }

    /// <summary>
    /// Sorts the given sorted ascending by using the insertion sort algorithm
    /// </summary>
    /// <param name="sortedce"></param>
    /// <returns></returns>
    static int[] InsertionSort(int[] sequence)
    {
      int[] sorted = new int[sequence.Length];
      sequence.CopyTo(sorted, 0);
      for (int i = 0; i < sorted.Length - 1; i++)
      {
        for (int j = i + 1; j > 0; j--)
        {
          if (sorted[j - 1] > sorted[j])
          {
            int temp = sorted[j - 1];
            sorted[j - 1] = sorted[j];
            sorted[j] = temp;
          }
        }
      }

      return sorted;
    }

    /// <summary>
    /// Calculates an array of gaps with Donald Shells method
    /// </summary>
    /// <param name="lengthOfSequence"></param>
    /// <returns></returns>
    static int[] CalculateGapsForSequenceLikeDonaldShell(int lengthOfSequence)
    {
      int k = 1;
      List<int> gaps = new List<int>();
      while (true)
      {
        int gap = lengthOfSequence / (int) Math.Pow(2, k);
        if (gap <= 0)
        {
          return gaps.ToArray();
        }

        gaps.Add(gap);
        k++;
      }
    }

    /// <summary>
    /// Prints an array of numbers to the console
    /// </summary>
    /// <param name="sequence"></param>
    static void PrintSequenceToConsole(int[] sequence)
    {
      for (int i = 0; i < sequence.Length; i++)
      {
        Console.Write($"{sequence[i]} ");
      }

      Console.WriteLine();
    }

    /// <summary>
    /// Generates a random sorted of numbers
    /// </summary>
    /// <param name="length">array size</param>
    /// <param name="lowerBound">minimum int which can be generated</param>
    /// <param name="upperBound">maximum int which can be generated</param>
    /// <returns></returns>
    static int[] GenerateSequence(int length, int lowerBound = 0, int upperBound = 1000)
    {
      int[] sequenz = new int[length];
      Random random = new Random();
      for (int i = 0; i < length; i++)
      {
        sequenz[i] = random.Next(lowerBound, upperBound);
      }

      return sequenz;
    }

    /// <summary>
    /// Waits for console input after sending the given message.
    /// Checks the input if it's a 32bit integer and returns it.
    /// If input was invalid, user has to repeat the input until its valid.
    /// </summary>
    /// <param name="message">Message which will be displayed to the user</param>
    /// <returns></returns>
    static int ReadNumberFromConsole(string message)
    {
      bool successful = false;
      do
      {
        Console.Write(message);
        string input = Console.ReadLine();
        int number;
        successful = Int32.TryParse(input, NumberStyles.Integer, null, out number);
        if (successful)
          return number;
        Console.WriteLine("Error: The number must be 32bit integer.");
      } while (!successful);

      return 0;
    }
  }
}