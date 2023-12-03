using System.Numerics;

public class S3{
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($@"B:\Projects\AdventOfCode2023\Inputs\{fileNumber}.txt");
		var locationDictionary = new Dictionary<Vector2, char>();
		var symbolsArray = new[] {'*', '%', '&', '@', '+', '#', '/', '$', '=', '-'};
		var potentialPartLocations = new Dictionary<int, List<Vector2>>();
		var partLocations = new Dictionary<Vector2, int>();
		var foundParts = new List<int>();

		for (var y = 0; y < stringArray.Length; y++){
			var lineArray = stringArray[y].ToCharArray();
			for (var x = 0; x < lineArray.Length; x++){
				locationDictionary.Add(new Vector2(x, y), lineArray[x]);
			}
		}

		var count = 0;
		foreach (var (location, value) in locationDictionary){
			if (value == '.'){
				continue;
			}
			
			if (symbolsArray.Contains(value)){
				var x = location.X;
				var y = location.Y;
				var suspectedParts = new List<Vector2>();

				for (var i = -1; i < 2; i++){
					for (var j = -1; j < 2; j++){
						if (i == 0 && j == 0)
							continue;
						var xCoord = x + i;
						var yCord = y + j;
						if (xCoord >= 0 && yCord >= 0 ){
							suspectedParts.Add(new Vector2(xCoord, yCord));
						}
					}
				}
				potentialPartLocations.Add(count, suspectedParts);
				count++;
			} else{
				int.TryParse(value.ToString(), out var integer);
				partLocations.Add(location, integer);
			}
		}
		for (var y = 0; y < partLocations.Count; y++){
			for (var x = 0; x < partLocations.Count; x++){
				var location = new Vector2(x, y);
				if (partLocations.TryGetValue(location, out var value)){

					var firstPosition = new Vector2(x + 1, y);
					var secondPosition = new Vector2(x + 2, y);
					var number = value;
					
					if (partLocations.TryGetValue(firstPosition, out var firstValue)){
						number *= 10;
						number += firstValue;
						partLocations[location] = number;
						partLocations[firstPosition] = number;
						x++;
						if (partLocations.TryGetValue(secondPosition, out var secondValue)){
							number *= 10;
							number += secondValue;
							partLocations[location] = number;
							partLocations[firstPosition] = number;
							partLocations[secondPosition] = number;
							x++;
						}
					}
				}
			}
		}
		
		foreach (var instance in potentialPartLocations){
			var valueList = new List<int>();
			foreach (var location in instance.Value){
				if (partLocations.TryGetValue(location, out var value)){
					valueList.Add(value);
				}
			}
			valueList = valueList.Distinct().ToList();
			foundParts.AddRange(valueList);
		}
		foreach (var item in foundParts){
			Console.WriteLine(item);
		}
		var result = foundParts.Sum();


		Console.WriteLine("S3: " + result);
		// return;
	}
}