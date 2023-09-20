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
using InGame.ForUnit.ForData;
using InGame.ForUnit;
using InGame.ForBattle.ForTime;
using InGame.ForCam;

namespace InGame
{
    public class Owner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Game Data")]
        [SerializeField] private ChapterGroup   _chapterGroup   = null;

        [Header("2. Manage Group")]
        [SerializeField] private UnitController _unitController = null;
        [SerializeField] private CamController  _camController  = null;
        [SerializeField] private Transform _test = null;

        [Header("3. UI Owner")]
        [SerializeField] private UIOwner        _uiOwner        = null;

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

        public UIOwner        UIOwner        => _uiOwner;
        public UnitController UnitController => _unitController;
        public CamController  CamController  => _camController;   

        public Transform Test => _test;
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
            JsonParser               .LoadJson();
            UserDataSystem           .Load    ();
            CurrencySystem.Instance  .OnInit  ();
            SpritePoolSystem.Instance.OnInit  ();
            _chapterGroup            .OnInit  ();

            // [Test] Unit 추가
            _Test_AddOwnedUnit();

            // State 초기화
            StateMachine.Instance.ChangeState(EStateType.Village);

            yield return null;
        }

        private void Update()
        {
            StateMachine.Instance.OnUpdate();
        }
        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Chapter GetToChapter(int chapterCount                ) => _chapterGroup.GetToChapter(chapterCount);
        public Stage   GetToStage  (int chapterCount, int stageCount) => _chapterGroup.GetToStage(chapterCount, stageCount);

        // --------------------------------------------------
        // Functions - Test
        // --------------------------------------------------
        private void _Test_AddOwnedUnit()
        {
            UnitData unknownHero = new UnitData
            (
                1, 1, 20, "무명용사",
                EJobType.Warrior, EGradeType.Nomal, EUnitType.UnknownHero, ESpecType.Light, EAttackPosType.Front,
                1f, 150, 5000, 30, 30, 200, 5, 1f,
                "슬래시 다운", 7
            );

            UnitData helio = new UnitData
            (
                3, 1, 60, "헬리오",
                EJobType.Warrior, EGradeType.Epic, EUnitType.Helio, ESpecType.Fire, EAttackPosType.Front,
                1.25f, 247, 9317, 60, 60, 200, 7, 1f,
                "저지먼트", 9
            );

            UnitData cerus = new UnitData
            (
                2, 1, 40, "케루스",
                EJobType.Support, EGradeType.Rare, EUnitType.Cerus, ESpecType.Wind, EAttackPosType.Rear,
                3f, 151, 6195, 28, 28, 200, 5, 1f,
                "토네이도 스윕", 8
            );

            UnitData pelson = new UnitData
            (
                1, 1, 20, "펠슨",
                EJobType.Wizard, EGradeType.Nomal, EUnitType.Pelson, ESpecType.Stone, EAttackPosType.Center,
                2f, 187, 3533, 16, 40, 200, 7, 1f,
                "어스퀘이크", 8
            );

            UnitData flame = new UnitData
            (
                2, 1, 40, "플라메",
                EJobType.Archer, EGradeType.Rare, EUnitType.Flame, ESpecType.Fire, EAttackPosType.Center,
                2.25f, 214, 4703, 42, 70, 200, 5, 1f,
                "플레임 볼", 9
            );

            UserDataSystem.AddToOwnedUnitDataList(unknownHero);
            UserDataSystem.AddToOwnedUnitDataList(helio      );
            UserDataSystem.AddToOwnedUnitDataList(cerus      );
            UserDataSystem.AddToOwnedUnitDataList(pelson     );
            UserDataSystem.AddToOwnedUnitDataList(flame      );
        }
    }
}