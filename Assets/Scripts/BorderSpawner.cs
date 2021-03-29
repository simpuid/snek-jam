using System;

using Cells;

using Scriptable;

using UnityEngine;

public class BorderSpawner : MonoBehaviour {
	public Map map;
	public Level level;
	public GameObject borderPrefab;

	private void Awake() {
		var rect = level.bound;
		var transformCache = GetComponent<Transform>();
		for (var i = rect.xMin; i < rect.xMax; i++) {
			Spawn(new Vector2Int(i, rect.yMin - 1), new Vector2Int(i, rect.yMax), transformCache);
		}

		for (var i = rect.yMin; i < rect.yMax; i++) {
			Spawn(new Vector2Int(rect.xMin - 1, i), new Vector2Int(rect.xMax, i), transformCache);
		}
	}

	private void Spawn(Vector2Int first, Vector2Int second, Transform transformCache) {
		var firstBorder = Instantiate(borderPrefab).GetComponent<WrapCell>();
		var secondBorder = Instantiate(borderPrefab).GetComponent<WrapCell>();
		firstBorder.GetComponent<Transform>().SetParent(transformCache);
		secondBorder.GetComponent<Transform>().SetParent(transformCache);
		firstBorder.wrapPosition = second;
		secondBorder.wrapPosition = first;
		firstBorder.Drop(first);
		secondBorder.Drop(second);
	}
}
