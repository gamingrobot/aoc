<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var content = File.ReadAllText(@"C:\Projects\adventofcode\aoc-2020\input\4-1.txt");
	var passports = content.Split("\n\n");
	var cleanPassports = passports.Select(x => x.Replace('\n', ' '));
	cleanPassports.Dump();
	var valid = 0;
	foreach(var pass in cleanPassports){
		pass.Dump();
		var result = ValidPassport(pass);
		//pass.Dump();
		//result.Dump();
		if (result)
		{
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
	if(requiredFields.Except(pairs.Keys).Any())
	{
		//pairs.Dump("Required Feilds Invalid");
		return false;
	}
	var birthYear = int.Parse(pairs["byr"]);
	//at least 1920 and at most 2002
	if(!(birthYear >= 1920 && birthYear <= 2002))
	{
		birthYear.Dump("Birth Year Invalid");
		return false;
	}
	var issueYear = int.Parse(pairs["iyr"]);
	//at least 2010 and at most 2020
	if (!(issueYear >= 2010 && issueYear <= 2020))
	{
		issueYear.Dump("Issue Year Invalid");
		return false;
	}
	var expireYear = int.Parse(pairs["eyr"]);
	//at least 2020 and at most 2030
	if (!(expireYear >= 2020 && expireYear <= 2030))
	{
		expireYear.Dump("Expire Year Invalid");
		return false;
	}
	var height = pairs["hgt"];
	//	a number followed by either cm or in:
	//    If cm, the number must be at least 150 and at most 193.
	//	If in, the number must be at least 59 and at most 76.
	if(!(height.EndsWith("cm") || height.EndsWith("in")))
	{
		height.Dump("Height Units Invalid");
		return false;
	}
	var heightNumber = int.Parse(height.Substring(0, height.Length - 2));
	if(height.EndsWith("cm") && !(heightNumber >= 150 && heightNumber <= 193))
	{
		height.Dump("Height cm Invalid");
		return false;
	}
	if (height.EndsWith("in") && !(heightNumber >= 59 && heightNumber <= 76))
	{
		height.Dump("Height in Invalid");
		return false;
	}
	var hairColor = pairs["hcl"];
	if (!Regex.IsMatch(hairColor, @"^#[0-9a-f]{6}$"))
	{
		hairColor.Dump("Hair Color Invalid");
		return false;
	}
	var eyeColor = pairs["ecl"];
	var validEyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
	if(!validEyeColors.Contains(eyeColor))
	{
		eyeColor.Dump("Eye Color Invalid");
		return false;
	}
	var passportId = pairs["pid"];
	if (!Regex.IsMatch(passportId, @"^[0-9]{9}$"))
	{
		passportId.Dump("Passport Id Invalid");
		return false;
	}
	//pairs.Dump();
	return true;
}

