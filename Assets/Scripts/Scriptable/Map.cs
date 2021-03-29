using System.Collections.Generic;

using UnityEngine;

using Cells;

namespace Scriptable {
	[CreateAssetMenu]
	public class Map : ScriptableObject {
		private readonly Dictionary<Vector2Int, Cell> _map = new Dictionary<Vector2Int, Cell>();
		private readonly HashSet<Vector2Int> _set = new HashSet<Vector2Int>();
		private readonly Dictionary<Vector2Int, Food> _foodMap = new Dictionary<Vector2Int, Food>();

		public Cell GetCell(Vector2Int position) {
			return _map.TryGetValue(position, out var cell) ? cell : null;
		}

		public bool SetCell(Vector2Int position, Cell cell) {
			_map.Add(position, cell);
			return true;
		}

		public bool RemoveCell(Vector2Int position) {
			return _map.Remove(position);
		}

		public void MarkCell(Vector2Int position) {
			_set.Add(position);
		}

		public void UnmarkCell(Vector2Int position) {
			_set.Remove(position);
		}

		public bool IsMarked(Vector2Int position) {
			return _set.Contains(position);
		}

		public Food GetFood(Vector2Int position) {
			return _foodMap.TryGetValue(position, out var food) ? food : null;
		}

		public void SetFood(Vector2Int position, Food food) {
			_foodMap.Add(position, food);
		}

		public void RemoveFood(Vector2Int position) {
			_foodMap.Remove(position);
		}

		public Vector2Int? GenerateFoodPosition(RectInt range) {
			var list = new List<Vector2Int>();
			foreach (var point in range.allPositionsWithin) {
				if (!IsMarked(point) && !_foodMap.ContainsKey(point)) {
					list.Add(point);
				}
			}

			if (list.Count == 0) {
				return null;
			}

			return list[Random.Range(0, list.Count)];
		}

		public int FoodCount() {
			return _foodMap.Count;
		}

	}
}
