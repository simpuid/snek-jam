using System;

using UnityEngine;

namespace Snake {

	public enum Direction {
		Right = 0,
		Up = 1,
		Left = 2,
		Down = 3,
	}

	public static class DirectionExtension {
		public static Vector2Int ToVector2Int(this Direction direction) {
			return direction switch {
				Direction.Right => Vector2Int.right,
				Direction.Up => Vector2Int.up,
				Direction.Left => Vector2Int.left,
				Direction.Down => Vector2Int.down,
				_ => throw new ArgumentOutOfRangeException(nameof(direction))
			};
		}

		public static int ToDegree(this Direction direction) {
			return direction switch {
				Direction.Right => 0,
				Direction.Up => 90,
				Direction.Left => 180,
				Direction.Down => 270,
				_ => throw new ArgumentOutOfRangeException(nameof(direction))
			};
		}
	}
}
