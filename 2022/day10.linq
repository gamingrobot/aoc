<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 10);
    //var test = Helpers.LoadTestInput(2022, 10);
    var parsed = ConvertInput(content);


    Part1(parsed).Dump("Part1");
    Part2(parsed);
}

int Part1(Queue<Instruction> input)
{
    var registerX = 1;
    var cycle = 1;
    var signalTotal = 0;
    var inst = input.Dequeue();
    var instWait = false;
    while (true)
    {
        if (cycle == 20 || (cycle - 20) % 40 == 0)
        {
            var signal = (cycle * registerX);
            signal.Dump(cycle.ToString());
            signalTotal += signal;
        }
        
        //inst.Dump(cycle.ToString());
        switch (inst.OpCode)
        {
            case OpCode.NoOp:
                break;
            case OpCode.AddX:
                if (instWait)
                {
                    registerX += inst.Argument.Value;
                    instWait = false;
                }
                else
                {
                    instWait = true;
                }
                break;
        }

        if (!instWait)
        {
            if (!input.TryDequeue(out inst))
            {
                break;
            }
        }
        cycle++;
    }
    return signalTotal;
}


void Part2(Queue<Instruction> input)
{
    var registerX = 1;
    var cycle = 1;
    var pixels = new bool[240];
    var inst = input.Dequeue();
    var instWait = false;
    while (true)
    {
        //inst.Dump(cycle.ToString());
        switch (inst.OpCode)
        {
            case OpCode.NoOp:
                break;
            case OpCode.AddX:
                if (instWait)
                {
                    registerX += inst.Argument.Value;
                    instWait = false;
                }
                else
                {
                    instWait = true;
                }
                break;
        }

        var col = cycle % 40;
        if (col == registerX - 1 || col == registerX || col == registerX + 1) //3 pixels wide
        {
            pixels[cycle] = true;
        }

        if (!instWait)
        {
            if (!input.TryDequeue(out inst))
            {
                break;
            }
        }
        cycle++;
    }
    
    foreach(var row in pixels.Select(x => x ? "#" : ".").Batch(40))
    {
        string.Join("", row).Dump();
    }
}

Queue<Instruction> ConvertInput(List<string> content)
{
    var output = new Queue<Instruction>();
    foreach (var line in content)
    {
        var parts = line.Split(" ");
        switch (parts[0])
        {
            case "noop":
                output.Enqueue(new Instruction(OpCode.NoOp, null));
                break;
            case "addx":
                output.Enqueue(new Instruction(OpCode.AddX, int.Parse(parts[1])));
                break;
        }
    }
    return output;
}

class Instruction
{
    public Instruction(OpCode opcode, int? argument)
    {
        OpCode = opcode;
        Argument = argument;
    }
    public OpCode OpCode;
    public int? Argument;
}

enum OpCode
{
    NoOp,
    AddX,
}
