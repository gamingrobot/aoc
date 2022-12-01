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

		var first = int.Parse(rangeDirty[0]);
		var second = int.Parse(rangeDirty[1]);
		
		var firstChar = password[first-1];
		var secondChar = password[second-1];
		//entry.Dump();
		if ((firstChar == letter || secondChar == letter) && !(firstChar == letter && secondChar == letter))
		{
		//	"Valid".Dump();
			valid++;
		}
	}
	valid.Dump();
}
