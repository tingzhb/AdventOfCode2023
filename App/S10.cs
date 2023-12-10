using System.Collections;
using System.Numerics;

public class S10 {
	public static void RunSolution(string fileNumber){
		// var stringArray = File.ReadAllLines($"B:\\Projects\\AdventOfCode2023\\Inputs\\{fileNumber}.txt");
		var lineArray = File.ReadAllLines($"/Users/ben/Projects/AdventOfCode2023/Inputs/{fileNumber}.txt");
		var mapPlan = new Dictionary<Vector2, MapTile>();
		var startLocation = new Vector2();

		for (var x = 0; x < lineArray.Length; x++){
			var characters = lineArray[x].ToCharArray();
			for (var y = 0; y < characters.Length; y++){
				var location = new Vector2(x, y);
				var name = characters[y];
				var mapTile = new MapTile();
				mapTile.Initialize(name);
				if (mapTile.Type == PipeType.Start){
					startLocation = location;
				}
				mapPlan.Add(location, mapTile);
			}
		}

		FindNextTile(startLocation);

		void FindNextTile(Vector2 location){
			mapPlan.TryGetValue(location, out var mapTile);
			foreach (var accessPoint in mapTile.Access){
				var nextTileLocation = location + accessPoint;
				FindNextTile(nextTileLocation);
			}
		}
	}

	enum PipeType {
		Vertical, Horizontal, NorthEast, NorthWest, SouthWest, SouthEast, Ground, Start
	}
	
	class MapTile {
		public PipeType Type { get; private set; }
		public char Name { get; private set; }
		public List<Vector2> Access { get; private set; }
		
		public  void Initialize (char name){
			Name = name;
			Access = new List<Vector2>();
			
			var north = new Vector2(0, 1);
			var south = new Vector2(0, -1);
			var east = new Vector2(-1, 0);
			var west = new Vector2(1, 0);
			switch (name){
				case '|':
					Type = PipeType.Vertical;
					Access.Add(north);
					Access.Add(south);
					break;
				case '-':
					Type = PipeType.Horizontal;
					Access.Add(east);
					Access.Add(west);
					break;
				case 'L':
					Type = PipeType.NorthEast;
					Access.Add(north);
					Access.Add(east);
					break;
				case 'J':
					Type = PipeType.NorthWest;
					Access.Add(north);
					Access.Add(west);
					break;
				case '7':
					Type = PipeType.SouthWest;
					Access.Add(south);
					Access.Add(west);
					break;
				case 'F':
					Type = PipeType.SouthEast;
					Access.Add(south);
					Access.Add(east);
					break;
				case '.':
					Type = PipeType.Ground;
					break;
				case 'S':
					Type = PipeType.Start;
					Access.Add(north);
					Access.Add(south);
					Access.Add(east);
					Access.Add(west);
					break;
				default:
					Type = PipeType.Ground;
					break;
			}
			
		}
	}
}