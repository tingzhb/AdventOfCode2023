using System.Collections;
using System.Numerics;

public class S10 {
	public static void RunSolution(string fileNumber){
		var lineArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		// var lineArray = File.ReadAllLines($"/Users/ben/Projects/AdventOfCode2023/Inputs/{fileNumber}.txt");
		var mapPlan = new Dictionary<Vector2, MapTile>();
		var startLocation = new Vector2();
		var tilesVisited = 0;
		var path = new Dictionary<Vector2, MapTile>();
		var maxX = lineArray.Length;
		var maxY = lineArray[0].ToCharArray().Length;
		var surroundedTiles = new List<Vector2>();
		
		for (var y = 0; y < maxX; y++){
			var characters = lineArray[y].ToCharArray();
			for (var x = 0; x < characters.Length; x++){
				var location = new Vector2(x, y);
				var name = characters[x];
				var mapTile = new MapTile();
				mapTile.Initialize(name);
				if (mapTile.Type == PipeType.Start){
					startLocation = location;
				}
				mapPlan.Add(location, mapTile);
			}
		}

		mapPlan.TryGetValue(new Vector2(3, 2), out var test);


		FindNextTile(startLocation);

		FindSurroundedTiles();
		return;

		void FindSurroundedTiles(){
			foreach (var visitedTile in path){
				// CheckRight(visitedTile);
			}
		}

		void CheckRight(KeyValuePair<Vector2, MapTile> visitedTile){
			var nextTile = new MapTile();
			var checkingDirection = Directions.East;


			
			while (!nextTile.Visited){

				
				
				var nextTileLocation = visitedTile.Key + checkingDirection;
				mapPlan.TryGetValue(nextTileLocation, out nextTile);
			}
		}
		void FindNextTile(Vector2 currentLocation){
			var startFound = false;
			var previousLocation = currentLocation;
			while (!startFound){
				mapPlan[currentLocation].Visited = true;
				path.Add(currentLocation, mapPlan[currentLocation]);
				
				mapPlan.TryGetValue(currentLocation, out var mapTile);
				if (mapTile.Access.Count == 0){
					break;
				}
				Console.WriteLine(mapTile.Type);
				foreach (var accessPoint in mapTile.Access){
					var potentialLocation = currentLocation + accessPoint;
					if (potentialLocation - startLocation == Vector2.Zero){
						startFound = true;
						var result = (tilesVisited + 1) / 2;
						Console.WriteLine(result);
						break;
					} 						
					if (potentialLocation - previousLocation != Vector2.Zero){
						// Console.WriteLine("Current Location: " + currentLocation);
						// Console.WriteLine("AP: " + accessPoint);
						// Console.WriteLine("New Location: " + potentialLocation);
						previousLocation = currentLocation;
						currentLocation = potentialLocation;
						tilesVisited++;
						break;
					}
				}
			}
		}
	}

	enum PipeType {
		Vertical, Horizontal, NorthEast, NorthWest, SouthWest, SouthEast, Ground, Start
	}

	struct Directions{
		public static readonly Vector2 North = new (0,-1);
		public static readonly Vector2 South = new (0, 1);
		public static readonly Vector2 East = new (1, 0);
		public static readonly Vector2 West = new (-1, 0);
	}
	class MapTile {
		public PipeType Type { get; private set; }
		public char Name { get; private set; }
		public List<Vector2> Access { get; private set; }
		
		public bool Visited { get; set; }
		
		public void Initialize (char name){
			Name = name;
			Access = new List<Vector2>();
			
			// var north = new Vector2(0, -1);
			// var south = new Vector2(0, 1);
			// var east = new Vector2(1, 0);
			// var west = new Vector2(-1, 0);
			switch (name){
				case '|':
					Type = PipeType.Vertical;
					Access.Add(Directions.North);
					Access.Add(Directions.South);
					break;
				case '-':
					Type = PipeType.Horizontal;
					Access.Add(Directions.East);
					Access.Add(Directions.West);
					break;
				case 'L':
					Type = PipeType.NorthEast;
					Access.Add(Directions.North);
					Access.Add(Directions.East);
					break;
				case 'J':
					Type = PipeType.NorthWest;
					Access.Add(Directions.North);
					Access.Add(Directions.West);
					break;
				case '7':
					Type = PipeType.SouthWest;
					Access.Add(Directions.South);
					Access.Add(Directions.West);
					break;
				case 'F':
					Type = PipeType.SouthEast;
					Access.Add(Directions.South);
					Access.Add(Directions.East);
					break;
				case '.':
					Type = PipeType.Ground;
					break;
				case 'S':
					Type = PipeType.Start;
					// Access.Add(north);
					// Access.Add(south);
					Access.Add(Directions.East);
					// Access.Add(west);
					break;
				default:
					Type = PipeType.Ground;
					break;
			}
			
		}
	}
}