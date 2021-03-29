using System;

using UnityEngine;

namespace Scriptable {
	[CreateAssetMenu]
	public class Menu : ScriptableObject {
		private int _score;
		private bool _played;

		public int GetScore() {
			return _score;
		}

		public void SetScore(int score) {
			_score = score;
		}

		public bool GetPlayed() {
			return _played;
		}

		public void SetPlayed(bool played) {
			_played = played;
		}
	}
}
