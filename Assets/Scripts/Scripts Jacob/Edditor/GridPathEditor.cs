using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPath))]


public class GridPathEditor : Editor
{
    private GridPath grid;

    private void OnEnable()
    {
        grid = (GridPath)target;
    }
}
