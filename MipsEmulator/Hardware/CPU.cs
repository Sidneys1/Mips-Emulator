using System;
using System.ComponentModel;
using MipsEmulator.Instructions;
using MipsEmulator.Instructions.Jump;
using MipsEmulator.Instructions.Reg;

//using MipsEmulator.Instructions.Immediate;

namespace MipsEmulator.Hardware {
    internal class CPU : INotifyPropertyChanged {
        public Memory MainMemory;

        public CPU() {
            MainMemory = new Memory(Constants.LastMemoryAddress);

            for (var i = 0; i < Registers.Length; i++)
                Registers[i] = new MemoryItem();

            CurrentRunLevel = RunLevel.Code;
            Registers[(uint) Register.TrapStatus].Value =
                (uint) (InterruptStatus.SysCall | InterruptStatus.Ofv | InterruptStatus.Int | InterruptStatus.Ibus);
        }

        public RunLevel CurrentRunLevel { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Tick() {
            // Get Opcode from memory location at IP

            var memEntry = MainMemory[InstructionPointer / 4];

            var opcode = (Opcodes) memEntry.Opcode;
            switch (opcode) {
                case 0x0:
                    var funct = (Functcodes) memEntry.Funct;
                    switch (funct) {
                        case Functcodes.Add:
                            Add.Process(this, memEntry);
                            break;

                        case Functcodes.ShiftLeftLogicalImmediate:
                            SLL.Process(this, memEntry);
                            break;

                        default:
                            throw new NotImplementedException(
                                $"Opcode {(int) opcode} Funct {(int) memEntry.Funct} not implemented!");
                    }
                    break;

                case Opcodes.Jump:
                    J.Process(this, memEntry);
                    break;

                default:
                    throw new NotImplementedException($"Opcode {opcode} not implemented!");
            }
        }

        #region Registers

        public MemoryItem[] Registers { get; } = new MemoryItem[32];

        #region Register Get/Set

        /// <summary>
        ///     Sets a register value.
        /// </summary>
        /// <param name="reg">The register to set</param>
        /// <param name="value">The value to place in register reg</param>
        public void SetRegister(Register reg, uint value) {
            switch (reg) {
                // Register 0 is Read-Only
                case Register.Reg0:
                    return;

                // Register 1 is reserved for Assembler use
                case Register.at:
                    if (CurrentRunLevel != RunLevel.Assembler)
                        goto default;
                    throw new UnauthorizedAccessException("Register AT is only accessible to Assembler-Level code!");

                default:
                    Registers[(int) reg].Value = value;
                    return;
            }
        }

        /// <summary>
        ///     Retreives a register value
        /// </summary>
        /// <param name="reg">The register to retrieve</param>
        /// <returns>The value in register reg</returns>
        public uint GetRegister(Register reg) {
            return Registers[(int) reg].Value;
        }

        #endregion

        #endregion

        #region Instruction Pointer

        private uint _instructionPointer;

        public uint InstructionPointer {
            get { return _instructionPointer; }
            set {
                if (_instructionPointer > Constants.LastMemoryAddress) {
                    _instructionPointer = Constants.LastMemoryAddress;
                    throw new IndexOutOfRangeException("Attempted to set PC out of Memory Range.");
                }
                _instructionPointer = value;
            }
        }

        // ReSharper disable InconsistentNaming
        private uint _nextIP = Constants.PcAdvance;

        public uint NextIP {
            get { return _nextIP; }
            set {
                if (_nextIP > Constants.LastMemoryAddress) {
                    _nextIP = Constants.LastMemoryAddress;
                    throw new IndexOutOfRangeException("Attempted to set PC out of Memory Range.");
                }
                _nextIP = value;
            }
        }

        // ReSharper restore InconsistentNaming

        #endregion
    }

    // ReSharper disable InconsistentNaming
    public enum Register {
        Reg0 = 0,
        Zero = Reg0,
        Reg1 = 1,

        at = Reg1,
        Reg2 = 2,
        Reg3 = 3,
        v0 = Reg2,
        v1 = Reg3,
        Reg4 = 4,
        Reg5 = 5,
        Reg6 = 6,
        Reg7 = 7,
        a0 = Reg4,
        a1 = Reg5,
        a2 = Reg6,
        a3 = Reg7,
        Reg8 = 8,
        Reg9 = 9,
        Reg10 = 10,
        Reg11 = 11,
        Reg12 = 12,
        Reg13 = 13,
        Reg14 = 14,
        Reg15 = 15,
        t0 = Reg8,
        t1 = Reg9,
        t2 = Reg10,
        t3 = Reg11,
        t4 = Reg12,
        t5 = Reg13,
        t6 = Reg14,
        t7 = Reg15,
        Reg16 = 16,
        Reg17 = 17,
        Reg18 = 18,
        Reg19 = 19,
        Reg20 = 20,
        Reg21 = 21,
        Reg22 = 22,
        Reg23 = 23,
        s0 = Reg16,
        s1 = Reg17,
        s2 = Reg18,
        s3 = Reg19,
        s4 = Reg20,
        s5 = Reg21,
        s6 = Reg22,
        s7 = Reg23,
        Reg24 = 24,
        Reg25 = 25,
        t8 = Reg24,
        t9 = Reg25,
        Reg26 = 26,
        Reg27 = 27,
        k0 = Reg26,
        k1 = Reg27,
        TrapCause = k0,
        TrapStatus = k1,
        Reg28 = 28,
        gp = Reg28,
        Reg29 = 29,
        sp = Reg29,
        Reg30 = 30,
        s8 = 30
    }

    public enum RunLevel {
        Assembler,
        Kernel,
        OS,
        Code
    }

    public enum InterruptCause {
        Int = 0x0,
        ExternalInterrupt = Int,
        Ibus = 0x1,
        InstructionBusError = Ibus,
        Ovf = 0x2,
        ArithmeticOverflow = Ovf,
        SysCall = 0x3,
        SystemCall = SysCall
    }

    [Flags]
    public enum InterruptStatus {
        SysCall = 0x1,
        Ofv = 0x2,
        Ibus = 0x4,
        Int = 0x8
    }

    // ReSharper restore InconsistentNaming
}