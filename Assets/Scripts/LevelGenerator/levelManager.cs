using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public BoardManager BoardScript;

	// Use this for initialization
	void Awake ()
    {
        BoardScript = GetComponent<BoardManager>();
        InitGame();
	}
    void InitGame()
    {
        BoardScript.SetupScene(3);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
