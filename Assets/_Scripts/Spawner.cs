using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public LevelManager levelManager;
    bool obstacleIsTouch = false;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player" && !obstacleIsTouch)
        {
            obstacleIsTouch = true;
            levelManager.spawnerTrigger = gameObject;
            Destroy(gameObject, levelManager.destroyTime);
            levelManager.SpawnNewLevel();

        }
    }
}
