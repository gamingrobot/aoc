<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var numbers = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\1-1.txt").Select(x => int.Parse(x)).ToList();
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
