<Query Kind="Program">
  <Namespace>System.Buffers</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input", "5-1.txt");
	var seats = File.ReadAllLines(inputFile).ToList();

	var decodedSeats = new List<Seat>();
	foreach (var seatRaw in seats)
	{
		//seatRaw.Dump();
		var decoded = DecodeSeat(seatRaw);
		decodedSeats.Add(decoded);
	}
	var plane = new bool[128, 8];
	foreach (var d in decodedSeats){
		plane[d.Row, d.Column] = true;
	}
	plane.Dump();
}

public Seat DecodeSeat(string seat)
{
	var rowData = seat.Substring(0, 7);
	//rowData.Dump();
	var rowUpper = 127;
	var rowLower = 0;
	var row = 0;
	for (var rowCount = 0; rowCount < 6; rowCount++)
	{
		//rowData[rowCount].Dump();
		//lower half
		if (rowData[rowCount] == 'F')
		{
			rowUpper -= (int)Math.Round((rowUpper - rowLower) / 2.0);
		}
		//upper half
		if (rowData[rowCount] == 'B')
		{
			rowLower += (int)Math.Round((rowUpper - rowLower) / 2.0);
		}
	}
	//lower half
	if (rowData[6] == 'F')
	{
		row = rowLower;
	}
	//upper half
	if (rowData[6] == 'B')
	{
		row = rowUpper;
	}
	//row.Dump();

	var columnData = seat.Substring(7, 3);
	var columnUpper = 7;
	var columnLower = 0;
	var column = 0;
	for (var columnCount = 0; columnCount < 2; columnCount++)
	{
		//columnData[columnCount].Dump();
		//lower half
		if (columnData[columnCount] == 'L')
		{
			columnUpper -= (int)Math.Round((columnUpper - columnLower) / 2.0);
		}
		//upper half
		if (columnData[columnCount] == 'R')
		{
			columnLower += (int)Math.Round((columnUpper - columnLower) / 2.0);
		}
		//columnUpper.Dump();
		//columnLower.Dump();
	}
	//lower half
	if (columnData[2] == 'L')
	{
		column = columnLower;
	}
	//upper half
	if (columnData[2] == 'R')
	{
		column = columnUpper;
	}
	//column.Dump();
	return new Seat{
		Row = row,
		Column = column
	};
}


public class Seat
{
	public int Row {get;set;}
	public int Column { get; set;}
}
