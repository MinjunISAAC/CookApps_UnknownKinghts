using InGame.ForChapterGroup.ForChapter;
using InGame.ForChapterGroup.ForStage;
using InGame.ForItem.ForReward;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.ForData.ForUser;

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

        [Header("4. Enter Group")]
        [SerializeField] private Button          _BTN_EnterBattle   = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<RewardItemView> _rewardItemViewGroup = new List<RewardItemView>();
        private bool                 _isInit              = false;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit(Chapter targetchapter, Stage targetStage, UserData.ClearData clearData, List<RewardItemData> rewardList)
        {
            if (clearData == null)
            {
                _SetToClearStar(0);
                _ResetToRewardItem();
                _SetToClearStar(0);
                SetToStageText(targetchapter.Step, targetStage.Step);
            }
            else
            {
                var starCount = _starGroup.Count;
                var chapter   = clearData.Chapter;
                var stage     = clearData.Stage;
            
                _SetToClearStar   (starCount);
                _ResetToRewardItem();
                SetToStageText   (chapter, stage);
            }

            for (int i = 0; i < rewardList.Count; i++)
            {
                var itemData = rewardList[i];
                var item     = Instantiate(_rewardItemOrigin, _rewardItemParents);

                item.OnInit(itemData);

                _rewardItemViewGroup.Add(item); 
            }
        }

        public void SetToStageText(int chapter, int stage)
        {
            _TMP_Stage.text = $"{chapter}-{stage}";
        }

        public void SetToButtonEvent(Action onClickPrevStage, Action onClickNextStage, Action onClickEnterBattle)
        {
            if (!_isInit)
            {
                _BTN_PrevStage.onClick  .AddListener(() => onClickPrevStage  ());
                _BTN_NextStage.onClick  .AddListener(() => onClickNextStage  ());
                _BTN_EnterBattle.onClick.AddListener(() => onClickEnterBattle());
                _isInit = true;
            }
        }

        // ----- Private
        private void _SetToClearStar(int starCount)
        {
            for (int i = 0; i < starCount; i++)
            {
                var star = _starGroup[i];
                star.gameObject.SetActive(true);
            }

            for (int i = starCount; i < _starGroup.Count; i++)
            {
                var star = _starGroup[i];
                star.gameObject.SetActive(false);
            }
        }


        private void _ResetToRewardItem()
        {
            if (_rewardItemViewGroup.Count <= 0)
                return;

            for(int i = _rewardItemViewGroup.Count - 1; i >= 0; i--)
            {
                var item = _rewardItemViewGroup[i];
                Destroy(item.gameObject);
            }

            _rewardItemViewGroup.Clear();
        }
    }
}