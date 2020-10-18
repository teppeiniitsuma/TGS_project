﻿using UnityEngine;
using UnityEngine.UI;
using DualShockInput;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private Image _panelImage;
    [SerializeField] private Image[] _yesImage = new Image[2];
    [SerializeField] private Image[] _noImage = new Image[2];
    [SerializeField] private ScenarioMessageUseCase useCase;
    [SerializeField] FadeController _fade;

    bool _selects = true; // yes == false , no == true
    bool _isPause = false;
    [SerializeField] private SceneType _sceneType;
    public enum SceneType
    {
        StageScene,
        ScenarioScene,
    }

    void Start()
    {
        PanelReset();
    }

    void PanelReset()
    {
        _yesImage[0].gameObject.SetActive(false);
        _yesImage[1].gameObject.SetActive(true);
        _noImage[0].gameObject.SetActive(true);
        _noImage[1].gameObject.SetActive(false);
    }
    void PauseView()
    {
        _panelImage.gameObject.SetActive(true);
        if (_selects) 
        {
            _yesImage[0].gameObject.SetActive(true);
            _yesImage[1].gameObject.SetActive(false);
            _noImage[0].gameObject.SetActive(false);
            _noImage[1].gameObject.SetActive(true);
        }
        else
        {
            PanelReset();
        }
    }

    void PauseNotView()
    {
        PanelReset();
        _panelImage.gameObject.SetActive(false);
    }
    void Update()
    {
        if(_sceneType == SceneType.StageScene)
        {
            if (Input.GetKeyDown(KeyCode.P) || DSInput.PushDown(DSButton.Option)) { GameManager.Instance.SetGameState(GameManager.GameState.Pause); }
            if (GameManager.Instance.GetGameState == GameManager.GameState.Pause)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow)) { _selects = true; }
                if (Input.GetKeyDown(KeyCode.LeftArrow)) { _selects = false; }
                PauseView();
                if (DSInput.PushDown(DSButton.Circle))
                {
                    PanelReset();
                    _fade.Fade(false, () => StageConsole.MyLoadScene(StageConsole.MyScene.Title));
                }
                else if (DSInput.PushDown(DSButton.Cross))
                {
                    PanelReset(); GameManager.Instance.SetGameState(GameManager.GameState.Main);
                }
                if (_selects)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        PanelReset(); GameManager.Instance.SetGameState(GameManager.GameState.Main);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        PanelReset();
                        _fade.Fade(false, () => StageConsole.MyLoadScene(StageConsole.MyScene.Title));
                    }
                }
            }
            else
            {
                PauseNotView();
            }
        }
        else if(_sceneType == SceneType.ScenarioScene)
        {
            if (Input.GetKeyDown(KeyCode.P) || DSInput.PushDown(DSButton.Option)) { _isPause = true; }
            if (Input.GetKeyDown(KeyCode.RightArrow)) { _selects = true; }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) { _selects = false; }
            if (_isPause)
            {
                PauseView();
                if (DSInput.PushDown(DSButton.Circle))
                {
                    PanelReset();
                    if (useCase.GetScenarioNum == 0)
                    {
                        _fade.Fade(false, () => StageConsole.MyLoadScene(StageConsole.MyScene.Tutorial));
                    }
                    else
                    {
                        // エピローグ時の処理
                        _fade.Fade(false, () => StageConsole.MyLoadScene(StageConsole.MyScene.Title));
                    }
                }
                else if (DSInput.PushDown(DSButton.Cross))
                {
                    PanelReset();
                    _panelImage.gameObject.SetActive(false);
                    _isPause = false;
                }
                if (_selects)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        PanelReset();
                        _panelImage.gameObject.SetActive(false);
                        _isPause = false;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        PanelReset();
                        if (useCase.GetScenarioNum == 0)
                        {
                            _fade.Fade(false, () => StageConsole.MyLoadScene(StageConsole.MyScene.Tutorial));
                        }
                        else
                        {
                            // エピローグ時の処理
                            _fade.Fade(false, () => StageConsole.MyLoadScene(StageConsole.MyScene.Title));
                        }
                            
                    }
                }
            }
        }
        
    }
}
