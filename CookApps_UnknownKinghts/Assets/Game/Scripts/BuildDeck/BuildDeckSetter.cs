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
        public void SetToBuildEnemyDeck(List<Unit> enemyUnitList) => _enemyBuildDeck.SetToUnit(enemyUnitList);

        public void SetToBuildPlayerDeck(List<Unit> playerUnitList) => _playerBuildDeck.SetToUnit(playerUnitList);

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