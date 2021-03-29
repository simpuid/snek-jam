using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;

using Cells;

using Scriptable;

using Random = System.Random;

public class Dragger : MonoBehaviour {
	public Level level;
	public Map map;
	public GameObject[] cells;
	public float inventryScale;
	public AudioSource placeSource;
	public AudioSource errorSource;
	public AudioSource changeSourceA;
	public AudioSource changeSourceB;
	private Camera _camera;
	private Vector2Int _lastPosition;
	private static Cell _cell;

	private void Awake() {
		SetCell(null);
		_camera = Camera.main;
	}

	private void Update() {
		if (_cell is null) {
			if (Input.GetMouseButtonDown(0)) {
				if (Input.mousePosition.y <= Screen.height * inventryScale) {
					var index = Mathf.RoundToInt(Mathf.Floor(Input.mousePosition.x / Screen.width * cells.Length));
					_cell = Instantiate(cells[index]).GetComponent<Cell>();
					_cell.gameObject.SetActive(false);
					_lastPosition = -Vector2Int.one;
				}
			}
		} else {
			_cell.gameObject.SetActive(Input.mousePosition.y > Screen.height * inventryScale);

			var position = ClampBound(GetMousePosition(), level.bound);
			if (Input.GetMouseButtonUp(0)) {
				if (Input.mousePosition.y > Screen.height * inventryScale) {
					if (map.GetCell(position) is null) {
						placeSource.Play();
					} else {
						errorSource.Play();
					}

					_cell.Drop(position);
				} else {
					Destroy(_cell.gameObject);
				}

				_cell = null;
			} else {
				_cell.Drag(position);
				if (position != _lastPosition) {
					_lastPosition = position;
					if ((position.x + position.y) % 2 == 0) {
						changeSourceA.Play();
					} else {
						changeSourceB.Play();
					}
				}
			}
		}
	}

	private Vector2Int GetMousePosition() {
		var floatPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
		return new Vector2Int(Mathf.RoundToInt(floatPosition.x), Mathf.RoundToInt(floatPosition.y));
	}

	private static Vector2Int ClampBound(Vector2Int position, RectInt rectangle) {
		var x = Clamp(position.x, rectangle.xMin, rectangle.xMax - 1);
		var y = Clamp(position.y, rectangle.yMin, rectangle.yMax - 1);
		return new Vector2Int(x, y);
	}

	private static int Clamp(int x, int min, int max) {
		if (x < min) return min;
		if (x > max) return max;
		return x;
	}

	public static void SetCell(Cell cell) {
		_cell = cell;
	}

	public static Cell GetCell() {
		return _cell;
	}
}
