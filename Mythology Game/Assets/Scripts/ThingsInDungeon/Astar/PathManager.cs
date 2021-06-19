using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
public class PathManager : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();

    static PathManager instance;
    PathAI pathfinding;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<PathAI>();
    }

    void Update()
    {
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }

    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate {
            instance.pathfinding.Astar(PathGrid.mstrGrid[(int)request.pathStart.x, (int)request.pathStart.y], PathGrid.mstrGrid[(int)request.pathEnd.x,(int) request.pathEnd.y]);
        };
        threadStart.Invoke();
    }

    public void FinishedProcessingPath(PathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }



}

public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }

}

public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }
}
