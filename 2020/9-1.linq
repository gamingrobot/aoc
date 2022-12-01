<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "9-1.txt");
	var content = File.ReadAllLines(inputFile);
	var numbers = content.Select(x => long.Parse(x)).ToList();
	var preable = 25;
	for(var start = 0; (start + preable + 1) <= numbers.Count; start++)
	{
		var currentSlice = numbers.Skip(start).Take(preable).ToList();
		//currentSlice.Dump();
		var nextNumber = numbers[start + preable];
		//nextNumber.Dump();
		var found = false;
		for(var i = 0; i < preable; i++){
			for(var j = 0; j < preable; j++){
				if(currentSlice[i] + currentSlice[j] == nextNumber){
					found = true;
				}
			}
		}
		if(!found){
			nextNumber.Dump();
			break;
		}
	}
}
