using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = System.IO.Directory.GetCurrentDirectory() + "\\test.asm";
            AsmToBin p = new AsmToBin();
            p.ParseAndTransform(filePath);
            Console.WriteLine(filePath);

            Regex regex = new Regex(@"(MOV|ADD|SUB|CMP|AND|OR|XOR) R(\d),R(\d)");
            string string1 = "MOV R2,R7";

            Match match = regex.Match(string1);
            if (match.Success)
            {
                string value1 = match.Groups[1].Value;
                string value2 = match.Groups[2].Value;
                string string3 = match.Groups[3].Value;
                Console.WriteLine("GROUP 1 and 2: {0} {1} {2}", value1, value2, string3);
            }
        }
    }
}
