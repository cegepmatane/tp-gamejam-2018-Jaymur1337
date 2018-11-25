using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateCTRL : MonoBehaviour {

    private float Hp = 2;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            print("Collision entre " + this.tag + " et lune balle du joueur");
            Hp--;
            Destroy(collider.gameObject);
            if (Hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
