using InGame.ForChapterGroup.ForChapter;
using InGame.ForChapterGroup.ForStage;
using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForBattle
{
    public class BattleData
    {
        public BattleData(Chapter chapter, Stage stage, List<Unit> playerUnit, List<Unit> enemyUnit)
        {
            ChapterInfo      = chapter;
            StageInfo        = stage;
            PlayerUnit       = playerUnit;
            EnemyUnit        = enemyUnit;
        }

        public Chapter    ChapterInfo;
        public Stage      StageInfo;
        public List<Unit> PlayerUnit;
        public List<Unit> EnemyUnit;
    }
}