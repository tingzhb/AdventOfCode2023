public class S4 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var totalPoints = 0;
		var wonCountList = new List<int>();
		
		foreach (var card in stringArray){
			var cardData = card.Split(':');
			var cards = cardData[1].Split('|');
			var winningNumberArray = cards[0].Split(' ');
			var scratchNumberArray = cards[1].Split(' ');
			var winningNumbers = new List<int>();
			var scratchNumbers = new List<int>();
			
			var points = 0;
			
			DeleteSpaces(winningNumberArray, winningNumbers);
			DeleteSpaces(scratchNumberArray, scratchNumbers);

			var wonNumbers = winningNumbers.Where(winningNumber => scratchNumbers.Contains(winningNumber)).ToList();
			var wonCount = wonNumbers.Count;
			wonCountList.Add(wonCount);
			
			totalPoints = CalculatePoints(wonCount, points, totalPoints);
		}
		Console.WriteLine("S4: " + totalPoints);
	}
	static int CalculatePoints(int wonCount, int points, int totalPoints){
		if (wonCount > 0){
			points = 1;
			for (var i = 1; i < wonCount; i++){
				points *= 2;
			}
		}
		totalPoints += points;
		return totalPoints;
	}
	static void DeleteSpaces(string[] winningNumberArray, List<int> winningNumbers){
		foreach (var winningNumber in winningNumberArray){
			if (int.TryParse(winningNumber, out var number) && number > 0){
				winningNumbers.Add(number);
			}
		}
	}
}