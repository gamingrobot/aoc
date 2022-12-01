<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var rows = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\3-1.txt").ToList();
	CountPath(rows, 1, 1).Dump("Right 1, down 1");
	CountPath(rows, 3, 1).Dump("Right 3, down 1");
	CountPath(rows, 5, 1).Dump("Right 5, down 1");
	CountPath(rows, 7, 1).Dump("Right 7, down 1");
	CountPath(rows, 1, 2).Dump("Right 1, down 2");
	
}

int CountPath(List<string> rows, int rightStep, int downStep)
{
	var trees = 0;
	var offset = 0;
	var inputWidth = rows[0].Length;
	for (var i = downStep; i < rows.Count; i += downStep)
	{
		offset += rightStep;
		if (offset >= inputWidth)
		{
			offset -= inputWidth;
		}
		//offset.Dump();
		if (rows[i][offset] == '#')
		{
			trees++;
		}
	}
	return trees;
}
