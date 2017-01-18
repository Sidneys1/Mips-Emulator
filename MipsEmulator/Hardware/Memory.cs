namespace MipsEmulator.Hardware {
    internal class Memory {
        public Memory(uint memorySizeBytes) {
            MemoryItems = new MemoryItem[memorySizeBytes / 4];

            for (var i = 0; i < MemoryItems.Length; i++)
                MemoryItems[i] = new MemoryItem();
        }

        public MemoryItem[] MemoryItems { get; }

        public MemoryItem this[uint index] => MemoryItems[index];
        //private set { MemoryItems[index] = value; }
    }
}