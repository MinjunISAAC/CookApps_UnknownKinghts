// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForState.ForUI;
using InGame.ForState;

namespace InGame.ForUI
{
    public class UIOwner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("State UI Group")]
        [SerializeField] private VillageView     _villageView     = null;
        [SerializeField] private BattleReadyView _battleReadyView = null;
        [SerializeField] private BuildDeckView   _buildDeckView   = null;
        [SerializeField] private BattleView      _battleView      = null;
        [SerializeField] private ResultView      _resultView      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Fucntions - Nomal
        // --------------------------------------------------
        public StateView GetStateUI()
        {
            var currentState = StateMachine.Instance.CurrentState;
            switch (currentState)
            {
                case EStateType.Village     : return _villageView;
                case EStateType.BattleReady : return _battleReadyView;
                case EStateType.BuildDeck   : return _buildDeckView;
                case EStateType.Battle      : return _battleView;
                case EStateType.Result      : return _resultView;
                default                     : return null;
            }
        }

    }
}