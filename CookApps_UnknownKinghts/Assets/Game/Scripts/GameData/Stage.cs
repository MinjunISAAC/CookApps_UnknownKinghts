using InGame.ForItem.ForReward;
using InGame.ForUnit.ForData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForChapterGroup.ForStage
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Stage", fileName = "Stage")]
    public class Stage : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Step Number")]
        [SerializeField] private int                  _stageStep   = 0;

        [Header("2. Reward Group")]
        [SerializeField] private List<RewardItemData> _rewardList = null;

        [Header("3. Unit Group")]
        [SerializeField] private List<UnitData>       _unitList   = null;

        [Header("4. Play Option")]
        [SerializeField] private float                _playTime   = 0f;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int                  StageStep   => _stageStep;
        public List<RewardItemData> RewardList  => _rewardList;
        public List<UnitData>       UnitList    => _unitList;
        public float                PlayTime    => _playTime;
    }
}