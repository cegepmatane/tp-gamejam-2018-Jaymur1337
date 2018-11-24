using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGrid))]

public class MapGridEditor : Editor
{
    private MapGrid grid;

    private void OnEnable()
    {
        grid = (MapGrid)target;
    }

    private void OnSceneGUI()
    {
        Event e = Event.current;

        // L'ID du control actuel
        int controlId = GUIUtility.GetControlID(FocusType.Passive);

        if (e.type == EventType.MouseDown && e.control)
        {
            // Empeche de selectionner un autre objet en cliquant dans la scene
            GUIUtility.hotControl = controlId;

            // Position cliquée dans le world
            Vector2 t_ClickPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
            MapGrid.GridPoint t_ClickedGridPoint = grid.WorldPointToGridPoint(t_ClickPos);

            if (t_ClickedGridPoint == null)
            {
                Debug.Log("Clicked outside the grid");
                //Stop l'exécution
                return;
            }

            if (grid.SelectedTile < 0 || grid.SelectedTile >= grid.AvailableTiles.Length)
            {
                Debug.Log("Selected tile invalid");
                //Stop l'exécution
                return;
            }
            Vector3 t_TilePos = grid.GridPointToWorldPoint(t_ClickedGridPoint);
            GameObject t_TilePrefab = grid.AvailableTiles[grid.SelectedTile];
            GameObject t_NewTile = Instantiate(grid.AvailableTiles[grid.SelectedTile], t_TilePos, Quaternion.identity);
            t_NewTile.transform.parent = grid.transform;
            //t_NewTile.name = t_TilePrefab.name + "("+ t_ClickedGridPoint.x + ","+ t_ClickedGridPoint.y +")";
            t_NewTile.name = string.Format("{0}({1}, {2})", t_TilePrefab.name, t_ClickedGridPoint.x, t_ClickedGridPoint.y);
            t_NewTile.transform.localScale = new Vector3(grid.CellWidth, grid.CellHeight, 1);
            //Debug.Log("Control + Clicked GridPoint (" + t_ClickedGridPoint.x + ", " + t_ClickedGridPoint.y + ")");
        }
    }

   
}
