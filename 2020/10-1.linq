<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "10-1.txt");
	var content = File.ReadAllLines(inputFile);
	var numbers = new List<int> { 0 }; //plane
	numbers.AddRange(content.Select(x => int.Parse(x)).ToList());
	numbers.Sort();
	numbers.Add(numbers.Last() + 3); //laptop
	var jolt1 = 0;
	var jolt2 = 0;
	var jolt3 = 0;
	for(var i = 0; i < numbers.Count - 1; i++)
	{
		//numbers[i].Dump("number");
		var joltDiff = (numbers[i + 1] - numbers[i]);
		switch(joltDiff){
			case 1:
				jolt1++;
				break;
			case 2:
				jolt2++;
				break;
			case 3:
				jolt3++;
				break;
		}
	}
	jolt1.Dump("jolt1");
	jolt2.Dump("jolt2");
	jolt3.Dump("jolt3");
	(jolt1 * jolt3).Dump();
	
}
