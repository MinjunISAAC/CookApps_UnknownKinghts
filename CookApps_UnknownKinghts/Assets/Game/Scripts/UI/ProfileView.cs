// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InGame.ForUI
{
    public class ProfileView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Image Group")]
        [SerializeField] private Image           _IMG_UserPhoto = null;
        
        [Header("Rect Group")]
        [SerializeField] private RectTransform   _RECT_ExpFrame = null;
        [SerializeField] private RectTransform   _RECT_ExpGuage = null;

        [Header("Text Group")]
        [SerializeField] private TextMeshProUGUI _TMP_UserLevel = null;
        [SerializeField] private TextMeshProUGUI _TMP_UserId    = null;
        [SerializeField] private TextMeshProUGUI _TMP_Exp       = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private Vector2   _expRectSize     = Vector2.zero;
        private int       _levelUpExp      = 0;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(int level, int exp, int levelUpExp, string userName)
        {
            _TMP_UserLevel.text = $"Lv.{level}";
            _TMP_UserId   .text = $"{userName}";
            _TMP_Exp      .text = $"{exp}/{_levelUpExp}";

            _expRectSize             = _RECT_ExpFrame.rect.size;
            _expRectSize.x           = (exp * levelUpExp) / (float)_levelUpExp;
            _RECT_ExpGuage.sizeDelta = _expRectSize;
        }
    }
}