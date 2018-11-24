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

    public GameObject m_EndTile;

    [Header("Sounds")]
    public AudioClip DieSond;

    public CharPathFinder niga;


    // Use this for initialization
    void Start()
    {
        Instantiate(m_EndTile);
        //transform.position = CurrentPath.Path[0].position;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            NextTargetId = 1;
            m_Path = null;
            CurrentPath = null;
            m_EndTile = GameObject.FindGameObjectWithTag("EndTile");

            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;

            m_EndTile.transform.position = pz;

            niga.FindEndPoint();

            m_Path = GameObject.FindObjectOfType<CharPathFinder>().GetPath(this.transform);

            if (m_Path.Tiles.Count < 2)
            {
                Debug.LogError("PATH INVALID - < 2 elements");
                Destroy(gameObject);
            }

            AiActive = true;

        }
        if (!AiActive)
            return;
        Vector2 t_Direction = m_Path.Tiles[NextTargetId].transform.position - transform.position;

        if (t_Direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(t_Direction.y, t_Direction.x) * Mathf.Rad2Deg;
            GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        transform.Translate(t_Direction.normalized * Time.deltaTime * Speed);

        float c2 = t_Direction.sqrMagnitude;

        //transform.Rotate(t_Direction.normalized);

        //quaternion. look rotation

        if (c2 < DistanceCheck * DistanceCheck)
        {
            //si on est rendu au bout
            if (++NextTargetId == m_Path.Tiles.Count)
            {
                AiActive = false;
                niga.enabled = false;
                niga.enabled = true;
                //StartCoroutine(Die());
            }
        }







    }



    private void OnDrawGizmosSelected()
    {
        //lenght - 1 car on trace aps de ling a partire du renier element

        // if (!DisplayPath) return;

        Gizmos.color = Color.green;


        if (m_Path == null || m_Path.Tiles == null) return;

        for (int i = 0; i < m_Path.Tiles.Count - 1; i++)
        {
            Gizmos.DrawLine(m_Path.Tiles[i].transform.position, m_Path.Tiles[i + 1].transform.position);
        }
    }


}