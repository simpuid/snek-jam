using System;

using Scriptable;

using UnityEngine;

public class CameraController : MonoBehaviour {
	public Level level;
	public float sideBar;
	public float padding;
	private Camera _camera;
	private Transform _transform;
	private Rect _safeArea;

	private void Awake() {
		_camera = GetComponent<Camera>();
		_transform = GetComponent<Transform>();
		Handle(Screen.safeArea);
	}

	private void Update() {
		if (Screen.safeArea != _safeArea) {
			Handle(Screen.safeArea);
		}
	}

	private void Handle(Rect safeArea) {
		var offset = safeArea.height * sideBar;
		safeArea.height -= offset;
		safeArea.y += offset;
		if (safeArea.width / safeArea.height > (float) level.bound.width / level.bound.height) {
			var worldSize = level.bound.height + padding * 2.0f;
			var screenSize = safeArea.height;
			_camera.orthographicSize = worldSize * Screen.height * 0.5f / screenSize;
		} else {
			var worldSize = level.bound.width + padding * 2.0f;
			var screenSize = safeArea.width;
			_camera.orthographicSize =
				(worldSize * Screen.width * 0.5f / screenSize) * ((float) Screen.height / Screen.width);
		}

		var expectedCenter = _camera.ScreenToWorldPoint(safeArea.center);
		var levelCenter = new Vector3((level.bound.width - 1) * 0.5f, (level.bound.height - 1) * 0.5f);
		var position = _transform.position + levelCenter - expectedCenter;
		position.z = -10.0f;
		_transform.position = position;
		_safeArea = safeArea;
	}
}
