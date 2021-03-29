using UnityEngine;

namespace Scriptable {
	[CreateAssetMenu]
	public class Game : ScriptableObject {
		private int _score;
		private int _bonus;
		private int _lastBonus;
		private Vector2Int _head;
		private bool _pause;
		private bool _sound = true;
		private const int MAX = 10;
		private const int MIN = 5;

		public void Reset() {
			_score = 0;
			_bonus = MAX;
			_lastBonus = 0;
		}

		public int GetScore() {
			return _score;
		}

		public int GetBonus() {
			return _bonus;
		}

		public void IncrementScore() {
			_score += _bonus;
			_lastBonus = _bonus;
			_bonus = MAX;
		}

		public void Penalty() {
			_bonus--;
			if (_bonus < MIN) {
				_bonus = MIN;
			}
		}

		public Vector2Int GetHead() {
			return _head;
		}

		public void SetHead(Vector2Int head) {
			_head = head;
		}

		public bool GetPause() {
			return _pause;
		}

		public void SetPause(bool pause) {
			_pause = pause;
		}

		public bool GetSound() {
			return _sound;
		}

		public void SetSound(bool sound) {
			_sound = sound;
			AudioListener.volume = sound ? 1.0f : 0.0f;
		}

		public int GetLastBonus() {
			return _lastBonus;
		}
	}
}
