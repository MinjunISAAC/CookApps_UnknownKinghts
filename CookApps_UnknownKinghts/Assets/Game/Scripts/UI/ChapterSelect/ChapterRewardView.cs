using InGame.ForItem.ForReward;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterRewardView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Star Group")]
        [SerializeField] private List<Image>     _starGroup         = new List<Image>();

        [Header("2. Stage Info Group")]
        [SerializeField] private Button          _BTN_PrevStage     = null;
        [SerializeField] private Button          _BTN_NextStage     = null;
        [SerializeField] private TextMeshProUGUI _TMP_Stage         = null;

        [Header("3. Reward Item Group")]
        [SerializeField] private RewardItemView  _rewardItemOrigin  = null;
        [SerializeField] private Transform       _rewardItemParents = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<RewardItemView> _rewardItemViewGroup = null;
    }
}