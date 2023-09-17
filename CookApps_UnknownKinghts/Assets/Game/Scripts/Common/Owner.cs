// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using JsonUtil;
using Utility.ForCurrency;
using Utility.ForData.ForUser;
using InGame.ForUI;
using InGame.ForState;
using InGame.ForChapterGroup;
using InGame.ForChapterGroup.ForChapter;
using InGame.ForChapterGroup.ForStage;

namespace InGame
{
    public class Owner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Game Data")]
        [SerializeField] private ChapterGroup _chapterGroup = null;

        [Header("2. UI Owner")]
        [SerializeField] private UIOwner      _uiOwner      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Owner NullableInstance
        {
            get;
            private set;
        } = null;

        public UIOwner UIOwner => _uiOwner;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake() { NullableInstance = this; }

        private IEnumerator Start()
        {
            // [TODO] 기본 셋팅에 필요한 내용 적용
            // 1. Json Fil Load
            // 2. User Data Load
            // 3. Currency System Init
            // 4. Chapter Data Init
            JsonParser             .LoadJson();
            UserDataSystem         .CreateToUserSaveData();
            UserDataSystem         .Load  ();
            CurrencySystem.Instance.OnInit();
            _chapterGroup.OnInit();

            //UserDataSystem.Save();

            var data = UserDataSystem.GetToClearData(1, 2);
            Debug.Log($"Data Test 1 : {data} | {UserDataSystem.TestData()}");

            UserDataSystem.SetToClearData(1, 1, 2);
            UserDataSystem.SetToClearData(1, 2, 3);
            UserDataSystem.SetToClearData(1, 3, 3);
            UserDataSystem.SetToClearData(1, 4, 3);

            Debug.Log($"Data Test 2 : {data} | {UserDataSystem.TestData()}");







            // State 초기화
            StateMachine.Instance.ChangeState(EStateType.Village);




            yield return null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Chapter GetToChapter(int chapterCount                ) => _chapterGroup.GetToChapter(chapterCount);
        public Stage   GetToStage  (int chapterCount, int stageCount) => _chapterGroup.GetToStage(chapterCount, stageCount);
    }
}