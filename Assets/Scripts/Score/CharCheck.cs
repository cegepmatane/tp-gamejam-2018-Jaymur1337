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
        PickUp t_PickUp = collision.GetComponent<PickUp>();

        if (t_PickUp == null)
        {
            Debug.Log("No Coin");
            return;
        }
        score.ScoreUp(true);
    }

}
