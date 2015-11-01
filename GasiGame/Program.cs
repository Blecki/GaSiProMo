using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    private static void EpicWriteLine(String text)
    {
        String[] words = text.Split(' ');
        StringBuilder buffer = new StringBuilder();

        foreach (String word in words)
        {
            buffer.Append(word);

            if (buffer.Length >= 80)
            {
                String line = buffer.ToString().Substring(0, buffer.Length - word.Length);
                Console.WriteLine(line);
                buffer.Clear();
                buffer.Append(word);
            }

            buffer.Append(" ");

        }

        Console.WriteLine(buffer.ToString());
    }

    static void Main(string[] args)
    {
        var driver = new RMUD.SinglePlayer.Driver();
        driver.Start(typeof(Space.Game).Assembly, EpicWriteLine);
        while (driver.IsRunning)
            driver.Input(Console.ReadLine());
        Console.WriteLine("[Press any key to exit..]");
        Console.ReadKey();
    }
}