using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utility.ForCurrency.ForUI
{
    public class CurrencyView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Currency Type")]
        [SerializeField] private ECurrencyType   _currenyType = ECurrencyType.Unknown;

        [Header("UI Group")]
        [SerializeField] private TextMeshProUGUI _TMP_Value   = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void RefreshToCurreny(int value)
        {
            if (_TMP_Value == null)
            {
                Debug.LogError($"<color=red>[CurrencyView.RefreshToCurreny] {_currenyType}의 View가 존재하지 않습니다.</color>");
            }

            _TMP_Value.text = _Format(value);
        }

        public void RefreshToCurreny(int value, int maxValue)
        {
            if (_TMP_Value == null)
            {
                Debug.LogError($"<color=red>[CurrencyView.RefreshToCurreny] {_currenyType}의 View가 존재하지 않습니다.</color>");
            }

            _TMP_Value.text = _Format(value, maxValue);
        }

        // ----- Private
        private string _Format(int value              ) => string.Format("{0:n0}", value);
        private string _Format(int value, int maxValue) => $"{value}/{maxValue}";
    }
}