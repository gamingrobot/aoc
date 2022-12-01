<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "2-1.txt");
	var passwordEntries = File.ReadAllLines(inputFile).ToList();
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
