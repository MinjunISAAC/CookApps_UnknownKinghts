// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace Utility.ForData.ForUser 
{
    public class UserData : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private int _level          = 1;

        [SerializeField] private int _currencyHealth = 100;
        [SerializeField] private int _currencyCoin   = 0;
        [SerializeField] private int _currencyGem    = 0;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
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
    }
}