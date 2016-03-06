using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MipsEmulator.Hardware
{
	internal class Memory
	{
		public MemoryItem[] MemoryItems { get; }

		public Memory(uint memorySizeBytes)
		{
			MemoryItems = new MemoryItem[memorySizeBytes / 4];

			for (int i = 0; i < MemoryItems.Length; i++)
			{
				MemoryItems[i] = new MemoryItem();
			}
		}

		public MemoryItem this[uint index]
		{
			get { return MemoryItems[index]; }
			private set { MemoryItems[index] = value; }
		}
	}
}
