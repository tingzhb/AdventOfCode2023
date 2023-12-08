using System.Collections;
using System.Numerics;

public class S8 {
	public static void RunSolution(string fileNumber){
		// var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var lineArray = File.ReadAllLines($"/Users/ben/Projects/AdventOfCode2023/Inputs/{fileNumber}.txt");
		var directions = lineArray[0].ToCharArray();
		var nodes = new SortedList<string, Tuple<string, string>>();
		var steps = 0;
		var currentNode = "AAA";
		
		AddNodes(lineArray, nodes);

		while (currentNode != "ZZZ"){
			foreach (var direction in directions){
				currentNode = FindNextNode(currentNode, nodes, direction);
				steps++;
			}
		}

		Console.WriteLine(steps);
	}

	static string FindNextNode(string currentNode, SortedList<string, Tuple<string, string>> nodes, char direction){
		nodes.TryGetValue(currentNode, out var nextOptions);
		if (direction == 'L'){
			return nextOptions.Item1;
		}
		return nextOptions.Item2;
	}
	static void AddNodes(string[] lineArray, SortedList<string, Tuple<string, string>> nodes){

		for (var i = 2; i < lineArray.Length; i++){
			var strings = lineArray[i].Split(" ");
			nodes.Add(strings[0], new Tuple<string, string>(SanitizeString(strings[2]), SanitizeString(strings[3])));
		}
	}

	static string SanitizeString(string line){
		var charArray = line.ToCharArray();
		var newLine = "";
		var start = 0;
		if (charArray[0] == '('){
			start = 1;
		}
		for (var i = start; i < charArray.Length - 1; i++){
			newLine += charArray[i];
		}
		return newLine;
	}
}