using CoreData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.ForCurrency.ForUI;
using Utility.ForData.ForUser;

namespace Utility.ForCurrency
{
    public class CurrencySystem : MonoBehaviour
    {
        // --------------------------------------------------
        // Singleton
        // --------------------------------------------------
        // ----- Constructor
        private CurrencySystem() { }

        // ----- Static Variables
        private static CurrencySystem _instance = null;

        // ----- Variables
        private const string FILE_PATH = "CurrenySystem";
        private bool _isSingleton = false;

        // ----- Property
        public static CurrencySystem Instance
        {
            get
            {
                if (null == _instance)
                {
                    var existingInstance = FindObjectOfType<CurrencySystem>();

                    if (existingInstance != null)
                    {
                        _instance = existingInstance;
                    }
                    else
                    {
                        var origin = Resources.Load<CurrencySystem>(FILE_PATH);
                        _instance  = Instantiate<CurrencySystem>(origin);
                        _instance._isSingleton = true;
                        
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Canvas")]
        [SerializeField] private RectTransform _RECT_Canvas   = null;

        [Header("Asset View Group")]
        [SerializeField] private CurrencyView  _coinHudView   = null;
        [SerializeField] private CurrencyView  _gemHudView    = null;
        [SerializeField] private CurrencyView  _healthHudView = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            // User Save Data Load
            UserDataSystem.Load();
            
            if (null == _instance)
            {
                var existingInstance = FindObjectOfType<CurrencySystem>();

                if (existingInstance != null)
                {
                    _instance = existingInstance;
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            if (_isSingleton)
                _instance = null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit()
        {
            if (_healthHudView == null || _coinHudView == null || _gemHudView == null)
            {
                Debug.LogError($"<color=red>[CurrencySystem.OnInit] Currency들의 View가 존재하지 않는 View가 존재합니다.</color>");
                return;
            }

            var userLevel   = UserDataSystem.GetToLevel ();
            var coinValue   = UserDataSystem.GetToCoin  ();
            var gemValue    = UserDataSystem.GetToGem   ();
            var healthValue = UserDataSystem.GetToHealth();
            var maxHealth   = CoreDataHelper.GetToMaxHealth(userLevel);

            _coinHudView   .RefreshToCurreny(coinValue  );
            _gemHudView    .RefreshToCurreny(gemValue   );
            _healthHudView .RefreshToCurreny(healthValue, maxHealth);
        }

        public void RefreshCredit(ECurrencyType type)
        {
            var userLevel   = UserDataSystem.GetToLevel    ();
            var coinValue   = UserDataSystem.GetToCoin     ();
            var gemValue    = UserDataSystem.GetToGem      ();
            var healthValue = UserDataSystem.GetToHealth   ();
            var maxHealth   = CoreDataHelper.GetToMaxHealth(userLevel);

            switch (type)
            {
                case ECurrencyType.Coin   : _coinHudView   .RefreshToCurreny(coinValue             ); break;
                case ECurrencyType.Gem    : _gemHudView    .RefreshToCurreny(gemValue              ); break;
                case ECurrencyType.Health : _healthHudView .RefreshToCurreny(healthValue, maxHealth); break;
            }
        }

        public void RefreshAllCredit()
        {
            var userLevel   = UserDataSystem.GetToLevel    ();
            var coinValue   = UserDataSystem.GetToCoin     ();
            var gemValue    = UserDataSystem.GetToGem      ();
            var healthValue = UserDataSystem.GetToHealth   ();
            var maxHealth   = CoreDataHelper.GetToMaxHealth(userLevel);

            _coinHudView  .RefreshToCurreny(coinValue             );
            _gemHudView   .RefreshToCurreny(gemValue              );
            _healthHudView.RefreshToCurreny(healthValue, maxHealth);
        }
    }
}