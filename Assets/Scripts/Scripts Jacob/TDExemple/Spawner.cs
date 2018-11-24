using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Transform m_SpawnPoint;
    [SerializeField]
    private GameObject m_Enemy;

    private void Start()
    {
        StartCoroutine(SpawnRoutine(10, 2, 5));
    }

    private void SpawnEnemy()
    {
        Instantiate(m_Enemy, m_SpawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine(int a_EnemyCount, float a_SpawnInterval, float a_InitDelay)
    {
        yield return new WaitForSeconds(a_InitDelay);

        for (int i = 0; i < a_EnemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2);
        }
    
    }
}
