using System.Collections;
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
        Debug.Log("Collision");
        CharCTRL t_Perso = collision.GetComponent<CharCTRL>();

        if (t_Perso == null)
        {
            Debug.Log("No Char");
            return;
        }
        score.ScoreUp();
    }


}
