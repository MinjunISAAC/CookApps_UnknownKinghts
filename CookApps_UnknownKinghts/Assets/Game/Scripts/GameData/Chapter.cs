using InGame.ForChapterGroup.ForStage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForChapterGroup.ForChapter
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Chapter", fileName = "Chapter")]
    public class Chapter : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Chapter Info")]
        [SerializeField] private string      _name = "";
        [SerializeField] private int         _step = 0;

        [Header("Stage Group")]
        [SerializeField] private List<Stage> _stageList = new List<Stage>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<int, Stage> _stageSet = new Dictionary<int, Stage>();

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public string Name          => _name;   
        public int    Step          => _step;
        public int    StageQuantity => _stageSet.Count;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit()
        {
            for (int i = 0; i < _stageList.Count; i++)
            {
                var stage     = _stageList[i];
                var stageStep = stage.StageStep;

                _stageSet.Add(stageStep, stage);
            }

            // [TODO] Test 종료 시, 주석처리 로직
            Debug.Log($"<color=orange>[Chater.OnInit] Chapter {_step}은 총 {_stageSet.Count}개의 Stage로 초기화 되었습니다.</color>");
        }

        public Stage GetToStage(int stageNum)
        {
            if (_stageSet.TryGetValue(stageNum, out var stage))
                return stage;
            else
            {
                Debug.LogError($"<color=red>[Chapter.GetToStage] [Chapter {_step}] 정보에 [Stage {stageNum}]의 정보가 존재하지 않습니다.</color>");
                return null;
            }
        }
    }
}