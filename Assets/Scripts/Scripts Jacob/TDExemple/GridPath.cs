using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPath : MonoBehaviour
{
    public Color PathColor = Color.red;
    public bool displayPath = true;
    public Transform[] Path;

    private void Start()
    {
        PathsManager.instance.GridPaths.Add(this);
    }

    private void OnDrawGizmosSelected()
    {
        if (!displayPath) return;

        Gizmos.color = PathColor;
        //length -1 1 pcq ne trace pas la ligne du dernier//
        for (int i = 0; i < Path.Length - 1; i++)
        {
            Gizmos.DrawLine(Path[i].position, Path[i + 1].position);
        }
    }

}
