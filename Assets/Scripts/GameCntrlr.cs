﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



public class GameCntrlr : MonoBehaviour
{

	private CubePos nowCube = new CubePos(0, 1, 0);
	public float cubeChangePlaceSpeed = 0.5f;
	public Transform cubeToPlace;
	public GameObject cubeToCreate, allCubes;
	private Rigidbody allCubesRb;

	private List<Vector3> allCubesPositions = new List<Vector3> {
		new Vector3(0, 0, 0),
		new Vector3(1, 0, 0),
		new Vector3(-1, 0, 0),
		new Vector3(0, 1, 0),
		new Vector3(0, 0, 1),
		new Vector3(0, 0, -1),
		new Vector3(1, 0, 1),
		new Vector3(-1, 0, -1),
		new Vector3(-1, 0, 1),
		new Vector3(1, 0, -1),
	};

	private void Start()
	{
		allCubesRb = allCubes.GetComponent<Rigidbody>();
		StartCoroutine(ShowCubePlace());
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
		{
#if !UNITY_EDITOR
			if (Input.GETTOUCH(0).phase != TouchPhase.Began) return;
#endif
			GameObject newCube = Instantiate(cubeToCreate, cubeToPlace.position, Quaternion.identity) as GameObject;
			newCube.transform.SetParent(allCubes.transform);
			nowCube.setVector(cubeToPlace.position);
			allCubesPositions.Add(nowCube.getVector());
			allCubesRb.isKinematic = true;
			allCubesRb.isKinematic = false;
			SpawnPosition();
		}
	}

	IEnumerator ShowCubePlace()
	{
		while (true)
		{
			SpawnPosition();
			yield return new WaitForSeconds(cubeChangePlaceSpeed);
		}
	}

	private void SpawnPosition()
	{
		List<Vector3> positions = new List<Vector3>();
		if (IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)) && nowCube.x + 1 != cubeToPlace.position.x)
			positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
		if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)) && nowCube.x - 1 != cubeToPlace.position.x)
			positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));
		if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)) && nowCube.y + 1 != cubeToPlace.position.y)
			positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
		if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)) && nowCube.y - 1 != cubeToPlace.position.y)
			positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));
		if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)) && nowCube.z + 1 != cubeToPlace.position.z)
			positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
		if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)) && nowCube.z - 1 != cubeToPlace.position.z)
			positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));
		cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
	}

	private bool IsPositionEmpty(Vector3 targetPos)
	{
		if (targetPos.y == 0)
			return false;
		foreach (Vector3 pos in allCubesPositions)
			if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
				return false;
		return true;
	}
}

struct CubePos
{
	public int x, y, z;

	public CubePos(int x, int y, int z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3 getVector()
	{
		return new Vector3(x, y, z);
	}

	public void setVector(Vector3 pos)
	{
		x = Convert.ToInt32(pos.x);
		y = Convert.ToInt32(pos.y);
		z = Convert.ToInt32(pos.z);
	}
}

































//Bug Version
/*
public class GameCntrlr : MonoBehaviour {

private CubePos nowCube = new CubePos(0, 1, 0);
public float cubeChangeSpeed = 0.5f;
public Transform cubeToPlace;

private List<Vector3> allCubePositions = new List<Vector3>(){
new Vector(0,0,0);
new Vector(1,0,0);
new Vector(-1,0,0);
new Vector(0,1,0);
new Vector(0,0,1);
new Vector(0,0,-1);
new Vector(1,0,1);
new Vector(-1,0,-1);
new Vector(-1,0,1);
new Vector(1,0,-1); 
}

private void Start()
{
StartCoroutine(ShowCubePlace());
}
private void SpawnPositions() {
List<Vector3> positions new List<Vector3>();
if (IsPositionEmpty(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z)))
positions.Add(new Vector3(nowCube.x + 1, nowCube.y, nowCube.z));
else if (IsPositionEmpty(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z)))
positions.Add(new Vector3(nowCube.x - 1, nowCube.y, nowCube.z));

else if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z)))
positions.Add(new Vector3(nowCube.x, nowCube.y + 1, nowCube.z));
else if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z)))
positions.Add(new Vector3(nowCube.x, nowCube.y - 1, nowCube.z));

else if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1)))
positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z + 1));
else if (IsPositionEmpty(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1)))
positions.Add(new Vector3(nowCube.x, nowCube.y, nowCube.z - 1));

cubeToPlace.position = positions[UnityEngine.Random.Range(0, positions.Count)];
}
private bool IsPositionEmpty(Vector3 targetPos)
{
if(targetPos.y == 0)
return false;

foreach(Vector3 pos in allCubesPositions)
{
	if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
		return false;
}
return true;
}
}




IEnumerator ShowCubePlace ()
{
while (true)
{
SpawnPositions();
yield return new WaitForSeconds(cubeChangeSpeed);
}
}
}

*/