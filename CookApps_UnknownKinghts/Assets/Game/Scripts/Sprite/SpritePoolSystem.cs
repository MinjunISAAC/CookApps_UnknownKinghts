using InGame.ForChapterGroup.ForStage;
using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.ForData.ForUser;

public class SpritePoolSystem : MonoBehaviour
{
    // --------------------------------------------------
    // Singleton
    // --------------------------------------------------
    // ----- Constructor
    private SpritePoolSystem() { }

    // ----- Static Variables
    private static SpritePoolSystem _instance = null;

    // ----- Variables
    private const string FILE_PATH = "SpritePoolSystem";
    private bool _isSingleton = false;

    // ----- Property
    public static SpritePoolSystem Instance
    {
        get
        {
            if (null == _instance)
            {
                var existingInstance = FindObjectOfType<SpritePoolSystem>();

                if (existingInstance != null)
                {
                    _instance = existingInstance;
                }
                else
                {
                    var origin = Resources.Load<SpritePoolSystem>(FILE_PATH);
                    _instance = Instantiate<SpritePoolSystem>(origin);
                    _instance._isSingleton = true;
                    _instance.OnInit();

                    DontDestroyOnLoad(_instance.gameObject);
                }
            }

            return _instance;
        }
    }

    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private SpritePool _pool = null;

    // --------------------------------------------------
    // Functions - Event
    // --------------------------------------------------
    private void Awake()
    {
        if (null == _instance)
        {
            var existingInstance = FindObjectOfType<SpritePoolSystem>();

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
        _pool.OnInit();
    }

    public Sprite GetToSpecSprite      (ESpecType type)  => _pool.GetToSpecSprite(type);
    public Sprite GetToUnitSprite      (EUnitType type)  => _pool.GetToUnitSprite(type);
    public Sprite GetToJobSprite       (EJobType type)   => _pool.GetToJobSprite(type);
    public Sprite GetToGradeFrameSprite(EGradeType type) => _pool.GetToGradeFrameSprite(type);
}
