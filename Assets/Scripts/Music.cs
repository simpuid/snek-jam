using System;

using Scriptable;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {
	public AudioSource source;
	public Game game;
	public GameScene gameScene;
	public float high;
	public float low;

	private string _lastScene;
	private bool _pause;

	private void Awake() {
		DontDestroyOnLoad(gameObject);
		SceneManager.LoadScene(gameScene.menuScene);
	}

	private void Update() {
		if (SceneManager.GetActiveScene().name != _lastScene) {
			HandleScene();
		}

		if (game.GetPause() != _pause) {
			_pause = game.GetPause();
			HandleScene();
		}
	}

	void HandleScene() {
		_lastScene = SceneManager.GetActiveScene().name;
		if (_lastScene == gameScene.menuScene || _lastScene == gameScene.startScene ||
		    _lastScene == gameScene.creditScene) {
			source.volume = low;
		} else if (_lastScene == gameScene.gameScene) {
			source.volume = game.GetPause() ? low : high;
		}
	}
}
