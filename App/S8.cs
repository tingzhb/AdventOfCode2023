using System.Collections;
using System.Numerics;

public class S8 {
	public static void RunSolution(string fileNumber){
		// var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var lineArray = File.ReadAllLines($"/Users/ben/Projects/AdventOfCode2023/Inputs/{fileNumber}.txt");
		var directions = lineArray[0].ToCharArray();
		var nodes = new SortedList<string, Tuple<string, string>>();
		var startNodes = new SortedList<string, bool>();
		var lcm = new SortedList<string, int>();

		AddNodes(lineArray, nodes);

		foreach (var node in nodes.Where(node => EndsWith(node.Key) == "A")){
			startNodes.Add(node.Key, false);
		}
		var nodeList = startNodes;
		var currentStep = 0;

		while (nodeList.ContainsValue(false)){
			var newNodeList = new SortedList<string, bool>();
			
			foreach (var node in nodeList){
				var nextNode = node.Key;
				nodes.TryGetValue(nextNode, out var nextNodes);
				nextNode = directions[currentStep % directions.Length] == 'L' ? nextNodes.Item1 : nextNodes.Item2;
				
				if (EndsWith(nextNode) == "Z"){
					newNodeList.Add(nextNode, true);
					lcm.TryAdd(nextNode, currentStep + 1);
				} else {
					newNodeList.Add(nextNode, false);
				}
			}
			nodeList = newNodeList;
			currentStep++;
			if (lcm.Count >= startNodes.Count){
				foreach (var value in lcm){
					Console.WriteLine(value.Value);
				}
				break;
			}
		}
		Console.WriteLine(currentStep);
	}

	static string EndsWith(string input){
		var characters = input.ToCharArray();
		return characters[^1].ToString();
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