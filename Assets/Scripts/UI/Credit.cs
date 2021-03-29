using Scriptable;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI {
	public class Credit : MonoBehaviour {
		public GameScene gameScene;
		[TextArea]
		public string[] texts;
		private Text _text;
		private int _index;

		private void Awake() {
			_text = GetComponent<Text>();
			_index = 0;
			_text.text = texts[_index];
			_index++;
		}

		private void Update() {
			if (Input.GetMouseButtonDown(0)) {
				if (_index < texts.Length) {
					_text.text = texts[_index];
				} else {
					SceneManager.LoadScene(gameScene.menuScene);
				}

				_index++;
			}
		}
	}

}
