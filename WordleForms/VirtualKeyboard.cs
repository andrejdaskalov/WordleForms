using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleForms
{
    public class VirtualKeyboard
    {
        public Key [][] KeyMatrix { get; set; }
        public Dictionary<string,Key> KeyDictionary { get; set; }
        private static int Padding = 5;


        public VirtualKeyboard()
        {
            KeyDictionary = new Dictionary<string,Key> ();
            KeyMatrix = new Key[3][];
            KeyMatrix[0] = new Key[]
            {
                new Key("q"), new Key("w"), new Key("e"), new Key("r"), new Key("t"), new Key("y"), new Key("u"),
                new Key("i"), new Key("o"), new Key("p")
            };
            KeyMatrix[1] = new Key[9] 
            {
            new Key("a"), new Key("s"), new Key("d"), new Key("f"), new Key("g"), new Key("h"), new Key("j"),
            new Key("k"), new Key("l")
            };
            KeyMatrix[2] = new Key[7]
            {
            new Key("z"), new Key("x"), new Key("c"), new Key("v"), new Key("b"), new Key("n"), new Key("m")
            };

            for (int i = 0; i < KeyMatrix.Length; i++)
            {
                for (int j = 0; j < KeyMatrix[i].Length; j++)
                {
                    KeyMatrix[i][j].Bounds = new Rectangle(j * Key.Width + (j + 1) * Padding + i*i*(Key.Width/2)+70,
                        i * Key.Height + (i + 1) * Padding + 500, Key.Width, Key.Height);
                    KeyDictionary.Add(KeyMatrix[i][j].Content, KeyMatrix[i][j]);
                }
            }
        }

        public void Draw(Graphics g)
        {
            for (int i = 0; i < KeyMatrix.Length; i++)
            {
                for (int j = 0; j < KeyMatrix[i].Length; j++)
                {
                    KeyMatrix[i][j].Draw(g);
                }
            }
        }
    }
}
