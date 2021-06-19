using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Node = Nodies.Node;
using UnityEditor;
using System.IO;
public class MapMaker : MonoBehaviour
{

    public Node Source = new Node();
    public Node Target = new Node();
    [SerializeField]
    GameObject player;
    [SerializeField]
    float tileSize = 16.5f;
    public int worldSizex = 30;
    public int worldSizey = 30;
    Node[,] grid;
    int[] parentArray = new int[50];
    Stack<Node> path = new Stack<Node>();
    [SerializeField]
    int newSquaresTravel = 30;
    [SerializeField]
    bool getPieces = false;
    //Zero rooms will have clear centers
    [SerializeField]
    List<GameObject> upRooms;
    [SerializeField]
    List<GameObject> downRooms;
    [SerializeField]
    List<GameObject> leftRooms;
    [SerializeField]
    List<GameObject> rightRooms;
    [SerializeField]
    List<GameObject> leftRightRooms;
    [SerializeField]
    List<GameObject> leftRightDownRooms;
    [SerializeField]
    List<GameObject> leftRightUpRooms;
    [SerializeField]
    List<GameObject> upDownRooms;
    [SerializeField]
    List<GameObject> upLeftRooms;
    [SerializeField]
    List<GameObject> upRightRooms;
    [SerializeField]
    List<GameObject> downLeftRooms;
    [SerializeField]
    List<GameObject> downRightRooms;
    [SerializeField]
    List<GameObject> upLeftDown;
    [SerializeField]
    List<GameObject> upRightDown;
    [SerializeField]
    List<GameObject> intersection;
    [SerializeField]
    List<GameObject> lastRooms;
    [SerializeField]
    List<GameObject> bossRooms;
    public static MapMaker mapThingy;

