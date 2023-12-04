public class S4 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var totalPoints = 0;
		
		foreach (var card in stringArray){
			var cardData = card.Split(':');
			var cards = cardData[1].Split('|');
			var winningNumberArray = cards[0].Split(' ');
			var scratchNumberArray = cards[1].Split(' ');
			var winningNumbers = new List<int>();
			var scratchNumbers = new List<int>();
			
			var wonNumbers = new List<int>();
			var points = 0;

			foreach (var winningNumber in winningNumberArray){
				if (int.TryParse(winningNumber, out var number) && number > 0){
					winningNumbers.Add(number);
				}
			}

			foreach (var scratchNumber in scratchNumberArray){
				if (int.TryParse(scratchNumber, out var number) && number >0){
					scratchNumbers.Add(number);
				}
			}
			
			foreach (var winningNumber in winningNumbers){
				if (scratchNumbers.Contains(winningNumber)){
					wonNumbers.Add(winningNumber);
				}
			}
			var wonCount = wonNumbers.Count;
			Console.WriteLine(wonCount);
			if (wonCount > 0){
				points = 1;
				for (var i = 1; i < wonCount; i++){
					points *= 2;
				}
			}
			totalPoints += points;
		}
		Console.WriteLine("S4: " + totalPoints);
		
	}
}