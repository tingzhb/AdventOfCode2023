public class S2 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($@"B:\Projects\AdventOfCode2023\Inputs\{fileNumber}.txt");
		var idSum = 0;
		
		foreach (var entry in stringArray){
			var result = entry.Split(':');
			var showings = result[1].Split(';');
			var passed = true;
			
			foreach (var showing in showings){
				var cubes = showing.Split(',');
				
				foreach (var ballInfo in cubes){
					var cube = ballInfo.Split(" ");
					int.TryParse(cube[1], out var ballQuantity);
					var cubeColor = cube[2];

					if (!CheckMax(ballQuantity, cubeColor)){
						passed = false;
					}
				}
			}
			if (passed){
				var gameId = result[0].Split(" ");
				int.TryParse(gameId[1], out var id);
				idSum += id;
			}
		}
		
		Console.WriteLine("S1: " + idSum);
		
		return;

		bool CheckMax(int quantity, string color){
			return color switch{
				"blue" => 14 - quantity >= 0,
				"green" => 13 - quantity >= 0,
				"red" => 12 - quantity >= 0,
				_ => false
			};
		}
	}
}