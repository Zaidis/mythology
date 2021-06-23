using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathGrid : MonoBehaviour
{

    // Center of room X coord, Y coord
    public Vector2Int neighbor_N;
    public Vector2Int neighbor_E;
    public Vector2Int neighbor_S;
    public Vector2Int neighbor_W;

    public float nodeRadius = 0.1f;
    float nodeDiameter;
    [SerializeField]
    LayerMask obstacle;
    public static int Xlen = 250; // being the rightmost X coordinate
    public static int Ylen = 250;
    public static int IDcount = 0;
    public static GameObject player;
    public PathNode [] current_neighbors;
    public float nodeXLength;

    static float ws;
    public int current_neighbors_len;
    public int Xidx;
    public int Yidx;

    public PathNode node;
    public static PathNode[,] mstrGrid;
    [SerializeField]
    bool showGizmos = false;
    
    //World bounds, node side length, 
    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = 2 * nodeRadius;
        Xlen = (int)MapMaker.mapThingy.getWorldSizeX();
        Ylen = (int)MapMaker.mapThingy.getWorldSizeY();
        print(Xlen);
        // each node takes up 5x5 space so div the world size by 10 to create appropriate # of path node entries in the array
        player = GameObject.Find("Player");
        ws = MapMaker.mapThingy.getWorldSizeX(); // world size is length of one side
        nodeXLength = (ws / Xlen); 
        mstrGrid = new PathNode[Xlen, Ylen];
        

        for (int x = 0; x < Xlen ; x++)
        {

            for (int y = 0; y < Ylen ; y++)
            {
                Vector3 worldPoint = gameObject.transform.position + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint,nodeRadius,obstacle));
                mstrGrid[x, y] = new PathNode(new Vector2Int(x,y), nodeXLength,walkable, IDcount );
                IDcount++;
            }
        }

                
        /*
        // on start get unwalkable. This gets all unwalkable game objects and translates their raw coords to a node location.
        GameObject[] roadblocks = GameObject.FindGameObjectsWithTag("unwalkable");
        Vector3 position = transform.position;
        for (int i = 0; i < roadblocks.Length; i++)
        {
            mstrGrid[Translate(roadblocks[i].transform.position).x, Translate(roadblocks[i].transform.position).y].walkable = false;
        }
        
        // Sets up previous array at time of grid creation as to get appropriate sizing
        PathAI.prev = new int [IDcount];
        for (int i = 0; i < IDcount; i++)
        {
            PathAI.prev[i] = -1;
        }
        */
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (mstrGrid != null && UnityEditor.EditorApplication.isPlaying && showGizmos)
        {
            
            foreach (PathNode n in mstrGrid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube((Vector2)n.nodePosition, Vector2.one * (nodeDiameter - .1f));
            }
            
        }
    }
#endif
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2Int playerlocation = Translate(player.transform.position);
        Xidx = playerlocation.x;
        Yidx = playerlocation.y;
        current_neighbors = mstrGrid[Xidx, Yidx].FindNeighbors();
        current_neighbors_len = current_neighbors.Length ;
    }
    
    public static double updatehScore(PathNode n)
    {
        // distance function to find the distance from the player location to this node
        Vector2Int targetNode = PathGrid.Translate(player.transform.position);
        // the grid index of this node is stored in nodeposition. simply count the h + V distance
        int xdist = Mathf.Abs(n.nodePosition.x - targetNode.x);
        int ydist = Mathf.Abs(n.nodePosition.y - targetNode.y);
        return (double)Mathf.Sqrt((float)Math.Pow(xdist, 2) + (float)Math.Pow(ydist, 2));
    }
    
    public static Vector2Int Translate(Vector3 position)
    {
        // Take in a vector3 position in the game space, translate to path grid index in form of a vector2
        // Numbers between 0 and 1 where n * world size = coordinate
        float nXposition = position.x / ws;
        float nYposition = position.y / ws;
        // Node diameter is accounted for by rounding
        int xg = Mathf.RoundToInt(nXposition * Xlen);
        int yg = Mathf.RoundToInt(nYposition * Xlen);
        return new Vector2Int(xg, yg);
    }
   
}
        