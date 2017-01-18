namespace MipsEmulator {
    internal static class Constants {
        // Constants
        public static readonly uint LastMemoryAddress = 16 * 4;

        public static readonly uint PcAdvance = 4;

        public static readonly uint NOP = 0x00000000;

        // Masks
        public static readonly byte SixBitMask = 0x3F;

        public static readonly byte FiveBitMask = 0x1F;

        public static readonly ushort SixteenBitMask = 0xFFFF;

        public static readonly uint TwentySixBitMask = 0x3FFFFFF;
    }
}