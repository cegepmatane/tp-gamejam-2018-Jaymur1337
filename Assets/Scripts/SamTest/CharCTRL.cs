using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharCTRL : MonoBehaviour
{

    public float HP;
    public float Speed = 1;
    public float DistanceCheck = 0.05f;

    private Path m_Path;
    private Path CurrentPath;
    private int NextTargetId = 1;

    private bool AiActive = false;

    public GameObject m_EndTilePref;
    private GameObject m_EndTile;

    private CharPathFinder PlayerPathFinder;


    public ScoreManager score;
    public GameObject Gun;
    public GameObject Bullet;

    private bool Shooting = false;
    private float ShootTimer;

    [Header("Sounds")]
    public AudioClip DieSond;
    public AudioClip BlockedSond;

    
    // Use this for initialization
    void Start()
    {
        score = GameObject.FindObjectOfType<ScoreManager>();
        PlayerPathFinder = GameObject.FindObjectOfType<CharPathFinder>();
        m_EndTile = Instantiate(m_EndTilePref);
        m_EndTile.transform.position = this.transform.position;
        m_EndTile.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_EndTile.SetActive(true);
            NextTargetId = 1;
            m_Path = null;
            CurrentPath = null;
            m_EndTile = GameObject.FindGameObjectWithTag("EndTile");
            AiActive = true;

            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;


            m_EndTile.transform.position = pz;

            PlayerPathFinder.FindEndPoint();

            m_Path = GameObject.FindObjectOfType<CharPathFinder>().GetPath(this.transform);
            Debug.Log(m_Path);

            if (m_Path.Tiles.Count < 2)
            {
                Debug.LogError("PATH INVALID - < 2 elements");
                m_EndTile.SetActive(false);
                AiActive = false;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {

            Vector3 pz2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz2.z = 0;
            pz2 = pz2 - transform.position;

            Vector2 t_ShootingDirection = pz2;

            if (t_ShootingDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(t_ShootingDirection.y, t_ShootingDirection.x) * Mathf.Rad2Deg;
                GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }

            GameObject ShootedBullet = Instantiate(Bullet);
            ShootedBullet.transform.position = Gun.transform.position;
            ShootedBullet.transform.rotation = Gun.transform.rotation;
            Shooting = true;
            ShootTimer = 1;

        }
            if (!AiActive)
            return;

        Vector2 t_Direction = m_Path.Tiles[NextTargetId].transform.position - transform.position;

        if (t_Direction != Vector2.zero)
        {
            ShootTimer -= Time.deltaTime;
            if (ShootTimer < 0)
            {
                Shooting = false;
            }
            float angle = Mathf.Atan2(t_Direction.y, t_Direction.x) * Mathf.Rad2Deg;
            if (!Shooting)
            {
                GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
        }

        transform.Translate(t_Direction.normalized * Time.deltaTime * Speed);

        float c2 = t_Direction.sqrMagnitude;

        if (c2 < DistanceCheck * DistanceCheck)
        {
            //si on est rendu au bout
            if (++NextTargetId == m_Path.Tiles.Count)
            {
                m_EndTile.SetActive(false);
                AiActive = false;
                PlayerPathFinder.enabled = false;
                PlayerPathFinder.enabled = true;
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
}