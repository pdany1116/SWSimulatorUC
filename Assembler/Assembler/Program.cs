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

        }
    }
}
