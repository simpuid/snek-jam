using System;

using Scriptable;

using UnityEngine;

public class PauseController : MonoBehaviour {
	public GameObject pauseScreen;
	public Game game;

	private void Awake() {
		SetPause(false);
	}

	private void Update() {
		if (game.GetPause()) {
			if (Input.GetMouseButtonDown(0)) {
				SetPause(false);
			}
		} else {
			if (Input.GetMouseButtonDown(1)) {
				SetPause(true);
			}
		}
	}

	private void SetPause(bool pause) {
		game.SetPause(pause);
		pauseScreen.SetActive(pause);
		var cell = Dragger.GetCell();
		if (!(cell is null)) {
			Destroy(cell.gameObject);
			Dragger.SetCell(null);
		}
	}
}
