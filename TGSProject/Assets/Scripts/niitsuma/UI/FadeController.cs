﻿using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    GameManager _gm;
    PlayerReload _load;

    [SerializeField] private float fadeInSpeed = 3;
    [SerializeField] private float fadeOutSpeed = 1;
    [SerializeField, Header("最初にフェード演出を入れるか")] private bool isStartFade = false;
    private SpriteRenderer _fadeUI;
    private float alpha = 0;
    private Color myColor;
    private Color colorAlphaZero;
    private System.Action _callback = null;


    void Awake()
    {
        _fadeUI = GetComponent<SpriteRenderer>();
        _gm = GameManager.Instance;
        myColor = _fadeUI.color;
        colorAlphaZero = new Color(myColor.r, myColor.g, myColor.b, 0);
    }

    void Start()
    {
        _load = FindObjectOfType<PlayerReload>();
        if(isStartFade) StartCoroutine(FadeIN());
    }
    /// <summary>
    /// 明るくする
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeIN()
    {
        // 念のため初期化
        _fadeUI.color = myColor;
        alpha = _fadeUI.color.a;
        if(null != _load) _load.Reload();
        while (0 < _fadeUI.color.a)
        {
            _fadeUI.color = new Color(_fadeUI.color.r, _fadeUI.color.g, _fadeUI.color.b, alpha);
            alpha -= Time.deltaTime / fadeInSpeed;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        if(null != _gm) { _gm.SetGameState(GameManager.GameState.Main); }
        // ループを抜けたらcallback
        if (_callback != null) _callback();
    }
    /// <summary>
    /// 暗くする
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOUT()
    {
        // 念のため初期化
        _fadeUI.color = colorAlphaZero;
        alpha = _fadeUI.color.a;
        while (_fadeUI.color.a < 1)
        {
            _fadeUI.color = new Color(_fadeUI.color.r, _fadeUI.color.g, _fadeUI.color.b, alpha);
            alpha += Time.deltaTime / fadeOutSpeed;
            yield return null;
        }
        if(null != _gm) { _gm.SetGameState(GameManager.GameState.SetUp); }
        yield return new WaitForSeconds(1);
        // ループを抜けたらcallback
        if (_callback != null) _callback();
    }
    /// <summary>
    /// フェードイン・アウト
    /// </summary>
    /// <param name="n">falseならフェードアウト, true ならフェードイン</param>
    public void Fade(bool n, System.Action callback = null)
    {
        _callback = callback;
        if(!n) { StartCoroutine(FadeOUT()); }
        else { StartCoroutine(FadeIN()); }
    }
    /// <summary>
    /// Fade前にカラーを初期化する
    /// </summary>
    public void ColorInitialize()
    {
        _fadeUI.color = colorAlphaZero;
    }
}
