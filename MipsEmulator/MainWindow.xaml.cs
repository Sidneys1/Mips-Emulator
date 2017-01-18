using System.Diagnostics;
using System.Windows;
using MipsEmulator.Hardware;
using MipsEmulator.Instructions;

namespace MipsEmulator {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = Model;

            Model.Registers[(int) Register.t0].Value = 100;


            Model.MainMemory.MemoryItems[0] = new MemoryItem {
                Opcode = (byte) Opcodes.Add,
                RegS = (byte) Register.t0,
                RegT = (byte) Register.v0,
                RegD = (byte) Register.v0,
                Funct = (byte) Functcodes.Add
            };
            Model.MainMemory.MemoryItems[1] = new MemoryItem {Opcode = (byte) Opcodes.J, Address = 0x0};

            var s = new Stopwatch();
            s.Start();
            for (var i = 0; i < 1000000; i++) {
                Model.Tick();
                Model.Tick();
                Model.Tick();
            }
            s.Stop();

            MessageBox.Show(s.ElapsedMilliseconds + "ms");
            //Trace.WriteLine(, "Runtime:");
        }

        internal CPU Model { get;
            //private set { _model = value; }
        } = new CPU();
    }
}