<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var content = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\21-1-sample.txt");
	var lineRegex = new Regex(@"^^([a-z ]*) \(contains ([a-z, ]*)\)$", RegexOptions.Singleline | RegexOptions.Compiled);
	
	var possibleAllergens = new Dictionary<string, List<string>>();
	foreach(var line in content){
		//preprocessing
		var regexParts = lineRegex.Match(line);
		//regexParts.Groups.Dump(line);
		var ingredients = regexParts.Groups[1].Value.Split(" ");
		var allergens = regexParts.Groups[2].Value.Split(", ");
		ingredients.Dump();
		allergens.Dump();
		foreach(var ing in ingredients){
			if(!possibleAllergens.ContainsKey(ing)){
				possibleAllergens[ing] = new List<string>();
			}
			possibleAllergens[ing].AddRange(allergens);
		}
	}
	
	possibleAllergens.Dump();
	
}