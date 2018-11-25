using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score = 0;
    public Text ScoreText;

    public int Hp = 5;
    public Text HpText;

    public void ScoreUp()
    {
        Score++;
        ScoreText.text = "Score : " + Score;
    }

    public void RemoveHp()
    {
        Hp--;
        HpText.text = "Hp : " + Hp;
        if (Hp == 0)
            GameOver();
    }

    public void GameOver()
    {

    }

}
