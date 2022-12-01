<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "9-1.txt");
	var content = File.ReadAllLines(inputFile);
	var numbers = content.Select(x => long.Parse(x)).ToList();
	var finalNumber = 1492208709;
	for(var start = 0; start < numbers.Count; start++)
	{
		long total = 0;
		for(var end = start; end < numbers.Count; end++){
			total += numbers[end];
			if (total == finalNumber)
			{
				$"start: {start} {numbers[start]} end: {end} {numbers[end]}".Dump();
				var seg = numbers.Skip(start).Take((end - start) + 1).ToList();
				seg.Sum().Dump();
				seg.Sort();
				seg.Dump();
				$"answer: {seg.First() + seg.Last()}".Dump();
				break;
			}
		}
	}
}
