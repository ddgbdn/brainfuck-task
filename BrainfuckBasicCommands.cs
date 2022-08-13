using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
			vm.RegisterCommand('+', b => { unchecked { b.Memory[b.MemoryPointer]++; } });
			vm.RegisterCommand('-', b => { unchecked { b.Memory[b.MemoryPointer]--; } });
			vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)(read() % 256); });
			vm.RegisterCommand('>', b => 
			{
				b.MemoryPointer++;
				if (b.MemoryPointer == b.Memory.Length) b.MemoryPointer = 0;
			});
			vm.RegisterCommand('<', b => 
			{
				b.MemoryPointer--; 
				if (b.MemoryPointer == -1) b.MemoryPointer = b.Memory.Length - 1;
			});
			for (char c = '0'; c <= 'z'; c++)
			{
				if (!char.IsLetterOrDigit(c)) continue;
				var letter = (byte)c;
				vm.RegisterCommand(c, b => { b.Memory[b.MemoryPointer] = letter; });
			}
		}
	}
}