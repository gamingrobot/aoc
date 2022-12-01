<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var rows = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\3-1.txt").ToList();
	var trees = 0;
	var offset = 0;
	var inputWidth = rows[0].Length;
	foreach(var row in rows.Skip(1)){
		offset += 3; //right offset
		if(offset >= inputWidth)
		{
			offset -= inputWidth;
		}
		offset.Dump();
		if(row[offset] == '#'){
			trees++;
		}
	}
	trees.Dump();
}
