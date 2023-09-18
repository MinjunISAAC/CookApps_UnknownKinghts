// ----- C#
using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForState.ForBuildDeck
{
    public class BuildDeckSetter : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private BuildDeck _playerBuildDeck = null;
        [SerializeField] private BuildDeck _enemyBuildDeck  = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToBuildDeck(List<Unit> playerUnitList, List<Unit> enemyUnitList)
        {
            for (int i = 0; i < playerUnitList.Count; i++)
            {
                var unit = playerUnitList[i];
                _playerBuildDeck.SetToUnit(unit);
            }

            for (int i = 0; i < enemyUnitList.Count; i++)
            {
                var unit = enemyUnitList[i];
                _enemyBuildDeck.SetToUnit(unit);
            }
        }

        public void SetToBuildDeck(List<Unit> playerUnitList)
        {
            for (int i = 0; i < playerUnitList.Count; i++)
            {
                var unit = playerUnitList[i];
                _playerBuildDeck.SetToUnit(unit);
            }
        }

        public void RefreshToBuildDeck(bool isAdd, Unit unit)
        {
            if (isAdd) _playerBuildDeck.IncludeToUnit(unit);
            else       _playerBuildDeck.ExcludeToUnit(unit);
        }

        public void ResetToBuildDeck()
        {
            _playerBuildDeck.ResetToUnit();
            _enemyBuildDeck .ResetToUnit();
        }
    }
}