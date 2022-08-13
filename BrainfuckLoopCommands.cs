using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
        {
            var loopEndByStart = new Dictionary<int, int>();
            var loopStartByEnd = new Dictionary<int, int>();
            FindLoops(vm, loopEndByStart, loopStartByEnd);
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = loopEndByStart[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b => 
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = loopStartByEnd[b.InstructionPointer];
            });
        }

        private static void FindLoops
            (IVirtualMachine vm, Dictionary<int, int> loopEndByStart, Dictionary<int, int> loopStartByEnd)
        {
            var openedBrackets = new Stack<int>();
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[') openedBrackets.Push(i);
                if (vm.Instructions[i] == ']')
                {
                    loopEndByStart.Add(openedBrackets.Peek(), i);
                    loopStartByEnd.Add(i, openedBrackets.Pop());
                }
            }
        }
    }
}