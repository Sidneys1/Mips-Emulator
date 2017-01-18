using MipsEmulator.Hardware;

namespace MipsEmulator.Instructions.Reg {
    // ReSharper disable once InconsistentNaming
    internal class SLL {
        public static void Process(CPU parent, MemoryItem memEntry) {
            parent.SetRegister((Register) memEntry.RegD,
                parent.GetRegister((Register) memEntry.RegS) << (int) parent.GetRegister((Register) memEntry.RegT));
            parent.InstructionPointer = parent.NextIP;
            parent.NextIP = parent.InstructionPointer + Constants.PcAdvance;
        }
    }
}