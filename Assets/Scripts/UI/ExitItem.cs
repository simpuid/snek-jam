using System;

using UnityEngine;

namespace UI {
	public class ExitItem : MonoBehaviour {
		private void Awake() {
			if (Application.isEditor || Application.platform == RuntimePlatform.WebGLPlayer) {
				gameObject.SetActive(false);
			}
		}
	}
}
