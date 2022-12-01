<Query Kind="Program">
  <NuGetReference>QuikGraph</NuGetReference>
  <Namespace>QuikGraph</Namespace>
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var content = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\7-1.txt");
	var lineRegex = new Regex(@"^([a-z ]*) bags contain (?:([0-9] [a-z ]*) bag[s]?[,.]?[ ]?)+$", RegexOptions.Singleline | RegexOptions.Compiled);
	var bagRegex = new Regex(@"^([0-9]) ([a-z ]*)$", RegexOptions.Singleline | RegexOptions.Compiled);
	var graph = new BidirectionalGraph<string, TaggedEdge<string, int>>();
	foreach(var line in content){
		//preprocessing
		var regexParts = lineRegex.Match(line);
		//regexParts.Groups.Dump(line);
		var bag = regexParts.Groups[1].Value.Trim();
		var connectionParts = regexParts.Groups[2].Captures.Select(x => bagRegex.Match(x.Value)).ToList();
		var connections = connectionParts.Select(x => new BagData { Count = int.Parse(x.Groups[1].Value), Color = x.Groups[2].Value.Trim() }).ToList();
		//line.Dump();
		//bag.Dump();
		//connections.Dump();
		//graph
		foreach (var c in connections)
		{
			if(!graph.ContainsVertex(bag))
			{
				graph.AddVertex(bag);
			}
			if (!graph.ContainsVertex(c.Color))
			{
				graph.AddVertex(c.Color);
			}
			graph.AddEdge(new TaggedEdge<string, int>(bag, c.Color, c.Count));
		}
	}
	
	//graph.Dump();
	
	//var final = new List<string>();
	(WalkBack(graph, "shiny gold", 1) -1).Dump();
	//final.Distinct().Dump();
}

public class BagData {
	public int Count {get;set;}
	public string Color {get;set;}
}

public int WalkBack(BidirectionalGraph<string, TaggedEdge<string, int>> graph, string vertex, int parentCount){
	var leafTotal = parentCount;
	foreach(var e in graph.OutEdges(vertex)){
		e.Dump();
		var newCount = (e.Tag * parentCount);
		//newCount.Dump("newCount");
		leafTotal += WalkBack(graph, e.Target, newCount);
	}
	leafTotal.Dump("leafTotal");
	return leafTotal;
}

