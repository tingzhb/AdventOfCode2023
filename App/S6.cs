public class S6 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var timesDistance = new List<int>();
		var raceInstances = 0;
		var result = 1;
		
		GetEntries(stringArray, timesDistance);
		raceInstances = timesDistance.Count/2;

		
		for (var i = 0; i < raceInstances; i++){
			var availableTime = timesDistance[i];
			var recordDistance = timesDistance[i + raceInstances];
			result *= GetBeatTimes(availableTime, recordDistance);
		}
		Console.WriteLine("S6: "+ result);
	}

	static int GetBeatTimes(int availableTime, int recordDistance){
		var beatTimes = 0;
		for (var time = 0; time <= availableTime; time++){
			var distance = time * (availableTime - time);
			if (distance > recordDistance){
				beatTimes++;
			}
		}
		return beatTimes;
	}

	static Tuple<float, float> QuadraticConversion(float a, float b, float c){
		var root1 = 0f;
		var root2 = 0f;
		var discriminant = b * b - 4 * a * c;

		if (discriminant > 0){
			root1 = (-b + MathF.Sqrt(discriminant)) / (2 * a);
			root2 = (-b - MathF.Sqrt(discriminant)) / (2 * a);
		}
		if (discriminant == 0){
			root1 = root2 = -b / (2 * a);
		}
		return new Tuple<float, float>(root1, root2);
	} 
	
	static void GetEntries(string[] stringArray, List<int> timesDistance){
		foreach (var entry in stringArray){
			var lines = entry.Split(":");
			for (var j = 1; j < lines.Length; j++){
				var data = lines[j].Split(" ");
				foreach (var number in data){
					int.TryParse(number, out var integer);
					if (integer > 0){
						timesDistance.Add(integer);
					}
				}
			}
		}
	}
}