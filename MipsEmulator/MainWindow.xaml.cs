using MipsEmulator.Hardware;
using MipsEmulator.Instructions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MipsEmulator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		CPU _model = new CPU();
		internal CPU Model
		{
			get { return _model; }
			//private set { _model = value; }
		}

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = Model;

			Model.Registers[(int)Register.t0].Value = 100;


			Model.MainMemory.MemoryItems[0] = new MemoryItem() { Opcode = (byte)Opcodes.Add, RegS = (byte)Register.t0, RegT = (byte)Register.v0, RegD = (byte)Register.v0, Funct = (byte)Functcodes.Add };
			Model.MainMemory.MemoryItems[1] = new MemoryItem() { Opcode = (byte)Opcodes.J, Address = 0x0 };

			Stopwatch s = new Stopwatch();
			s.Start();
			for (int i = 0; i < 1000000; i++) {
				Model.Tick();
				Model.Tick();
				Model.Tick();
			}
			s.Stop();

			MessageBox.Show(s.ElapsedMilliseconds.ToString() + "ms");
			//Trace.WriteLine(, "Runtime:");
		}
	}
}
