using InGame.ForUnit;
using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForBuildDeck
{
    public class UnitDeckCard : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("UI Components Group")]
        [SerializeField] private TextMeshProUGUI _TMP_Level    = null;
        [SerializeField] private Image           _IMG_Frame    = null;
        [SerializeField] private Image           _IMG_Photo    = null;
        [SerializeField] private Image           _IMG_SpecIcon = null;
        [SerializeField] private Image           _IMG_JobIcon  = null;
        [SerializeField] private List<Image>     _starGroup    = null;
        [SerializeField] private Button          _BTN_Click    = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private EUnitType _unitType = EUnitType.UnknownHero;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EUnitType UnitType => _unitType;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToUnitCard(UnitData data, Action<EUnitType> onClickBtn)
        {
            var unitLevel   = data.Level;
            var starCount   = data.Star;
            var unitGrade   = data.GradeType;
            var unitProfile = data.Profile;
            var unitSpec    = data.SpecType;
            var unitJob     = data.JobType;

            _unitType = data.UnitType;

            _IMG_Frame   .sprite = SpritePoolSystem.Instance.GetToGradeFrameSprite(unitGrade);
            _IMG_SpecIcon.sprite = SpritePoolSystem.Instance.GetToSpecSprite      (unitSpec);
            _IMG_JobIcon .sprite = SpritePoolSystem.Instance.GetToJobSprite       (unitJob);
            _IMG_Photo   .sprite = unitProfile;

            _TMP_Level.text = $"Lv.{unitLevel}";

            _SetToStar(starCount);

            _BTN_Click.onClick.AddListener(() => { Debug.Log($"Call {_unitType}"); onClickBtn(_unitType); });
        }

        // ----- Private
        private void _SetToStar(int starCount)
        {
            for(int i = 0; i < starCount; i++)
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
    }
}