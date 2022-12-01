<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "10-1.txt");
	var content = File.ReadAllLines(inputFile);
	var numbers = new List<int> {0}; //plane
	numbers.AddRange(content.Select(x => int.Parse(x)).ToList());
	numbers.Sort();
	numbers.Add(numbers.Last() + 3); //laptop
	var cache = new long[numbers.Count];
	CountPaths(numbers, cache, 0).Dump("total");
	
}

public long CountPaths(List<int> numbers, long[] cache, int index)
{
	//numbers.Dump();
	if(index + 1 == numbers.Count){
		return 1;
	}

	if (cache[index] != 0)
	{
		return cache[index];
	}
	long count = 0;
	for(int i = index+1; i < numbers.Count; i++)
	{
		if (numbers[i] - numbers[index] <= 3)
		{
			count += CountPaths(numbers, cache, i);
		}
	}
	cache[index] = count;
	//count.Dump();
	return count;
}

