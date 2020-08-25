﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    public GameState GetGameState { get { return _gameState; }}
    public EventState GetEventState { get { return _eventState; } }
    public PlayerInfoCounter Information { get { return _info; } }
    public UIManager UIInfo { get { return _uiManager; } }
    // プレイヤーが光の範囲内にいるか
    public bool InLightRange { get; private set; } = true;

    [SerializeField] private GameState _gameState;
    private EventState _eventState = EventState.Default;
    static GameManager _instance;
    private PlayerInfoCounter _info;
    private UIManager _uiManager;

    public ResultData GetResultData { get { return r_data; } set { r_data = value; } }
    private ResultData r_data = new ResultData();

    private void Awake()
    {
        _instance = this;
        _info = FindObjectOfType<PlayerInfoCounter>();
        _uiManager = FindObjectOfType<UIManager>();
        _info.Initialize();
    }

    /// <summary>
    /// ゲームの状態管理用
    /// </summary>
    public enum GameState
    {
        SetUp = 0,
        Main,   
        Road,   // ロード
        Event,  // イベント用
        Damage, // エネミーdamage用
        Pause,  // ポーズ画面用
        GameOver,
        Result,
    }
    /// <summary>
    /// イベント管理用
    /// </summary>
    public enum EventState
    {
        Default,
        AttackEvent, // 毛虫の恩返し用のイベント
        GimmickEvent,
        ScenarioEvent,

    }
    /// <summary>
    /// ゲームステートを書き換える
    /// </summary>
    /// <param name="g"></param>
    public void SetGameState(GameState g)
    {
        _gameState = g;
    }
    /// <summary>
    /// イベント用に状態を切り替える
    /// </summary>
    /// <param name="e"></param>
    public void SetEventState(EventState e)
    {
        _eventState = e;
    }
    /// <summary>
    /// 単独行動時ライトの範囲内でなければfalseにする
    /// </summary>
    /// <param name="light"></param>
    public void SetLightPos(bool light)
    {
        InLightRange = light;
    }
}
