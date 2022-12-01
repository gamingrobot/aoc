<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "4-1.txt");
	var content = File.ReadAllText(inputFile);
	var passports = content.Split("\n\n");
	var cleanPassports = passports.Select(x => x.Replace('\n', ' '));
	var valid = 0;
	foreach(var pass in cleanPassports){
		if(ValidPassport(pass)){
			valid++;
		}
	}
	valid.Dump();
}

bool ValidPassport(string pass)
{
	var requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" }; //cid not required
	var pairsRaw = pass.Split(' ');
	var pairs = pairsRaw.Where(x => !string.IsNullOrEmpty(x)).ToDictionary(x => x.Substring(0, 3), y => y.Substring(4));
	//pairs.Dump();
	var invalid = requiredFields.Except(pairs.Keys).Any();
	return !invalid;
}

