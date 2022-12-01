<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var passwordEntries = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\2-1.txt").ToList();
	var valid = 0;
	foreach(var entry in passwordEntries){
		var bits = entry.Split(' ');
		var rangeDirty = bits[0].Split('-');
		var letter = bits[1][0];
		var password  = bits[2];

		var min = int.Parse(rangeDirty[0]);
		var max = int.Parse(rangeDirty[1]);
		
		var count = password.Count(x => x == letter);
		//entry.Dump();
		//count.Dump(password);
		if(count <= max && count >= min){
			//"Valid".Dump();
			valid++;
		}
	}
	valid.Dump();
}
