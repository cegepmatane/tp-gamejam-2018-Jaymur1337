﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
	// Use this for initialization
	void Awake ()
    {
        if (levelManager.instance == null)

            //Instantiate gameManager prefab
            Instantiate(gameManager);
    }
	
}
