using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
	public Transform target;
	[SerializeField]
	float speed = 5;
	Vector3[] path;
	int targetIndex;

	private void Start()
	{
		target = null;
	}
	private void Update()
	{
		if (target == null)
		{
			StopAllCoroutines();
		}
		if(target != null)
		{
			try
			{
				PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
			}
			catch(Exception e)
            {
				StopAllCoroutines();
            }
		}
	}
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			try
			{
				StopCoroutine("FollowPath");
			}
			catch (Exception e)
			{
				StopAllCoroutines();
			}			
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}/*
	public void OnDrawGizmos()
	{
		if(path!= null)
		{
			for(int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);
				
				if(i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}*/
}
