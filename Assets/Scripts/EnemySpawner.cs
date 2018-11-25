using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Zombie;
    public float MinSpawnTime = 1f;
    public float MaxSpawnTime = 5f;


    // Use this for initialization
    void Start ()
    {
        float t_InitialSpawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
        StartCoroutine(SpawnZombie(t_InitialSpawnTime));
    }
	
	

    private IEnumerator SpawnZombie(float a_Delay)
    {
        yield return new WaitForSeconds(a_Delay); 

        Instantiate(Zombie, this.transform.position, Quaternion.identity);


        float t_NewDelay = Random.Range(MinSpawnTime, MaxSpawnTime);
        StartCoroutine(SpawnZombie(t_NewDelay));


    }
}
