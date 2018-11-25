using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score = 0;
    public Text ScoreText;
    public void ScoreUp()
    {
        Score++;
        ScoreText.text = "Score : " + Score;
    }

    public void RemoveHp()
    {

    }

}
