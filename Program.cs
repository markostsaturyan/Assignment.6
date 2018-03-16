using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Dictionaries;

namespace MyDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlackDictionary<int, int> redBlack = new RedBlackDictionary<int, int>();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();

            Stopwatch time = new Stopwatch();
            Random random = new Random();

            int a = 320;
            time.Start();
            while (a != 0)
            {
                redBlack.Add(a, random.Next(1,150));
                a--;
            }
            time.Stop();
            Console.WriteLine("320 elements added in red-black dictionary: " + time.Elapsed.TotalMilliseconds + "miliseconds");
            time.Reset();

            a = 320;
            time.Start();
            while (a != 0)
            {
                 dictionary.Add(a, random.Next(1, 150));
                a--;
            }
            time.Stop();
            Console.WriteLine("320 elements added in C# dictionary: " + time.Elapsed.TotalMilliseconds + "miliseconds");
            time.Reset();

            redBlack.Clear();
            dictionary.Clear();

            a = 640;
            time.Start();
            while (a != 0)
            {
                redBlack.Add(a, random.Next(1, 150));
                a--;
            }
            time.Stop();
            Console.WriteLine("640 elements added in red-black dictionary: " + time.Elapsed.TotalMilliseconds + "miliseconds");
            time.Reset();

            a = 640;
            time.Start();
            while (a != 0)
            {
                 dictionary.Add(a, random.Next(1, 150));
                a--;
            }
            time.Stop();
            Console.WriteLine("640 elements added in C# dictionary: " + time.Elapsed.TotalMilliseconds + "miliseconds");
            time.Reset();

            redBlack.Clear();
            dictionary.Clear();

            a = 1280;
            time.Start();
            while (a != 0)
            {
                redBlack.Add(a, random.Next(1, 150));
                a--;
            }
            time.Stop();
            Console.WriteLine("1280 elements added in red-black dictionary: " + time.Elapsed.TotalMilliseconds + "miliseconds");
            time.Reset();

            a = 1280;
            time.Start();
            while (a != 0)
            {
                dictionary.Add(a, random.Next(1, 150));
                a--;
            }
            time.Stop();
            Console.WriteLine("1280 elements added in C# dictionary: " + time.Elapsed.TotalMilliseconds + "miliseconds");
            time.Reset();

            Console.Read();
        }
    }
}
