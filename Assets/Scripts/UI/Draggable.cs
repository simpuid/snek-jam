using System;

using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
		public Canvas canvas;
		private RectTransform _transform;
		private CanvasGroup _canvasGroup;

		private void Awake() {
			_transform = GetComponent<RectTransform>();
			_canvasGroup = GetComponent<CanvasGroup>();
		}

		public void OnPointerDown(PointerEventData eventData) {
		}

		public void OnBeginDrag(PointerEventData eventData) {
			_canvasGroup.blocksRaycasts = false;
		}

		public void OnEndDrag(PointerEventData eventData) {
			_canvasGroup.blocksRaycasts = true;
		}

		public void OnDrag(PointerEventData eventData) {
			_transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
		}
	}
}
