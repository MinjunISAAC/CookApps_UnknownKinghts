using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitHit : MonoBehaviour
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private TextMeshProUGUI _TMP_Value = null;
    [SerializeField] private Animation       _anim      = null;

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    public void Show(int powerValue)
    {
        StartCoroutine(_Co_Show(powerValue));
    }

    private string _Format(int value) => string.Format("{0:n0}", value);

    // --------------------------------------------------
    // Functions - Coroutine
    // --------------------------------------------------
    private IEnumerator _Co_Show(int powerValue)
    {
        _TMP_Value.gameObject.SetActive(true);
        _TMP_Value.text = _Format(powerValue);

        var animLength = _anim.clip.length;
        _anim.Play();

        yield return new WaitForSeconds(animLength);

        Destroy(gameObject);
    }
}
