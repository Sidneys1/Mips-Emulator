using System;
using MipsEmulator.Hardware;

namespace MipsEmulator.Instructions.Reg
{
	internal static class Add
	{
		public static void Process(CPU parent, MemoryItem memEntry)
		{
			int x, y;

			var ux = parent.GetRegister((Register)memEntry.RegS);
			var uy = parent.GetRegister((Register)memEntry.RegT);

			unchecked
			{ // Convert unsigned to signed at the bit level.
				x = (int)ux;
				y = (int)uy;
			}

			try
			{ // Catch overflow with checked()
				int z = checked(x + y);
			}
			catch (OverflowException)
			{
				// set overflow trap
				parent.SetRegister(Register.TrapCause, (uint)InterruptCause.ArithmeticOverflow);
			}

			parent.SetRegister((Register)memEntry.RegD, ux + uy);

			parent.InstructionPointer = parent.NextIP;
			parent.NextIP = parent.InstructionPointer + Constants.PcAdvance;
		}
	}
}
