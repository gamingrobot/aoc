<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2022, 7);
    var parsed = ConvertInput(content);
    
    CalculateSize(parsed);
    
    Part1(parsed).Dump("Part1");
    Part2(parsed).Dump("Part2");
}

int Part1(ADirectory root)
{
    var sum = 0;
    FindSmallDirs(root, 100000, ref sum);
    return sum;
}

int Part2(ADirectory root)
{
    var totalSize = 70000000;
    var unsedReq = 30000000;
    var freeSpace = totalSize - root.Size;
    var needToFree = unsedReq - freeSpace;

    (freeSpace).Dump("Free Space");
    (root.Size).Dump("Used Space");
    (needToFree).Dump("Need to Free Space");

    var eligible = new Dictionary<string, int>();
    FindDeleteDirs(root, needToFree, eligible);
    
    eligible.Dump();

    return eligible.Values.OrderBy(x => x).First();
}

void FindDeleteDirs(ADirectory node, int size, Dictionary<string, int> eligible)
{
    if(node.Size >= size)
    {
        eligible.Add(node.Name, node.Size);
    }
    foreach (var dir in node.SubDirs.Values)
    {
        FindDeleteDirs(dir, size, eligible);
    }
}

void FindSmallDirs(ADirectory node, int size, ref int sum)
{
    if (node.Size < size)
    {
        sum += node.Size;
    }
    foreach (var dir in node.SubDirs.Values)
    {
        FindSmallDirs(dir, size, ref sum);
    }
}


int CalculateSize(ADirectory node)
{
    var size = node.Files.Values.Sum();
    foreach (var dir in node.SubDirs.Values)
    {
        size += CalculateSize(dir);
    }
    node.Size = size;
    return size;
}

ADirectory ConvertInput(List<string> input)
{
    var root = new ADirectory("/", null);
    var current = root;
    foreach(var line in input.Skip(1))
    {
        if(line.StartsWith("$ cd")) //cd
        {
            var dir = line.Substring(5); //"$ cd "
            if(dir == "..")
            {
                //$"Exiting {current.Name} to {current.Parent.Name}".Dump();
                current = current.Parent;     
            }
            else
            {
                //$"Entering {dir} from {current.Name}".Dump();
                current = current.SubDirs[dir];
            }
        }
        else if(line.StartsWith("$ ls")) //ls
        {
            //do nothing
        }
        else if(line.StartsWith("dir")) //dir from ls
        {
            var dir = line.Substring(4); //"dir "
            if(!current.SubDirs.ContainsKey(dir))
            {
                //$"New Directory {dir} in {current.Name}".Dump();
                var newDir = new ADirectory(dir, current);
                current.SubDirs.Add(dir, newDir);
            }
        }
        else //must be a file
        {
            var parts = line.Split(' ');
            var size = int.Parse(parts[0]);
            var name = parts[1];
            //name.Dump(size.ToString());
            if (!current.SubDirs.ContainsKey(name))
            {
                //$"New File {name} in {current.Name}".Dump();
                var newDir = new ADirectory(name, current);
                current.Files.Add(name, size);
            }
        }
    }
    return root;
}

class ADirectory 
{
    public ADirectory(string name, ADirectory parent)
    {
        Name = name;
        Parent = parent;
        SubDirs = new Dictionary<string, ADirectory>();
        Files = new Dictionary<string, int>();
    }
    public string Name;
    public ADirectory Parent;
    public int Size;
    public Dictionary<string, ADirectory> SubDirs;
    public Dictionary<string, int> Files;
}