using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InGame.ForMap
{ 
    public class MapMoveController : MonoBehaviour, IDragHandler
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private RectTransform _RECT_Canvas = null;
        [SerializeField] private RectTransform _RECT_Map    = null;
        
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Vector2   _prevPos      = Vector2.zero;
        private Coroutine _co_MoveToMap = null;

        // --------------------------------------------------
        // Fucntions - Nomal
        // --------------------------------------------------
        public void OnDrag(PointerEventData eventData)
        {
            LockScreenBound(eventData);
        }

        // --------------------------------------------------
        // Fucntions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void MoveToMap(Vector2 pos)
        {
            _RECT_Map.anchoredPosition = pos;
        }

        public void MoveToMap(Vector2 pos, float duration, Action doneCallBack = null)
        {
            if (_co_MoveToMap == null)
                _co_MoveToMap = StartCoroutine(_Co_MoveToMap(pos, duration, doneCallBack));
        }

        public void ResetToMap() 
        { 
            _RECT_Map.anchoredPosition = Vector2.zero;
        }

        // ----- Private
        private void LockScreenBound(PointerEventData eventData)
        {
            Vector2 newPosition = _RECT_Map.anchoredPosition + eventData.delta;

            if (newPosition.x <= _RECT_Canvas.rect.size.x && newPosition.x >= -_RECT_Canvas.rect.size.x &&
                newPosition.y <= _RECT_Canvas.rect.size.y && newPosition.y >= -_RECT_Canvas.rect.size.y)
            {
                _RECT_Map.anchoredPosition = newPosition;
                _prevPos = newPosition;
            }
            else
            {
                Vector2 clampedPosition = new Vector2(
                    Mathf.Clamp(newPosition.x, -_RECT_Canvas.rect.size.x, _RECT_Canvas.rect.size.x),
                    Mathf.Clamp(newPosition.y, -_RECT_Canvas.rect.size.y, _RECT_Canvas.rect.size.y)
                );

                _RECT_Map.anchoredPosition = clampedPosition;
                _prevPos = clampedPosition;
            }
        }

        // --------------------------------------------------
        // Fucntions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_MoveToMap(Vector2 pos, float duration, Action doneCallBack)
        {
            var sec      = 0.0f;
            var startPos = _RECT_Map.anchoredPosition;
            var endPos   = pos;

            while (sec < duration)
            {
                sec += Time.deltaTime;

                _RECT_Map.anchoredPosition = Vector2.Lerp(startPos, endPos, sec/duration);
                yield return null;
            }

            _RECT_Map.anchoredPosition = endPos;

            _co_MoveToMap = null;
            doneCallBack?.Invoke();
        }
    }
}