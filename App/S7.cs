using System.Numerics;

public class S7 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var cardList = new List<Card>();
		var scoreList = new List<int>();

		
		foreach (var line in stringArray){
			var cardValueList = new List<int>();
			
			var section = line.Split(" ");
			
			int.TryParse(section[1], out var bet);

			var symbols = section[0].ToCharArray();

			foreach (var symbol in symbols){
				var value = CardToNumbers(symbol);
				cardValueList.Add(value);
			}
			var card = new Card{
				Values = cardValueList,
				Bet = bet,
				Wins = 1
			};
			cardList.Add(card);
		}
		foreach (var card in cardList){
			card.Suit = EvaluateCards(card);
		}
		var orderedList = cardList.OrderByDescending(x => (int) x.Suit).ToList();
		
		// var orderedListLength = orderedList.Count;
		//
		// for (var i = orderedListLength - 1; i >= 0; i--){
		// 	orderedList[i].Wins = i + 1;
		// }


		foreach (var card in orderedList){
			foreach (var testCard in orderedList){
				if (card == testCard)
					continue;
				
				if (card.Suit < testCard.Suit){
					card.Wins++;
				}
				else if (card.Suit == testCard.Suit){
					for (var j = 0; j < card.Values.Count; j++){
						// Console.WriteLine(card.Suit + " " + card.Values[j] + " VS " + card.Suit + " " + testCard.Values[j]);
						if (card.Values[j] > testCard.Values[j]){
							card.Wins++;
							j = card.Values.Count + 1;
						}
						else if (card.Values[j] < testCard.Values[j]){
							j = card.Values.Count + 1;
						}
					}
				}
			}
		}
		foreach (var card in cardList){
			var score = card.Wins * card.Bet;
			scoreList.Add(score);
		}
		Console.WriteLine(scoreList.Sum());
		
	}

	
	static Suits EvaluateCards(Card card){
		var matchingList = new SortedList<int, int>();
		var cleanedList = new SortedList<int, int>();
		foreach (var cardValue in card.Values){
			if (matchingList.ContainsKey(cardValue)){
				matchingList[cardValue]++;
			} else {
				matchingList.Add(cardValue, 1);
			}
		}
		foreach (var match in matchingList){
			if (match.Value >= 1){
				cleanedList.Add(match.Key, match.Value);
			}
		}
		var matches = cleanedList.Count;
		cleanedList.TryGetValue(1, out var jokers);
		Console.WriteLine(jokers);
		
		if (matches == 0){
			return Suits.None;
		}

		if (matches == 1){
			return cleanedList.GetValueAtIndex(0) switch{
				2 => Suits.One,
				3 => Suits.Three,
				4 => Suits.Four,
				5 => Suits.Five,
			};
		}
		if (matches == 2){
			if (cleanedList.GetValueAtIndex(0) ==  cleanedList.GetValueAtIndex(1)){
				return Suits.Two;
			}
			return Suits.Full;
		}
		return Suits.None;
	}
	
	
	static int CardToNumbers(char cardSymbol){
		int.TryParse(cardSymbol.ToString(), out var number);
		if (number > 0){
			return number;
		}
		switch (cardSymbol){
			case 'A':
				return 14;
			case 'K':
				return 13;
			case 'Q':
				return 12;
			case 'J':
				return 1;
			case 'T':
				return 10;
			default:
				throw new Exception();
		}
	}

	class Card {
		public List<int> Values { get; set; }
		public int Bet { get; set; }
		public int Wins { get; set; }
		public Suits Suit { get; set; }
	}

	enum Suits {
		Five, Four, Full, Three, Two, One, None
	}
}