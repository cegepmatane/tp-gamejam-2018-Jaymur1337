using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysics : MonoBehaviour
{

    public int BulletSpeed = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(Vector3.up * Time.deltaTime * BulletSpeed);
        this.transform.position += this.transform.up * Time.deltaTime * BulletSpeed;
    }
}