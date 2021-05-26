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

        #region memory
        public static byte[] instructionMemory = new byte[10000]; // instruction memory
        /// <summary>
        /// from 0 -> 10000 instruction memory
        /// from 10000 -> 60000 data memory
        /// the rest is stack memory
        /// </summary>
        public static byte[] Memory = new byte[65536];
        #endregion

        #region variables 
        public static byte FLAG; //7 - x, 6 - x, 5 - x, 4 - x, 3 - OF, 2 - Z, 1 - C, 0 - S
        public static ushort PC, DBUS, T, IVR, ADR, MDR, IR, RBUS, SBUS, ALU;
        public static ushort SP = 65535;
        public static ushort []Register = new ushort[16];
        

        //instructiuni logice elemanetare
        public static bool PdALU, PmFLAG, PmFLAGCond, PdFLAG_DBUS, PdFLAG_SBUS, PdRG_DBUS, PdRG_SBUS, PmRG, PmSP, PdSP_DBUS, PdSP_SBUS, PmT, PdT_DBUS, PdT_SBUS,
                    PdPC_SBUS, PdPC_DBUS, PmPC, PmADR, PdADR_DBUS, PdADR_SBUS, PmMDR, PdMDR_DBUS, PdMDR_SBUS, PmIR, PdIR_SBUS, PdIR_DBUS, RD, DDBUS, SSBUS, PdADR, WR,
                    PdMDR, PdIVR_DBUS, PdIVR_SBUS, PmIVR;
        #endregion

        #region informations from decoding
        // public static bool PdPC, 
        // all for current instruction in the program
        public static ushort instructionClass; // 0 - 2 operand, 1 - 1 operand, 2 - jump/branch, 3 - others
        public static ushort opcode;
        public static ushort mas;
        public static ushort sourceRegister;
        public static ushort mad;
        public static ushort destinationRegister;
        public static short offset;

        //indexes or operands after instruction codes in memory
        public static ushort immediateOperand;
        public static ushort index;
        public static ushort instructionCode;
        #endregion

        #region generators conditions
        //conditions for phase generator
        public bool TINT, TIF, TOF, TEX;

        public static ushort currentPhase;  // 0 - no phase, 1 - instruction fetch, 2 - operand fetch, 3 - execution, 4 - interrupt
        public static ushort currentImpulse; // from 1 to n 
        #endregion

        public Simulator(string file)
        {
           
            instructionMemory = File.ReadAllBytes(file);

            for(int i = 0; i < instructionMemory.Length; i++)
            {
                Memory[i] = instructionMemory[i];
            }

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
            else if( (instructionCode & (7 << 13)) == 32768)
            {
                //instructiune cu un operand
                instructionClass = 1;

                opcode = (ushort)(instructionCode >> 6);
                mad = (ushort)((ushort)(instructionCode >> 4) & 3);
                destinationRegister = (ushort)(instructionCode & 15);
            }
            else if( ((instructionCode & (7 << 13)) == 49152) || ((instructionCode & (255 << 8)) == 65280 ))
            {
                //instructiune salt
                instructionClass = 2;

                opcode = (ushort)(instructionCode >> 8);
                offset = (short)(instructionCode & 255);
            }
            else if( (instructionCode & (7 << 13)) == 57344)
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
                    #region IF impulses
                    if (currentImpulse == 0)
                    {
                        resetCLE();

                        currentImpulse = 1;
                    }
                    else if(currentImpulse == 1)
                    {
                        resetCLE();
                        
                        DBUS = PC; // PdPC
                        PdPC_DBUS = true;
                        ALU = DBUS; // DBUS
                        DDBUS = true;
                        RBUS = ALU; // PdALU
                        PdALU = true;
                        ADR = RBUS; // PmAdr
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if(currentImpulse == 2)
                    {
                        resetCLE();

                        instructionCode = (ushort)((ushort)((ushort)Memory[PC] << 8) | (ushort)Memory[PC + 1]); // RD 
                        IR = instructionCode; //PmIR
                        PmIR = true;
                        decoder(IR);
                        PC = (ushort)(PC + 2); //+2PC


                        currentImpulse = 3;
                        
                    }
                    else if(currentImpulse == 3)
                    {
                        resetCLE();

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
                    #endregion
                    break;
                // OF
                case 2:
                    #region OF impulses
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
                                case 0:
                                    if (currentImpulse == 1)
                                    {
                                        resetCLE();

                                        DBUS = PC; //pdPC
                                        ALU = DBUS; //DBUS
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; // PmAdr

                                        PdPC_DBUS = true;
                                        DDBUS = true;
                                        PdALU = true;
                                        PmADR = true;

                                        currentImpulse = 2;

                                    }
                                    else if (currentImpulse == 2)
                                    {
                                        resetCLE();

                                        immediateOperand = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); // RD 
                                        RBUS = immediateOperand; //PdMem
                                        T = RBUS; //PmT 
                                        PC = (ushort)(PC + 2); //+2PC

                                        PdADR = true;
                                        RD = true;
                                        PmT = true;


                                        TEX = true;
                                        phase_generator();
                                    }
                                    break;
                                case 1:
                                    if(currentImpulse == 1)
                                    {
                                        resetCLE();

                                        DBUS = Register[destinationRegister]; //PdRG
                                        ALU = DBUS; // DBUS
                                        RBUS = ALU; // pdALU
                                        T = RBUS;  // PmT

                                        PdRG_DBUS = true;
                                        DDBUS = true;
                                        PdALU = true;
                                        PmT = true;

                                        TEX = true;
                                        phase_generator();
                                    }
                                    break;
                                case 2:
                                    if (currentImpulse == 1)
                                    {
                                        resetCLE();

                                        DBUS = Register[destinationRegister]; //PdRG
                                        ALU = DBUS; // DBUS
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; //PmADR

                                        PdRG_DBUS = true;
                                        DDBUS = true;
                                        PdALU = true;
                                        PmADR = true;

                                        currentImpulse = 2;
                                    }
                                    else if (currentImpulse == 2)
                                    {
                                        resetCLE();

                                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                                        T = RBUS; //PmT

                                        RD = true;
                                        PmT = true;

                                        TEX = true;
                                        phase_generator();
                                    }
                                    break;
                                case 3:
                                    if (currentImpulse == 1)
                                    {
                                        resetCLE();

                                        DBUS = PC; //pdPC
                                        ALU = DBUS; //DBUS
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; // PmAdr

                                        PdPC_DBUS = true;
                                        DDBUS = true;
                                        PdALU = true;
                                        PmADR = true;


                                        currentImpulse = 2;
                                    }
                                    else if (currentImpulse == 2)
                                    {
                                        resetCLE();

                                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                                        RBUS = index; //PdMem
                                        T = RBUS; //PmT
                                        PC = (ushort)(PC + 2); //+2PC

                                        RD = true;
                                        PmT = true;

                                        currentImpulse = 3;
                                    }
                                    else if (currentImpulse == 3)
                                    {
                                        resetCLE();

                                        DBUS = T; //pdT
                                        SBUS = Register[destinationRegister]; //pdRG
                                        ALU = (ushort)(DBUS + SBUS); //SUM
                                        RBUS = ALU; //pdALU
                                        ADR = RBUS; //pmADR

                                        PdT_DBUS = true;
                                        PdRG_SBUS = true;
                                        SSBUS = true;
                                        DDBUS = true;
                                        PdALU = true;
                                        PmADR = true;

                                        currentImpulse = 4;

                                    }
                                    else if (currentImpulse == 4)
                                    {
                                        resetCLE();

                                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                                        T = RBUS; //PmT

                                        RD = true;
                                        PmT = true;

                                        TEX = true;
                                        phase_generator();

                                    }

                                    break;

                            }

                            break;

                    }
                    #endregion
                    break;
                //EX
                case 3:
                    #region EX impulses
                    switch (opcode)
                    {
                        case 0:
                            EX_phase_MOV();
                            break;
                        case 1:
                            EX_phase_ADD();
                            break;
                        case 2:
                            EX_phase_SUB();
                            break;
                        case 3:
                            EX_phase_CMP();
                            break;
                        case 4:
                            EX_phase_AND();
                            break;
                        case 5:
                            EX_phase_OR();
                            break;
                        case 6:
                            EX_phase_XOR();
                            break;
                        case 512:
                            EX_phase_CLR();
                            break;
                        case 513:
                            EX_phase_NEG();
                            break;
                        case 514:
                            EX_phase_INC();
                            break;
                        case 515:
                            EX_phase_DEC();
                            break;
                        case 516:
                            EX_phase_ASL();
                            break;
                        case 517:
                            EX_phase_ASR();
                            break;
                        case 518:
                            EX_phase_LSR();
                            break;
                        case 519:
                            EX_phase_ROL();
                            break;
                        case 520:
                            EX_phase_ROR();
                            break;
                        case 521:
                            EX_phase_RLC();
                            break;
                        case 522:
                            EX_phase_RRC();
                            break;
                        case 523:
                            EX_phase_JMP();
                            break;
                        case 524:
                            EX_phase_CALL();
                            break;
                        case 525:
                            EX_phase_PUSH();
                            break;
                        case 526:
                            EX_phase_POP();
                            break;
                        case 192:
                            EX_phase_BR();
                            break;
                        case 193:
                            EX_phase_BNE();
                            break;
                        case 194:
                            EX_phase_BEQ();
                            break;
                        case 195:
                            EX_phase_BPL();
                            break;
                        case 196:
                            EX_phase_BMI();
                            break;
                        case 197:
                            EX_phase_BCS();
                            break;
                        case 198:
                            EX_phase_BCC();
                            break;
                        case 199:
                            EX_phase_BVS();
                            break;
                        case 200:
                            EX_phase_BVC();
                            break;
                        case 57344:
                            EX_phase_CLC();
                            break;
                        case 57345:
                            EX_phase_CLV();
                            break;
                        case 57346:
                            EX_phase_CLZ();
                            break;
                        case 57347:
                            EX_phase_CLS();
                            break;
                        case 57348:
                            EX_phase_CCC();
                            break;
                        case 57349:
                            EX_phase_SEC();
                            break;
                        case 57350:
                            EX_phase_SEV();
                            break;
                        case 57351:
                            EX_phase_SEZ();
                            break;
                        case 57352:
                            EX_phase_SES();
                            break;
                        case 57353:
                            EX_phase_SCC();
                            break;
                        case 57354:
                            EX_phase_NOP();
                            break;
                        case 57355:
                            EX_phase_RET();
                            break;
                        case 57356:
                            EX_phase_RETI();
                            break;
                        case 57359:
                            EX_phase_PUSHPC();
                            break;
                        case 57360:
                            EX_phase_POPPC();
                            break;
                        case 57361:
                            EX_phase_PUSHFLAG();
                            break;
                        case 57362:
                            EX_phase_POPFLAG();
                            break;
                        default:
                            
                            if (currentImpulse == 1)
                            {
                                resetCLE();

                                TIF = true;
                                phase_generator();
                            }

                            break;

                    }
                    #endregion
                    break;
                //TINT
                case 4:
                    #region INT impulses
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        UInt16 RETIcommand = Convert.ToUInt16(57356);
                        Memory[30000] = (byte)(RETIcommand >> 8);
                        Memory[30001] = (byte)(RETIcommand);

                        IVR = 30000;
                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if(currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        ADR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = FLAG; //pdFLAGS_SBUS
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR

                        SP = (ushort)(SP - 2);


                        PdFLAG_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        currentImpulse = 4;

                    }
                    else if (currentImpulse == 4)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        ADR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 5;

                    }
                    else if(currentImpulse == 5)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC_SBUS
                        ALU = DBUS; //DDBUS
                        RBUS = ALU; //pdALU

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        currentImpulse = 6;

                    }
                    else if (currentImpulse == 6)
                    {
                        resetCLE();

                        DBUS = IVR; //pdivr dbus
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        PC = RBUS; //pmadr

                        PdIVR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        TIF = true;
                        phase_generator();
                    }
                    //else if(currentImpulse == 7)
                    //{
                    //    resetCLE();
                    //    RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                    //    PC = RBUS; //PmPC
                    //    var test = (ushort)((ushort)((ushort)Memory[30000] << 8) | (ushort)Memory[30000 + 1]);

                    //    RD = true;
                    //    PdADR = true;
                    //    PmPC = true;

                    //    TIF = true;
                    //    phase_generator();

                    //}
                    #endregion
                    break;

            }


        }

        #region OF functions for 2 operands
        public void OF_phase_destination_AD()
        {

            switch (mas)
            {
                case 0:
                    if(currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
         
                    }
                    else if(currentImpulse == 2)
                    {
                        resetCLE();

                        immediateOperand = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = immediateOperand; //PdMem
                        T = RBUS; //PmT 
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;


                        TEX = true;
                        phase_generator();
                    }
                    break;
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[sourceRegister]; //PdRG                      
                        ALU = DBUS; // DBUS                       
                        RBUS = ALU; // pdALU                       
                        T = RBUS;  // PmT

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //PmADR

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr          

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[sourceRegister]; //pdRG
                        ALU = (ushort)(DBUS + SBUS); //SUM
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //pmADR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 4;

                    }
                    else if(currentImpulse == 4)
                    {
                        resetCLE();

                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

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
                        resetCLE();

                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdRG_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        immediateOperand = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = immediateOperand; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }
                    break;
                case 1:


                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdRG_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; // pdALU
                        T = RBUS;  //PmT

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }
                    break;
                case 2:


                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdRG_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //PmADR

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }
                    break;

                case 3:
                    
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SBUS = Register[destinationRegister]; //pdRG
                        ALU = SBUS; //SBUS
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdRG_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 2;

                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr          

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[sourceRegister]; //pdRG
                        ALU = (ushort)(DBUS + SBUS); //SUM
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //pmADR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        resetCLE();

                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

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
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if(currentImpulse == 2)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        resetCLE();

                        immediateOperand = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = immediateOperand; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }

                    break;
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;


                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        resetCLE();

                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; // pdALU
                        T = RBUS;  // PmT

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }

                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT
                        PC = (ushort)(PC + 2); //+2PC

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        resetCLE();

                        DBUS = Register[sourceRegister]; //PdRG
                        ALU = DBUS; // DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //PmADR

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        resetCLE();

                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        TEX = true;
                        phase_generator();
                    }

                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[sourceRegister]; //pdRG
                        ALU = (ushort)(DBUS + SBUS); //SUM
                        RBUS = ALU; //pdALU
                        ADR = RBUS; //pmADR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;


                        currentImpulse = 4;
                    }
                    else if (currentImpulse == 4)
                    {
                        resetCLE();

                        RBUS = Memory[10000 + ADR]; //RD, pdMEM 
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        currentImpulse = 5;
                    }
                    else if (currentImpulse == 5)
                    {
                        resetCLE();

                        DBUS = PC; //pdPC
                        ALU = DBUS; //DBUS
                        RBUS = ALU; //pdALU
                        ADR = RBUS; // PmAdr   

                        PdPC_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 6;
                    }
                    else if (currentImpulse == 6)
                    {
                        resetCLE();

                        index = (ushort)((ushort)((ushort)instructionMemory[ADR] << 8) | (ushort)instructionMemory[ADR + 1]); // RD 
                        RBUS = index; //PdMem
                        T = RBUS; //PmT

                        PdADR = true;
                        RD = true;
                        PmT = true;

                        PC = (ushort)(PC + 2); //+2PC

                        currentImpulse = 7;
                    }
                    else if (currentImpulse == 7)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        SBUS = Register[destinationRegister]; // pdRG
                        ALU = (ushort)(SBUS + DBUS); //SUM
                        RBUS = ALU; //pdALU
                        MDR = RBUS; //pmMDR

                        PdT_DBUS = true;
                        PdRG_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        TEX = true;
                        phase_generator();
                    }

                    break;


            }

        }

        #endregion
              
        #region execution functions for 2 operands
        public void EX_phase_MOV()
        {
            switch (mad)
            {

                case 1:
                    if(currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T; //pdT
                        ALU = DBUS; //ddbus
                        RBUS = ALU; //PdALU
                        Register[destinationRegister] = RBUS; // pmRG

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if(Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if(currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T; // pdT
                        ALU = DBUS; //DDBUS
                        RBUS = ALU; //pdALU
                        Memory[10000 + MDR] = (byte)(RBUS >> 8);
                        Memory[10000 + MDR + 1] = (byte)(RBUS); //WR

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T; // pdT
                        ALU = DBUS; //DDBUS
                        RBUS = ALU; //pdALU
                        Memory[10000 + MDR] = (byte)(RBUS >> 8);
                        Memory[10000 + MDR + 1] = (byte)(RBUS); //WR

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                        break;

            }
        }

        
        public void EX_phase_ADD()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; // pdRG
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS + SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);
                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS + SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);


                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;
                        PmFLAGCond = true;


                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS + SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

            }

        }

        public void EX_phase_SUB()
            {
                switch (mad)
                {
                    case 1:
                        if (currentImpulse == 1)
                        {
                            resetCLE();

                            DBUS = Register[destinationRegister]; // pdRG
                            SBUS = T; //pdT
                            ALU = (ushort)(DBUS - SBUS); // ssbus ddbus
                            RBUS = ALU; //pdalu
                            Register[destinationRegister] = RBUS; //pmrg

                            setting_FLAG_bits(RBUS, DBUS, SBUS);

                            if (RBUS > DBUS)
                            {
                            FLAG = (byte)(FLAG | (1 << 3));
                            }
                            else
                            {
                            FLAG = (byte)(FLAG & ~(1 << 3));
                            }

                            PmFLAGCond = true;
                            PdRG_DBUS = true;
                            PdT_SBUS = true;
                            SSBUS = true;
                            DDBUS = true;
                            PdALU = true;
                            PmRG = true;

                            if (Form1.interruptCheck == true)
                            {
                                TINT = true;
                            }
                            else
                            {
                                TIF = true;
                            }

                            phase_generator();
                        }
                        break;

                    case 2:
                        if (currentImpulse == 1)
                        {
                            resetCLE();

                            DBUS = MDR; // pdMDR
                            ALU = DBUS; // ddbus
                            RBUS = ALU; //pdalu
                            ADR = RBUS; // pmadr

                            PdMDR_DBUS = true;
                            DDBUS = true;
                            PdALU = true;
                            PmADR = true;

                            currentImpulse = 2;
                        }
                        else if (currentImpulse == 2)
                        {
                            resetCLE();
                            
                            RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                            IVR = RBUS; //pmIVR

                            RD = true;
                            PdADR = true;
                            PmIVR = true;

                            currentImpulse = 3;
                        }
                        else if (currentImpulse == 3)
                        {
                            resetCLE();

                            DBUS = IVR; //pdIVR
                            SBUS = T; //pdT
                            ALU = (ushort)(DBUS - SBUS); // ssbus ddbus
                            RBUS = ALU; //pdalu

                            setting_FLAG_bits(RBUS, DBUS, SBUS);
                            PmFLAGCond = true;

                            if (RBUS > DBUS)
                            {
                            FLAG = (byte)(FLAG | (1 << 3));
                            }
                            else
                            {
                            FLAG = (byte)(FLAG & ~(1 << 3));
                            }

                            Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                            Memory[10000 + MDR + 1] = (byte)(RBUS);

                            PdIVR_DBUS = true;
                            PdT_SBUS = true;
                            SSBUS = true;
                            DDBUS = true;
                            PdALU = true;
                            WR = true;
                            PdMDR = true;

                            if (Form1.interruptCheck == true)
                            {
                                TINT = true;
                            }
                            else
                            {
                                TIF = true;
                            }

                            phase_generator();
                        }
                        break;

                    case 3:
                        if (currentImpulse == 1)
                        {
                            resetCLE();

                            DBUS = MDR; // pdMDR
                            ALU = DBUS; // ddbus
                            RBUS = ALU; //pdalu
                            ADR = RBUS; // pmadr

                            PdMDR_DBUS = true;
                            DDBUS = true;
                            PdALU = true;
                            PmADR = true;

                            currentImpulse = 2;
                        }
                        else if (currentImpulse == 2)
                        {
                            resetCLE();

                            RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                            IVR = RBUS; //pmIVR

                            RD = true;
                            PdADR = true;
                            PmIVR = true;

                            currentImpulse = 3;
                        }
                        else if (currentImpulse == 3)
                        {
                            resetCLE();

                            DBUS = IVR; //pdIVR
                            SBUS = T; //pdT
                            ALU = (ushort)(DBUS + SBUS); // ssbus ddbus
                            RBUS = ALU; //pdalu

                            setting_FLAG_bits(RBUS, DBUS, SBUS);
                            PmFLAGCond = true;

                            if (RBUS > DBUS)
                            {
                            FLAG = (byte)(FLAG | (1 << 3));
                            }
                            else
                            {
                            FLAG = (byte)(FLAG & ~(1 << 3));
                            }

                            Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                            Memory[10000 + MDR + 1] = (byte)(RBUS);

                            PdIVR_DBUS = true;
                            PdT_SBUS = true;
                            SSBUS = true;
                            DDBUS = true;
                            PdALU = true;
                            WR = true;
                            PdMDR = true;

                            if (Form1.interruptCheck == true)
                            {
                                TINT = true;
                            }
                            else
                            {
                                TIF = true;
                            }

                            phase_generator();
                        }
                        break;

                }
            }

        public void EX_phase_AND()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; // pdRG
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS & SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);
                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS & SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);


                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;
                        PmFLAGCond = true;


                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS & SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

            }

        }

        public void EX_phase_OR()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; // pdRG
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS | SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);
                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS | SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);


                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;
                        PmFLAGCond = true;


                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS | SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

            }

        }

        public void EX_phase_XOR()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; // pdRG
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS ^ SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);
                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS ^ SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);


                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;
                        PmFLAGCond = true;


                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS ^ SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + MDR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + MDR + 1] = (byte)(RBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

            }

        }

        public void EX_phase_CMP()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; // pdRG
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS - SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);
                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS - SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmFLAGCond = true;


                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = MDR; // pdMDR
                        ALU = DBUS; // ddbus
                        RBUS = ALU; //pdalu
                        ADR = RBUS; // pmadr

                        PdMDR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        IVR = RBUS; //pmIVR

                        RD = true;
                        PdADR = true;
                        PmIVR = true;

                        currentImpulse = 3;
                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        DBUS = IVR; //pdIVR
                        SBUS = T; //pdT
                        ALU = (ushort)(DBUS ^ SBUS); // ssbus ddbus
                        RBUS = ALU; //pdalu

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        PdIVR_DBUS = true;
                        PdT_SBUS = true;
                        SSBUS = true;
                        DDBUS = true;
                        PdALU = true;


                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;

            }

        }

        #endregion

        #region execution functions for 1 operand

        private void EX_phase_CLR()
        {
            switch (mad)
            {
                case 1:
                    if(currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS & 0); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if(currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS & 0);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS & 0);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_NEG()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(~DBUS); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(~DBUS);
                        RBUS = ALU;

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(~DBUS);
                        RBUS = ALU;

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_INC()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS + 1); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS + 1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS + 1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_DEC()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS - 1); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS - 1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS - 1);
                        RBUS = ALU;
                        
                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_ASL() // asl = lsl
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS<<1); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));
                       
                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS<<1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS<<1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_ASR()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)((DBUS>>1) | (DBUS & 1<<15)); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(ushort)((DBUS >> 1) | (DBUS & 1 << 15));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(ushort)((DBUS >> 1) | (DBUS & 1 << 15));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_LSR()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS >> 1); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS >> 1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS >> 1);
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_ROL() 
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS << 1 | (DBUS & 1)); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS << 1 | (DBUS & 1));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS << 1 | (DBUS & 1));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_ROR()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS >> 1 | ((DBUS & 1) << 15)); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS >> 1 | ((DBUS & 1) << 15));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)((DBUS >> 1) | ((DBUS & 1) << 15));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_RLC()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS << 1 | ((FLAG & 2) >> 1)); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS << 1 | ((FLAG & 2) >> 1)); ;
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS << 1 | ((FLAG & 2) >> 1));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1 << 15)) >> 14));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_RRC()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = Register[destinationRegister]; //pdRG
                        ALU = (ushort)(DBUS >> 1 | ((FLAG & 2) << 14)); //ddbus
                        RBUS = ALU; //pdalu
                        Register[destinationRegister] = RBUS; //pmrg

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS >> 1 | ((FLAG & 2) << 14));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        DBUS = T;
                        ALU = (ushort)(DBUS >> 1 | ((FLAG & 2) << 14));
                        RBUS = ALU;

                        setting_FLAG_bits(RBUS, DBUS, SBUS);

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        FLAG = (byte)(FLAG | ((DBUS & (1)) << 1));

                        PmFLAGCond = true;
                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }


        }

        private void EX_phase_PUSH()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        ADR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = Register[destinationRegister]; //pdFLAGS_SBUS
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR

                        SP = (ushort)(SP - 2);


                        PdRG_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        ADR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = T; //pdT
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR

                        SP = (ushort)(SP - 2);


                        PdT_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        ADR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmADR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = T; //pdT
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR

                        SP = (ushort)(SP - 2);


                        PdT_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
            }

            
        
        }

        private void EX_phase_POP()
        {
            switch (mad)
            {
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SBUS = SP; //pdSP
                        ALU = SBUS; //SSBUS
                        RBUS = ALU; // pdALU
                        ADR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmADR = true;


                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                        Register[destinationRegister] = RBUS; //PmPC

                        SP = (ushort)(SP + 2);

                        RD = true;
                        PdADR = true;
                        PmRG = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();

                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SBUS = SP; //pdSP
                        ALU = SBUS; //SSBUS
                        RBUS = ALU; // pdALU
                        MDR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;


                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[MDR] << 8) | (ushort)Memory[MDR + 1]); //RD, pdADR
                        T = RBUS; //PmPC

                        SP = (ushort)(SP + 2);

                        RD = true;
                        PdADR = true;
                        PmT = true;
       

                    }
                    else if(currentImpulse == 3)
                    {
                        DBUS = T;
                        ALU = DBUS;
                        RBUS = ALU;

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SBUS = SP; //pdSP
                        ALU = SBUS; //SSBUS
                        RBUS = ALU; // pdALU
                        MDR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;


                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        RBUS = (ushort)((ushort)((ushort)Memory[MDR] << 8) | (ushort)Memory[MDR + 1]); //RD, pdADR
                        T = RBUS; //PmPC

                        SP = (ushort)(SP + 2);

                        RD = true;
                        PdADR = true;
                        PmT = true;


                    }
                    else if (currentImpulse == 3)
                    {
                        DBUS = T;
                        ALU = DBUS;
                        RBUS = ALU;

                        Memory[10000 + ADR] = (byte)(RBUS >> 8); //WR, pdMDR
                        Memory[10000 + ADR + 1] = (byte)(RBUS);

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        WR = true;
                        PdADR = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }



        }

        private void EX_phase_JMP()
        {
            switch (mad)
            {
                case 0:
                    if( currentImpulse == 1)
                    {
                        DBUS = T;
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 1:
                    if (currentImpulse == 1)
                    {
                        DBUS = Register[destinationRegister];
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        DBUS = ADR;
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdADR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        DBUS = ADR;
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdADR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }

        }

        private void EX_phase_CALL()
        {
            switch (mad)
            {
                case 0:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        MDR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = PC; //pdFLAGS_SBUS
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR


                        PdPC_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        currentImpulse = 4;

                    }
                    if (currentImpulse == 4)
                    {
                        DBUS = T;
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdT_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 1:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        MDR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = PC; //pdFLAGS_SBUS
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR


                        PdPC_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        currentImpulse = 4;

                    }
                    if (currentImpulse == 4)
                    {
                        DBUS = Register[destinationRegister];
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdRG_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 2:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        MDR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = PC; //pdFLAGS_SBUS
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR


                        PdPC_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        currentImpulse = 4;

                    }
                    if (currentImpulse == 4)
                    {
                        DBUS = ADR;
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdADR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
                case 3:
                    if (currentImpulse == 1)
                    {
                        resetCLE();

                        SP = (ushort)(SP - 2);

                        currentImpulse = 2;
                    }
                    else if (currentImpulse == 2)
                    {
                        resetCLE();

                        SBUS = SP; // pdsp
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu
                        MDR = RBUS; //pmADR

                        PdSP_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        PmMDR = true;

                        currentImpulse = 3;

                    }
                    else if (currentImpulse == 3)
                    {
                        resetCLE();

                        SBUS = PC; //pdFLAGS_SBUS
                        ALU = SBUS; //ssbus
                        RBUS = ALU; // pdalu

                        Memory[SP] = (byte)(RBUS >> 8);
                        Memory[SP + 1] = (byte)(RBUS); //WR


                        PdPC_SBUS = true;
                        SSBUS = true;
                        PdALU = true;
                        WR = true;
                        PdMDR = true;

                        currentImpulse = 4;

                    }
                    if (currentImpulse == 4)
                    {
                        DBUS = ADR;
                        ALU = DBUS;
                        RBUS = ALU;
                        PC = RBUS;

                        PdADR_DBUS = true;
                        DDBUS = true;
                        PdALU = true;
                        PmPC = true;

                        if (Form1.interruptCheck == true)
                        {
                            TINT = true;
                        }
                        else
                        {
                            TIF = true;
                        }

                        phase_generator();
                    }
                    break;
            }

        }
        #endregion

        #region functions for branch

        private void EX_phase_BR()
        {

            if(currentImpulse == 1)
            {
                resetCLE();

                SBUS = IR; //pdIR
                ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                RBUS = ALU; //pdalu
                T = RBUS;

                PdIR_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmT = true;

                currentImpulse = 2;
            }
            else if(currentImpulse == 2)
            {
                resetCLE();

                DBUS = PC; //pdPC
                SBUS = T; //pdT
                if (offset > 127)
                {
                    ALU = (ushort)(DBUS + (offset - 256));
                }
                else
                {
                    ALU = (ushort)(DBUS + offset);
                }
                RBUS = ALU; //pdalu
                PC = ALU;

                PdPC_DBUS = true;
                PdT_SBUS = true;
                SSBUS = true;
                DDBUS = true;
                PdALU = true;
                PmPC = true;


                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }


        private void EX_phase_BNE()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if((FLAG & (1<<2)) == 0)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;

                    
                }
                currentImpulse = 2;

            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1 << 2)) == 0)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if(offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_BEQ()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1 << 2)) == 4)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;

                    
                }
                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1 << 2)) == 4)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }


        private void EX_phase_BPL()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1)) == 0)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;

                   

                }
                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1)) == 0)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_BMI()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1)) == 1)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;

                    
                }
                currentImpulse = 2;

            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1)) == 1)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }


        private void EX_phase_BCS()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1<<1)) == 2)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;

                   
                }
                currentImpulse = 2;

            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1<<1)) == 2)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_BCC()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1<<1)) == 0)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;
                }

                currentImpulse = 2;

            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1<<1)) == 0)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_BVS()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1<<3)) == 8)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;
                }

                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1<<3)) == 8)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_BVC()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                if ((FLAG & (1<<3)) == 0)
                {
                    SBUS = IR; //pdIR
                    ALU = (ushort)(SBUS & 255); //(SBUS & 255) offset
                    RBUS = ALU; //pdalu
                    T = RBUS;

                    PdIR_SBUS = true;
                    SSBUS = true;
                    PdALU = true;
                    PmT = true;
                }

                currentImpulse = 2;

            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                if ((FLAG & (1<<3)) == 0)
                {
                    DBUS = PC; //pdPC
                    SBUS = T; //pdT
                    if (offset > 127)
                    {
                        ALU = (ushort)(DBUS + (offset - 256));
                    }
                    else
                    {
                        ALU = (ushort)(DBUS + offset);
                    }
                    RBUS = ALU; //pdalu
                    PC = ALU;

                    PdPC_DBUS = true;
                    PdT_SBUS = true;
                    SSBUS = true;
                    DDBUS = true;
                    PdALU = true;
                    PmPC = true;

                }

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        #endregion

        #region functions for others

        public void EX_phase_CLC()
        {
            if(currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG & ~(1 << 1)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

               
                phase_generator();

            }

        }

        public void EX_phase_CLZ()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG & ~(1 << 2)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                
                phase_generator();

            }

        }

        //overflow
        public void EX_phase_CLV() 
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG & ~(1 << 3)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_CLS()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG & ~(1)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_SEC()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG | (1 << 1)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_SEZ()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG | (1 << 2)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }
        //overflow
        public void EX_phase_SEV()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG | (1 << 3)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_SES()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG | (1)); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_SCC()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG | 15); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_CCC()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                DBUS = FLAG; //pdFlag
                ALU = DBUS; // DDBUS

                FLAG = (byte)(FLAG & 0); //pmFlagCond

                PdFLAG_DBUS = true;
                DDBUS = true;
                PmFLAGCond = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_NOP()
        {
            if(currentImpulse == 1)
            {
                resetCLE();

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_RETI()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                Form1.interruptCheck = false;

                SBUS = SP; //pdSP
                ALU = SBUS; //SSBUS
                RBUS = ALU; // pdALU
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;

                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                PC = RBUS; //PmPC

                SP = (ushort)(SP + 2);

                RD = true;
                PdADR = true;
                PmPC = true;

                currentImpulse = 3;

            }
            else if (currentImpulse == 3)
            {
                resetCLE();

                SBUS = SP; //pdSP
                ALU = SBUS; //SSBUS
                RBUS = ALU; // pdALU
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;

                currentImpulse = 4;

            }
            else if (currentImpulse == 4)
            {
                resetCLE();

                RBUS = (ushort)((ushort)((ushort)Memory[SP] << 8) | (ushort)Memory[SP + 1]); //RD, pdADR
                FLAG = (byte)RBUS; //PmFLAG

                SP = (ushort)(SP + 2);

                RD = true;
                PdADR = true;
                PmFLAG = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();
            }
        }

        public void EX_phase_RET()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                SBUS = SP; //pdSP
                ALU = SBUS; //SSBUS
                RBUS = ALU; // pdALU
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;

                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                PC = RBUS; //PmPC

                SP = (ushort)(SP + 2);

                RD = true;
                PdADR = true;
                PmPC = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }
        }

        private void EX_phase_PUSHPC()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                SP = (ushort)(SP - 2);

                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                SBUS = SP; // pdsp
                ALU = SBUS; //ssbus
                RBUS = ALU; // pdalu
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;

                currentImpulse = 3;

            }
            else if (currentImpulse == 3)
            {
                resetCLE();

                SBUS = PC; //pdFLAGS_SBUS
                ALU = SBUS; //ssbus
                RBUS = ALU; // pdalu

                Memory[SP] = (byte)(RBUS >> 8);
                Memory[SP + 1] = (byte)(RBUS); //WR

              


                PdPC_SBUS = true;
                SSBUS = true;
                PdALU = true;
                WR = true;
                PdADR = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        private void EX_phase_PUSHFLAG()
        {

            if (currentImpulse == 1)
            {
                resetCLE();

                SP = (ushort)(SP - 2);

                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                SBUS = SP; // pdsp
                ALU = SBUS; //ssbus
                RBUS = ALU; // pdalu
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;

                currentImpulse = 3;

            }
            else if (currentImpulse == 3)
            {
                resetCLE();

                SBUS = FLAG; //pdFLAGS_SBUS
                ALU = SBUS; //ssbus
                RBUS = ALU; // pdalu

                Memory[SP] = (byte)(RBUS >> 8);
                Memory[SP + 1] = (byte)(RBUS); //WR



                PdFLAG_SBUS = true;
                SSBUS = true;
                PdALU = true;
                WR = true;
                PdADR = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }

        }

        public void EX_phase_POPPC()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                SBUS = SP; //pdSP
                ALU = SBUS; //SSBUS
                RBUS = ALU; // pdALU
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;


                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                PC = RBUS; //PmPC

                SP = (ushort)(SP + 2);

                RD = true;
                PdADR = true;
                PmPC = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }
        }

        public void EX_phase_POPFLAG()
        {
            if (currentImpulse == 1)
            {
                resetCLE();

                SBUS = SP; //pdSP
                ALU = SBUS; //SSBUS
                RBUS = ALU; // pdALU
                ADR = RBUS; //pmADR

                PdSP_SBUS = true;
                SSBUS = true;
                PdALU = true;
                PmADR = true;

                currentImpulse = 2;
            }
            else if (currentImpulse == 2)
            {
                resetCLE();

                RBUS = (ushort)((ushort)((ushort)Memory[ADR] << 8) | (ushort)Memory[ADR + 1]); //RD, pdADR
                FLAG = (byte)RBUS; //PmPC

                SP = (ushort)(SP + 2);

                RD = true;
                PdADR = true;
                PmFLAG = true;

                if (Form1.interruptCheck == true)
                {
                    TINT = true;
                }
                else
                {
                    TIF = true;
                }

                phase_generator();

            }
        }

        #endregion

        private void setting_FLAG_bits(ushort RBUS, ushort DBUS, ushort SBUS)
        {

            if (RBUS == 0)
            {
                FLAG = (byte)(FLAG | (1 << 2));
            }
            else
            {
                FLAG = (byte)(FLAG & ~(1 << 2));
            }

            if (RBUS < DBUS)
            {
                FLAG = (byte)(FLAG | (1 << 3));
            }
            else
            {
                FLAG = (byte)(FLAG & ~(1 << 3));
            }

            if (RBUS >> 15 == 1)
            {
                FLAG = (byte)(FLAG | 1);
            }
            else
            {
                FLAG = (byte)(FLAG & ~1);
            }

            if (SBUS + DBUS > 65535)
            {
                FLAG = (byte)(FLAG | (1 << 1));
            }
            else
            {
                FLAG = (byte)(FLAG & ~(1 << 1));
            }

        }

        //resetam comenzile logice elementare
        public void resetCLE()
        {

            PdALU = false;
            PmFLAG = false;
            PmFLAGCond = false;
            PdFLAG_DBUS = false;
            PdFLAG_SBUS = false;
            PdRG_DBUS = false;
            PdRG_SBUS = false;
            PmRG = false;
            PmSP = false;
            PdSP_DBUS = false;
            PdSP_SBUS = false;
            PmT = false;
            PdT_DBUS = false;
            PdT_SBUS = false;
            PdPC_SBUS = false;
            PdPC_DBUS = false;
            PmPC = false;
            PmADR = false;
            PdADR_DBUS = false;
            PdADR_SBUS = false;
            PmMDR = false;
            PdMDR_DBUS = false;
            PdMDR_SBUS = false;
            PmIR = false;
            PdIR_SBUS = false;
            PdIR_DBUS = false;
            RD = false;
            DDBUS = false;
            SSBUS = false;
            PdIVR_DBUS = false;
            PdIVR_SBUS = false;
            WR = false;
            PmFLAGCond = false;

        }
    }
}
