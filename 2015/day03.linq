<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2015, 3);
    var input = content.Single().ToCharArray();

    Part1(input).Dump("Part1");
    Part2(input).Dump("Part2");
}

int Part1(char[] input)
{
    var grid = new AGrid<int>();
    var location = new APoint(0, 0);
    grid[location] = 1;
    foreach(var c in input)
    {
        switch(c)
        {
            case '^':
                location.Y++;
                break;
            case 'v':
                location.Y--;
                break;
            case '>':
                location.X++;
                break;
            case '<':
                location.X--;
                break;

        };
        if (grid.ContainsKey(location))
        {
            grid[location]++;
        }
        else
        {
            grid[location] = 1;
        }

    }
    //grid.Dump();
    return grid.Values.Count();
}

int Part2(char[] input)
{
    var grid = new AGrid<int>();
    var santaLocation = new APoint(0, 0);
    var roboLocation = new APoint(0, 0);
    grid[santaLocation] = 1;
    grid[roboLocation] = 1;
    var isRobotTurn = false;
    foreach (var c in input)
    {
        var actorLocation = new APoint(0, 0);
        switch (c)
        {
            case '^':
                actorLocation.Y++;
                break;
            case 'v':
                actorLocation.Y--;
                break;
            case '>':
                actorLocation.X++;
                break;
            case '<':
                actorLocation.X--;
                break;
        };
        if(isRobotTurn)
        {
            roboLocation.X += actorLocation.X;
            roboLocation.Y += actorLocation.Y;
            if (grid.ContainsKey(roboLocation))
            {
                grid[roboLocation]++;
            }
            else
            {
                grid[roboLocation] = 1;
            }
        }
        else
        {
            santaLocation.X += actorLocation.X;
            santaLocation.Y += actorLocation.Y;
            if (grid.ContainsKey(santaLocation))
            {
                grid[santaLocation]++;
            }
            else
            {
                grid[santaLocation] = 1;
            }
        }
        isRobotTurn = !isRobotTurn;
    }
    //grid.Dump();
    return grid.Values.Count();
}