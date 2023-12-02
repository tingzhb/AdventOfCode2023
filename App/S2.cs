public class S2 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($@"B:\Projects\AdventOfCode2023\Inputs\{fileNumber}.txt");
		var idSum = 0;
		var totalPower = 0;
		
		foreach (var entry in stringArray){
			var result = entry.Split(':');
			var showings = result[1].Split(';');
			var passed = true;
			

			var blue = 0;
			var green = 0;
			var red = 0;
			
			foreach (var showing in showings){
				var cubes = showing.Split(',');
				foreach (var cubeInfo in cubes){
					var cube = cubeInfo.Split(" ");
					int.TryParse(cube[1], out var cubeQuantity);
					var cubeColor = cube[2];
					
					switch (cubeColor){
						case "blue":
							blue = GetBiggerNumber(blue, cubeQuantity);
							break;
						case "green":
							green = GetBiggerNumber(green, cubeQuantity);
							break;
						case "red":
							red = GetBiggerNumber(red, cubeQuantity);
							break;
					}
					
					if (!CheckMax(cubeQuantity, cubeColor)){
						passed = false;
					}
				}
			}
			var power = blue * green * red;
			totalPower += power;
			
			if (passed){
				var gameId = result[0].Split(" ");
				int.TryParse(gameId[1], out var id);
				idSum += id;
			}
		}
		
		Console.WriteLine("S2: " + idSum);
		Console.WriteLine("S2a: " + totalPower);
		
		return;

		bool CheckMax(int quantity, string color){
			return color switch{
				"blue" => 14 - quantity >= 0,
				"green" => 13 - quantity >= 0,
				"red" => 12 - quantity >= 0,
				_ => false
			};
		}

		int GetBiggerNumber(int previous, int current){
			return previous > current ? previous : current;
		}
	}
}