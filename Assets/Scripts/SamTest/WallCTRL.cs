using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCTRL : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            print("Collision entre " + this.tag + " et lune balle du joueur");
            Destroy(collider.gameObject);
        }
    }
}
