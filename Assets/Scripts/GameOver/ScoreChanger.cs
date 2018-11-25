using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanger : MonoBehaviour
{
    public ScoreManager score;
    public Text Text;

    private void Awake()
    {
        score = GameObject.FindObjectOfType<ScoreManager>();
        Text.text = "SCORE : " + score.Score;
        Destroy(score.gameObject);
    }
}
