<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 9);
    var parsed = ConvertInput(content);

    var test = new List<string> {
        "R 4",
        "U 4",
        "L 3",
        "D 1",
        "R 4",
        "D 1",
        "L 5",
        "R 2"};

    var test2 = new List<string> {
        "R 5",
        "U 8",
        "L 8",
        "D 3",
        "R 17",
        "D 10",
        "L 25",
        "U 20"};


    //var parsed = ConvertInput(test2);

    Part1(parsed).Dump("Part1");
    Part2(parsed).Dump("Part2");
}

int Part1(List<Instruction> parsed)
{
    var grid = new AGrid<Visit>();
    var head = new APoint(0, 0);
    var tail = new APoint(0, 0);
    grid[head] = new Visit
    {
        Head = true,
        Tail = true,
        Visited = true
    };
    foreach (var inst in parsed)
    {
        for (var i = 0; i < inst.Distance; i++)
        {
            head = MoveHeadKnot(grid, head, inst.Direction);
            tail = MoveTailKnot(grid, head, tail, true);
            //grid.Dump();
            //"".Dump();
        }
    }
    return grid.Values.Where(x => x.Visited).Count();
}

int Part2(List<Instruction> parsed)
{
    var grid = new AGrid<Visit>();
    var head = new APoint(0, 0);
    var tails = new List<APoint> {
        new APoint(0, 0), //1
        new APoint(0, 0), //2
        new APoint(0, 0), //3
        new APoint(0, 0), //4
        new APoint(0, 0), //5
        new APoint(0, 0), //6
        new APoint(0, 0), //7
        new APoint(0, 0), //8
        new APoint(0, 0), //9
    };
    grid[head] = new Visit
    {
        Head = true,
        Tail = true,
        Visited = true
    };
    foreach (var inst in parsed)
    {
        for (var i = 0; i < inst.Distance; i++)
        {
            head = MoveHeadKnot(grid, head, inst.Direction);
            var following = head;
            for(int j = 0; j < tails.Count; j++)
            {
                var countVisit = j == 8; //last knot is tracked
                tails[j] = MoveTailKnot(grid, following, tails[j], countVisit);
                following = tails[j];
            }
            //grid.Dump();
            //"".Dump();
        }
    }
    return grid.Values.Where(x => x.Visited).Count();
}

APoint MoveHeadKnot(AGrid<Visit> grid, APoint head, Direction direction)
{
    var previous = grid[head];
    switch (direction)
    {
        case Direction.Up:
            head.Y++;
            break;
        case Direction.Down:
            head.Y--;
            break;
        case Direction.Left:
            head.X--;
            break;
        case Direction.Right:
            head.X++;
            break;
    }
    if (grid.TryGetValue(head, out var current))
    {
        current.Head = true;
    }
    else
    {
        grid[head] = new Visit
        {
            Head = true
        };
    }
    previous.Head = false;
    return head;
}

APoint MoveTailKnot(AGrid<Visit> grid, APoint head, APoint tail, bool countVisit)
{
    var previous = grid[tail];
    var headOffset = new APoint(head.X - tail.X, head.Y - tail.Y);
    if (Math.Abs(headOffset.X) > 1 || Math.Abs(headOffset.Y) > 1)
    {
        if (headOffset.X > 0)
        {
            tail.X++;
        }
        else if (headOffset.X < 0)
        {
            tail.X--;
        }

        if (headOffset.Y > 0)
        {
            tail.Y++;
        }
        else if (headOffset.Y < 0)
        {
            tail.Y--;
        }

        if (grid.TryGetValue(tail, out var current))
        {
            current.Tail = true;
            if(countVisit)
            {
                current.Visited = true;
            }
        }
        else
        {
            grid[tail] = new Visit
            {
                Tail = true,
                Visited = countVisit
            };
        }
        previous.Tail = false;
    }
    return tail;
}


List<Instruction> ConvertInput(List<string> content)
{
    return content.Select(x => new Instruction
    {
        Direction = (Direction)x[0],
        Distance = int.Parse(x.Substring(2))
    }).ToList();
}

class Visit
{
    public bool Head;
    public bool Tail;
    public bool Visited;

    public override string ToString()
    {
        if (Head && Tail)
        {
            return "B";
        }
        else if (Head)
        {
            return "H";
        }
        else if (Tail)
        {
            return "T";
        }
        return Visited ? "#" : ".";
    }
}

class Instruction
{
    public Direction Direction;
    public int Distance;
}

enum Direction
{
    Up = 'U',
    Down = 'D',
    Left = 'L',
    Right = 'R'
}