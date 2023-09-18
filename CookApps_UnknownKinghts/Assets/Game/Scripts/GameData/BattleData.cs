using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForBattle
{
    public class BattleData
    {
        public BattleData(List<Unit> playerUnit, List<Unit> enemyUnit)
        {
            PlayerUnit = playerUnit;
            EnemyUnit  = enemyUnit;
        }

        public List<Unit> PlayerUnit;
        public List<Unit> EnemyUnit;
    }
}