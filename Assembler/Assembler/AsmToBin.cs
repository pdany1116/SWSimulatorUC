using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assembler
{
    class AsmToBin
    {

        public void ParseAndTransform(string path)
        {
            string[] lines = File.ReadAllLines(path);
            
            for(int i=0; i<lines.Length; i++)
            {
                lines[i] = lines[i].ToUpper();
            }


            foreach(string line in lines)
            {
                char[] delimiters = { ':', ',', ' ','\t'};
                string[] words = line.Split(delimiters);
                UInt16 instructionCode = 0;
                // opcode mas rs mad rd
                // 0000 00 0000 00 0000
                switch (words[0])
                {
                    case "MOV":
                        break;
                    case "ADD":
                        instructionCode = (UInt16)(instructionCode | (1 << 12));
                        break;
                    case "SUB":
                        instructionCode = (UInt16)(instructionCode | (2 << 12));
                        break;
                    case "CMP":
                        instructionCode = (UInt16)(instructionCode | (3 << 12));
                        break;
                    case "AND":
                        instructionCode = (UInt16)(instructionCode | (4 << 12));
                        break;
                    case "OR":
                        instructionCode = (UInt16)(instructionCode | (5 << 12));
                        break;
                    case "XOR":
                        instructionCode = (UInt16)(instructionCode | (6 << 12));
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }

               
                    if(words.Length == 3)
                    {
                        if (words[1].StartsWith("R") && UInt16.TryParse(words[2], out UInt16 operand))
                        {
                            UInt16 mas = 0;
                            UInt16 mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            Console.WriteLine(instructionCode);
                            Console.WriteLine(operand);
                            Console.WriteLine();
                        }
                        else if (words[1].StartsWith("R") && words[2].StartsWith("R"))
                        {
                            UInt16 mas = 1;
                            UInt16 mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));
                            UInt16 indexSource = UInt16.Parse(words[2].Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            Console.WriteLine(instructionCode);
                            Console.WriteLine();
                        }
                        else if (words[1].StartsWith("R") && words[2].StartsWith("(") && words[2].EndsWith(")"))
                        {

                            string registru = words[2].Trim('(', ')');
                            UInt16 mas = 2;
                            UInt16 mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));
                            UInt16 indexSource = UInt16.Parse(registru.Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("R") && words[2].Contains("("))
                        {
                            UInt16 mas = 3;
                            UInt16 mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));
                            UInt16 indexSource = 0;
                            UInt16 index = 0;

                            if (words[2].IndexOf('(') == 0)
                            {
                                string[] registerAndIndex = words[2].Split(')');
                                indexSource = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[2].Split('(', ')');
                                indexSource = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = UInt16.Parse(registerAndIndex[0]);
                            }

                            // UInt16 indexSource = UInt16.Parse(words[2].Substring(words[2].IndexOf('(') + 2, 1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && UInt16.TryParse(words[2], out UInt16 operand1))
                        {
                            string registru = words[1].Trim('(', ')');
                            UInt16 mas = 0;
                            UInt16 mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registru.Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            Console.WriteLine(instructionCode);
                            Console.WriteLine(operand1);
                            Console.WriteLine();
                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && words[2].StartsWith("R"))
                        {
                            string registru = words[1].Trim('(', ')');
                            UInt16 mas = 1;
                            UInt16 mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registru.Substring(1));
                            UInt16 indexSource = UInt16.Parse(words[2].Substring(1));

                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && words[2].StartsWith("(") && words[2].EndsWith(")"))
                        {
                            string registruDest = words[1].Trim('(', ')');
                            string registruSursa = words[2].Trim('(', ')');
                            UInt16 mas = 2;
                            UInt16 mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registruDest.Substring(1));
                            UInt16 indexSource = UInt16.Parse(registruSursa.Substring(1));

                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | indexDestination);

                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && words[2].Contains("("))
                        {
                            string registruDest = words[1].Trim('(', ')');
                            UInt16 mas = 3;
                            UInt16 mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registruDest.Substring(1));
                            UInt16 indexSource = 0;
                            UInt16 index = 0;
                            if (words[2].IndexOf('(') == 0)
                            {
                                string[] registerAndIndex = words[2].Split(')');
                                indexSource = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[2].Split('(', ')');
                                indexSource = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = UInt16.Parse(registerAndIndex[0]);
                            }


                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | indexDestination);

                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if(words[1].Contains("(") && UInt16.TryParse(words[2], out UInt16 operand2))
                        {
                            UInt16 mas = 0;
                            UInt16 mad = 3;
                            UInt16 indexDestination = 0;
                            UInt16 index = 0;
                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = UInt16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine(operand2);
                            Console.WriteLine();

                        }
                        else if (words[1].Contains("(") && words[2].StartsWith("R"))
                        {
                            UInt16 mas = 1;
                            UInt16 mad = 3;
                            UInt16 indexDestination = 0;
                            UInt16 index = 0;
                            UInt16 indexSource = UInt16.Parse(words[2].Substring(1));

                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = UInt16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();

                        }
                        else if(words[1].Contains("(") && words[2].StartsWith("(") && words[2].EndsWith(")"))
                        {
                            string registruSursa = words[2].Trim('(', ')');
                            UInt16 mas = 2;
                            UInt16 mad = 3;
                            UInt16 indexDestination = 0;
                            UInt16 index = 0;
                            UInt16 indexSource = UInt16.Parse(registruSursa.Substring(1));

                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = UInt16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();


                        }
                        else if(words[1].Contains("(") && words[2].Contains("("))
                        {
                            UInt16 mas = 3;
                            UInt16 mad = 3;
                            UInt16 indexDestination = 0;
                            UInt16 indexD = 0;
                            UInt16 indexSource = 0;
                            UInt16 indexS = 0;

                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                indexD = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                indexD = UInt16.Parse(registerAndIndex[0]);
                            }


                            if (words[2].IndexOf('(') == 0)
                            {
                                string[] registerAndIndex = words[2].Split(')');
                                indexSource = UInt16.Parse(registerAndIndex[0].Substring(2));
                                indexS = UInt16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[2].Split('(', ')');
                                indexSource = UInt16.Parse(registerAndIndex[1].Substring(1));
                                indexS = UInt16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));


                            Console.WriteLine(instructionCode);
                            Console.WriteLine(indexS);
                            Console.WriteLine(indexD);
                            Console.WriteLine();

                        }
                        else
                        {
                            Console.WriteLine("instructiune incorecta");
                        }



                    


                }

            }


        }
    
    
    }


}
