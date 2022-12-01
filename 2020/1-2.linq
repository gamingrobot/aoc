<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "1-1.txt");
	var numbers = File.ReadAllLines(inputFile).Select(x => int.Parse(x)).ToList();
	//numbers.Dump();
	var length = numbers.Count();
	
	for(var i = 0; i < length; i++)
	{
		for (var j = 0; j < length; j++)
		{
			for (var t = 0; t < length; t++)
			{
				if (numbers[i] + numbers[j] + numbers[t] == 2020)
				{
					(numbers[i] * numbers[j] * numbers[t]).Dump();
					break;
				}
			}
		}
	}
}
