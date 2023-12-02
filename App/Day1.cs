public class Day1 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($@"B:\Projects\AdventOfCode2023\Inputs\{fileNumber}.txt");
		var twinNumberList = new List<int>();

		foreach (var stringEntry in stringArray){
			var numberList = new List<int>();
			var charArray = stringEntry.ToCharArray();
			int twinNumber;

			foreach (var character in charArray){
				int.TryParse(character.ToString(), out var entry);

				if (entry != 0){
					numberList.Add(entry);
				}
			}

			if (numberList.Count == 1){
				twinNumber = numberList[0] * 10 + numberList[0];
			} else {
				twinNumber = numberList[0] * 10 + numberList[^1];
			}
			twinNumberList.Add(twinNumber);
		}
		var result = twinNumberList.Sum();
		Console.WriteLine("Day 1: " + result);
	}
}