using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : MonoBehaviour
{
    public static PathsManager instance;

    public List<GridPath> GridPaths;

    private void Awake()
    {
        instance = this;

        GridPaths = new List<GridPath>();
    }
}
