﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCamera : MonoBehaviour
{
    SpiderEnemy spider;

    private void Start()
    {
        spider = transform.parent.GetComponent<SpiderEnemy>();
    }
    private void OnBecameVisible()
    {
        spider.isCamera = false;
        GetComponent<Animator>().enabled = true;
    }

    private void OnBecameInvisible()
    {
        spider.isCamera = true;
        GetComponent<Animator>().enabled = false;
    }

}