using System;

using Scriptable;

using UnityEngine;

public class FlyerSpawner : MonoBehaviour {
	public Game game;
	public GameObject flyer;
	public Vector2 offset;
	private int _score;
	private Transform _transform;

	private void Awake() {
		_transform = GetComponent<Transform>();
		HandleScore();
	}

	private void Update() {
		if (_score != game.GetScore()) {
			HandleScore();
		}
	}

	private void HandleScore() {
		_score = game.GetScore();
		var fly = Instantiate(flyer).GetComponent<Flyer>();
		fly.Spawn("<size=40>+" + game.GetLastBonus() + "</size>\n" + _score.ToString(), game.GetHead() + offset,
			_transform);
	}
}
