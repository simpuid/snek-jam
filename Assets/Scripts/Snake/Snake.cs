using System;
using System.Collections;

using Scriptable;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Snake {
	[RequireComponent(typeof(MeshFilter))]
	public class Snake : MonoBehaviour {
		public Vector2Int startPosition;
		public int startLength;
		public float snakeWidth;
		public float tickRate;
		public SpriteRenderer headRenderer;
		public Transform headTransform;
		public Sprite headNormal;
		public Sprite headEat;
		public Sprite headDead;
		public Map map;
		public Level level;
		public Game game;
		public GameScene gameScene;
		public Menu menu;
		public AudioSource chompSource;
		public AudioSource gameOverSource;
		public AudioSource pickSource;

		private MeshFilter _filter;
		private Controller _controller;
		private Renderer _renderer;
		private float _tick;
		private Direction _nextDirection;
		private bool _alive;

		private void Awake() {
			_filter = GetComponent<MeshFilter>();
			var mesh = new Mesh();
			_filter.mesh = mesh;
			_renderer = new Renderer(snakeWidth, mesh);
			_controller = new Controller(map, level, game, startPosition, Direction.Right, startLength);
			_tick = 0;
			_nextDirection = _controller.GetDirection();
			_alive = true;
			game.Reset();
		}

		private void OnDestroy() {
			_controller.Clear();
		}

		private void Update() {
			if (_alive && !game.GetPause()) {
				_tick += Time.deltaTime * tickRate;
				while (_tick > 1.0f) {
					_tick -= 1.0f;
					var dead = _controller.Tick(_nextDirection, out var pick);
					if (pick) {
						pickSource.Play();
					}

					_nextDirection = _controller.GetDirection();
					if (dead) {
						_alive = false;
						gameOverSource.Play();
						StartCoroutine(DeadCoroutine());
					}

					if (_controller.IsEating()) {
						chompSource.Play();
					}
				}

				var tailTick = _controller.IsEating() ? 0.0f : _tick;
				_renderer.Render(_controller.GetPoints(), _controller.GetDirection().ToVector2Int(), tailTick, _tick);
				var interpolatedHead =
					_controller.GetHead() + (Vector2) _controller.GetDirection().ToVector2Int() * _tick;
				headTransform.position = interpolatedHead;
				headTransform.eulerAngles = new Vector3(0.0f, 0.0f, _controller.GetDirection().ToDegree());
				headRenderer.sprite = _controller.IsEating() ? headEat : headNormal;
			} else {
				headRenderer.sprite = headDead;
			}
		}

		private IEnumerator DeadCoroutine() {
			yield return new WaitForSeconds(2);
			menu.SetScore(game.GetScore());
			menu.SetPlayed(true);
			SceneManager.LoadScene(gameScene.menuScene);
		}
	}
}
