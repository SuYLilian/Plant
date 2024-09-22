using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static float obstacleSpeed;

    private void Awake()
    {
        //obstacleSpeed = FindObjectOfType<LoppingBG>().speed*10;
    }

    void Update()
    {
        gameObject.transform.position -= new Vector3(0, obstacleSpeed *Time.deltaTime);
    }

   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="DestroyObstacle")
        {
            Destroy(gameObject);
        }
    }*/
}
