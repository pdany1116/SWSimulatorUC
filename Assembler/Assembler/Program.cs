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
            string outputFilePath = System.IO.Directory.GetCurrentDirectory() + "\\test.bin";
            AsmToBin p = new AsmToBin();
            p.ParseAndTransform(filePath, outputFilePath);
            Console.WriteLine(filePath);

        }
    }
}
