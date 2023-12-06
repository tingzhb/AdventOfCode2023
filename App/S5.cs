using System.Numerics;

public class S5 {
	public static void RunSolution(string fileNumber){
		var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var seedList = new List<Tuple<BigInteger, BigInteger>>();
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

	static void FindLocation(List<List<BigInteger>> mapList, List<Tuple<BigInteger, BigInteger>> seedList, List<BigInteger> resultList){
		var seedFound = false;

		BigInteger locationNumber = 0;

		while (resultList.Count <= 25){				
			var previousSeed = locationNumber;
			
			for (var i = mapList.Count - 1; i >= 0; i--){
				var map = mapList[i];
				for (var j = 0; j < map.Count; j += 3){
					var affectedLines = map[j + 2] - 1;
					var destinationStart = map[j];
					var destinationEnd = map[j] + affectedLines;
					var sourceStart = map[j + 1];
					var sourceEnd = sourceStart + affectedLines;
					if (previousSeed >= destinationStart && previousSeed <= destinationEnd){
						var resultDelta = previousSeed - destinationStart;
						previousSeed = sourceStart + resultDelta;
					}
				}
				foreach (var seed in seedList){
					if (seed.Item1 <= previousSeed && seed.Item2 >= previousSeed){
						BigInteger result = 0;

						foreach (var map2 in mapList){
							if (result == 0){
								result = previousSeed;
							}
							for (var k = 0; k < map2.Count; k += 3){
								var affectedLines = map2[k + 2] - 1;
								var destinationStart = map2[k];
								var sourceStart = map2[k + 1];
								var sourceEnd = sourceStart + affectedLines;
								if (result >= sourceStart && result <= sourceEnd){
									var resultDelta = result - sourceStart;
									result = resultDelta + destinationStart;
								}
							}
						}
						resultList.Add(locationNumber);
					}
				}
			}
			
			locationNumber++;
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
	// static void FindSeeds(string[] stringArray, List<BigInteger> seedList){
	// 	var seedLine = stringArray[0].Split(" ");
	// 	for (var i = 1; i < seedLine.Length; i++){
	// 		BigInteger.TryParse(seedLine[i], out var seedNumber);
	// 		seedList.Add(seedNumber);
	// 	}
	// }
	
	static void FindSeeds(string[] stringArray, List<Tuple<BigInteger, BigInteger>> seedList){
		var seedDetailsList = new List<BigInteger>();
		var seedLine = stringArray[0].Split(" ");
		for (var i = 1; i < seedLine.Length; i++){
			BigInteger.TryParse(seedLine[i], out var seedDetails);
			seedDetailsList.Add(seedDetails);
		}
		for (var i = 0; i < seedDetailsList.Count; i += 2){
			var seedStart = seedDetailsList[i];
			var seedEnd = seedDetailsList[i + 1] + seedStart - 1;
			seedList.Add(new Tuple<BigInteger, BigInteger>(seedStart, seedEnd));
		}
	}
}