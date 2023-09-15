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
        [SerializeField] private int _health       = 100;
        [SerializeField] private int _currencyCoin = 0;
        [SerializeField] private int _currencyGem  = 0;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int Health
        {
            get => _health;
            set => _health = value;
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