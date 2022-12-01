<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Runtime.Serialization</Namespace>
</Query>

void Main()
{
	var content = File.ReadAllLines(@"C:\Projects\adventofcode\aoc-2020\input\11-1.txt");
	var mapping = new Dictionary<char, State>
	{
		{ 'L', State.Open },
		{ '#', State.Closed },
		{ '.', State.Floor }
	};

	var room = content.Select(x => x.ToArray().Select(y => mapping[y]).ToArray()).ToArray();
	var maxRow = room.Length;
	var maxColumn = room[0].Length;

	//DumpState(room);
	var done = false;
	var count = 1;
	while (!done)
	{
		"".Dump();
		count.Dump();
		DumpState(room);
		done = true;
		var mutableRoom = room.Select(x => x.ToArray()).ToArray();
		for (var row = 0; row < maxRow; row++)
		{
			for (var column = 0; column < maxColumn; column++)
			{
				if (room[row][column] == State.Floor)
				{
					continue;
				}
				var occupied = GetOccupiedCount(room, row, column, maxRow, maxColumn);
				//occupied.Dump($"{row},{column}");
				if (occupied == 0 && mutableRoom[row][column] != State.Closed)
				{
					done = false;
					mutableRoom[row][column] = State.Closed;
				}
				else if (occupied >= 4 && mutableRoom[row][column] != State.Open)
				{
					done = false;
					mutableRoom[row][column] = State.Open;
				}
			}
		}
		room = mutableRoom;
		//"".Dump();
		//DumpState(room);
		count++;
	}

	var seatCount = 0;
	for (var row = 0; row < maxRow; row++)
	{
		for (var column = 0; column < maxColumn; column++)
		{
			if(room[row][column] == State.Closed){
				seatCount++;
			}
		}
	}
	seatCount.Dump("Seats Occupied");
}
public int GetOccupiedCount(State[][] state, int row, int column, int maxRow, int maxColumn)
{
	var count = 0;
	//check up left
	if(row - 1 >= 0 && column - 1 >= 0)
	{
		if(state[row-1][column-1] == State.Closed)
		{
			//"up left".Dump();
			count++;
		}
	}
	//check up
	if (row - 1 >= 0)
	{
		if (state[row - 1][column] == State.Closed)
		{
			//"up".Dump();
			count++;
		}
	}
	//check up right
	if (row - 1 >= 0 && column + 1 < maxColumn)
	{
		if (state[row - 1][column + 1] == State.Closed)
		{
			//"up right".Dump();
			count++;
		}
	}
	//check right
	if (column + 1 < maxColumn)
	{
		if (state[row][column + 1] == State.Closed)
		{
			//"right".Dump();
			count++;
		}
	}
	//check down right
	if (row + 1 < maxRow && column + 1 < maxColumn)
	{
		if (state[row + 1][column + 1] == State.Closed)
		{
			//"down right".Dump();
			count++;
		}
	}
	//check down
	if (row + 1 < maxRow)
	{
		if (state[row + 1][column] == State.Closed)
		{
			//"down".Dump();
			count++;
		}
	}
	//check  down left
	if (row + 1 < maxRow && column - 1 >= 0)
	{
		if (state[row + 1][column - 1] == State.Closed)
		{
			//"down left".Dump();
			count++;
		}
	}
	//check left
	if (column - 1 >= 0)
	{
		if (state[row][column - 1] == State.Closed)
		{
			//"left".Dump();
			count++;
		}
	}
	return count;
}

public void DumpState(State[][] state){
	var sb = new StringBuilder();
	foreach(var row in state){
		foreach(var seat in row){
			switch(seat){
				case State.Open:
					sb.Append('L');
					break;
				case State.Closed:
					sb.Append('#');
					break;
				case State.Floor:
					sb.Append('.');
					break;
			}
		}
		sb.AppendLine();
	}
	//monospace dump
	Util.WithStyle(sb.ToString(), ";font-family:Consolas,'Lucida Console','Courier New',monospace").Dump();
}

public enum State
{
	Floor,
	Open,
	Closed
}