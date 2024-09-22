using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject spawnerTrigger;
    public GameObject levelParent;
    public GameObject[] levels;

    public float levelSpace;
    public float destroyTime;

    private void Start()
    {
        /*for (int i = 0; i < 2; i++)
        {
            int r = Random.Range(0, levels.Length);

            GameObject newLevel = Instantiate(levels[r], levelParent.transform);
            newLevel.transform.localPosition = new Vector3(newLevel.transform.localPosition.x, newLevel.transform.localPosition.y + levelSpace * (i+1), newLevel.transform.localPosition.z);
        }*/
    }

    public void SpawnNewLevel()
    {
        int r = Random.Range(0, levels.Length);

        GameObject newLevel = Instantiate(levels[r], levelParent.transform);
        newLevel.transform.localPosition = new Vector3(spawnerTrigger.transform.localPosition.x, spawnerTrigger.transform.localPosition.y + levelSpace*2 , spawnerTrigger.transform.localPosition.z);
           /* new Vector3(spawnerTrigger.transform.position.x, spawnerTrigger.transform.position.y + levelSpace * 2,
                              spawnerTrigger.transform.position.z), Quaternion.identity);*/
    }
}
