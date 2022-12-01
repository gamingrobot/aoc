<Query Kind="Program">
  <Namespace>QuikGraph</Namespace>
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
	
	var counter = 0;
	var position = 0;
	while(position <= instructions.Count){
		var op = instructions[position];
		if(op.Touched){
			counter.Dump();
			break;
		}
		//op.Dump();
		switch(op.OpCode)
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
		op.Touched = true;
	}
	
}

public enum OpCode {
	jmp,
	acc,
	nop
}

public class Instruction {
	public OpCode OpCode {get; set;}
	public int Value {get;set;}
	public bool Touched {get;set;}
}