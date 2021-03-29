using System;

using Scriptable;

using UnityEngine;

public class FoodSpawner : MonoBehaviour {
	public Map map;
	public Level level;
	public GameObject food;
	public int foodLimit;

	private Transform _transform;

	void Spawn() {
		while (map.FoodCount() < foodLimit) {
			var newPosition = map.GenerateFoodPosition(level.bound);
			if (newPosition.HasValue) {
				var f = Instantiate(food).GetComponent<Food>();
				f.Spawn(newPosition.Value, _transform);
			} else {
				return;
			}
		}
	}

	private void Awake() {
		_transform = GetComponent<Transform>();
		Spawn();
	}

	private void Update() {
		Spawn();
	}
}
