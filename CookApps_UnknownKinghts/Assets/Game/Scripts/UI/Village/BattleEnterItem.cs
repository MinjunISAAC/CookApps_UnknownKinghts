// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForBattle;

namespace InGame.ForState.ForUI
{
    public class BattleEnterItem : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Battle Type")]
        [SerializeField] private EBattleType _battleType  = EBattleType.Unknown;

        [Header("2. UI Components")]
        [SerializeField] private Button      _BTN_OnClick = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EBattleType BattleType => _battleType;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(Action<EBattleType> onClickAction)
        {
            _BTN_OnClick.onClick.AddListener
            (
                () => { onClickAction(_battleType); }
            );
        }
    }
}