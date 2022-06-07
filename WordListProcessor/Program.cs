using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WordListProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<string>();
            var path = @"C:\Users\Andrej\Documents\FINKI\vizuelno programiranje\WordleForms\WordListProcessor\wordle-word-list.txt";
            foreach (var line in File.ReadLines(path))
            {
                list.Add(line);
            }

            Stream stream =
                File.Create(
                    @"C:\Users\Andrej\Documents\FINKI\vizuelno programiranje\WordleForms\WordListProcessor\wordlist.bin");
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, list);

            Console.WriteLine($"Wrote {list.Count} lines to file");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
