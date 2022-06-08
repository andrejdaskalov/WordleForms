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
            var rootpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var list = new List<string>();
            var path = rootpath + @"\wordle-word-list.txt";
            foreach (var line in File.ReadLines(path))
            {
                list.Add(line);
            }

            var answerList = new List<string>();
            var pathAnswer = rootpath + @"\wordle-answer-list.txt";
            foreach (var line in File.ReadLines(pathAnswer))
            {
                answerList.Add(line);
            }

            BinaryFormatter formatter = new BinaryFormatter();
            Stream stream;

            using (stream = File.Create(rootpath + @"\wordlist.bin"))
            {
                formatter.Serialize(stream, list);
            }  
            using (stream = File.Create(rootpath + @"\answerlist.bin"))
            {
                formatter.Serialize(stream, answerList);
            }    
            
            
            

            Console.WriteLine($"Wrote {list.Count} lines to file");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
