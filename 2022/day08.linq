<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 8);
    var parsed = ConvertInput(content);

//    var test = new List<string> {
//        "30373",
//        "25512",
//        "65332",
//        "33549",
//        "35390"};
//
//    var parsed = ConvertInput(test);

    Part1(parsed).Dump("Part1");
}

int Part1(AGrid<Tree> grid)
{
    var max = grid.GetMax();
    var location = new APoint(0, 0);
    //edges are already visible
    for (location.Y = 1; max.Y > location.Y; location.Y++)
    {
        for (location.X = 1; max.X > location.X; location.X++)
        {
            var currentTree = grid[location];
            var north = VisibleInDirection(grid, location, max, Direction.North);
            var south = VisibleInDirection(grid, location, max, Direction.South);
            var east = VisibleInDirection(grid, location, max, Direction.East);
            var west = VisibleInDirection(grid, location, max, Direction.West);
            //$"{location.X},{location.Y}:{currentTree.Height} North: {north}, South: {south}, East: {east}, West: {west}".Dump();
            if (!north && !south && !east && !west)
            {
                currentTree.Visible = false;
            }
        }
    }
    grid.Dump();
    return grid.Values.Where(x => x.Visible).Count();
}

bool VisibleInDirection(AGrid<Tree> grid, APoint start, APoint max, Direction direction)
{
    var currentTree = grid[start];
    var look = AdjustLocation(start, direction);
    while (CheckCondition(look, max, direction))
    {
        var lookingTree = grid[look];
        if (lookingTree.Height >= currentTree.Height)
        {
            return false;
        }
        look = AdjustLocation(look, direction);
    }
    return true;
}

bool CheckCondition(APoint current, APoint max, Direction direction)
{
    switch (direction)
    {
        case Direction.North:
            return current.Y >= 0;
        case Direction.South:
            return current.Y <= max.Y;
        case Direction.East:
            return current.X <= max.X;
        case Direction.West:
            return current.X >= 0;
        default:
            throw new IndexOutOfRangeException("Unknown direction");
    }
}

APoint AdjustLocation(APoint current, Direction direction)
{
    switch (direction)
    {
        case Direction.North:
            current.Y--;
            return current;
        case Direction.South:
            current.Y++;
            return current;
        case Direction.East:
            current.X++;
            return current;
        case Direction.West:
            current.X--;
            return current;
        default:
            throw new IndexOutOfRangeException("Unknown direction");
    }
}

AGrid<Tree> ConvertInput(List<string> input)
{
    var grid = new AGrid<Tree>();
    var location = new APoint(0, 0);
    foreach (var line in input)
    {
        location.X = 0;
        foreach (var ch in line)
        {
            //Make everthing visible then mark not visible later
            grid[location] = new Tree(ch - '0', true);
            location.X++;
        }
        location.Y++;
    }
    return grid;
}

class Tree
{
    public Tree(int height, bool visible)
    {
        Height = height;
        Visible = visible;
    }
    public int Height;
    public bool Visible;
    public override string ToString()
    {
        //return Height.ToString();
        return Visible ? "Y" : "N";
    }
}

enum Direction
{
    North,
    South,
    East,
    West
}
