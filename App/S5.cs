using System.Numerics;

public class S5 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var seedList = new List<BigInteger>();
		var sectionList = new List<BigInteger>();
		var mapList = new List<List<BigInteger>>();
		var seedToSoilList = new List<BigInteger>();
		var soilToFertilizerList = new List<BigInteger>();
		var fertilizerToWaterList = new List<BigInteger>();
		var waterToLightList = new List<BigInteger>();
		var lightToTemperatureList = new List<BigInteger>();
		var temperatureToHumidityList = new List<BigInteger>();
		var humidityToLocationList = new List<BigInteger>();
		var lastList = new List<BigInteger>();
		var resultList = new List<BigInteger>();
		

		mapList.Add(seedToSoilList);
		mapList.Add(soilToFertilizerList);
		mapList.Add(fertilizerToWaterList);
		mapList.Add(waterToLightList);
		mapList.Add(lightToTemperatureList);
		mapList.Add(temperatureToHumidityList);
		mapList.Add(humidityToLocationList);
		mapList.Add(lastList);
		
		FindSeeds(stringArray, seedList);

		FindSectionLines(stringArray, sectionList);
		
		AddDataToLists(stringArray, sectionList, mapList);
		FindLocation(mapList, seedList, resultList);
		
		Console.WriteLine("S5: "+ resultList.Min());
	}

	static void FindLocation(List<List<BigInteger>> mapList, List<BigInteger> seedList, List<BigInteger> resultList){
		foreach (var seed in seedList){
			BigInteger result = 0;
			if (seed == 0){
				break;
			}
			foreach (var map in mapList){
				if (result == 0){
					result = seed;
				}
				for (var i = 0; i < map.Count; i += 3){
					var affectedLines = map[i + 2];
					var destinationStart = map[i];
					var sourceStart = map[i + 1];
					var sourceEnd = sourceStart + affectedLines;

					if (result >= sourceStart && result <= sourceEnd){
						var resultDelta = result - sourceStart;
						result = resultDelta + destinationStart;
						break;
					}
				}
			}
			resultList.Add(result);
		}
	}

	static void AddDataToLists(string[] stringArray, List<BigInteger> sectionList, List<List<BigInteger>> mapList){
		for (var i = 0; i < sectionList.Count - 1; i++){
			var startIndex = sectionList[i] + 1;
			var endIndex = sectionList[i + 1] - 1;
			for (var j = startIndex; j < endIndex; j++){
				var stringLine = stringArray[(int) j].Split(" ");
				foreach (var mapString in stringLine){
					BigInteger.TryParse(mapString, out var map);
					mapList[i].Add(map);
				}
			}
		}
	}
	static void FindSectionLines(string[] stringArray, List<BigInteger> sectionList){
		for (var i = 0; i < stringArray.Length; i++){
			var line = stringArray[i];
			switch (line){
				case "seed-to-soil map:":
					sectionList.Add(i);
					break;
				case "soil-to-fertilizer map:":
					sectionList.Add(i);
					break;
				case "fertilizer-to-water map:":
					sectionList.Add(i);
					break;
				case "water-to-light map:":
					sectionList.Add(i);
					break;
				case "light-to-temperature map:":
					sectionList.Add(i);
					break;
				case "temperature-to-humidity map:":
					sectionList.Add(i);
					break;
				case "humidity-to-location map:":
					sectionList.Add(i);
					break;
				case "last:":
					sectionList.Add(i);
					break;
			}
		}
	}
	static void FindSeeds(string[] stringArray, List<BigInteger> seedList){
		var seedLine = stringArray[0].Split(" ");
		for (var i = 1; i < seedLine.Length; i++){
			BigInteger.TryParse(seedLine[i], out var seedNumber);
			seedList.Add(seedNumber);
		}
	}
	
	// static void FindSeeds(string[] stringArray, List<BigInteger> seedList){
	// 	var seedDetailsList = new List<BigInteger>();
	// 	var seedLine = stringArray[0].Split(" ");
	// 	for (var i = 1; i < seedLine.Length; i++){
	// 		BigInteger.TryParse(seedLine[i], out var seedDetails);
	// 		seedDetailsList.Add(seedDetails);
	// 	}
	// 	for (var i = 0; i < seedDetailsList.Count; i += 2){
	// 		var seedStart = seedDetailsList[i];
	// 		var seedEnd = seedDetailsList[i + 1] + seedStart;
	// 		for (var j = seedStart; j <= seedEnd; j++){
	// 			seedList.Add(j);
	// 		}
	// 	}
	// }
}