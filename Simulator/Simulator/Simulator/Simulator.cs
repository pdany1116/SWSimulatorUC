using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    

    class Simulator
    {
        public bool compiled = false;

        private byte[] instructionMemory = new byte[10000]; // instruction memory
        private byte[] dataMemory = new byte[10000];
        private Stack<byte> stack = new Stack<byte>();

        public static byte FLAG;
        public static ushort PC, DBUS, SP, T, IVR, ADR, MDR, IR, RBUS, SBUS, ALU;
        public static ushort []Register = new ushort[16];
       
        // public static bool PdPC, 
        // all for current instruction in the program
        public static ushort instructionClass; // 0 - 2 operand, 1 - 1 operand, 2 - jump/branch, 3 - others
        public static ushort opcode;
        public static ushort mas;
        public static ushort sourceRegister;
        public static ushort mad;
        public static ushort destinationRegister;
        public static ushort offset;

        public static ushort immediateOperand;
        public static ushort index;

        public static ushort currentPhase;  // 0 - no phase, 1 - instruction fetch, 2 - operand fetch, 3 - execution, 4 - interrupt
        public static ushort currentImpulse; // from 1 to n 

        // conditions for phase generator
        public bool TINT, TIF, TOF, TEX;

        public static ushort instructionCode;

        public Simulator(string file)
        {
           
            instructionMemory = File.ReadAllBytes(file);
            PC = 0;
            compiled = true;

        }


        public void decoder(ushort instructionCode)
        {
            
            if( (instructionCode >> 15) == 0 )
            {
                //instructiune cu 2 operanzi
                instructionClass = 0;

                opcode = (ushort)(instructionCode >> 12);
                mas = (ushort)((ushort)(instructionCode >> 10) & 3);
                sourceRegister = (ushort)((ushort)(instructionCode >> 6) & 15);
                mad = (ushort)((ushort)(instructionCode >> 4) & 3);
                destinationRegister = (ushort)(instructionCode & 15);
            }
            else if( (instructionCode>>15) == 1 )
            {
                //instructiune cu un operand
                instructionClass = 1;

                opcode = (ushort)(instructionCode >> 6);
                mad = (ushort)((ushort)(instructionCode >> 4) & 3);
                destinationRegister = (ushort)(instructionCode & 15);
            }
            else if( (instructionCode>>14) == 3)
            {
                //instructiune salt
                instructionClass = 2;

                opcode = (ushort)(instructionCode >> 8);
                offset = (ushort)(instructionCode & 127);
            }
            else if( (instructionCode>>13) == 7)
            {
                //instructiune diversa
                instructionClass = 3;

                opcode = instructionCode;

            }
        }


        public void phase_generator()
        {
            
            if(TINT == true && TIF == false && TOF == false && TEX == false)
            {
                currentPhase = 4;
                currentImpulse = 1;
                TINT = false;
            }
            else if( TIF == true && TOF == false && TEX == false && TINT == false)
            {
                currentPhase = 1;
                currentImpulse = 1; 
                TIF = false;

            }else if (TOF == true && TEX == false && TINT == false && TIF == false)
            {
                currentPhase = 2;
                currentImpulse = 1;
                TOF = false;
            }
            else if(TEX == true && TIF == false && TOF == false && TINT == false)
            {
                currentPhase = 3;
                currentImpulse = 1;
                TEX = false;
            }
            
        }

        
        public void impulseGenerator()
        {
            if(currentPhase == 0)
            {
                currentPhase = 1;

            }

            switch (currentPhase)
            {
                // IF
                case 1:
                    if (currentImpulse == 0)
                    {
                        currentImpulse = 1;
                    }
                    else if(currentImpulse == 1)
                    {
                        
                        DBUS = PC; // PdPC
                        ALU = DBUS; // DBUS
                        RBUS = ALU; // PdALU
                        ADR = RBUS; // PmAdr

                        currentImpulse = 2;
                    }
                    else if(currentImpulse == 2)
                    {
                        
                        instructionCode = (ushort)((ushort)((ushort)instructionMemory[PC] << 8) | (ushort)instructionMemory[PC + 1]); // RD 
                        RBUS = instructionCode; // PdMem
                        IR = RBUS; //PmIR
                        decoder(IR);
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                        
                    }
                    else if(currentImpulse == 3)
                    {
                        
                        switch (instructionClass)
                        {
                            case 0:
                                TOF = true;
                                break;
                            case 1:
                                TOF = true;
                                break;
                            case 2:
                                TEX = true;
                                break;
                            case 3:
                                TEX = true;
                                break;
                        }

                        phase_generator();

                    }
                    break;
                // OF
                case 2:

                    switch (instructionClass)
                    {

                        case 0:

                            // 2 operands fetch
                            switch (mad)
                            {
                                case 1:
                                    OF_phase_destination_AD();
                                    break;
                                case 2:
                                    OF_phase_destination_AI();
                                    break;
                                case 3:
                                    OF_phase_destination_AX();
                                    break;
                            }
                            break;
                        case 1:
                            //1 operand fetch
                            // in ADR avem unde sa scriem in faza EX si in T valoarea initiala din memorie
                            switch (mad)
                            {
                                case 1:
                                    if(currentImpulse == 1)
                                    {
                                        DBUS = Register[destinationRegister]; //PdRG
                                        ALU = DBUS; // DBUS
                                        RBUS = ALU; // pdALU
                                        T = RBUS;  // PmT

                                        TEX = true;
                                        phase_generator();
                                    }
                                    break;
                                case 2:
                                    if (currentImpulse == 1)
                                    {
                                        DBUS = Register[destinationRegister]; //PdRG
                                        ALU = DBUS; // DBUS
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; //PmADR

                                        currentImpulse = 2;
                                    }
                                    else if (currentImpulse == 2)
                                    {
                                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                                        T = RBUS; //PmT

                                        TEX = true;
                                        phase_generator();
                                    }
                                    break;
                                case 3:
                                    if (currentImpulse == 1)
                                    {

                                        DBUS = PC; //pdPC
                                        ALU = DBUS; //DBUS
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; // PmAdr          

                                        currentImpulse = 2;
                                    }
                                    else if (currentImpulse == 2)
                                    {
                                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                                        RBUS = index; //PdMem
                                        T = RBUS; //PmT
                                        PC = (ushort)(PC + 2); //+2PC

                                        currentImpulse = 3;
                                    }
                                    else if (currentImpulse == 3)
                                    {
                                        DBUS = T; //pdT
                                        SBUS = Register[destinationRegister]; //pdRG
                                        ALU = (ushort)(DBUS + SBUS); //SUM
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; //pmADR

                                        currentImpulse = 4;

                                    }
                                    else if (currentImpulse == 4)
                                    {
                                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                                        T = RBUS; //PmT

                                        TEX = true;
                                        phase_generator();

                                    }

                                    break;

                            }

                            break;

                    }

                    break;
                //EX
                case 3:
                    if(currentImpulse == 1)
                    {
                        TIF = true;
                        phase_generator();
                    }

                    break;
                //TINT
                case 4:
                    
                    break;
            }


        }


        public void OF_phase_destination_AD()
        {

            switch (mas)
            {
                case 0:
                    if(currentImpulse == 1)
                    {

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        currentImpulse = 2;
         
                    }
                    else if(currentImpulse == 2)
                    {
                        immediateOperand = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = immediateOperand; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        TEX = true;
                        phase_generator();
                    }
                    break;
                case 1:
                    if (currentImpulse == 1)
                    {
                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; // pdALU
                        T = RBUS;  // PmT

                        TEX = true;
                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //PmADR

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        TEX = true;
                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr          

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[sourceRegister]; //pdRG
                        ALU = (ushort)(DBUS + SBUS); //SUM
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //pmADR

                        currentImpulse = 4;

                    }
                    else if(currentImpulse == 4)
                    {
                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        TEX = true;
                        phase_generator();

                    }

                    break;


            }

        }

        // pregatim in MDR unde trebuie sa scriem, adresa si in T ce trebuie sa scriem
        public void OF_phase_destination_AI()
        {
            switch (mas)
            {

                case 0:


                    if (currentImpulse == 1)
                    {
                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        immediateOperand = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = immediateOperand; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        TEX = true;
                        phase_generator();
                    }
                    break;
                case 1:


                    if (currentImpulse == 1)
                    {
                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {
                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; // pdALU
                        T = RBUS;  //PmT

                        TEX = true;
                        phase_generator();
                    }
                    break;
                case 2:


                    if (currentImpulse == 1)
                    {
                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {
                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //PmADR

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        TEX = true;
                        phase_generator();
                    }
                    break;

                case 3:
                    
                    if (currentImpulse == 1)
                    {
                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr          

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[sourceRegister]; //pdRG
                        ALU = (ushort)(DBUS + SBUS); //SUM
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //pmADR

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        TEX = true;
                        phase_generator();
                    }

                    break;

            }
                    

        }
        //pregatim in MDR adresa unde trebuie sa punem, si in T ce trebuie sa punem
        public void OF_phase_destination_AX()
        {
            switch (mas)
            {
                case 0:
                    if(currentImpulse == 1)
                    {
                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        currentImpulse = 2;
                    }
                    else if(currentImpulse == 2)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        immediateOperand = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = immediateOperand; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        TEX = true;
                        phase_generator();
                    }

                    break;
                case 1:
                    if (currentImpulse == 1)
                    {
                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; // pdALU
                        T = RBUS;  // PmT

                        TEX = true;
                        phase_generator();
                    }

                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //PmADR

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        TEX = true;
                        phase_generator();
                    }

                    break;
                case 3:
                    if (currentImpulse == 1)
                    {

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr          

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[sourceRegister]; //pdRG
                        ALU = (ushort)(DBUS + SBUS); //SUM
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //pmADR

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        RBUS = dataMemory[ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        currentImpulse = 6;
                    }
                    else if (currentImpulse == 6)
                    {
                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 7;
                    }
                    else if (currentImpulse == 7)
                    {
                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        TEX = true;
                        phase_generator();
                    }

                    break;


            }

        }





    }
}
