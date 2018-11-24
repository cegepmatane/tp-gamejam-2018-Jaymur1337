using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class MapGrid : MonoBehaviour
{
    
    // Une classe pour pouvoir return null si on clique en dehors de la grille
    public class GridPoint
    {
        public int x, y;
        public override bool Equals(object obj)
        {
            if(obj == null)return false;

            GridPoint objGridPoint = (GridPoint)obj;

            if(this.x != objGridPoint.x)
            {
                return false;
            }
            if(this.y != objGridPoint.y)
            {
                return false;
            }
            return true;
        }
    }
    [HideInInspector]
    public Tile[,] Tiles;

    public bool DisplayOn = true;
    public float CellHeight = 1f;
    public float CellWidth = 1f;
    public Color GridColor = Color.yellow;

    public int GridSize = 100;

    public GameObject[] AvailableTiles;
    public int SelectedTile;

    //private Tile[] Tiles;


    private void Awake()
    {
        Tiles = new Tile[GridSize, GridSize];
        List<Tile> t_Tiles = new List<Tile>(GetComponentsInChildren<Tile>());

        // Calcul de pos de chaque tuile
        foreach (Tile t_Tile in t_Tiles)
        {
            t_Tile.GridPoint = WorldPointToGridPoint(t_Tile.transform.position);
            Tiles[t_Tile.GridPoint.x, t_Tile.GridPoint.y] = t_Tile;
        }

        //int t_MaxGridX = t_Tiles.Max(t => t.GridPoint.x);
        //int t_MaxGridY = t_Tiles.Max(t => t.GridPoint.y);

    }
    private void OnDrawGizmosSelected()
    {
        if (!DisplayOn)
            return;
        Gizmos.color = GridColor;

        for (int i = 0; i <= GridSize; i++)
        {
            // Draw Line Horizontal
            //MODIFIED
            float t_PosX = (i * CellWidth) - (GridSize * CellWidth / 2);

            float t_LineHeight = CellHeight * GridSize;
            Gizmos.DrawLine(
                        new Vector3(t_PosX, -(t_LineHeight / 2), 0),
                        new Vector3(t_PosX, t_LineHeight / 2, 0)
                        );

            // Draw Line Vertical
            //MODIFIED
            float t_PosY = (i * CellHeight) - (GridSize * CellHeight / 2);

            float t_LineWidth = CellWidth * GridSize;
            Gizmos.DrawLine(
                        new Vector3(-(t_LineWidth / 2), t_PosY, 0),
                        new Vector3(t_LineWidth / 2, t_PosY, 0)
                        );
        }
    }
    public MapGrid.GridPoint WorldPointToGridPoint(Vector2 a_Point)
    {
        // La moitier de la taille de la grille
        float t_GridHalfTotalWidth = CellWidth * GridSize / 2;
        float t_GridHalfTotalHeight = CellHeight * GridSize / 2;

        // return si on clique en dehors de la grille
        if (a_Point.x < -t_GridHalfTotalWidth || a_Point.x > t_GridHalfTotalWidth)
        {
            Debug.Log("test");
            return null;
        }
        if (a_Point.y < -t_GridHalfTotalHeight || a_Point.y > t_GridHalfTotalHeight)
        {
            Debug.Log("test");
            return null;
        }
        var t_GripPoint = new GridPoint();

        t_GripPoint.x = (int)((a_Point.x + t_GridHalfTotalWidth) / CellWidth);
        t_GripPoint.y = (int)((a_Point.y + t_GridHalfTotalHeight) / CellHeight);
        // Inverse
        t_GripPoint.y = -t_GripPoint.y + GridSize - 1;

        return t_GripPoint;
    }

    public Tile GetTile(GridPoint a_GridPoint)
    {
        return Tiles[a_GridPoint.x, a_GridPoint.y];
    }

    public Tile GetTile(int a_X, int a_Y)
    {
        return Tiles[a_X, a_Y];
    }


    public Vector3 GridPointToWorldPoint(MapGrid.GridPoint a_GridPoint)
    {
        float t_GridHalfTotalWidth = CellWidth * GridSize / 2;
        float t_GridHalfTotalHeight = CellHeight * GridSize / 2;

        Vector2 t_WorldPoint;
        t_WorldPoint.x = (a_GridPoint.x * CellWidth) + (CellWidth / 2) - t_GridHalfTotalWidth;
        t_WorldPoint.y = -((a_GridPoint.y * CellHeight) + (CellHeight / 2) - t_GridHalfTotalHeight);


        return t_WorldPoint;
    }
}
