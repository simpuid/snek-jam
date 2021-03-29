using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class Flyer : MonoBehaviour {
	public float distance;
	public float speed;
	private TextMesh _textMesh;
	private Transform _transform;
	private Vector2 _position;
	private float _time;

	private void Awake() {
		_textMesh = GetComponent<TextMesh>();
		_transform = GetComponent<Transform>();
	}

	private void Update() {
		_time += Time.deltaTime * speed;
		if (_time > distance) {
			Destroy(gameObject);
			return;
		}

		_transform.position = _position + Vector2.up * _time;
	}

	public void Spawn(string text, Vector2 position, Transform parent) {
		_position = position;
		_transform.position = position;
		_time = 0;
		_textMesh.text = text;
		_transform.SetParent(parent);
	}
}
