﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : BaseEnemy
{
    [SerializeField]
    private float hoge;
    //[SerializeField]
    private float _aho = -17f;
    private Animator _anim;
    private float _taim = 3f;

    // Start is called before the first frame update
    void Start()
    {
        base.enemyID = EnemyType.Plant;
        hoge += Time.deltaTime;
        _anim = GetComponent<Animator>();
    }

    private void LuisuKill()
    {
        Vector2 LuisPos = player.position;
        if (GameManager.Instance.GetGameState == GameManager.GameState.EventStart)
        {
            transform.position = new Vector2(LuisPos.x,
                Mathf.MoveTowards(transform.position.y, LuisPos.y + _aho, hoge));
            GetComponent<Animator>().enabled = true;
            _taim -= Time.deltaTime;
            if(_taim == 0)
            {
                GameManager.Instance.Information.DecreaseHP();
            }
        }
        else
        {  
            transform.position = new Vector2(LuisPos.x, -22f);
        }
    }

    private void LuisEnd()
    {
        //if()
    }

    // Update is called once per frame
    void Update()
    {
        LuisuKill();
    }
}
