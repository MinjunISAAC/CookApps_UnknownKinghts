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
        private void LockScreenBound(PointerEventData eventData)
        {
            Vector2 newPosition = _RECT_Map.anchoredPosition + eventData.delta;

            // 화면 경계에 도달한 경우
            if (newPosition.x <= _RECT_Canvas.rect.size.x && newPosition.x >= -_RECT_Canvas.rect.size.x &&
                newPosition.y <= _RECT_Canvas.rect.size.y && newPosition.y >= -_RECT_Canvas.rect.size.y)
            {
                _RECT_Map.anchoredPosition = newPosition;
                _prevPos = newPosition;
            }
            else
            {
                // 경계를 넘어가지 않도록 위치를 제한
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