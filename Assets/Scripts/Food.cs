using System;

using Scriptable;

using UnityEngine;

public class Food : MonoBehaviour {
	public Map map;
	public Vector2Int position;

	private Transform _transform;

	private void Awake() {
		_transform = GetComponent<Transform>();
	}

	public void Spawn(Vector2Int newPos, Transform parent) {
		position = newPos;
		if (map.GetFood(position) is null) {
			_transform.position = (Vector2) position;
			map.SetFood(position, this);
		} else {
			Destroy(gameObject);
		}

		_transform.SetParent(parent);
	}

	private void OnDestroy() {
		if (map.GetFood(position) == this) {
			map.RemoveFood(position);
		}
	}
}
