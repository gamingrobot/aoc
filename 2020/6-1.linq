<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "6-1.txt");
	var content = File.ReadAllText(inputFile);
	var groups = content.Split("\n\n");
	var total = 0;
	foreach (var group in groups)
	{
		//group.Dump("Group");
		var questions = group.Replace("\n", null);
		questions.Dump("Questions");
		var count = questions.ToArray().Distinct().Count();
		count.Dump();
		total += count;
	}
	total.Dump();
}