    List<Node> unFilterdNodalList = new List<Node>();
    List<Node> nodalList = new List<Node>();
    List<Object> possiblyRoom = new List<Object>();
    bool bossRoom = false;
    void ClearAllRooms()
    {
    upRooms.Clear();
    downRooms.Clear();
    leftRooms.Clear();
    rightRooms.Clear();
    leftRightRooms.Clear();
    leftRightDownRooms.Clear();
    leftRightUpRooms.Clear();
    upDownRooms.Clear();
    upLeftRooms.Clear();
    upRightRooms.Clear();
    downLeftRooms.Clear();
    downRightRooms.Clear();
    upLeftDown.Clear();
    upRightDown.Clear();
    intersection.Clear();
}
    void FillOutRooms()
    {
        Object[] fileEntries = Resources.LoadAll("Rooms", typeof(GameObject));
        /*
        foreach (string fileName in fileEntries)
        {
            int assetPathIndex = fileName.IndexOf("Assets");
            string localPath = fileName.Substring(assetPathIndex);
            Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(Object));
            if (t != null)
                possiblyRoom.Add(t);
        }
        */
       // for (int k = 0; k < possiblyRoom.Count; k++)
          //  print(possiblyRoom[k]);

        ClearAllRooms();
        foreach (Object file in fileEntries)
        {
            if (file.GetType() == typeof(GameObject))
            {
                // GameObject t = (GameObject)Instantiate(piece, new Vector2(-50,-50), Quaternion.identity);
                GameObject t = (GameObject)(file);
                if (t.CompareTag("Up") || t.CompareTag("EndUp"))
                {
                    if(t.CompareTag("Up") && upRooms.Count < 1)
                        upRooms.Add(t);
                    else if (t.CompareTag("Up") && upRooms.Count >= 1)
                        upRooms.Insert(upRooms.Count - 1, t);
                    if(t.CompareTag("EndUp"))
                    {
                        upRooms.Add(t);
                    }
                }
                if (t.CompareTag("Down") || t.CompareTag("EndDown"))
                {
                    if (t.CompareTag("Down") && downRooms.Count < 1)
                        downRooms.Add(t);
                    else if (t.CompareTag("Down") && downRooms.Count >= 1)
                        downRooms.Insert(downRooms.Count - 1, t);
                    if (t.CompareTag("EndDown"))
                    {
                        downRooms.Add(t);
                    }
                }
                if (t.CompareTag("DownUp") || t.CompareTag("EndDownUp"))
                {
                    if (t.CompareTag("DownUp") && upDownRooms.Count < 1)
                        upDownRooms.Add(t);
                    else if (t.CompareTag("DownUp") && upDownRooms.Count >= 1)
                        upDownRooms.Insert(upDownRooms.Count - 1, t);
                    if (t.CompareTag("EndDownUp"))
                    {
                        upDownRooms.Add(t);
                    }
                }
                if (t.CompareTag("Left")|| t.CompareTag("EndLeft"))
                {
                    if (t.CompareTag("Left") && leftRooms.Count < 1)
                        leftRooms.Add(t);
                    else if (t.CompareTag("Left") && leftRooms.Count >= 1)
                        leftRooms.Insert(leftRooms.Count - 1, t);
                    if (t.CompareTag("EndLeft"))
                    {
                        leftRooms.Add(t);
                    }
                }
                if (t.CompareTag("LeftUp") || t.CompareTag("EndLeftUp"))
                {
                    if (t.CompareTag("LeftUp") && upLeftRooms.Count < 1)
                    {

                        upLeftRooms.Add(t);
                    }
                    else if (t.CompareTag("LeftUp") && upLeftRooms.Count >= 1)
                    {
                        upLeftRooms.Insert(upLeftRooms.Count - 1, t);
                    }
                    if (t.CompareTag("EndLeftUp"))
                    {
                        upLeftRooms.Add(t);
                    }
                }
                if (t.CompareTag("LeftDown")|| t.CompareTag("EndLeftDown"))
                {
                    if (t.CompareTag("LeftDown") && downLeftRooms.Count < 1)
                        downLeftRooms.Add(t);
                    else if (t.CompareTag("LeftDown") && downLeftRooms.Count >= 1)
                        downLeftRooms.Insert(downLeftRooms.Count - 1, t);
                    if (t.CompareTag("EndLeftDown"))
                    {
                        downLeftRooms.Add(t);
                    }
                }
                if (t.CompareTag("LeftDownUp")|| t.CompareTag("EndLeftDownUp"))
                {
                    if (t.CompareTag("LeftDownUp") && upLeftDown.Count < 1)
                        upLeftDown.Add(t);
                    else if (t.CompareTag("LeftDownUp") && upLeftDown.Count >= 1)
                        upLeftDown.Insert(upLeftDown.Count - 1, t);
                    if (t.CompareTag("EndLeftDownUp"))
                    {
                        upLeftDown.Add(t);
                    }
                }
                if (t.CompareTag("Right")|| t.CompareTag("EndRight"))
                {
                    if (t.CompareTag("Right") && rightRooms.Count < 1)
                        rightRooms.Add(t);
                    else if (t.CompareTag("Right") && rightRooms.Count >= 1)
                        rightRooms.Insert(rightRooms.Count - 1, t);
                    if (t.CompareTag("EndRight"))
                    {
                        rightRooms.Add(t);
                    }
                }
                if (t.CompareTag("RightUp") || t.CompareTag("EndRightUp"))
                {
                    if (t.CompareTag("RightUp") && upRightRooms.Count < 1)
                        upRightRooms.Add(t);
                    else if (t.CompareTag("RightUp") && upRightRooms.Count >= 1)
                        upRightRooms.Insert(upRightRooms.Count - 1, t);
                    if (t.CompareTag("EndRightUp"))
                    {
                        upRightRooms.Add(t);
                    }
                }
                if (t.CompareTag("RightDown") || t.CompareTag("EndRightDown"))
                {
                    if (t.CompareTag("RightDown") && downRightRooms.Count < 1)
                        downRightRooms.Add(t);
                    else if (t.CompareTag("RightDown") && downRightRooms.Count >= 1)
                        downRightRooms.Insert(downRightRooms.Count - 1, t);
                    if (t.CompareTag("EndRightDown"))
                    {
                        downRightRooms.Add(t);
                    }
                }
                if (t.CompareTag("RightDownUp")|| t.CompareTag("EndRightDownUp"))
                {
                    if (t.CompareTag("RightDownUp") && upRightDown.Count < 1)
                        upRightDown.Add(t);
                    else if (t.CompareTag("RightDownUp") && upRightDown.Count >= 1)
                        upRightDown.Insert(upRightDown.Count - 1, t);
                    if (t.CompareTag("EndRightDownUp"))
                    {
                        upRightDown.Add(t);
                    }
                }
                if (t.CompareTag("RightLeft") || t.CompareTag("EndRightLeft"))
                {
                    if (t.CompareTag("RightLeft") && leftRightRooms.Count < 1)
                        leftRightRooms.Add(t);
                    else if (t.CompareTag("RightLeft") && leftRightRooms.Count >= 1)
                        leftRightRooms.Insert(leftRightRooms.Count - 1, t);
                    if (t.CompareTag("EndRightLeft"))
                    {
                        leftRightRooms.Add(t);
                    }
                }
                if (t.CompareTag("RightLeftUp") || t.CompareTag("EndRightLeftUp"))
                {
                    if (t.CompareTag("RightLeftUp") && leftRightUpRooms.Count < 1)
                        leftRightUpRooms.Add(t);
                    else if (t.CompareTag("RightLeftUp") && leftRightUpRooms.Count >= 1)
                        leftRightUpRooms.Insert(leftRightUpRooms.Count - 1, t);
                    if (t.CompareTag("EndRightLeftUp"))
                    {
                        leftRightUpRooms.Add(t);
                    }
                }
                if (t.CompareTag("RightLeftDown") || t.CompareTag("EndRightLeftDown"))
                {
                    if (t.CompareTag("RightLeftDown") && leftRightDownRooms.Count < 1)
                        leftRightDownRooms.Add(t);
                    else if (t.CompareTag("RightLeftDown") && leftRightDownRooms.Count >= 1)
                        leftRightDownRooms.Insert(leftRightDownRooms.Count - 1, t);
                    if (t.CompareTag("EndRightLeftDown"))
                    {
                        leftRightDownRooms.Add(t);
                    }
                }
                if (t.CompareTag("RightLeftDownUp") || t.CompareTag("EndRightLeftDownUp"))
                {
                    if (t.CompareTag("RightLeftDownUp") && intersection.Count < 1)
                        intersection.Add(t);
                    else if (t.CompareTag("RightLeftDownUp") && intersection.Count >= 1)
                        intersection.Insert(intersection.Count - 1, t);
                    if (t.CompareTag("EndRightLeftDownUp"))
                    {
                        intersection.Add(t);
                    }
                }
               
            }
            
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.instance != null && GameManager.instance.getLevel() % 3 == 0)
        {
            int chance = Random.Range(1, 20);
            if (chance >= 5)
            {
                bossRoom = true;
            }
        }
        if (bossRoom)
        {
            mapThingy = this;
            grid = new Node[worldSizex, worldSizey];
            int id = 1;
            for (int i = 0; i < worldSizex; i++)
            {
                for (int j = 0; j < worldSizey; j++)
                {
                    grid[i, j] = new Node();
                    grid[i, j].id = id;
                    id++;
                }
            }
            int room = Random.Range(0, bossRooms.Count - 1);
            Node bossNode = grid[worldSizex / 2, worldSizey / 2];
            bossNode.AddNodalPosition(worldSizex / 2, worldSizey / 2, tileSize);
            GameObject tmp = Instantiate(bossRooms[room], new Vector2(bossNode.positionX,bossNode.positionY), Quaternion.identity);
            nodalList.Add(bossNode);
        }
        else
        {
            if (getPieces)
            {
                FillOutRooms();
            }
            mapThingy = this;
            grid = new Node[worldSizex, worldSizey];
            int id = 1;
            for (int i = 0; i < worldSizex; i++)
            {
                for (int j = 0; j < worldSizey; j++)
                {
                    grid[i, j] = new Node();
                    grid[i, j].id = id;
                    id++;
                }
            }
            Source.id = 0;
            grid[worldSizex / 2, worldSizey / 2] = Source;
            grid[worldSizex / 2, worldSizey / 2].AddNodalPosition(worldSizex, worldSizey, tileSize);
            path.Push(Source);
            startWalk();
        }
    }
    public float getWorldSize()
    {
        return tileSize * worldSizey;
    }
    void startWalk()
    {
        Vector2Int currentPos = new Vector2Int(worldSizex / 2, worldSizey / 2);
        while (currentPos.x < worldSizex && currentPos.y < worldSizey && currentPos.x > 0 && currentPos.y > 0 && newSquaresTravel > 0)
        {
            int dir = Random.Range(0, 3);
            Vector2 dirToGo = new Vector2(0, 0);
            switch (dir)
            {
                case 0:
                    dirToGo = Vector2Int.right;
                    break;
                case 1:
                    dirToGo = Vector2Int.left;
                    break;
                case 2:
                    dirToGo = Vector2Int.down;
                    break;
                case 3:
                    dirToGo = Vector2Int.up;
                    break;

                default:
                    break;
            }
            if (currentPos.x == 49 && dirToGo.x == 1)
            {
                dirToGo = Vector2Int.left;

            }
            if (currentPos.y == 49 && dirToGo.y == 1)
            {
                dirToGo = Vector2Int.down;
            }
            if (currentPos.x == 1 && dirToGo.x == -1)
            {
                dirToGo = Vector2Int.down;

            }
            if (currentPos.y == 1 && dirToGo.y == -1)
            {
                dirToGo = Vector2Int.up;
            }
            currentPos.x += (int)dirToGo.x;
            currentPos.y += (int)dirToGo.y;
            grid[currentPos.x, currentPos.y].AddNodalPosition(currentPos.x, currentPos.y, tileSize);
            path.Peek().AddParent(grid[currentPos.x, currentPos.y]);
            grid[currentPos.x, currentPos.y].AddParent(path.Peek());
            path.Push(grid[currentPos.x, currentPos.y]);
            newSquaresTravel--;
        }
        /*
        for(int i = 0; i < path.Count; i++)
        {
            Node tmp = path.Pop();
            print("( " + tmp.nodalPositionX +","+ tmp.nodalPositionY + " )");
        }*/
        CreateMap(path);
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (worldSizey/2 *tileSize), transform.position.y + (worldSizey / 2 * tileSize)), new Vector2(worldSizex* tileSize,worldSizey * tileSize));
        /*
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawCube(new Vector3(n.positionX - 3, n.positionY, 0), Vector3.one * (tileSize - .1f));
            }
        }
        */
    }
    public List<Node> getNodeList()
    {
        return nodalList;
    }
    void filterNodes()
    {
        nodalList.Add(unFilterdNodalList[0]);
        bool exists = false;
        for (int i = 1; i < unFilterdNodalList.Count; i++)
        {
            for(int j = 0; j < nodalList.Count; j++)
            {
                if (unFilterdNodalList[i].positionX == nodalList[j].positionX && unFilterdNodalList[i].positionY == nodalList[j].positionY)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                nodalList.Add(unFilterdNodalList[i]);
                
            }
            if (exists)
                exists = !exists;
        }
    }
    void CreateMap(Stack<Node> path)
    {
        Target = path.Peek();
        while (path.Count > 0)
        {
            
            Node tmp = path.Pop();
            unFilterdNodalList.Add(tmp);
            Node[] parentArray = new Node[tmp.RetrieveParent().Length];
            bool up, down, left, right;
            float dirX = 0;
            float dirY = 0;
            up = down = left = right = false;
            parentArray = tmp.RetrieveParent();

            
            for (int i = 0; i < tmp.GetParentPos(); i++)
            {
                dirX = parentArray[i].nodalPositionX - tmp.nodalPositionX;
                dirY = parentArray[i].nodalPositionY - tmp.nodalPositionY;
                if (dirX > 0 && dirY == 0)
                {
                    right = true;
                }
                if (dirX < 0 && dirY == 0)
                {
                    left = true;
                }
                if (dirX == 0 && dirY > 0)
                {
                    up = true;
                }
                if (dirX == 0 && dirY < 0)
                {
                    down = true;
                }
            }
            if (right && left && up && down)
            {

                //intersection
                if (path.Count >1)
                Instantiate(intersection[Random.Range(0, intersection.Count -1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(intersection[intersection.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && left && up && !down)
            {
                //right left up
                if(path.Count > 1)
                
                Instantiate(leftRightUpRooms[Random.Range(0, leftRightUpRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(leftRightUpRooms[leftRightUpRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }


            }
            else if (!right && left && !up && down)
            {
                //right left up
                if(path.Count > 1)
                Instantiate(downLeftRooms[Random.Range(0, leftRightUpRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(downLeftRooms[leftRightUpRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && left && !up && down)
            {
                //right left down
                if(path.Count > 1)
                Instantiate(leftRightDownRooms[Random.Range(0, leftRightDownRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(leftRightDownRooms[leftRightDownRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && !left && up && down)
            {
                //right up down
                if(path.Count > 1)
                Instantiate(upRightDown[Random.Range(0, upRightDown.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(upRightDown[upRightDown.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }
                
            }
            else if (!right && left && up && down)
            {
                //left up down
                if(path.Count > 1)
                Instantiate(upLeftDown[Random.Range(0, upLeftDown.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(upLeftDown[upLeftDown.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && left && !up && !down)
            {
                //right left
                if(path.Count > 1)
                Instantiate(leftRightRooms[Random.Range(0, leftRightRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(leftRightRooms[leftRightRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && !left && !up && down)
            {
                //right down
                if(path.Count > 1)
                Instantiate(downRightRooms[Random.Range(0, downRightRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(downRightRooms[downRightRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && !left && up && !down)
            {
                //right up
                if(path.Count > 1)
                Instantiate(upRightRooms[Random.Range(0, upRightRooms.Count -1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(upRightRooms[upRightRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (!right && left && up && !down)
            {
                //up left
                if(path.Count > 1)
                Instantiate(upLeftRooms[Random.Range(0, upLeftRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(upLeftRooms[upLeftRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (!right && !left && up && down)
            {
                //up down
                if(path.Count > 1)
                Instantiate(upDownRooms[Random.Range(0, upDownRooms.Count- 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(upDownRooms[upDownRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (right && !left && !up && !down)
            {
                //right
                if(path.Count > 1)
                Instantiate(rightRooms[Random.Range(0, rightRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else 
                {
                    Instantiate(rightRooms[rightRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (!right && !left && !up && down)
            {
                //down
                if(path.Count >1)
                Instantiate(downRooms[Random.Range(0, downRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(downRooms[downRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (!right && left && !up && !down)
            {
                //left
                if(path.Count > 1)
                Instantiate(leftRooms[Random.Range(0, leftRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(leftRooms[leftRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
            else if (!right && !left && up && !down)
            {
                //up
                if(path.Count > 1)
                Instantiate(upRooms[Random.Range(0, upRooms.Count - 1)], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                else
                {
                    Instantiate(upRooms[upRooms.Count - 1], new Vector2(tmp.positionX, tmp.positionY), Quaternion.identity);
                }

            }
        }
        filterNodes();
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(MapMaker))]
public class CustomInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Zero rooms should have clear");
        EditorGUILayout.LabelField("centers for Exits and Entrances");
        EditorGUILayout.LabelField("For the Dungeon Layout.");
    }
}
#endif
