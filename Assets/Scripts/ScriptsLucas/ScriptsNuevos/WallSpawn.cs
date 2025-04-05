using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    public Transform boat;
    public GameObject[] wallPrefabs;
    public float wallSpacingZ = 10f;
    public int initialWallPairs = 5;
    public float spawnAheadDistance = 30f;
    public float destroyBehindDistance = 20f;
    public float sideOffsetX = 5f;
    public float wallY = 22f;
    public float randomTiltZ = 10f;

    private float nextSpawnZ = 0f;
    private List<int> prefabPool = new List<int>();
    private List<GameObject> spawnedWalls = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialWallPairs; i++)
        {
            SpawnWallPair();
        }
    }

    void Update()
    {
        if (boat.position.z + spawnAheadDistance > nextSpawnZ)
        {
            SpawnWallPair();
        }

        CleanupWalls();
    }

    void SpawnWallPair()
    {
        int leftIndex = GetNextPrefabIndex();
        int rightIndex = GetNextPrefabIndex(exclude: leftIndex);

        float leftX = (leftIndex == 0) ? 90f : -90f;
        float rightX = (rightIndex == 0) ? 90f : -90f;

        float leftZ = (leftIndex == 0) ?
            0f + Random.Range(-randomTiltZ, randomTiltZ) :
            180f + Random.Range(-randomTiltZ, randomTiltZ);

        float rightZ = (rightIndex == 0) ?
            180f + Random.Range(-randomTiltZ, randomTiltZ) :
            Random.Range(-randomTiltZ, randomTiltZ);

        Quaternion leftRot = Quaternion.Euler(leftX, 0f, leftZ);
        Quaternion rightRot = Quaternion.Euler(rightX, 0f, rightZ);

        Vector3 leftPos = new Vector3(-sideOffsetX, wallY, nextSpawnZ);
        Vector3 rightPos = new Vector3(sideOffsetX, wallY, nextSpawnZ);

        GameObject leftWall = Instantiate(wallPrefabs[leftIndex], leftPos, leftRot);
        GameObject rightWall = Instantiate(wallPrefabs[rightIndex], rightPos, rightRot);

        spawnedWalls.Add(leftWall);
        spawnedWalls.Add(rightWall);

        nextSpawnZ += wallSpacingZ;
    }

    int GetNextPrefabIndex(int? exclude = null)
    {
        if (prefabPool.Count == 0)
        {
            for (int i = 0; i < wallPrefabs.Length; i++)
            {
                if (exclude == null || i != exclude) prefabPool.Add(i);
            }
        }

        int choice = Random.Range(0, prefabPool.Count);
        int selected = prefabPool[choice];
        prefabPool.RemoveAt(choice);
        return selected;
    }

    void CleanupWalls()
    {
        float destroyZ = boat.position.z - destroyBehindDistance;

        for (int i = spawnedWalls.Count - 1; i >= 0; i--)
        {
            if (spawnedWalls[i] == null) continue;

            if (spawnedWalls[i].transform.position.z < destroyZ)
            {
                Destroy(spawnedWalls[i]);
                spawnedWalls.RemoveAt(i);
            }
        }
    }
}