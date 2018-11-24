using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp;
    public float Speed = 1;
    public float DistanceCheck = 0.05f;

    private Path m_Path;
    private int NextTargetID = 1;
    private bool AIActive = true;

    [Header("Sounds")]
    public AudioClip DieSound;

    private void Start()
    {
        m_Path = GameObject.FindObjectOfType<PathFinder>().GetPath(transform);
        if (m_Path.Tiles.Count < 2)
        {
            Debug.LogError("PATH INVALIDE COLIS - < QUE 2 ÉLEMENTS");
            Destroy(gameObject);
        }

        //transform.position = m_Path.Path[0].position;
    }

    private void Update()
    {

        if (!AIActive) return;
        Vector2 t_Direction = m_Path.Tiles[NextTargetID].transform.position - transform.position;


        transform.Translate(t_Direction.normalized * Speed * Time.deltaTime);

        float c2 = t_Direction.sqrMagnitude;

        if (c2 < DistanceCheck * DistanceCheck)
        {
            //Si tes rendu

            if (++NextTargetID == m_Path.Tiles.Count)
            {
                StartCoroutine(Die());
            }

        }
    }

    IEnumerator Die()
    {
        AudioSource t_AudioSource = GetComponent<AudioSource>();
        t_AudioSource.PlayOneShot(DieSound);

        AIActive = false;

        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(DieSound.length);

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        //if (!displayPath) return;
        Gizmos.color = Color.red;
        if (m_Path.Tiles == null) return;
        //Gizmos.color = PathColor;
        //length -1 1 pcq ne trace pas la ligne du dernier//
        for (int i = 0; i < m_Path.Tiles.Count - 1; i++)
        {
            Gizmos.DrawLine(m_Path.Tiles[i].transform.position, m_Path.Tiles[i + 1].transform.position);
        }
    }
}
