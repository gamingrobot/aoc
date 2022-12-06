<Query Kind="Program">
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

#load "..\common.linq"

async Task Main()
{
    var content = await Helpers.LoadInput(2015, 4);
    var input = content.Single();

    Part1(input).Dump("Part1");
    Part2(input).Dump("Part2");
}

int Part1(string input)
{
    MD5 md5Hasher = MD5.Create();
    var number = 0;
    while(true)
    {
        var check = input + number.ToString();

        byte[] data = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(check));
        if (BitConverter.ToString(data).StartsWith("00-00-0"))
        {
            check.Dump();
            break;
        }
        number++;
    }
    
    return number;
}

int Part2(string input)
{
    MD5 md5Hasher = MD5.Create();
    var number = 0;
    while (true)
    {
        var check = input + number.ToString();

        byte[] data = md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(check));
        if (BitConverter.ToString(data).StartsWith("00-00-00"))
        {
            check.Dump();
            break;
        }
        number++;
    }

    return number;
}
