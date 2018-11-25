using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{


    public int Score = 0;
    public Text ScoreText;

    public int Hp = 5;
    public Text HpText;
    private int CoinCounter = 0;
    public Collider2D ExitCollider;

    private List<GameObject> CoinList;
    private BoardManager board;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        board = GameObject.FindObjectOfType<BoardManager>();
        CoinList = board.GetCoins();
        ExitCollider = board.exit.GetComponent<Collider2D>();
        foreach(GameObject coin in CoinList)
        {
            CoinCounter++;
        }
    }

    public void ScoreUp(bool IsCoin)
    {
        Score++;
        ScoreText.text = "Score : " + Score;
        if (IsCoin)
            CoinCounter--;
        if(CoinCounter == 0)
        {
            OpenExit();
        }
    }

    public void RemoveHp()
    {
        Hp--;
        HpText.text = "Hp : " + Hp;
        if (Hp == 0)
            GameOver();
    }
    public void OpenExit()
    {
        ExitCollider.enabled = true;
    }


    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }

}
