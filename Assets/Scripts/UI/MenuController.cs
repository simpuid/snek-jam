using System;

using Scriptable;

using UnityEngine;
using UnityEngine.UI;

namespace UI {
	public class MenuController : MonoBehaviour {
		public Text header;
		public Menu menu;
		private void Awake() {
			if (!menu.GetPlayed()) {
				header.text = "Drag&Drop!\nRight Click to Pause";
			} else {
				header.text = "Your score was: " + menu.GetScore() + "\nNot bad! Wanna retry?";
			}
		}
	}
}
