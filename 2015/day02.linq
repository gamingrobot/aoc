<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2015, 2);
    var parsed = content.Select(x => ConvertInput(x)).ToList();

    Part1(parsed).Dump("Part1");
    Part2(parsed).Dump("Part2");
}

int Part1(List<Box> parsed)
{
    var surface = parsed.Select(x => (2 * x.Length * x.Width) + (2 * x.Width * x.Height) + (2 * x.Height * x.Length) + GetSmallestSideArea(x));
    //surface.Dump();
    return surface.Sum();
}

int Part2(List<Box> parsed)
{
    var surface = parsed.Select(x => GetSmallestSidePeremeter(x) + (x.Width * x.Length * x.Height));
    //surface.Dump();
    return surface.Sum();
}

int GetSmallestSidePeremeter(Box box)
{
    var side1 = 2*box.Length + 2*box.Width;
    var side2 = 2*box.Width + 2*box.Height;
    var side3 = 2*box.Height + 2*box.Length;
    if (side1 <= side2 && side1 <= side3)
    {
        return side1;
    }
    if (side2 <= side1 && side2 <= side3)
    {
        return side2;
    }
    if (side3 <= side1 && side3 <= side2)
    {
        return side3;
    }
    throw new InvalidOperationException("Cant find smallest side");
}

int GetSmallestSideArea(Box box)
{
    var side1 = box.Length * box.Width;
    var side2 = box.Width * box.Height;
    var side3 = box.Height * box.Length;
    if (side1 <= side2 && side1 <= side3)
    {
        return side1;
    }
    if (side2 <= side1 && side2 <= side3)
    {
        return side2;
    }
    if (side3 <= side1 && side3 <= side2)
    {
        return side3;
    }
    throw new InvalidOperationException("Cant find smallest side");
}

Box ConvertInput(string input)
{
    var parts = input.Split('x');
    return new Box
    {
        Width = int.Parse(parts[0]),
        Height = int.Parse(parts[1]),
        Length = int.Parse(parts[2])
    };
}

class Box
{
    public int Width;
    public int Height;
    public int Length;
}