using UnityEngine;

namespace Snake {
	public enum WrapStatus {
		Normal,
		WrapStart,
		WrapEnd
	}

	public readonly struct Point {
		public readonly Vector2Int Position;
		public readonly WrapStatus Wrap;

		public Point(Vector2Int position, WrapStatus wrap = WrapStatus.Normal) {
			Position = position;
			Wrap = wrap;
		}
	}

}
