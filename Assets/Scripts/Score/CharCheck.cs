using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCheck : MonoBehaviour
{
    public ScoreManager score;

    private void Start()
    {
        score = GameObject.FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        PickUp t_Perso = collision.GetComponent<PickUp>();

        if (t_Perso == null)
        {
            Debug.Log("No Char");
            return;
        }
        //score.ScoreUp();
    }

}
