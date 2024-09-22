using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclesCoin;

    public Vector2 spawnTime;

    public float count;

    int obstacleNum;

    public bool canSpawn = true;
    private void Update()
    {
        if(canSpawn)
        {
            count -= Time.deltaTime;
            if (count <= 0)
            {
                obstacleNum = Random.Range(0, obstaclesCoin.Length);
                Instantiate(obstaclesCoin[obstacleNum], new Vector3(Random.Range(-1.7f, 1.7f), 6, 0), Quaternion.identity);
                count = Random.Range(spawnTime.x, spawnTime.y);
            }
        }
       
    }

   
}
