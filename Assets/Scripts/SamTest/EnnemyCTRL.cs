using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnnemyCTRL : MonoBehaviour
{
    public float HP = 3;
    public float Speed = 1;
    public float DistanceCheck = 0.05f;

    private Path m_Path;
    private Path CurrentPath;
    private int NextTargetId = 1;

    private bool AiActive = true;

    private GameObject m_EndTile;

    private float DetectPlayer = 2;

    private bool FindThePath = false;

    public EnnemyPathFinder EnnemyPathFinder;

    private ScoreManager score;

    [Header("Sounds")]
    public AudioClip DieSond;

    // Use this for initialization
    void Start()
    {
        score = GameObject.FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
        if (!AiActive)
            return;

        DetectPlayer -= Time.deltaTime;

        if (DetectPlayer <= 0.3f && DetectPlayer >= 0f)
        {
            FindThePath = true;
        }

        if (DetectPlayer <= 0)
        {
            
            if (FindThePath)
            {
                FindThePath = false;

                NextTargetId = 1;
                m_Path = null;
                CurrentPath = null;

                m_EndTile = GameObject.FindGameObjectWithTag("Player");

                EnnemyPathFinder.FindEndPoint();

                m_Path = GameObject.FindObjectOfType<EnnemyPathFinder>().GetPath(this.transform);

                

                if (m_Path.Tiles.Count < 2)
                {
                    Debug.LogError("PATH INVALID - < 2 elements");
                }
            }

            Vector2 t_Direction = m_Path.Tiles[NextTargetId].transform.position - transform.position;

            transform.Translate(t_Direction.normalized * Time.deltaTime * Speed);

            float c2 = t_Direction.sqrMagnitude;

            if (c2 < DistanceCheck * DistanceCheck)
            {
                if (++NextTargetId == m_Path.Tiles.Count)
                {
                    FindThePath = false;
                    DetectPlayer = 2;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (m_Path == null || m_Path.Tiles == null) return;
        for (int i = 0; i < m_Path.Tiles.Count - 1; i++)
        {
            Gizmos.DrawLine(m_Path.Tiles[i].transform.position, m_Path.Tiles[i + 1].transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        Debug.LogWarning(collider);
        if (collider.gameObject.tag == "PlayerHitBox")
        {
            print("Collision entre " + this.tag + " et le joueur");
            score.RemoveHp();
        }

        if (collider.gameObject.tag == "Bullet")
        {
            print("Collision entre " + this.tag + " et lune balle du joueur");
            HP--;
        }
    }
}