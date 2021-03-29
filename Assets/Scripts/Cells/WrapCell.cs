using UnityEngine;

namespace Cells {
	public class WrapCell : Cell {
		public Vector2Int wrapPosition;

		public override bool Penalty() {
			return false;
		}
	}
}
