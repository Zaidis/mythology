using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathAI : MonoBehaviour
{

    Heap<PathNode> openSet;
    List<PathNode> closedSet = new List<PathNode>();
    public static int [] prev; // Set by PathGrid .start() all to -1. Stores path node ID values


    private void Start()
    {
        openSet = new Heap<PathNode>(MapMaker.mapThingy.worldSizex * MapMaker.mapThingy.worldSizey);
    }
    public List<PathNode> Astar(PathNode source, PathNode target)
    {
        PathNode s = source;
        s.gScore = 0;
        openSet.Add(s);


        // must be in open set before finding hscore
        //We know the cost to get to source from source is 0
        //So 0 becomes the gScore of source
        //Since we change gScore, we must also change fScore
        s.gScore = 0;
        s.hScore = PathGrid.updatehScore(s);
        s.fScore = s.hScore + s.gScore;
        PathNode current;
        while (openSet.Count != 0)
        {
            // Find lowest fScore node
            // Set current equal to that
            current = openSet.RemoveFirst();
            /*
            for (int j = 0; j < openSet.Count; j++)
            {
                if (current.fScore > openSet[j].fScore)
                {
                    current = openSet[j];
                }
            }
            */
            closedSet.Add(current);

            // Upon finding the target...
            if (current.ID == target.ID)
            {
                List<PathNode> path = new List<PathNode>();
                while (current.parent !=null)
                {
                    path.Add(current);
                    current = current.parent;
                }
                path.Reverse();
                return path;
            }

            // If target not found continue...

           foreach (PathNode neighbor in current.FindNeighbors()){
                int edgeWeight = 1;
                double tentativeGScore = current.gScore + edgeWeight;
                if (tentativeGScore < neighbor.gScore)
                {
                    neighbor.gScore = tentativeGScore;
                    neighbor.fScore = neighbor.gScore + neighbor.hScore;
                    neighbor.parent = current; // store the ID of previous node

                    //If we have found a better path
                    if (closedSet.Contains(neighbor))
                    {
                        closedSet.Remove(neighbor);
                        openSet.Add(neighbor);
                    } else if (openSet.Contains(neighbor) == false)
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }
        return null;
    }
    /*
    Vector3[] SimplifyPath(List<PathNode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        //only records specific points where the creature changes direction and heads towards that direction
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].nodePosition.x - path[i].nodePosition.x, path[i - 1].nodePosition.y - path[i].nodePosition.y);
            if (directionNew != directionOld)
            {
                waypoints.Add((Vector2)path[i - 1].nodePosition);
            }
            directionOld = directionNew;
        }
        //returns waypoints as an array of vector3s
        return waypoints.ToArray();
    }
    int GetDistance(PathNode nodeA, PathNode nodeB)
    {


            int dstX = (int)Mathf.Pow(nodeA.nodePosition.x - nodeB.nodePosition.x, 2);
            int dstY = (int)Mathf.Pow(nodeA.nodePosition.y - nodeB.nodePosition.y, 2);
            //This is a little more optimized since we aren't going to be dealing as large of values since the map is relatively small
            float heurstic = (float)Math.Sqrt(dstX + dstY);
            heurstic *= (1 + 0.001f);
            return (int)heurstic;
            

    }
    */
    PathNode FindNodeID(int ID)
    {
        // Query the master graph for node with matching ID.
        for (int x = 0; x < PathGrid.Xlen; x++)
        {

            for (int y = 0; y < PathGrid.Xlen; y++)
            {
                if (PathGrid.mstrGrid[x, y].ID == ID)
                {
                    return PathGrid.mstrGrid[x, y];
                }
            }
        }
        return null;
    }
}




