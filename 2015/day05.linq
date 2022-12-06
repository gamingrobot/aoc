<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2015, 5);

    Part1(content).Dump("Part1");
}

int Part1(List<string> input)
{
    return input.Where(x => CountVowels(x) >= 3 && ContainsAdjacentDuplicate(x) && !ContainsBadWords(x)).Count();
}

int CountVowels(string input)
{
    var chars = new char[] { 'a', 'e', 'i', 'o', 'u' };
    return input.Where(x => chars.Contains(x)).Count();
}

bool ContainsAdjacentDuplicate(string input)
{
    var chars = input.ToCharArray();
    char prev = ' ';
    foreach (var c in chars)
    {
        if (c == prev)
        {
            return true;
        }
        prev = c;
    }
    return false;
}

bool ContainsBadWords(string input)
{
    return input.Contains("ab") || input.Contains("cd") || input.Contains("pq") || input.Contains("xy");
}
