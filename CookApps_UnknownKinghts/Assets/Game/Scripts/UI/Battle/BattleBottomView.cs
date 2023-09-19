using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForBattle.ForUI
{
    public class BattleBottomView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Button _BTN_AutoSkill     = null;
        [SerializeField] private Button _BTN_TimeScale     = null;
        [SerializeField] private Image  _IMG_TimeScaleHide = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToTimeScaleBtn(Action onClickBtn)
        {
            _BTN_TimeScale.onClick.AddListener( () => onClickBtn() );
        }

        public void VisiableTimeScaleBtn(bool visiable) => _IMG_TimeScaleHide.gameObject.SetActive(!visiable);
    }
}