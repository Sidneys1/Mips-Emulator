﻿using System;
using System.ComponentModel;

namespace MipsEmulator
{
	/// <summary>
	/// Holds an integer value that can be a binding source
	/// </summary>
	class MemoryItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		uint _value;

		/// <summary>
		/// The Value of this MemoryItem or Register
		/// </summary>
		public uint Value
		{
			get { return _value; }
			set
			{
				if (_value == value) return;
				_value = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
			}
		}

		public int IntValue => (int)Value;

		public string TipValue => $"s{IntValue:N0}{Environment.NewLine}u{Value:N0}";

		#region Accessors

		/// <summary>
		/// Retrieve opcode of an instruction
		/// </summary>
		public byte Opcode
		{
			get
			{
				uint val = (_value >> 26) & Constants.SixBitMask;
				return Convert.ToByte(val);
			}

			set
			{
				_value &= 0x3FFFFFF;
				_value |= ((uint)value << 26);
				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Opcode)));
			}
		}

		/// <summary>
		/// Get First Register of Reg or Imm Instruction
		/// </summary>
		public byte RegS
		{
			get
			{
				uint val = (_value >> 21) & Constants.FiveBitMask;
				return Convert.ToByte(val);
			}

			set
			{
				_value &= 0xFC1FFFFF;
				_value |= ((uint)value << 21);
				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(RegS)));
			}
		}

		/// <summary>
		/// Get Second register of a Reg or Imm Instruction
		/// </summary>
		public byte RegT
		{
			get
			{
				uint val = (_value >> 16) & Constants.FiveBitMask;
				return Convert.ToByte(val);
			}

			set
			{
				_value &= 0xFFE0FFFF;
				_value |= ((uint)value << 16);

				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(RegT)));
			}
		}

		/// <summary>
		/// Get Third register of a Reg Instruction
		/// </summary>
		public byte RegD
		{
			get
			{
				uint val = (_value >> 11) & Constants.FiveBitMask;
				return Convert.ToByte(val);
			}

			set
			{
				_value &= 0xFFFF07FF;
				_value |= ((uint)value << 11);
				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(RegD)));
			}
		}

		/// <summary>
		/// Shift Amount of a Reg Instruction
		/// </summary>
		public byte Shamt
		{
			get
			{
				uint val = (_value >> 6) & Constants.FiveBitMask;
				return Convert.ToByte(val);
			}

			set
			{
				_value &= 0xFFFFF83F;
				_value |= ((uint)value << 6);

				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Shamt)));
			}
		}

		/// <summary>
		/// Function of a Reg Instruction
		/// </summary>
		public byte Funct
		{
			get
			{
				uint val = _value & Constants.SixBitMask;
				return Convert.ToByte(val);
			}

			set
			{
				_value &= 0xFFFFFFC0;
				_value |= value;

				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Funct)));
			}
		}

		/// <summary>
		/// Immediate Value of Imm Instruction
		/// </summary>
		public ushort Immediate
		{
			get
			{
				uint val = _value & Constants.SixteenBitMask;
				return Convert.ToUInt16(val);
			}

			set
			{
				_value &= 0xFFFF0000;
				_value |= value;

				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Immediate)));
			}
		}

		/// <summary>
		/// Address of a Jump Instruction
		/// </summary>
		public uint Address
		{
			get
			{
				return _value & Constants.TwentySixBitMask;
			}

			set
			{
				_value &= 0xFC000000;
				_value |= value;

				if (PropertyChanged == null) return;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(Address)));
			}
		}

		#endregion

		#region Ctor

		public MemoryItem() { }

		public MemoryItem(uint value)
		{
			Value = value;
		}

		#endregion
	}
}
