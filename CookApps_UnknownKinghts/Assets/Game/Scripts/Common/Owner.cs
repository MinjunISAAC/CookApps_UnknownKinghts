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
            // [TODO] �⺻ ���ÿ� �ʿ��� ���� ����
            // 1. Json Fil Load
            // 2. User Data Load
            // 3. Currency System Init
            // 4. Chapter Data Init
            JsonParser             .LoadJson();
            UserDataSystem         .Load  ();
            CurrencySystem.Instance.OnInit();
            _chapterGroup.OnInit();

            // [Test] Unit �߰�
            _Test_AddOwnedUnit();

            // State �ʱ�ȭ
            StateMachine.Instance.ChangeState(EStateType.Village);

            yield return null;
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
                1, 20, "������",
                EGradeType.Unknown, EUnitType.Unknown, ESpecType.Light, EAttackPosType.Front,
                150, 5000, 30, 30, 200, 5, 1f,
                "������ �ٿ�", 7
            );

            UnitData helio = new UnitData
            (
                1, 60, "�︮��",
                EGradeType.Epic, EUnitType.Nomal_Warrior, ESpecType.Fire, EAttackPosType.Front,
                247, 9317, 60, 60, 200, 7, 1f,
                "������Ʈ", 9
            );

            UnitData cerus = new UnitData
            (
                1, 40, "�ɷ罺",
                EGradeType.Rare, EUnitType.Nomal_Healer, ESpecType.Wind, EAttackPosType.Rear,
                151, 6195, 28, 28, 200, 5, 1f,
                "����̵� ����", 8
            );

            UnitData pelson = new UnitData
            (
                1, 20, "�罼",
                EGradeType.Nomal, EUnitType.Nomal_Wizard, ESpecType.Stone, EAttackPosType.Center,
                187, 3533, 16, 40, 200, 7, 1f,
                "�����ũ", 8
            );

            UnitData flame = new UnitData
            (
                1, 40, "�ö��",
                EGradeType.Rare, EUnitType.Nomal_Archer, ESpecType.Fire, EAttackPosType.Center,
                214, 4703, 42, 70, 200, 5, 1f,
                "�÷��� ��", 9
            );

            UserDataSystem.AddToOwnedUnitDataList(unknownHero);
            UserDataSystem.AddToOwnedUnitDataList(helio      );
            UserDataSystem.AddToOwnedUnitDataList(cerus      );
            UserDataSystem.AddToOwnedUnitDataList(pelson     );
            UserDataSystem.AddToOwnedUnitDataList(flame      );
        }
    }
}