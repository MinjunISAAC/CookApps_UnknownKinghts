using InGame.ForItem.ForReward;
using InGame.ForUnit;
using InGame.ForUnit.ForData;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForChapterGroup.ForStage
{
    [System.Serializable]
    public class SpecSpriteInfo
    {
        public ESpecType Type;
        public Sprite    Sprite;
    }

    [System.Serializable]
    public class UnitSpriteInfo
    {
        public EUnitType Type;
        public Sprite    Sprite;
    }

    [System.Serializable]
    public class JobSpriteInfo
    {
        public EJobType Type;
        public Sprite   Sprite;
    }

    [System.Serializable]
    public class GradeFrameSpriteInfo
    {
        public EGradeType Type;
        public Sprite     Sprite;
    }

    [CreateAssetMenu(menuName = "Pool/Create To SpritePool", fileName = "SpritePool")]
    public class SpritePool : ScriptableObject
    {

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Spec Sprite Pool")]
        [SerializeField] private List<SpecSpriteInfo>       _specSpriteList        = null;

        [Header("2. Unit Sprite Pool")]
        [SerializeField] private List<UnitSpriteInfo>       _unitSpriteList        = null;

        [Header("3. Job Sprite Pool")]
        [SerializeField] private List<JobSpriteInfo>        _jobSpriteList         = null;

        [Header("4. Grade Frame Sprite Pool")]
        [SerializeField] private List<GradeFrameSpriteInfo> _gradeFrameSpriteList  = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<ESpecType , Sprite> _specSpriteSet       = new Dictionary<ESpecType , Sprite>();
        private Dictionary<EUnitType , Sprite> _unitSpriteSet       = new Dictionary<EUnitType , Sprite>();
        private Dictionary<EJobType  , Sprite> _jobSpriteSet        = new Dictionary<EJobType  , Sprite>();
        private Dictionary<EGradeType, Sprite> _gradeFrameSpriteSet = new Dictionary<EGradeType, Sprite>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit()
        {
            for (int i = 0; i < _specSpriteList.Count; i++)
            {
                var info = _specSpriteList[i];
                
                if (!_specSpriteSet.ContainsKey(info.Type))
                    _specSpriteSet.Add(info.Type, info.Sprite);
            }

            for (int i = 0; i < _unitSpriteList.Count; i++)
            {
                var info = _unitSpriteList[i];

                if (!_unitSpriteSet.ContainsKey(info.Type))
                    _unitSpriteSet.Add(info.Type, info.Sprite);
            }

            for (int i = 0; i < _jobSpriteList.Count; i++)
            {
                var info = _jobSpriteList[i];

                if (!_jobSpriteSet.ContainsKey(info.Type))
                    _jobSpriteSet.Add(info.Type, info.Sprite);
            }

            for (int i = 0; i < _gradeFrameSpriteList.Count; i++)
            {
                var info = _gradeFrameSpriteList[i];

                if (!_gradeFrameSpriteSet.ContainsKey(info.Type))
                    _gradeFrameSpriteSet.Add(info.Type, info.Sprite);
            }
        }

        public Sprite GetToSpecSprite(ESpecType type)
        {
            if (_specSpriteSet.TryGetValue(type, out Sprite sprite)) return sprite;
            else 
            {
                Debug.LogError($"<color=red>[SpritePool.GetToSpecSprite] {type}에 해당하는 Sprite가 존재하지 않습니다.</color>");
                return null;
            }
        }
        public Sprite GetToUnitSprite(EUnitType type)
        {
            if (_unitSpriteSet.TryGetValue(type, out Sprite sprite)) return sprite;
            else
            {
                Debug.LogError($"<color=red>[SpritePool.GetToUnitSprite] {type}에 해당하는 Sprite가 존재하지 않습니다.</color>");
                return null;
            }
        }
        public Sprite GetToJobSprite(EJobType type)
        {
            if (_jobSpriteSet.TryGetValue(type, out Sprite sprite)) return sprite;
            else
            {
                Debug.LogError($"<color=red>[SpritePool.GetToJobSprite] {type}에 해당하는 Sprite가 존재하지 않습니다.</color>");
                return null;
            }
        }
        public Sprite GetToGradeFrameSprite(EGradeType type)
        {
            if (_gradeFrameSpriteSet.TryGetValue(type, out Sprite sprite)) return sprite;
            else
            {
                Debug.LogError($"<color=red>[SpritePool.GetToGradeFrameSprite] {type}에 해당하는 Sprite가 존재하지 않습니다.</color>");
                return null;
            }
        }
    }
}