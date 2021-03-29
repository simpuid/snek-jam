using Scriptable;

using UnityEngine;

namespace Cells {
	public class Cell : MonoBehaviour {
		public Map map;
		public Vector2Int position;
		private Transform _transform;

		private void Awake() {
			_transform = GetComponent<Transform>();
		}

		private void OnDestroy() {
			Pick();
			if (Dragger.GetCell() == this) {
				Dragger.SetCell(null);
			}
		}

		public void Pick() {
			if (map.GetCell(position) == this) {
				map.RemoveCell(position);
			}
		}

		public void Drag(Vector2Int hoverPosition) {
			_transform.position = (Vector2) hoverPosition;
		}

		public void Drop(Vector2Int dropPosition) {
			if (map.GetCell(dropPosition) is null) {
				map.SetCell(dropPosition, this);
				position = dropPosition;
				_transform.position = (Vector2) dropPosition;
			} else {
				Destroy(gameObject);
			}
		}

		public virtual bool Penalty() {
			return true;
		}

	}
}
