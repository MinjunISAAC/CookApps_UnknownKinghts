using InGame.ForChapterGroup.ForChapter;
using InGame.ForChapterGroup.ForStage;
using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForBattle
{
    public class ResultData
    {
        public Chapter   ChapterData;
        public Stage     StageData;
        
        public EUnitType MvpUnitType;
        public int       MvpKillCount;

        public int       ClearStarCount;
    }
}