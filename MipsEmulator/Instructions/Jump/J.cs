using MipsEmulator.Hardware;

namespace MipsEmulator.Instructions.Jump {
    internal static class J {
        public static void Process(CPU parent, MemoryItem memEntry) {
            parent.InstructionPointer = parent.NextIP;
            parent.NextIP = (parent.InstructionPointer & 0xF0000000) | (memEntry.Address << 2);
        }
    }
}