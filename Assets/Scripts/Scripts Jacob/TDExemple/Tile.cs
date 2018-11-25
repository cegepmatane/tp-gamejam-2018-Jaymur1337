using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int BaseCost = 1;
    [HideInInspector]
    public MapGrid.GridPoint GridPoint;
    public bool Active = true;

}
