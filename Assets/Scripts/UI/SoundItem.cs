using System;
using System.Collections;
using System.Collections.Generic;

using Scriptable;

using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class SoundItem : MonoBehaviour {
		public Text text;
		public Game game;
		private bool _expected;

		private void Awake() {
			SetExpectedSound();
		}

		private void SetExpectedSound() {
			_expected = !game.GetSound();
			text.text = "Sound " + (_expected ? "On" : "Off");
		}

		public void SetSound() {
			game.SetSound(_expected);
		}
	}

}
