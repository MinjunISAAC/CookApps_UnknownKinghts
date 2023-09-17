using InGame.ForItem.ForReward;
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
        [SerializeField] private int _step = 0;

        [Header("2. Reward Group")]
        [SerializeField] private List<RewardItemData> _rewardList = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int                  Step       => _step;
        public List<RewardItemData> RewardList => _rewardList;
    }
}