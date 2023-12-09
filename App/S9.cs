using System.Collections;
using System.Numerics;

public class S9 {
	public static void RunSolution(string fileNumber){
		// var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var lineArray = File.ReadAllLines($"/Users/ben/Projects/AdventOfCode2023/Inputs/{fileNumber}.txt");
		var dataLines = new List<List<int>>();

		var totalDifference = 0;
		
		foreach (var line in lineArray){
			var lineStrings = line.Split(" ");
			var dataLine = new List<int>();
			foreach (var stringNumber in lineStrings){
				int.TryParse(stringNumber, out var realNumber);
				dataLine.Add(realNumber);
			}
			dataLines.Add(dataLine);
		}

		foreach (var dataLine in dataLines){
			var done = false;
			var sampleLine = dataLine;
			var lines = new List<List<int>>();
			
			var lastNumbers = new List<int>();
			
			lines.Add(sampleLine);

			while (!done){
				var line = new List<int>();
				for (var i = 0; i < sampleLine.Count - 1; i++){
					var difference = sampleLine[i + 1] - sampleLine[i];
					line.Add(difference);
				}
				if (line.Count > 0){
					lines.Add(line);
				}

				if (line.Distinct().Count() <= 1){
					done = true;
				}
				
				sampleLine = line;
			}
			foreach (var line in lines){
				lastNumbers.Add(line[^1]);
			}
			var finalDifference = lastNumbers.Sum();
			totalDifference += finalDifference;
		}
		Console.WriteLine(totalDifference);
	}
}