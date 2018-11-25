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
    public Tile Exit;

    private List<GameObject> CoinList;
    private BoardManager board;
    

    private void Start()
    {
        board = GameObject.FindObjectOfType<BoardManager>();
        CoinList = board.GetCoins();
        Exit = board.exit.GetComponent<Tile>();
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
        Exit.BaseCost = 1;
    }


    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }

}
