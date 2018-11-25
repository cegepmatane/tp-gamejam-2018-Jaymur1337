using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnnemyPathFinder : MonoBehaviour
{
    private class Node
    {
        public Tile Tile;
        public int F;
        public int G;
        public int H;
        public Node Parent;
    }
    public MapGrid grid;

    public Transform EndTransform;

    private Tile m_EndTile;

    public bool DebugMode = false;

    private Node[,] m_Nodes;

    private EnnemyCTRL EnnemyObj;


    private void Start()
    {
        grid = (MapGrid)FindObjectOfType(typeof(MapGrid));
    }

    public void FindEndPoint()
    {

        m_Nodes = null;

        EndTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(EndTransform);

        MapGrid.GridPoint t_EndGridPoint = grid.WorldPointToGridPoint(EndTransform.position);
        Debug.Log(t_EndGridPoint);

        m_EndTile = grid.GetTile(t_EndGridPoint);

        Debug.Log(t_EndGridPoint.x + " " + t_EndGridPoint.y);

        m_Nodes = new Node[grid.GridSize, grid.GridSize];
        Debug.Log(m_Nodes);

        for (int i = 0; i < grid.GridSize; i++)
        {
            for (int j = 0; j < grid.GridSize; j++)
            {
                if (grid.Tiles[i, j] == null) continue;
                m_Nodes[i, j] = new Node();
                m_Nodes[i, j].Tile = grid.Tiles[i, j];
            }
        }
        foreach (Node n in m_Nodes)
        {
            if (n == null) continue;
            n.H = Heuristique(n.Tile.GridPoint.x, n.Tile.GridPoint.y, m_EndTile.GridPoint.x, m_EndTile.GridPoint.y);
        }

    }

    public Path GetPath(Transform a_Ennemy)
    {
        MapGrid.GridPoint t_StartGridPoint = grid.WorldPointToGridPoint(a_Ennemy.position);
        Tile t_StartTile = grid.GetTile(t_StartGridPoint);


        if (t_StartTile == null)
        {
            Debug.LogError("StartTile is null");
            return null;
        }
        if (m_EndTile == null)
        {
            Debug.LogError("EndTile is null");
            return null;
        }
        if (DebugMode)
        {
            t_StartTile.GetComponent<SpriteRenderer>().color = Color.green;
            m_EndTile.GetComponent<SpriteRenderer>().color = Color.red;
        }

        //temp
        bool t_Done = false;


        //debut algo
        List<Node> t_OpenList = new List<Node>();
        List<Node> t_ClosedList = new List<Node>();

        Node t_StartNode = m_Nodes[t_StartTile.GridPoint.x, t_StartTile.GridPoint.y];
        Node t_EndNode = m_Nodes[m_EndTile.GridPoint.x, m_EndTile.GridPoint.y];

        t_OpenList.Add(t_StartNode);
        
        while (!t_Done)
        {
            if(t_OpenList.Count == 0)
            {
                t_Done = true;
                Debug.LogWarning("No path found");
                break;
            }
            int minF = t_OpenList.Min(t => t.F);
            Node current = t_OpenList.Find(t => t.F == minF);
            t_OpenList.Remove(current);
            t_ClosedList.Add(current);

            if (current.Tile.GridPoint.Equals(m_EndTile.GridPoint))
            {
                t_Done = true;
                Debug.Log("Path Found");
                break;
            }

            List<Node> t_Neighbours = GetNeighbours(current);
            foreach(var n in t_Neighbours)
            {
                if (n.Tile.BaseCost == -1)
                    continue;
                if (t_ClosedList.Contains(n))
                    continue;

                int t_NeighbourCost = n.Tile.GridPoint.x == current.Tile.GridPoint.x || n.Tile.GridPoint.y == current.Tile.GridPoint.y ? 10 : 14;

                int t_NewPathCost = current.G + t_NeighbourCost * n.Tile.BaseCost;
                if (!t_OpenList.Contains(n) || t_NewPathCost < n.G)
                {
                    //callculer cout
                    n.G = t_NewPathCost;
                    n.G = n.G + n.H;
                    n.Parent = current;
                    if (!t_OpenList.Contains(n))
                        t_OpenList.Add(n);
                }
            }
        }


        Path t_Path = new Path();
        t_Path.Tiles = new List<Tile>();
        if (t_EndNode.Parent != null)
        {
            Node current = t_EndNode;
            t_Path.Tiles.Add(current.Tile);

            while (current.Parent != null)
            {
                t_Path.Tiles.Add(current.Parent.Tile);
                current = current.Parent;
            }
            t_Path.Tiles.Reverse();
        }
        return t_Path;
    }

    private int Heuristique(int a_CurentX, int a_CurentY, int a_EndX, int a_EndY)
    {
        int dx = Mathf.Abs(a_CurentX - a_EndX);
        int dy = Mathf.Abs(a_CurentY - a_EndY);

        return dx + dy;
    }

    private List<Node> GetNeighbours(Node a_Node)
    {
        int xMin, xMax, yMin, yMax;
        xMin = a_Node.Tile.GridPoint.x - 1;
        if (xMin < 0) xMin = 0;
        xMax = a_Node.Tile.GridPoint.x + 1;
        if (xMax >= grid.GridSize) xMax = grid.GridSize - 1;
        yMin = a_Node.Tile.GridPoint.y - 1;
        if (yMin < 0) yMin = 0;
        yMax = a_Node.Tile.GridPoint.y + 1;
        if (yMax >= grid.GridSize) yMax = grid.GridSize - 1;
        List<Node> t_NeighbourNodes = new List<Node>();
        for (int i = xMin; i<= xMax;i++)
        {
            for (int j = yMin; j <= yMax; j++)
            {
                if (i == 0 && j == 0) continue;
                Node t_Node = m_Nodes[i, j];
                if(t_Node != null)
                {
                    t_NeighbourNodes.Add(t_Node);
                }
            }
        }
        return t_NeighbourNodes;
    }
}
