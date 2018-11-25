using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public static levelManager instance = null;

    public BoardManager BoardScript;

	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
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
