﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace Assembler
{
    class AsmToBin
    {
        private string m_inputFilePath;
        private string m_outputFilePath;
        private List <byte> m_output = new List <byte> ();
        private string[] m_instructions = { "MOV", "ADD", "SUB", "CMP", "AND", "OR", "XOR", "CLR", "NEG", "INC", "DEC", "ASL", "ASR", "LSR", "ROL",
            "ROR", "RLC", "RRC", "JMP", "CALL", "PUSH", "POP", "BR", "BNE", "BEQ", "BPL", "BMI", "BCS", "BCC", "BVS", "BVC", "CLC", "CLV",
            "CLZ", "CLS", "CCC", "SEC", "SEV", "SEZ", "SES", "SCC", "NOP", "RET", "RETI", "HALT", "WAIT", "PUSHPC", "POPPC", "PUSHFLAG",
            "POPFLAG"};
        private Int16 m_bytesFromStart = 0;

        public void ParseAndTransform(string inputFilePath, string outputFilePath)
        {
            m_inputFilePath = inputFilePath;
            m_outputFilePath = outputFilePath;
            string[] lines = File.ReadAllLines(m_inputFilePath);
            
            for(int i=0; i<lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
                lines[i] = lines[i].ToUpper();
            }

            var lineNumber = 0;
            foreach(string line in lines)
            {
                char[] delimiters = {',', ' ','\t'};
                string[] words = line.Split(delimiters);
                UInt16 instructionCode = 0;

                StringCollection instructions = new StringCollection();
                instructions.AddRange(m_instructions);
                if (instructions.Contains(words[0]))
                {
                    m_bytesFromStart += 2;
                    Console.WriteLine(line);

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
                        case "BR":
                            instructionCode = (UInt16)(instructionCode | (192 << 8));
                            break;
                        case "BNE":
                            instructionCode = (UInt16)(instructionCode | (193 << 8));
                            break;
                        case "BEQ":
                            instructionCode = (UInt16)(instructionCode | (194 << 8));
                            break;
                        case "BPL":
                            instructionCode = (UInt16)(instructionCode | (195 << 8));
                            break;
                        case "BMI":
                            instructionCode = (UInt16)(instructionCode | (196 << 8));
                            break;
                        case "BCS":
                            instructionCode = (UInt16)(instructionCode | (197 << 8));
                            break;
                        case "BCC":
                            instructionCode = (UInt16)(instructionCode | (198 << 8));
                            break;
                        case "BVS":
                            instructionCode = (UInt16)(instructionCode | (199 << 8));
                            break;
                        case "BVC":
                            instructionCode = (UInt16)(instructionCode | (200 << 8));
                            break;
                        case "CLR":
                            instructionCode = (UInt16)(instructionCode | (512 << 6));
                            break;
                        case "NEG":
                            instructionCode = (UInt16)(instructionCode | (513 << 6));
                            break;
                        case "INC":
                            instructionCode = (UInt16)(instructionCode | (514 << 6));
                            break;
                        case "DEC":
                            instructionCode = (UInt16)(instructionCode | (515 << 6));
                            break;
                        case "ASL":
                            instructionCode = (UInt16)(instructionCode | (516 << 6));
                            break;
                        case "ASR":
                            instructionCode = (UInt16)(instructionCode | (517 << 6));
                            break;
                        case "LSR":
                            instructionCode = (UInt16)(instructionCode | (518 << 6));
                            break;
                        case "ROL":
                            instructionCode = (UInt16)(instructionCode | (519 << 6));
                            break;
                        case "ROR":
                            instructionCode = (UInt16)(instructionCode | (520 << 6));
                            break;
                        case "RLC":
                            instructionCode = (UInt16)(instructionCode | (521 << 6));
                            break;
                        case "RRC":
                            instructionCode = (UInt16)(instructionCode | (522 << 6));
                            break;
                        case "JMP":
                            instructionCode = (UInt16)(instructionCode | (523 << 6));
                            break;
                        case "CALL":
                            instructionCode = (UInt16)(instructionCode | (524 << 6));
                            break;
                        case "PUSH":
                            instructionCode = (UInt16)(instructionCode | (525 << 6));
                            break;
                        case "POP":
                            instructionCode = (UInt16)(instructionCode | (526 << 6));
                            break;
                        case "CLC":
                            instructionCode = 57344;
                            break;
                        case "CLV":
                            instructionCode = 57345;
                            break;
                        case "CLZ":
                            instructionCode = 57346;
                            break;
                        case "CLS":
                            instructionCode = 57347;
                            break;
                        case "CCC":
                            instructionCode = 57348;
                            break;
                        case "SEC":
                            instructionCode = 57349;
                            break;
                        case "SEV":
                            instructionCode = 57350;
                            break;
                        case "SEZ":
                            instructionCode = 57351;
                            break;
                        case "SES":
                            instructionCode = 57352;
                            break;
                        case "SCC":
                            instructionCode = 57353;
                            break;
                        case "NOP":
                            instructionCode = 57354;
                            break;
                        case "RET":
                            instructionCode = 57355;
                            break;
                        case "RETI":
                            instructionCode = 57356;
                            break;
                        case "HALT":
                            instructionCode = 57357;
                            break;
                        case "WAIT":
                            instructionCode = 57358;
                            break;
                        case "PUSH PC":
                            instructionCode = 57359;
                            break;
                        case "POP PC":
                            instructionCode = 57360;
                            break;
                        case "PUSH FLAG":
                            instructionCode = 57361;
                            break;
                        case "POP FLAG":
                            instructionCode = 57362;
                            break;
                        default:
                            Console.WriteLine("Default case");
                            Console.WriteLine();
                            break;
                    }

                    Int16 mas = -1;
                    Int16 mad = -1;

                    if (words.Length == 3)
                    {
                        if (words[1].StartsWith("R") && System.Text.RegularExpressions.Regex.IsMatch(words[2], @"^\d+H?$"))
                        {
                            Int16 operand = ResolveImmediateOperand(words[2]);
                            mas = 0;
                            mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode, operand);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(operand);
                            Console.WriteLine();
                        }
                        else if (words[1].StartsWith("R") && words[2].StartsWith("R"))
                        {
                            mas = 1;
                            mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));
                            UInt16 indexSource = UInt16.Parse(words[2].Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine();
                        }
                        else if (words[1].StartsWith("R") && words[2].StartsWith("(") && words[2].EndsWith(")"))
                        {

                            string registru = words[2].Trim('(', ')');
                            mas = 2;
                            mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));
                            UInt16 indexSource = UInt16.Parse(registru.Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("R") && words[2].Contains("("))
                        {
                            mas = 3;
                            mad = 1;
                            UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));
                            UInt16 indexSource = 0;
                            Int16 index = 0;

                            if (words[2].IndexOf('(') == 0)
                            {
                                string[] registerAndIndex = words[2].Split(')');
                                indexSource = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[2].Split('(', ')');
                                indexSource = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = Int16.Parse(registerAndIndex[0]);
                            }

                            // UInt16 indexSource = UInt16.Parse(words[2].Substring(words[2].IndexOf('(') + 2, 1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode, index);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && System.Text.RegularExpressions.Regex.IsMatch(words[2], @"^\d+H?$"))
                        {
                            Int16 operand = ResolveImmediateOperand(words[2]);
                            string registru = words[1].Trim('(', ')');
                            mas = 0;
                            mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registru.Substring(1));

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode, operand);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(operand);
                            Console.WriteLine();
                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && words[2].StartsWith("R"))
                        {
                            string registru = words[1].Trim('(', ')');
                            mas = 1;
                            mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registru.Substring(1));
                            UInt16 indexSource = UInt16.Parse(words[2].Substring(1));

                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && words[2].StartsWith("(") && words[2].EndsWith(")"))
                        {
                            string registruDest = words[1].Trim('(', ')');
                            string registruSursa = words[2].Trim('(', ')');
                            mas = 2;
                            mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registruDest.Substring(1));
                            UInt16 indexSource = UInt16.Parse(registruSursa.Substring(1));

                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | indexDestination);

                            AddToOutput(instructionCode);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine();

                        }
                        else if (words[1].StartsWith("(") && words[1].EndsWith(")") && words[2].Contains("("))
                        {
                            string registruDest = words[1].Trim('(', ')');
                            mas = 3;
                            mad = 2;
                            UInt16 indexDestination = UInt16.Parse(registruDest.Substring(1));
                            UInt16 indexSource = 0;
                            Int16 index = 0;
                            if (words[2].IndexOf('(') == 0)
                            {
                                string[] registerAndIndex = words[2].Split(')');
                                indexSource = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[2].Split('(', ')');
                                indexSource = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = Int16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | indexDestination);

                            AddToOutput(instructionCode, index);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();

                        }
                        else if (words[1].Contains("(") && System.Text.RegularExpressions.Regex.IsMatch(words[2], @"^\d+H?$"))
                        {
                            Int16 operand = ResolveImmediateOperand(words[2]);
                            mas = 0;
                            mad = 3;
                            UInt16 indexDestination = 0;
                            Int16 index = 0;
                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = Int16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode, index, operand);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine(operand);
                            Console.WriteLine();

                        }
                        else if (words[1].Contains("(") && words[2].StartsWith("R"))
                        {
                            mas = 1;
                            mad = 3;
                            UInt16 indexDestination = 0;
                            Int16 index = 0;
                            UInt16 indexSource = UInt16.Parse(words[2].Substring(1));

                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = Int16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode, index);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();

                        }
                        else if (words[1].Contains("(") && words[2].StartsWith("(") && words[2].EndsWith(")"))
                        {
                            string registruSursa = words[2].Trim('(', ')');
                            mas = 2;
                            mad = 3;
                            UInt16 indexDestination = 0;
                            Int16 index = 0;
                            UInt16 indexSource = UInt16.Parse(registruSursa.Substring(1));

                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                index = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                index = Int16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));

                            AddToOutput(instructionCode, index);
                            Console.WriteLine(instructionCode);
                            Console.WriteLine(index);
                            Console.WriteLine();
                        }
                        else if (words[1].Contains("(") && words[2].Contains("("))
                        {
                            mas = 3;
                            mad = 3;
                            UInt16 indexDestination = 0;
                            Int16 indexD = 0;
                            UInt16 indexSource = 0;
                            Int16 indexS = 0;

                            if (words[1].IndexOf('(') == 0)
                            {
                                string test = words[1];
                                string[] registerAndIndex = words[1].Split(')');
                                indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                indexD = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[1].Split('(', ')');
                                indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                indexD = Int16.Parse(registerAndIndex[0]);
                            }

                            if (words[2].IndexOf('(') == 0)
                            {
                                string[] registerAndIndex = words[2].Split(')');
                                indexSource = UInt16.Parse(registerAndIndex[0].Substring(2));
                                indexS = Int16.Parse(registerAndIndex[1]);
                            }
                            else
                            {
                                string[] registerAndIndex = words[2].Split('(', ')');
                                indexSource = UInt16.Parse(registerAndIndex[1].Substring(1));
                                indexS = Int16.Parse(registerAndIndex[0]);
                            }

                            instructionCode = (UInt16)(instructionCode | indexDestination);
                            instructionCode = (UInt16)(instructionCode | (mad << 4));
                            instructionCode = (UInt16)(instructionCode | (indexSource << 6));
                            instructionCode = (UInt16)(instructionCode | (mas << 10));


                            AddToOutput(instructionCode, indexS, indexD);
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
                    else if (words.Length == 2)
                    {
                        // Instr de salt 
                        if (words[0].StartsWith("B"))
                        {
                            // valoare hexa
                            if (System.Text.RegularExpressions.Regex.IsMatch(words[1], @"^\d+H$"))
                            {
                                string offsetStr = words[1].Remove(words[1].Length - 1, 1);
                                var offset = Convert.ToInt16(offsetStr, 16);
                                if (offset < -128 || offset > 127)
                                {
                                    Console.WriteLine("Offset overflow");
                                }
                                else if (offset % 2 == 1)
                                {
                                    Console.WriteLine("Offset must be even!");
                                }
                                else
                                {
                                    instructionCode = (UInt16)(instructionCode | offset);
                                }
                                AddToOutput(instructionCode);
                                Console.WriteLine(instructionCode);
                                Console.WriteLine();
                            }
                            // valoare decimala
                            else if (System.Text.RegularExpressions.Regex.IsMatch(words[1], @"^\d+$"))
                            {
                                var offset = Convert.ToInt16(words[1], 10);
                                if (offset < -128 || offset > 127)
                                {
                                    Console.WriteLine("Offset overflow");
                                }
                                else if (offset % 2 == 1)
                                {
                                    Console.WriteLine("Offset must be even!");
                                }
                                else
                                {
                                    instructionCode = (UInt16)(instructionCode | offset);
                                }
                                AddToOutput(instructionCode);
                                Console.WriteLine(instructionCode);
                                Console.WriteLine();
                            }
                            else
                            {
                                string label = words[1] + ":";

                                var offset = BytesToLabel(label) - m_bytesFromStart;
                                Console.WriteLine("Bytes from start : " + m_bytesFromStart);
                                Console.WriteLine("Bytes to label : " + BytesToLabel(label));
                                Console.WriteLine("OFFSET : " + offset);

                                if (offset < -128 || offset > 127)
                                {
                                    Console.WriteLine("Offset overflow");
                                }
                                else
                                {
                                    instructionCode = (UInt16)(instructionCode | offset);
                                }
                                AddToOutput(instructionCode);
                                Console.WriteLine(instructionCode);
                                Console.WriteLine();
                            }
                        }
                        // Instr cu 1 op 
                        else
                        {
                            // R7
                            if (words[1].StartsWith("R"))
                            {
                                mad = 1;
                                UInt16 indexDestination = UInt16.Parse(words[1].Substring(1));

                                instructionCode = (UInt16)(instructionCode | indexDestination);
                                instructionCode = (UInt16)(instructionCode | (mad << 4));
                                
                                AddToOutput(instructionCode);
                                Console.WriteLine(instructionCode);
                                Console.WriteLine();
                            }
                            // (R7)
                            else if (words[1].StartsWith("(") && words[1].EndsWith(")"))
                            {
                                string registru = words[1].Trim('(', ')');
                                mad = 2;
                                UInt16 indexDestination = UInt16.Parse(registru.Substring(1));

                                instructionCode = (UInt16)(instructionCode | indexDestination);
                                instructionCode = (UInt16)(instructionCode | (mad << 4));

                                AddToOutput(instructionCode);
                                Console.WriteLine(instructionCode);
                                Console.WriteLine();
                            }
                            // 15(R7), (R7)15
                            else if (words[1].Contains("("))
                            {
                                mad = 3;
                                UInt16 indexDestination = 0;
                                Int16 index = 0;
                                if (words[1].IndexOf('(') == 0)
                                {
                                    string[] registerAndIndex = words[1].Split(')');
                                    indexDestination = UInt16.Parse(registerAndIndex[0].Substring(2));
                                    index = Int16.Parse(registerAndIndex[1]);
                                }
                                else
                                {
                                    string[] registerAndIndex = words[1].Split('(', ')');
                                    indexDestination = UInt16.Parse(registerAndIndex[1].Substring(1));
                                    index = Int16.Parse(registerAndIndex[0]);
                                }

                                instructionCode = (UInt16)(instructionCode | indexDestination);
                                instructionCode = (UInt16)(instructionCode | (mad << 4));

                                AddToOutput(instructionCode, index);
                                Console.WriteLine(instructionCode);
                                Console.WriteLine(index);
                                Console.WriteLine();
                            }
                            // 17, 17H, label, proc
                            else
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(words[1], @"^\d+H?$"))
                                {
                                    mad = 0;
                                    Int16 operand = ResolveImmediateOperand(words[1]);
                                    instructionCode = (UInt16)(instructionCode | (mad << 4));

                                    AddToOutput(instructionCode, operand);
                                    Console.WriteLine(instructionCode);
                                    Console.WriteLine(operand);
                                    Console.WriteLine();
                                }
                                else
                                {
                                    if (words[0] == "JMP")
                                    {
                                        string label = words[1] + ":";
                                        if (BytesToLabel(label) != -1)
                                        {
                                            var adrOperand = BytesToLabel(label);
                                            Console.WriteLine("Bytes to label : " + BytesToLabel(label));
                                            mad = 0;
                                            instructionCode = (UInt16)(instructionCode | (mad << 4));

                                            AddToOutput(instructionCode, adrOperand);
                                            Console.WriteLine(instructionCode);
                                            Console.WriteLine(adrOperand);
                                            Console.WriteLine();
                                        }
                                    }
                                    else if (words[0] == "CALL")
                                    {
                                        if (BytesToProc(words[1]) != -1)
                                        {
                                            var adrOperand = BytesToProc(words[1]);
                                            Console.WriteLine("Bytes to proc : " + adrOperand);
                                            mad = 0;
                                            instructionCode = (UInt16)(instructionCode | (mad << 4));

                                            AddToOutput(instructionCode, adrOperand);
                                            Console.WriteLine(instructionCode);
                                            Console.WriteLine(adrOperand);
                                            Console.WriteLine();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (words.Length == 1)
                    {
                        AddToOutput(instructionCode);
                        Console.WriteLine(instructionCode);
                        Console.WriteLine();
                    }
                    if (mas == 0 || mas == 3)
                    {
                        m_bytesFromStart += 2;
                    }
                    if (mad == 0 || mad == 3)
                    {
                        m_bytesFromStart += 2;
                    }
                }
                else
                {
                    Console.WriteLine("NU INSTR: " + line);
                    Console.WriteLine();
                }
                lineNumber++;
            }

            File.WriteAllBytes(m_outputFilePath, m_output.ToArray());
        }
    
        private Int16 BytesToLabel(string label)
        {
            string[] lines = File.ReadAllLines(m_inputFilePath);
            StringCollection instructions = new StringCollection();
            instructions.AddRange(m_instructions);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].ToUpper();
                lines[i] = lines[i].Trim();
            }

            Int16 bytes = 0;
            bool labelFound = false;
            foreach (string line in lines)
            {
                char[] delimiters = { ',', ' ', '\t' };
                string[] words = line.Split(delimiters);

                foreach (string word in words)
                {
                    // if it is instruction +2
                    if (instructions.Contains(word))
                    {
                        bytes += 2;
                    }
                    // matches (R3)17, 17(R2), 13, 15H.
                    else if (System.Text.RegularExpressions.Regex.IsMatch(word, @"^\d+H?|\(R\d+\)\d+H?$"))
                    {
                        bytes += 2;
                    }
                    else if (word == label)
                    {
                        labelFound = true;
                        break;
                    }
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(line, @"^(JMP|CALL)\s+\w+"))
                {
                    bytes += 2;
                }
                if (labelFound)
                {
                    break;
                }
            }

            if (labelFound)
            {
                return bytes;
            }
            else
            {
                return -1;
            }
        }

        private Int16 BytesToProc(string proc)
        {
            string[] lines = File.ReadAllLines(m_inputFilePath);
            StringCollection instructions = new StringCollection();
            instructions.AddRange(m_instructions);

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].ToUpper();
                lines[i] = lines[i].Trim();
            }

            Int16 bytes = 0;
            bool procFound = false;
            foreach (string line in lines)
            {
                char[] delimiters = { ',', ' ', '\t' };
                string[] words = line.Split(delimiters);

                if (line == "PROC " + proc)
                {
                    procFound = true;
                    break;
                }
                // matches JMP label, CALL proc
                if (System.Text.RegularExpressions.Regex.IsMatch(line, @"^(JMP|CALL)\s+\w+"))
                {
                    bytes += 2;
                }
                foreach (string word in words)
                {
                    // if it is instruction +2
                    if (instructions.Contains(word))
                    {
                        bytes += 2;
                    }
                    // matches (R3)17, 17(R2), 13, 15H.
                    else if (System.Text.RegularExpressions.Regex.IsMatch(word, @"^\d+H?|\(R\d+\)\d+H?$"))
                    {
                        bytes += 2;
                    }
                }
            }

            if (procFound)
            {
                return bytes;
            }
            else
            {
                return -1;
            }
        }

        private void AddToOutput(UInt16 instructionCode, Int16 operand1 = Int16.MaxValue, Int16 operand2 = Int16.MaxValue)
        {
            byte high, low;

            high = (byte)(instructionCode >> 8);
            low = (byte)(instructionCode & 0x00FF);
            m_output.Add(high);
            m_output.Add(low);

            if (operand1 != Int16.MaxValue)
            {
                high = (byte)(operand1 >> 8);
                low = (byte)(operand1 & 0x00FF);
                m_output.Add(high);
                m_output.Add(low);
            }

            if (operand2 != Int16.MaxValue)
            {
                high = (byte)(operand2 >> 8);
                low = (byte)(operand2 & 0x00FF);
                m_output.Add(high);
                m_output.Add(low);
            }
        }

        // Prelucreaza o valoare imediata data in decimal/hexa si returneaza valoare in decimal
        private Int16 ResolveImmediateOperand(string operand)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(operand, @"^\d+H$"))
            {
                string operandStr = operand.Remove(operand.Length - 1, 1);
                return Convert.ToInt16(operandStr, 16);

            }
            // decimal immediate value
            else if (System.Text.RegularExpressions.Regex.IsMatch(operand, @"^\d+$"))
            {
                return Convert.ToInt16(operand, 10);
            }
            return Int16.MaxValue;
        }
    }
}
