<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 2);
    
    Part1(content).Dump("Part1");
    Part2(content).Dump("Part2");
}

int Part1(List<string> content)
{
    var parsed = content.Select(x => ConvertInputPart1(x));
    return parsed.Select(x => (int)x.Self + (int)GetOutcome(x)).Sum();
}

int Part2(List<string> content)
{
    var parsed = content.Select(x => ConvertInputPart2(x));
    return parsed.Select(x => (int)GetShape(x) + (int)x.Outcome).Sum();
}

(Shape Opponent, Shape Self) ConvertInputPart1(string input)
{
	var split = input.Split(" ");
    var optRaw = split[0][0];
    var selfRaw = split[1][0];
    return (ConvertShape(optRaw), ConvertShape(selfRaw));
}

Outcome GetOutcome((Shape Opponent, Shape Self) input)
{
    if (input.Opponent == input.Self)
    {
        return Outcome.Draw;
    }
    else if (input.Opponent == Shape.Rock && input.Self == Shape.Scissors)
    {
        return Outcome.Loss;
    }
    else if (input.Opponent == Shape.Rock && input.Self == Shape.Paper)
    {
        return Outcome.Win;
    }
    else if (input.Opponent == Shape.Paper && input.Self == Shape.Rock)
    {
        return Outcome.Loss;
    }
    else if (input.Opponent == Shape.Paper && input.Self == Shape.Scissors)
    {
        return Outcome.Win;
    }
    else if (input.Opponent == Shape.Scissors && input.Self == Shape.Paper)
    {
        return Outcome.Loss;
    }
    else if (input.Opponent == Shape.Scissors && input.Self == Shape.Rock)
    {
        return Outcome.Win;
    }
    throw new IndexOutOfRangeException($"Unknown Outcome {input}");
}

Shape ConvertShape(char input)
{
	switch(input)
    {
    	case 'A':
        case 'X':
            return Shape.Rock;
        case 'B':
        case 'Y':
            return Shape.Paper;
        case 'C':
        case 'Z':
            return Shape.Scissors;
    }
    throw new IndexOutOfRangeException($"Unknown Shape {input}");
}

//Part 2 Helpers

(Shape Opponent, Outcome Outcome) ConvertInputPart2(string input)
{
    var split = input.Split(" ");
    var optRaw = split[0][0];
    var outcomeRaw = split[1][0];
    return (ConvertShape(optRaw), ConvertOutput(outcomeRaw));
}

Shape GetShape((Shape Opponent, Outcome Outcome) input)
{
    if (input.Opponent == Shape.Rock && input.Outcome == Outcome.Loss)
    {
        return Shape.Scissors;
    }
    else if (input.Opponent == Shape.Rock && input.Outcome == Outcome.Draw)
    {
        return Shape.Rock;
    }
    else if (input.Opponent == Shape.Rock && input.Outcome == Outcome.Win)
    {
        return Shape.Paper;
    }
    else if (input.Opponent == Shape.Paper && input.Outcome == Outcome.Loss)
    {
        return Shape.Rock;
    }
    else if (input.Opponent == Shape.Paper && input.Outcome == Outcome.Draw)
    {
        return Shape.Paper;
    }
    else if (input.Opponent == Shape.Paper && input.Outcome == Outcome.Win)
    {
        return Shape.Scissors;
    }
    else if (input.Opponent == Shape.Scissors && input.Outcome == Outcome.Loss)
    {
        return Shape.Paper;
    }
    else if (input.Opponent == Shape.Scissors && input.Outcome == Outcome.Draw)
    {
        return Shape.Scissors;
    }
    else if (input.Opponent == Shape.Scissors && input.Outcome == Outcome.Win)
    {
        return Shape.Rock;
    }
    throw new IndexOutOfRangeException($"Unknown Shape {input}");
}

Outcome ConvertOutput(char input)
{
    switch (input)
    {
        case 'X':
            return Outcome.Loss;
        case 'Y':
            return Outcome.Draw;
        case 'Z':
            return Outcome.Win;
    }
    throw new IndexOutOfRangeException($"Unknown Output {input}");
}


enum Shape {
	Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Outcome
{
    Loss = 0,
    Draw = 3,
    Win = 6
}