using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }	

		private Dictionary<char, Action<IVirtualMachine>> actions
			= new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			InstructionPointer = 0;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			actions[symbol] = execute;
		}

		public void Run()
		{
			while (InstructionPointer < Instructions.Length)
            {
				if (actions.ContainsKey(Instructions[InstructionPointer]))
					actions[Instructions[InstructionPointer]](this);
				InstructionPointer++;
			}
		}
	}
}