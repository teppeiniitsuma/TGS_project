﻿using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    GameManager _gm;
    PlayerReload _load;

    [SerializeField] private float fadeInSpeed = 3;
    [SerializeField] private float fadeOutSpeed = 1;
    private SpriteRenderer _fadeUI;
    private float alpha = 0;
    bool outCheck = false;
    bool _fadeInFlag = false;

    void Awake()
    {
        _fadeUI = GetComponent<SpriteRenderer>();
        _gm = GameManager.Instance;
    }

    void Start()
    {
        _load = FindObjectOfType<PlayerReload>();
        StartCoroutine(FadeIN());
    }
    /// <summary>
    /// 明るくする
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIN()
    {
        Debug.Log("ええやん");
        // 念のため初期化
        _fadeUI.color = Color.black;
        alpha = _fadeUI.color.a;
        _load.Reload();
        while (0 < _fadeUI.color.a)
        {
            _fadeUI.color = new Color(_fadeUI.color.r, _fadeUI.color.g, _fadeUI.color.b, alpha);
            alpha -= Time.deltaTime / fadeInSpeed;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        _gm.SetGameState(GameManager.GameState.Main);
    }
    /// <summary>
    /// 暗くする
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOUT()
    {
        // 念のため初期化
        _fadeUI.color = Color.clear;
        alpha = _fadeUI.color.a;
        while (_fadeUI.color.a < 1)
        {
            _fadeUI.color = new Color(_fadeUI.color.r, _fadeUI.color.g, _fadeUI.color.b, alpha);
            alpha += Time.deltaTime / fadeOutSpeed;
            yield return null;
        }
        _gm.SetGameState(GameManager.GameState.SetUp);
        yield return new WaitForSeconds(1);
        _fadeInFlag = true;
    }

    void Update()
    {
        if (_gm.GetGameState == GameManager.GameState.Road) outCheck = true;
        if (outCheck && !_fadeInFlag) { outCheck = false; StartCoroutine(FadeOUT()); }
        if (_fadeInFlag) { _fadeInFlag = false; StartCoroutine(FadeIN()); }
    }
}
