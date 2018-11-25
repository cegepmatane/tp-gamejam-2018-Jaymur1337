using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysics : MonoBehaviour
{

    public float BulletSpeed = 5f;
    public float BulletTimer = 3f;

    [Header("Sounds")]
    public AudioClip Paff;

    // Use this for initialization
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {

        BulletTimer -= Time.deltaTime;

        //this.transform.Translate(Vector3.up * Time.deltaTime * BulletSpeed);
        this.transform.position += this.transform.up * Time.deltaTime * BulletSpeed;

        if (BulletTimer <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}