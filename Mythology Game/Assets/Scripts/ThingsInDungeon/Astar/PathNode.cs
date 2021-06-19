using Nodies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : IHeapItem<PathNode>
{
    // !World position to graph position translate function
    // Create static adj matrix 
    public Vector2Int nodePosition;
    //public float XLength;
    public double fScore = double.MaxValue;
    public double gScore = double.MaxValue;
    public double hScore = double.MaxValue;
    public int ID;
    public bool walkable = true;
    int heapIndex;
    public PathNode parent;
    
    // create constructor with right click
    public PathNode [] FindNeighbors()
    {
        // return [ N , E , S , W ]
        // Stores a vector2 for each neighbor. Vector 2 corresponds to row/col in mstrGrid[,]. Reverse Astar solve from player to given enemy
        // If no node exists, the nodePosition vector 2 will be -1 -1 
        // Creation of an empty path node means it returns a blank node with -1 values
        PathNode[] neighbors = new PathNode[4];
        PathNode[,] mstrGrid = PathGrid.mstrGrid;

        try
        {
            if (mstrGrid[nodePosition.x + 1, nodePosition.y].walkable)
            {
                neighbors[1] = mstrGrid[nodePosition.x + 1, nodePosition.y];
            }
            else
            {
                Debug.Log("E node is not walkable!");
                neighbors[1] = new PathNode();
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Eastern nodes!");
            neighbors[1] = new PathNode();
            neighbors[1].ID = -1;
        }
        try
        {
            if (mstrGrid[nodePosition.x - 1, nodePosition.y].walkable)
            {
                neighbors[3] = mstrGrid[nodePosition.x + 1, nodePosition.y];
            }
            else
            {
                Debug.Log("W node is not walkable!");
                neighbors[3] = new PathNode();
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Western nodes!");
            neighbors[3] = new PathNode();
        }
        try
        {
            if (mstrGrid[nodePosition.x, nodePosition.y + 1].walkable)
            {
                neighbors[0] = mstrGrid[nodePosition.x + 1, nodePosition.y];
            }
            else
            {
                Debug.Log("N is not walkable!");
                neighbors[0] = new PathNode();
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Northern nodes!");
            neighbors[0] = new PathNode();
        }
        try
        {
            if (mstrGrid[nodePosition.x, nodePosition.y - 1].walkable)
            {
                neighbors[2] = mstrGrid[nodePosition.x + 1, nodePosition.y];
            }
            else
            {
                Debug.Log("S node is not walkable!");
                neighbors[2] = new PathNode();
            }
        }
        catch (Exception e)
        {
            Debug.Log("No more Southern nodes!");
            neighbors[2] = new PathNode();
        }
        return neighbors;
    }
    public PathNode()
    {
        nodePosition = new Vector2Int(0, 0);
        this.ID = -1; // a blank node has an ID of -1
    }
    public PathNode(Vector2Int nodePosition, float sideLength,bool walkable, int ID)
    {
        this.nodePosition = nodePosition;
        this.ID = ID;
        //XLength = sideLength;
        this.walkable = walkable;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(PathNode nodeToCompare)
    {
        int compare = fScore.CompareTo(nodeToCompare.fScore);
        if (compare == 0)
        {
            compare = hScore.CompareTo(nodeToCompare.hScore);
        }
        return -compare;
    }
}


