﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public ScoreManager score;

    private void Start()
    {
        score = GameObject.FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharCheck t_char = collision.GetComponent<CharCheck>();
        Debug.Log(t_char);
    }

}
