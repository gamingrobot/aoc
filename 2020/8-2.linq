<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "8-1.txt");
	var instructionsRaw = File.ReadAllLines(inputFile);
	var instructions = new List<Instruction>();
	foreach(var line in instructionsRaw)
	{
		var lineRaw = line.Split(' ');
		var opcode = Enum.Parse<OpCode>(lineRaw[0]);
		var offset = int.Parse(lineRaw[1]);
		instructions.Add(new Instruction {
			OpCode = opcode,
			Value = offset
		});
	}
	
	var instructionCount = instructions.Count;
	foreach (var instruction in instructions)
	{
		if(instruction.OpCode != OpCode.jmp){
			continue;
		}
		//instruction.Dump(lineNumber.ToString());
		//lineNumber++;
		instruction.OpCode = OpCode.nop;
		var counter = 0;
		var position = 0;
		var loopCount = 0;
		var done = false;
		while (!done && position < instructionCount)
		{
			var op = instructions[position];
			if(loopCount > 1000){
				done = true;
			}
			//op.Dump();
			//$"{position} {op.OpCode} {op.Value} {op.TouchCount}".Dump();
			switch (op.OpCode)
			{
				case OpCode.nop:
					position++;
					break;
				case OpCode.acc:
					counter += op.Value;
					position++;
					break;
				case OpCode.jmp:
					position += op.Value;
					break;
			}
			loopCount++;
		}
		if (!done)
		{
			counter.Dump();
		}
		
		//revert opcode change
		instruction.OpCode = OpCode.jmp;
	}
	instructions.Dump();
	
}

public enum OpCode {
	jmp,
	acc,
	nop
}

public class Instruction {
	public OpCode OpCode {get; set;}
	public int Value {get;set;}
}