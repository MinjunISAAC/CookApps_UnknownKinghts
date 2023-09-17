using InGame.ForBattle;
using InGame.ForState.ForUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForVillage
{
    public class BottomView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<BattleEnterItem> _battleEnterItems = new List<BattleEnterItem>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(Action<EBattleType> onClickAction)
        {
            for (int i = 0; i < _battleEnterItems.Count; i++)
            {
                var battleEnterItem = _battleEnterItems[i];
                battleEnterItem.OnInit(onClickAction);
            }
        }
    }
}