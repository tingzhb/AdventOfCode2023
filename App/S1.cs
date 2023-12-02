public class S1 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($@"B:\Projects\AdventOfCode2023\Inputs\{fileNumber}.txt");
		var twinNumberList = new List<int>();
		var numbers = new Dictionary<string, int>();
		
		numbers.Add("one", 1);
		numbers.Add("two", 2);
		numbers.Add("three", 3);
		numbers.Add("four", 4);
		numbers.Add("five", 5);
		numbers.Add("six", 6);
		numbers.Add("seven", 7);
		numbers.Add("eight", 8);
		numbers.Add("nine", 9);
		
		
		foreach (var stringEntry in stringArray){
			var numberList = new List<int>();
			var editedString = ConvertTextToNumbers(numbers, stringEntry);
			var charArray = editedString.ToCharArray();
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
		Console.WriteLine("S1a: " + result);
		
		return;
		
		string ConvertTextToNumbers(Dictionary<string, int> numberDictionary, string stringEntry){
			var proceduralString = "";
			var resultString = "";
			foreach (var character in stringEntry){
				proceduralString += character;
				int.TryParse(character.ToString(), out var integer);
				if (integer > 0){
					resultString += integer;
				}
				foreach (var (key, value) in numberDictionary){
					if (proceduralString.Contains(key)){
						var newString = key.Remove(0,1);
						proceduralString = proceduralString.Replace(key, newString);
						resultString += value;
					}
				}
			}
			return resultString;
		}
	}
}