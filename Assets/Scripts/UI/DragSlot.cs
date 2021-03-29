using System;
using System.Collections;

using Scriptable;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI {
	[RequireComponent(typeof(RectTransform))]
	public class DragSlot : MonoBehaviour, IDropHandler {
		public GameScene gameScene;
		public AudioSource dropSource;
		private RectTransform _rectTransform;
		private GameObject _dropObject;

		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
			_dropObject = null;
		}

		public void OnDrop(PointerEventData eventData) {
			if (!(eventData.pointerDrag is null)) {
				eventData.pointerDrag.GetComponent<RectTransform>().SetParent(_rectTransform);
				_dropObject = eventData.pointerDrag;


				StartCoroutine(DropCouroutine());
			}
		}

		private IEnumerator DropCouroutine() {
			dropSource.Play();
			yield return new WaitForSeconds(0.2f);
			if (!(_dropObject.GetComponent<StartItem>() is null)) {
				StartAction();
			} else if (!(_dropObject.GetComponent<ExitItem>() is null)) {
				ExitAction();
			} else if (!(_dropObject.GetComponent<SoundItem>() is null)) {
				SoundAction(_dropObject.GetComponent<SoundItem>());
			} else if (!(_dropObject.GetComponent<CreditItem>() is null)) {
				CreditAction();
			}
		}

		private void ExitAction() {
			Application.Quit();
		}

		private void StartAction() {
			SceneManager.LoadScene(gameScene.gameScene);
		}

		private void SoundAction(SoundItem item) {
			item.SetSound();
			SceneManager.LoadScene(gameScene.menuScene);
		}

		private void CreditAction() {
			SceneManager.LoadScene(gameScene.creditScene);
		}
	}
}
