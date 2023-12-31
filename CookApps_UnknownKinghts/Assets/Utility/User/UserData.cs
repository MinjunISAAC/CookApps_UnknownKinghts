// ----- C#
using InGame.ForUnit.ForData;
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace Utility.ForData.ForUser
{
    [Serializable]
    public class UserData
    {
        [Serializable]
        public class ClearData
        {
            public int Chapter   = 0;
            public int Stage     = 0;
            public int ClearStar = 0;

            public ClearData(int chapter, int stage, int clearStar)
            {
                Chapter   = chapter;
                Stage     = stage;  
                ClearStar = clearStar;
            }
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private int             _level          = 1;
        [SerializeField] private int             _experience     = 0;
        [SerializeField] private int             _maxHealth      = 0;
        [SerializeField] private string          _userName       = "�׽�Ʈ";

        [SerializeField] private List<ClearData> _clearData      = new List<ClearData>();
        [SerializeField] private int             _lastChapter    = 1;
        [SerializeField] private int             _lastStage      = 1;

        [SerializeField] private int             _currencyHealth = 100;
        [SerializeField] private int             _currencyCoin   = 0;
        [SerializeField] private int             _currencyGem    = 0;

        [SerializeField] private List<UnitData> _ownedUnits      = new List<UnitData>();

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        #region <User Info>
        public int Level
        {
            get => _level;
            set => _level = value;
        }
        public int Experience
        {
            get => _experience;
            set => _experience = value;
        }
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }
        public string UserName
        {
            get => _userName;
            set => _userName = value;   
        }
        #endregion

        #region <Chapter Info>
        public List<ClearData> ClearDataList
        {
            get => _clearData;
            set => _clearData = value;
        }
        public int LastChapter
        {
            get => _lastChapter;
            set => _lastChapter = value;
        }
        public int LastStage
        {
            get => _lastStage;
            set => _lastStage = value;
        }
        #endregion

        #region <Curreny>
        public int CurrencyHealth
        {
            get => _currencyHealth;
            set => _currencyHealth = value;
        }
        public int CurrencyCoin
        {
            get => _currencyCoin;
            set => _currencyCoin = value;
        }
        public int CurrencyGem
        {
            get => _currencyGem;
            set => _currencyGem = value;
        }
        #endregion

        #region<Unit>
        public List<UnitData> OwnedUnits
        {
            get => _ownedUnits;
            set => _ownedUnits = value;
        }
        #endregion
    }
}