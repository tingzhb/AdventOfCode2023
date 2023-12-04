public class S4 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var totalPoints = 0;
		var wonCountList = new List<int>();
		var cardNumber = 0;
		var cardList = new List<GameCard>();
		
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

			var calculatedCard = new GameCard{
				CardNumber = cardNumber,
				Wins = wonCount
			};
			cardList.Add(calculatedCard);
			
			cardNumber++;
			
			totalPoints = CalculatePoints(wonCount, points, totalPoints);
		}
		Console.WriteLine("S4: " + totalPoints);

		for (var cardIndex = 0; cardIndex < cardList.Count; cardIndex++){
			var card = cardList[cardIndex];
			
			var wins = card.Wins;
			if (wins > 0){
				for (var i = 1; i < wins + 1; i++){
					cardList.Add(cardList[card.CardNumber + i]);
				}
			}
		}
		Console.WriteLine("S4A: " + cardList.Count);
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

	class GameCard {
		public int CardNumber { get; set; }
		public int Wins { get; set; }
	}
}