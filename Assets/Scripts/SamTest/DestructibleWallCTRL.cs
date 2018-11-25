using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWallCTRL : MonoBehaviour {

    private float Hp = 200;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            print("Collision entre " + this.tag + " et lune balle du joueur");
            Hp--;
            Destroy(collider.gameObject);
            if (Hp <= 0)
            {
                GetComponent<Tile>().Active = false;
                GameObject.FindObjectOfType<MapGrid>().CalcGrid();
                Destroy(this.gameObject);
            }
        }
    }

}
