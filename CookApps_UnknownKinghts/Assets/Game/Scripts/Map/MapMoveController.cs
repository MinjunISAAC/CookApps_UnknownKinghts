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
        private Vector2 _prevPos = Vector2.zero;

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

    }
}